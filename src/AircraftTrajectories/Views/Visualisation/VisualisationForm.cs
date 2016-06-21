using AircraftTrajectories.Presenters;
using System;
using System.Windows.Forms;

namespace AircraftTrajectories.Views.Visualisation
{
    public partial class VisualisationForm : MDIContainerForm, IVisualisationForm
    {
        public event EventHandler CalculateNoise;
        public event EventHandler PrepareVisualisation;

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

        public void CalculateNoiseClick()
        {
            CalculateNoise(this, EventArgs.Empty);
        }



        #region "EVENTS"

        private void VisualisationForm_Load(object sender, EventArgs e)
        {
            base.MDIContainerForm_Load();

            SettingsForm.MdiParent = this;
            SettingsForm.Show();
            SettingsForm.BringToFront();
        }

        protected void VisualisationForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        #endregion

        #region "ACCESS CONTROL ELEMENTS"

        public string TrajectoryFile
        {
            get
            {
                return SettingsForm.txtTrajectoryFile.Text;
            }
        }

        public string NoiseFile
        {
            get
            {
                return SettingsForm.txtNoiseFile.Text;
            }
        }

        public bool OneTrajectory
        {
            get
            {
                return SettingsForm.radioSingle.Checked;
            }
        }

        public bool ExternalNoise
        {
            get
            {
                return SettingsForm.radioExternal.Checked;
            }
        }

        #endregion
    }
}
