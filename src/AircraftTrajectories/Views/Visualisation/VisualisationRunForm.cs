using System;
using System.Windows.Forms;

namespace AircraftTrajectories.Views.Visualisation
{
    public partial class VisualisationRunForm : Form
    {
        public VisualisationRunForm()
        {
            InitializeComponent();
        }
        
        public string Message { get; set; }
        
        protected void VisualisationRunForm_Load(object sender, EventArgs e)
        {
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            ControlBox = false;
        }

        protected virtual void btnCancel_Click(object sender, EventArgs e)
        {
        }
    }
}
