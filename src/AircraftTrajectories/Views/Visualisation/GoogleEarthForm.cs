using System;
using System.Windows.Forms;

namespace AircraftTrajectories.Views.Visualisation
{
    public partial class GoogleEarthForm : Form
    {
        public GoogleEarthForm()
        {
            InitializeComponent();
        }

        private void GoogleEarthForm_Load(object sender, EventArgs e)
        {
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            ControlBox = false;
        }
    }
}
