using AircraftTrajectories.Models.IntegratedNoiseModel;
using AircraftTrajectories.Models.Space3D;
using AircraftTrajectories.Models.TemporalGrid;
using AircraftTrajectories.Models.Trajectory;
using AircraftTrajectories.Models.Visualisation;
using AircraftTrajectories.Models.Visualisation.AnimationSections;
using AircraftTrajectories.Models.Visualisation.KML;
using AircraftTrajectories.Models.Visualisation.KML.AnimationSections;
using AircraftTrajectories.Models.Visualisation.KML.AnimationSections.Cameras;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace AircraftTrajectories.Views
{

    public partial class Test : Form, IGoogleEarthForm
    {
        public Test()
        {
            InitializeComponent();
        }

        Aircraft aircraft;
        Trajectory trajectory;
        IntegratedNoiseModel noiseModel;

        private void Test_Load(object sender, EventArgs e)
        {
            var reader = new TrajectoryFileReader(CoordinateUnit.metric);
            trajectory = reader.createTrajectoryFromFile(Globals.currentDirectory + "track_schiphol.dat");

            aircraft = new Aircraft("GP7270", "wing");
            noiseModel = new IntegratedNoiseModel(trajectory, aircraft);
            noiseModel.StartCalculation(calculationCompleted, pbAnimation);
        }

        private void calculationCompleted()
        {
            var legend = new LegendCreator();
            legend.OutputLegendImage();
            legend.OutputLegendTitle();

            TemporalGrid temporalGrid = noiseModel.TemporalGrid;
            temporalGrid.LowerLeftCorner = new Point3D(104062, 475470, 0, CoordinateUnit.metric);
            temporalGrid.GridSize = 125;
            //GridConverter converter = new GridConverter(temporalGrid, GridTransformation.MAX);
            //temporalGrid = converter.transform();

            var populationData = getPopulationData();
            //populationData = new List<int[]>() { };
            //var animator = new Animator(trajectory, aircraft, temporalGrid, populationData);
            //animator.createAnimationKML();

            var camera = new FollowKMLAnimatorCamera(aircraft, trajectory);
            var sections = new List<KMLAnimatorSectionInterface>() {
                new AircraftKMLAnimator(aircraft, trajectory),
                new AirplotKMLAnimator(trajectory),
                new GroundplotKMLAnimator(trajectory),
                new ContourKMLAnimator(temporalGrid, trajectory, new List<int>() { 65, 70, 75 }),
                new AnnoyanceKMLAnimator(temporalGrid, populationData)
            };
            var animator = new KMLAnimator(sections, camera);
            animator.AnimationToFile(trajectory.Duration, Globals.currentDirectory + "test.kml");

            GoogleEarthServerForm googleEarthForm = new GoogleEarthServerForm();
            
            this.Hide();
            googleEarthForm.Closed += (s, args) => this.Close();
            googleEarthForm.Show();
        }

        private List<int[]> getPopulationData()
        {
            string currentFolder = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
            string rawData = File.ReadAllText(currentFolder + "/personen.dat");
            int[][] populationData;
            populationData = rawData
                .Split('\n')
                .Select(q =>
                    q.Split(new[] { "\t" }, StringSplitOptions.RemoveEmptyEntries)
                     .Select(x => {
                         return (int)decimal.Parse(x, NumberStyles.Float);
                     })
                     .ToArray()
                )
                .ToArray();

            var inGridPoints = new List<int[]>();
            int minX = 104062;
            int maxX = 112658;
            int minY = 475470;
            int maxY = 485564;
            foreach (int[] row in populationData)
            {
                if (row.Length < 3) { continue; }
                int x = row[0];
                int y = row[1];
                if (x > minX && x < maxX && y > minY && y < maxY)
                {
                    inGridPoints.Add(new int[] { x, y, row[2] });
                }
            }

            double chance = 0.03;
            var chosenPoints = new List<int[]>();
            int i = 0;
            foreach (int[] row in inGridPoints)
            {
                i++;
                if (randomBool(chance, i))
                {
                    chosenPoints.Add(new int[] { row[0], row[1], row[2] });
                }
            }
            return chosenPoints;
        }

        private Boolean randomBool(double chance, int seed)
        {
            Random randomListCell = new Random(seed);
            return (chance > randomListCell.NextDouble());
        }
    }
}