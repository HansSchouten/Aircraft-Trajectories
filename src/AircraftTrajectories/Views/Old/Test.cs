using AircraftTrajectories.Models.IntegratedNoiseModel;
using AircraftTrajectories.Models.Population;
using AircraftTrajectories.Models.Space3D;
using AircraftTrajectories.Models.TemporalGrid;
using AircraftTrajectories.Models.Trajectory;
using AircraftTrajectories.Models.Visualisation;
using AircraftTrajectories.Models.Visualisation.KML;
using AircraftTrajectories.Models.Visualisation.KML.AnimationSections;
using AircraftTrajectories.Models.Visualisation.KML.AnimationSections.Cameras;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace AircraftTrajectories.Views
{

    public partial class Test : Form
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
            noiseModel.StartCalculation(calculationCompleted, progressChanged);
        }

        private void progressChanged(int percentage)
        {

        }

        private void calculationCompleted()
        {
            var legend = new LegendCreator();
            legend.OutputLegendImage();
            legend.OutputLegendTitle();

            TemporalGrid temporalGrid = noiseModel.TemporalGrid;
            //temporalGrid.LowerLeftCorner = new Point3D(104062, 475470, 0, CoordinateUnit.metric);
            //temporalGrid.GridSize = 125;

            var population = new PopulationData(Globals.currentDirectory + "population.dat");

            var camera = new FollowKMLAnimatorCamera(aircraft, trajectory);
            var sections = new List<KMLAnimatorSectionInterface>() {
                new LegendKMLAnimator(),
                new AircraftKMLAnimator(aircraft, trajectory),
                new AirplotKMLAnimator(trajectory),
                new GroundplotKMLAnimator(trajectory),
                new ContourKMLAnimator(temporalGrid, trajectory, new List<int>() { 65, 70, 75 }),
                new AnnoyanceKMLAnimator(temporalGrid, population.getPopulationData())
            };
            var animator = new KMLAnimator(sections, camera);
            animator.AnimationToFile(trajectory.Duration, Globals.currentDirectory + "test.kml");

            GoogleEarthServerForm googleEarthForm = new GoogleEarthServerForm();
            
            this.Hide();
            googleEarthForm.Closed += (s, args) => this.Close();
            googleEarthForm.Show();
        }
    }
}