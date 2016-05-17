using AircraftTrajectories.Models.IntegratedNoiseModel;
using AircraftTrajectories.Models.Space3D;
using AircraftTrajectories.Models.TemporalGrid;
using AircraftTrajectories.Models.Trajectory;
using AircraftTrajectories.Models.Visualisation;
using System;
using System.ComponentModel;
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
            trajectory = reader.createTrajectoryFromFile("track_schiphol.dat");

            aircraft = new Aircraft("GP7270", "wing");
            noiseModel = new IntegratedNoiseModel(trajectory, aircraft);
            noiseModel.StartCalculation(calculationCompleted, pbAnimation);
        }

        private void calculationCompleted()
        {
            var temporalGrid = noiseModel.TemporalGrid;
            var animator = new Animator(trajectory, aircraft, temporalGrid);
            animator.createAnimationKML();

            GoogleEarthForm googleEarthForm = new GoogleEarthForm();
            
            this.Hide();
            googleEarthForm.kmlString = animator.kmlString;
            googleEarthForm.Closed += (s, args) => this.Close();
            googleEarthForm.Show();
        }
    }
}