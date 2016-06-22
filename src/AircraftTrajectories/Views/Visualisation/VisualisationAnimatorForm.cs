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
    }
}
