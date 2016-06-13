using System;
using System.Windows.Forms;

namespace AircraftTrajectories.Views.Optimisation
{
    public partial class OptimisationRunForm : Form
    {
        public OptimisationRunForm()
        {
            InitializeComponent();
        }

        private void OptimisationRunForm_Load(object sender, EventArgs e)
        {
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            ControlBox = false;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ((OptimisationForm)this.MdiParent).CancelOptimisation();
        }
    }
}
