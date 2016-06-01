using AircraftTrajectories.Models.Optimalisation;
using System;
using System.Windows.Forms;

namespace AircraftTrajectories.Views
{
    public partial class Optimize : Form
    {
        public Optimize()
        {
            InitializeComponent();
        }

        // Configuration
        public int firstHeight = 1500;
        public int v2 = 155;
        public int secondHeight = 3000;
        public int v_clean = 250;
        public double T_takeoff;
        public double T_climb;

        // State parameters
        public int v0;
        public int h0;
        // Control parameters
        public double throttle_setting;
        public double flight_path_angle;
        // Min/max
        public double angle_min;
        public double angle_max;
        public double throttle_min;
        public double throttle_max;

        private void Optimize_Load(object sender, EventArgs e)
        {
            Configure();
            FirstStep();
        }

        private void Configure()
        {
            v0 = v2 + 10;
            h0 = 0;
        }

        private void FirstStep()
        {
            throttle_setting = 1;
            var A = 0.5;
            var B = 0.5;
        }

        protected double Interpolate(double min, double max, double factor)
        {
            return (max - min) * factor + min;
        }
    }
}