using System;
using System.Windows.Forms;

namespace AircraftTrajectories.Views.Visualisation
{
    public partial class VisualisationSettingsForm : Form
    {
        public VisualisationSettingsForm()
        {
            InitializeComponent();
        }

        private void VisualisationSettingsForm_Load(object sender, EventArgs e)
        {
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            ControlBox = false;
        }

        private void btnBrowseTrajectory_Click(object sender, EventArgs e)
        {
            DialogResult result = openFile.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtTrajectoryFile.Text = openFile.FileName;
            }
        }

        private void btnBrowseNoise_Click(object sender, EventArgs e)
        {
            DialogResult result = openFile.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtNoiseFile.Text = openFile.FileName;
            }
        }

        private void radioExternal_CheckedChanged(object sender, EventArgs e)
        {
            txtNoiseFile.Enabled = true;
            btnBrowseNoise.Enabled = true;
        }

        private void radioINM_CheckedChanged(object sender, EventArgs e)
        {
            txtNoiseFile.Text = "";
            txtNoiseFile.Enabled = false;
            btnBrowseNoise.Enabled = false;
        }

        private void btnPrepare_Click(object sender, EventArgs e)
        {
            ((VisualisationForm)this.MdiParent).CalculateNoiseClick();
        }
    }
}
