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
            for (int t = 0; t < trajectory.Duration; t++)
            {
                chartAltitude.Series["Altitude [m]"].Points.AddXY(t, trajectory.Z(t));
            }
            chartAltitude.Series["Altitude [m]"].ChartType = SeriesChartType.FastLine;
        }

        protected void AutoScaleChart(Chart chart)
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
