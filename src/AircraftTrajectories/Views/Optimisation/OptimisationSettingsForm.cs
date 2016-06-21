using System;
using System.Windows.Forms;

namespace AircraftTrajectories.Views.Optimisation
{
    public partial class OptimisationSettingsForm : Form
    {
        public OptimisationSettingsForm()
        {
            InitializeComponent();
        }

        private void OptimisationSettingsForm_Load(object sender, EventArgs e)
        {
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            ControlBox = false;
        }

        private void btnOptimise_Click(object sender, EventArgs e)
        {
            ((OptimisationForm)this.MdiParent).RunOptimisationClick();
        }
    }
}