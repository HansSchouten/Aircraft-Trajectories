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
using MathNet.Numerics.Interpolation;

namespace AircraftTrajectories.Views
{
    public partial class TopView : Form
    {
        public TopView()
        {
            InitializeComponent();
        }

        Aircraft aircraft;
        Trajectory trajectory;
        IntegratedNoiseModel noiseModel;

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
            var _tData = new List<double>();
            var _xData = new List<double>();
            var _yData = new List<double>();
            var _zData = new List<double>();
            var trajectories = new List<Trajectory>();
            double xMin = int.MaxValue, yMin = int.MaxValue, xMax = int.MinValue, yMax = int.MinValue;
            double xMinT = int.MaxValue, yMinT = int.MaxValue, xMaxT = int.MinValue, yMaxT = int.MinValue;

            // Loop through the positions of all trajectories
            for (int i = 0; i < _trackData.Length; i++)
            {
                // Switch to the next trajectory
                if (_trackData[i][0] != flight_id && i > 0)
                {
                    var xSpline = CubicSpline.InterpolateNatural(_tData, _xData);
                    var ySpline = CubicSpline.InterpolateNatural(_tData, _yData);
                    var zSpline = CubicSpline.InterpolateNatural(_tData, _zData);
                    Aircraft aircraft = new Aircraft("GP7270", "wing");
                    Trajectory trajectory = new Trajectory(xSpline, ySpline, zSpline, null, null, aircraft);
                    trajectory.Duration = _tData.Count;
                    trajectories.Add(trajectory);
                    
                    // Reset for next iteration
                    _xData = new List<double>();
                    _yData = new List<double>();
                    _zData = new List<double>();
                    _tData = new List<double>();
                    xMinT = int.MaxValue;
                    yMinT = int.MaxValue;
                    xMaxT = int.MinValue;
                    yMaxT = int.MinValue;
                }
                flight_id = _trackData[i][0];


                // Parse the next position of the current trajectory
                //DateTime t = DateTime.Parse(_trackData[i][14]);
                double x = Convert.ToDouble(_trackData[i][4]);
                double y = Convert.ToDouble(_trackData[i][5]);
                double z = Convert.ToDouble(_trackData[i][6]) * 0.3040 * 100;
                
                _xData.Add(x);
                _yData.Add(y);
                _zData.Add(z);
                _tData.Add(_tData.Count);


                // Update global and trajectory corner coordinates
                xMin = Math.Min(xMin, x);
                xMax = Math.Max(xMax, x);
                yMin = Math.Min(yMin, y);
                yMax = Math.Max(yMax, y);
                xMinT = Math.Min(xMinT, x);
                xMaxT = Math.Max(xMaxT, x);
                yMinT = Math.Min(yMinT, y);
                yMaxT = Math.Max(yMaxT, y);
            }

            TemporalGrid temporalGrid = new TemporalGrid();
            temporalGrid.LowerLeftCorner = new Point3D(xMin, yMin, 0, CoordinateUnit.metric);
            temporalGrid.ReferencePoint = new Point3D(0, 0, 0, CoordinateUnit.metric);
            temporalGrid.ReferenceGeoPoint = new GeoPoint3D(4.7518, 52.309266, 0);
            temporalGrid.GridSize = 125;
            int counter = 0;
            foreach (Trajectory trajectory in trajectories)
            {
                counter++;
                Console.WriteLine(counter);
                if (counter > 5) {
                    break;
                }
                var INM = new IntegratedNoiseModel(trajectory, trajectory.Aircraft, false);
                INM.StartCalculation(INMCompleted);

                while (!completed) { }
                completed = false;

                Grid grid = INM.TemporalGrid.GetGrid(0);
                temporalGrid.AddGrid(grid);
            }


            var camera = new TopViewKMLAnimatorCamera(aircraft, trajectory, new GeoPoint3D(4.7518, 52.309266, 15000));
            var sections = new List<KMLAnimatorSectionInterface>() {
                new ContourKMLAnimator(temporalGrid, trajectory, new List<int>() { })
            };
            var animator = new KMLAnimator(sections, camera);
            animator.AnimationToFile(temporalGrid.GetNumberOfGrids(), Globals.currentDirectory + "topview_fullpath.kml");


            return;







            var reader = new TrajectoryFileReader(CoordinateUnit.metric);
            trajectory = reader.createTrajectoryFromFile(Globals.currentDirectory + "track_schiphol.dat");

            aircraft = new Aircraft("GP7270", "wing");
            noiseModel = new IntegratedNoiseModel(trajectory, aircraft);
            noiseModel.GridName = "schiphol_grid2D";
            noiseModel.StartCalculation(calculationCompleted, null);
        }

        bool completed = false;
        private void INMCompleted()
        {
            completed = true;
        }





        private void calculationCompleted()
        {
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
        }
    }
}
