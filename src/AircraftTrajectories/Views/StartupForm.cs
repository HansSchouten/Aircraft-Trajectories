using AircraftTrajectories.Models.Trajectory;
using AircraftTrajectories.Views.Optimisation;
using AircraftTrajectories.Views.Visualisation;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace AircraftTrajectories.Views
{
    public partial class StartupForm : Form
    {
        OptimisationForm OptimisationForm;
        VisualisationForm VisualisationForm;

        public StartupForm()
        {
            InitializeComponent();
            OptimisationForm = new OptimisationForm(this);
            VisualisationForm = new VisualisationForm(this);
        }

        private void Form_Paint(object sender, PaintEventArgs e)
        {
            var blackPen = new Pen(Color.DimGray, 1);
            var x = 0;
            var y = 0;
            var width = this.Width - 1;
            var height = this.Height - 1;
            e.Graphics.DrawRectangle(blackPen, x, y, width, height);
        }

        public void Visualise(Trajectory trajectory)
        {
            //OptimisationForm.Hide();
            VisualisationForm.Show();
            VisualisationForm.VisualiseTrajectory(trajectory);
        }

        private void btnOptimise_Click(object sender, EventArgs e)
        {
            OptimisationForm.Show();
            this.Hide();
        }

        private void btnVisualise_Click(object sender, EventArgs e)
        {
            VisualisationForm.Show();
            this.Hide();
        }




        private void pbClose_MouseEnter(object sender, EventArgs e)
        {
            pbClose.Image = Properties.Resources.close_hover;
        }

        private void pbClose_MouseLeave(object sender, EventArgs e)
        {
            pbClose.Image = Properties.Resources.close;
        }

        private void pbClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
	}
}
