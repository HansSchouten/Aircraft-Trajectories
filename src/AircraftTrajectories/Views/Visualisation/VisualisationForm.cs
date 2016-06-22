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
        public VisualisationAnimatorForm AnimatorForm { get; protected set; }


        /// <summary>
        /// Construct the VisualisationForm
        /// </summary>
        public VisualisationForm()
        {
            InitializeComponent();
            Presenter = new VisualisationPresenter(this);
            RunForm = new VisualisationRunForm();
            SettingsForm = new VisualisationSettingsForm();
            AnimatorForm = new VisualisationAnimatorForm();
        }




        public void CalculateNoiseClick()
        {
            RunForm.Message = "Calculating noise";
            RunForm.CancelCallback = CancelNoiseClick;
            RunForm.BringToFront();

            CalculateNoise(this, EventArgs.Empty);
        }
        public void CancelNoiseClick()
        {
            SettingsForm.BringToFront();
            CancelNoiseCalculation(this, EventArgs.Empty);
        }
        public void NoiseCalculationCompleted()
        {
            AnimatorForm.BringToFront();
        }



        public void PrepareVisualisationClick()
        {
            RunForm.Message = "Preparing visualisation";
            RunForm.CancelCallback = CancelPreparation;
            RunForm.BringToFront();

            PrepareVisualisation(this, EventArgs.Empty);
        }
        public void CancelPreparation()
        {
            AnimatorForm.BringToFront();
            CancelVisualisationPreparation(this, EventArgs.Empty);
        }
        public void PreparationCalculationCompleted()
        {

        }



        #region "EVENTS"

        private void VisualisationForm_Load(object sender, EventArgs e)
        {
            base.MDIContainerForm_Load();

            RunForm.MdiParent = this;
            RunForm.Show();
            RunForm.BringToFront();

            SettingsForm.MdiParent = this;
            SettingsForm.Show();
            SettingsForm.BringToFront();

            AnimatorForm.MdiParent = this;
            AnimatorForm.Show();
            AnimatorForm.BringToFront();

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
                string message = (value <= 1) ? "completed in 1min" : value + "min remaining";
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
