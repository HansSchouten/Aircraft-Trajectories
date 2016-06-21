using AircraftTrajectories.Presenters;
using System;
using System.Windows.Forms;

namespace AircraftTrajectories.Views.Visualisation
{
    public partial class VisualisationForm : MDIContainerForm, IVisualisationForm
    {
        public VisualisationPresenter Presenter { get; protected set; }
        public VisualisationSettingsForm SettingsForm { get; protected set; }


        /// <summary>
        /// Construct the VisualisationForm
        /// </summary>
        public VisualisationForm()
        {
            InitializeComponent();
            Presenter = new VisualisationPresenter(this);
            SettingsForm = new VisualisationSettingsForm();
        }



        #region "EVENTS"

        private void VisualisationForm_Load(object sender, EventArgs e)
        {
            base.MDIContainerForm_Load();

            SettingsForm.MdiParent = this;
            SettingsForm.Show();
            /*
            RunForm.MdiParent = this;
            RunForm.Show();
            CompletedForm.MdiParent = this;
            CompletedForm.Show();
            */

            SettingsForm.BringToFront();
        }

        protected void VisualisationForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        #endregion
    }
}
