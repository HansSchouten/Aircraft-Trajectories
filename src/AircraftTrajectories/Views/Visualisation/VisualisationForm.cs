using AircraftTrajectories.Presenters;
using System;
using System.Windows.Forms;

namespace AircraftTrajectories.Views.Visualisation
{
    public partial class VisualisationForm : MDIContainerForm, IVisualisationForm
    {
        public event EventHandler CalculateNoise;
        public event EventHandler CancelNoiseCalculation;
        public event EventHandler PrepareVisualisation;
        public event EventHandler CancelVisualisationPreparation;

        public VisualisationPresenter Presenter { get; protected set; }
        public VisualisationSettingsForm SettingsForm { get; protected set; }
        public VisualisationRunForm RunForm { get; protected set; }


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
            RunForm = new VisualisationCalculateNoiseForm();
            RunForm.MdiParent = this;
            RunForm.Show();
            RunForm.BringToFront();

            RunForm.lblPercentage.Text = RunForm.Message;
            RunForm.lblTimeLeft.Text = "estimating time left";

            CalculateNoise(this, EventArgs.Empty);
        }
        public void CancelNoiseClick()
        {
            SettingsForm.BringToFront();
            CancelNoiseCalculation(this, EventArgs.Empty);
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
            new StartupForm().Show();
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

        public int Percentage
        {
            set
            {
                RunForm.lblPercentage.Text = RunForm.Message + " " + value + "%";
            }
        }

        public int TimeLeft
        {
            set
            {
                string message = (value < 1) ? "completed in 1min" : value + "min remaining";
                RunForm.lblTimeLeft.Text = message;
            }
        }

        public void Invoke(MethodInvoker methodInvoker)
        {
            base.Invoke(methodInvoker);
        }

        #endregion
    }
}
