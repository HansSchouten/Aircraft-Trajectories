using AircraftTrajectories.Models.IntegratedNoiseModel;
using AircraftTrajectories.Models.Space3D;
using AircraftTrajectories.Models.TemporalGrid;
using AircraftTrajectories.Models.Trajectory;
using AircraftTrajectories.Models.Visualisation.KML;
using AircraftTrajectories.Models.Visualisation.KML.AnimationSections;
using AircraftTrajectories.Models.Visualisation.KML.AnimationSections.Cameras;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Linq;

namespace AircraftTrajectories.Views
{
    public partial class TopView : Form
    {
        public TopView()
        {
            InitializeComponent();
        }

        private void TopView_Load(object sender, EventArgs e)
        {
            // Parse the file containing multiple trajectories
            string rawTrackData = File.ReadAllText(Globals.currentDirectory + "inbound.txt");
            var _trackData = rawTrackData
                .Split('\n')
                .Select(q =>
                    q.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries)
                     .Select(Convert.ToString)
                     .ToArray()
                )
                .ToArray();


            // Define variables
            string flight_id = "";
            var trajectories = new List<Trajectory>();
            ReferencePoint referencePoint = new ReferencePoint(new GeoPoint3D(4.7066753, 52.3297923));
            TrajectoryGenerator trajectoryGenerator = new TrajectoryGenerator(new Aircraft("GP7270", "wing"), referencePoint);


            // Loop through the positions of all trajectories
            for (int i = 0; i < _trackData.Length; i++)
            {
                // Switch to the next trajectory
                if ( i == _trackData.Length-1 || (_trackData[i][0] != flight_id && i > 0) )
                {
                    trajectories.Add(trajectoryGenerator.GenerateTrajectory());

                    // Prepare next trajectory
                    var aircraft = new Aircraft("GP7270", "wing");
                    trajectoryGenerator = new TrajectoryGenerator(aircraft, referencePoint);
                }
                
                // Prevent failing on empty lines
                if (_trackData[i].Count() == 0) { continue; }
                flight_id = _trackData[i][0];
                
                // Parse the next position of the current trajectory
                //DateTime t = DateTime.Parse(_trackData[i][14]);
                double x = 0;
                double.TryParse(_trackData[i][4], out x);
                double y = 0;
                double.TryParse(_trackData[i][5], out y);
                double z = 0;
                double.TryParse(_trackData[i][6], out z);
                z = z * 0.3040 * 100;
                trajectoryGenerator.AddDatapoint(x, y, z, 200, 200000);
            }


            // Calculate the noise for each trajectory
            TemporalGrid temporalGrid = new TemporalGrid();
            int counter = 0;
            foreach (Trajectory trajectory in trajectories)
            {
                counter++;
                Console.WriteLine(counter);
                if (counter > 15) { break; }

                var INM = new IntegratedNoiseModel(trajectory, trajectory.Aircraft, false);
                INM.RunINMFullTrajectory();

                Grid grid = INM.TemporalGrid.GetGrid(0);
                Console.WriteLine(grid.LowerLeftCorner.X);
                Console.WriteLine(grid.LowerLeftCorner.Y);
                grid.ReferencePoint = referencePoint;
                temporalGrid.AddGrid(grid);
            }
            
            var camera = new TopViewKMLAnimatorCamera(new GeoPoint3D(4.7066753, 52.3297923, 22000));
            var sections = new List<KMLAnimatorSectionInterface>() {
                new ContourKMLAnimator(temporalGrid),
                new MultipleGroundplotKMLAnimator(trajectories)
            };
            var animator = new KMLAnimator(sections, camera);
            animator.Duration = 0;
            animator.AnimationToFile(temporalGrid.GetNumberOfGrids(), Globals.currentDirectory + "topview_fullpath.kml");
        }
        




        private void calculationCompleted()
        {
            /*
            TemporalGrid temporalGrid = noiseModel.TemporalGrid;
            var convertor = new GridConverter(temporalGrid, GridTransformation.MAX);
            var LAMaxTemporalGrid = convertor.transform();
            temporalGrid = LAMaxTemporalGrid;
            //temporalGrid = new TemporalGrid();
            //temporalGrid.AddGrid(LAMaxTemporalGrid.GetGrid(LAMaxTemporalGrid.GetNumberOfGrids() - 1));
            temporalGrid.LowerLeftCorner = new Point3D(104062, 475470, 0, CoordinateUnit.metric);
            temporalGrid.GridSize = 125;

            var camera = new TopViewKMLAnimatorCamera(aircraft, trajectory);
            var sections = new List<KMLAnimatorSectionInterface>() {
                new ContourKMLAnimator(temporalGrid, trajectory, new List<int>() { })
            };
            var animator = new KMLAnimator(sections, camera);
            animator.AnimationToFile(temporalGrid.GetNumberOfGrids(), Globals.currentDirectory + "topview_fullpath.kml");
            */
        }
    }
}
