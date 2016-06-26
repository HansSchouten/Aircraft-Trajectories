using AircraftTrajectories.Models.Trajectory;
using System;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace AircraftTrajectories.Views.Optimisation
{
    public partial class OptimisationCompletedForm : Form
    {
        public Trajectory trajectory;

        public OptimisationCompletedForm()
        {
            InitializeComponent();
        }

        private void OptimisationCompletedForm_Load(object sender, EventArgs e)
        {
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            ControlBox = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            ((OptimisationForm)this.MdiParent).SaveTrajectoryClick();
        }

        private void btnVisualise_Click(object sender, EventArgs e)
        {
            ((OptimisationForm)this.MdiParent).VisualiseTrajectoryClick();
        }

        public void RefreshGraph(Trajectory trajectory)
        {
            this.trajectory = trajectory;
            btnAltitudeTime.PerformClick();
        }

        private void btnAltitudeTime_Click(object sender, EventArgs e)
        {
            ResetChart();
            chart.Series.Add("Altitude");

            for (int t = 0; t < trajectory.Duration; t++)
            {
                chart.Series["Altitude"].Points.AddXY(t, trajectory.Z(t));
            }

            chart.Series["Altitude"].ChartType = SeriesChartType.FastLine;
            chart.ChartAreas[0].AxisX.LabelStyle.Format = "{0}sec";
            chart.ChartAreas[0].AxisY.LabelStyle.Format = "{0}m";
            chart.ChartAreas[0].AxisY.Minimum = 0;
            chart.ChartAreas[0].AxisX.Minimum = 0;
        }

        private void btnAltitudeDistance_Click(object sender, EventArgs e)
        {
            ResetChart();
            chart.Series.Add("Altitude");

            double distance = 0;
            double prevX = trajectory.X(0);
            double prevY = trajectory.Y(0);
            for (int t = 0; t < trajectory.Duration; t++)
            {
                double dX = trajectory.X(t) - prevX;
                double dY = trajectory.Y(t) - prevY;
                double dDistance = Math.Sqrt(dX * dX + dY * dY);
                distance += dDistance;

                chart.Series["Altitude"].Points.AddXY(distance/1000, trajectory.Z(t));
                prevX = trajectory.X(t);
                prevY = trajectory.Y(t);
            }

            chart.Series["Altitude"].ChartType = SeriesChartType.FastLine;
            chart.ChartAreas[0].AxisX.LabelStyle.Format = "{0}km";
            chart.ChartAreas[0].AxisY.LabelStyle.Format = "{0}m";
            chart.ChartAreas[0].AxisY.Minimum = 0;
            chart.ChartAreas[0].AxisX.Minimum = 0;
        }

        private void btnGroundpath_Click(object sender, EventArgs e)
        {
            ResetChart();
            chart.Series.Add("Groundpath");

            for (int t = 0; t < trajectory.Duration; t++)
            {
                chart.Series["Groundpath"].Points.AddXY(trajectory.Longitude(t), trajectory.Latitude(t));
            }

            AutoScaleChart();
            chart.ChartAreas[0].AxisY.LabelStyle.Format = "{0.00}°N";
            chart.ChartAreas[0].AxisX.LabelStyle.Format = "{0.00}°E";
            chart.Series["Groundpath"].ChartType = SeriesChartType.FastLine;
        }





        protected void ResetChart()
        {
            chart.ChartAreas.Clear();
            chart.ChartAreas.Add("");
            chart.Series.Clear();
        }

        protected void AutoScaleChart()
        {
            var points = chart.Series[0].Points;
            var minY = points.Min(y => y.YValues[0]);
            var maxY = points.Max(y => y.YValues[0]);
            var minX = points.Min(x => x.XValue);
            var maxX = points.Max(x => x.XValue);
            var yBound = (maxY - minY) * 0.05;
            var xBound = (maxX - minX) * 0.05;
            chart.ChartAreas[0].AxisY.Minimum = minY - yBound;
            chart.ChartAreas[0].AxisY.Maximum = maxY + yBound;
            chart.ChartAreas[0].AxisX.Minimum = minX - xBound;
            chart.ChartAreas[0].AxisX.Maximum = maxX + xBound;
        }
    }
}
