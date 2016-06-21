using System;
using System.Windows.Forms;

namespace AircraftTrajectories.Views.Optimisation
{
    public partial class OptimisationCompletedForm : Form
    {
        public OptimisationCompletedForm()
        {
            InitializeComponent();
        }

        private void OptimisationCompletedForm_Load(object sender, EventArgs e)
        {
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            ControlBox = false;
        }
    }
}
