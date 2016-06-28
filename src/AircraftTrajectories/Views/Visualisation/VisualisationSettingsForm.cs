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

            radioSingle.Checked = true;
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
            panelExternalNoiseFile.Visible = true;
        }

        private void radioINM_CheckedChanged(object sender, EventArgs e)
        {
            panelExternalNoiseFile.Visible = false;
        }

        private void radioCoordinateCustom_CheckedChanged(object sender, EventArgs e)
        {
            panelCoordinateReference.Visible = radioCoordinateCustom.Checked;
        }

        private void btnPrepare_Click(object sender, EventArgs e)
        {
            ((VisualisationForm)this.MdiParent).CalculateNoiseClick();
        }

        public int NoiseMetric;
        private void selectNoiseMetric_SelectedIndexChanged(object sender, EventArgs e)
        {
            NoiseMetric = selectNoiseMetric.SelectedIndex;
        }

        private void radioSingle_CheckedChanged(object sender, EventArgs e)
        {
            selectNoiseMetric.Items.Clear();

            selectNoiseMetric.Items.Add("LA");
            selectNoiseMetric.Items.Add("LAmax");
            selectNoiseMetric.Items.Add("SEL");
            selectNoiseMetric.Items.Add("EPNL");
            selectNoiseMetric.Items.Add("PNLTM");

            selectNoiseMetric.SelectedIndex = 0;
        }

        private void radioMultiple_CheckedChanged(object sender, EventArgs e)
        {
            selectNoiseMetric.Items.Clear();

            selectNoiseMetric.Items.Add("Lden");
            selectNoiseMetric.Items.Add("SEL");

            selectNoiseMetric.SelectedIndex = 0;
        }
    }
}
