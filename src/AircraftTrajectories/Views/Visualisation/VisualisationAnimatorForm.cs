using System;
using System.Windows.Forms;

namespace AircraftTrajectories.Views.Visualisation
{
    public partial class VisualisationAnimatorForm : Form
    {
        public VisualisationAnimatorForm()
        {
            InitializeComponent();
        }

        private void VisualisationAnimatorForm_Load(object sender, EventArgs e)
        {
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            ControlBox = false;
        }

        private void btnPrepare_Click(object sender, EventArgs e)
        {
            ((VisualisationForm)this.MdiParent).PrepareVisualisationClick();
        }

        private void cbContourGradient_CheckedChanged(object sender, EventArgs e)
        {
            pnlContourGradient.Enabled = cbContourGradient.Checked;
        }

        private void cbHighlightedContours_CheckedChanged(object sender, EventArgs e)
        {
            pnlHighlightedContours.Enabled = cbHighlightedContours.Checked;
        }

        private void txtContourStep_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtLowestContourValue_TextChanged(object sender, EventArgs e)
        {

        }

        private void pnlContourGradient_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
