﻿using System;
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            ((OptimisationForm)this.MdiParent).SaveTrajectoryClick();
        }

        private void btnVisualise_Click(object sender, EventArgs e)
        {
            ((OptimisationForm)this.MdiParent).VisualiseTrajectoryClick();
        }
    }
}
