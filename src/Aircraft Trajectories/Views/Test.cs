using AircraftTrajectories.Models.IntegratedNoiseModel;
using AircraftTrajectories.Models.Space3D;
using AircraftTrajectories.Models.Trajectory;
using System;
using System.Windows.Forms;

namespace AircraftTrajectories.Views
{
    public partial class Test : Form, IGoogleEarthForm
    {
        public Test()
        {
            InitializeComponent();
        }

        private void Test_Load(object sender, EventArgs e)
        {
            var reader = new TrajectoryFileReader(CoordinateUnit.metric);
            var trajectory = reader.createTrajectoryFromFile("track_schiphol.dat");

            var aircraft = new Aircraft("GP7270", "wing");
            var noiseModel = new IntegratedNoiseModel(trajectory, aircraft);
            var temporalGrid = noiseModel.CalculateNoise();
        }
    }
}
