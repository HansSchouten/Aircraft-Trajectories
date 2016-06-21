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
    }
}
