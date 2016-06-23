using AircraftTrajectories.Models.Space3D;
using AircraftTrajectories.Models.Trajectory;
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
        public event EventHandler VisualiseTrajectoryEvent;

        public VisualisationPresenter Presenter { get; protected set; }
        public VisualisationSettingsForm SettingsForm { get; protected set; }
        public VisualisationRunForm RunForm { get; protected set; }
        public VisualisationAnimatorForm AnimatorForm { get; protected set; }
        public GoogleEarthForm GoogleEarthForm { get; protected set; }


        /// <summary>
        /// Construct the VisualisationForm
        /// </summary>
        public VisualisationForm()
        {
            InitializeComponent();
        }




        public void CalculateNoiseClick()
        {
            /*
            //DEBUG
            GoogleEarthForm.BringToFront();
            GoogleEarthForm.Visualise("visualisation.kml");
            return;
            */

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
        public Trajectory Trajectory { get; set; }
        public void VisualiseTrajectory(Trajectory trajectory)
        {
            SettingsForm.txtTrajectoryFile.Text = "Optimisation result";
            Trajectory = trajectory;
            VisualiseTrajectoryEvent(this, EventArgs.Empty);
        }


        public void PrepareVisualisationClick()
        {
            RunForm.Message = "Preparing visualisation";
            RunForm.lblTimeLeft.Text = "";
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
            GoogleEarthForm.BringToFront();
            GoogleEarthForm.Visualise("visualisation.kml");
        }



        #region "EVENTS"

        private void VisualisationForm_Load(object sender, EventArgs e)
        {
            base.MDIContainerForm_Load();

            Presenter = new VisualisationPresenter(this);
            RunForm = new VisualisationRunForm();
            SettingsForm = new VisualisationSettingsForm();
            AnimatorForm = new VisualisationAnimatorForm();
            GoogleEarthForm = new GoogleEarthForm();


            RunForm.MdiParent = this;
            RunForm.Show();
            RunForm.BringToFront();

            SettingsForm.MdiParent = this;
            SettingsForm.Show();
            SettingsForm.BringToFront();

            AnimatorForm.MdiParent = this;
            AnimatorForm.Show();
            AnimatorForm.BringToFront();
            
            GoogleEarthForm.MdiParent = this;
            GoogleEarthForm.Show();
            GoogleEarthForm.BringToFront();

            SettingsForm.BringToFront();
        }

        protected void VisualisationForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
            //new StartupForm().Show();
        }


        #endregion



        #region "ACCESS CONTROL ELEMENTS"

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

        public bool CustomReference
        {
            get
            {
                return SettingsForm.radioCoordinateCustom.Checked;
            }
        }

        public bool Heatmap
        {
            get
            {
                return AnimatorForm.cbHeatmap.Checked;
            }
        }

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

        public string MapFile
        {
            get
            {
                return AnimatorForm.txtCustomMapFile.Text;
            }
        }

        public string ValueConversion
        {
            get
            {
                return AnimatorForm.ValueConversion;
            }
        }

        public GeoPoint3D GeoReference
        {
            get
            {
                return new GeoPoint3D(double.Parse(SettingsForm.txtReferenceLongitude.Text), double.Parse(SettingsForm.txtReferenceLatitude.Text));
            }
        }

        public Point3D MetricReference
        {
            get
            {
                return new Point3D(double.Parse(SettingsForm.txtReferenceX.Text), double.Parse(SettingsForm.txtReferenceY.Text));
            }
        }

        public GeoPoint3D MapUpperRight
        {
            get
            {
                return new GeoPoint3D(double.Parse(AnimatorForm.txtUpperRightLongitude.Text), double.Parse(AnimatorForm.txtUpperRightLatitude.Text));
            }
        }

        public GeoPoint3D MapBottomLeft
        {
            get
            {
                return new GeoPoint3D(double.Parse(AnimatorForm.txtBottomLeftLongitude.Text), double.Parse(AnimatorForm.txtBottomLeftLatitude.Text));
            }
        }

        public int NumberOfContours
        {
            get
            {
                return int.Parse(AnimatorForm.txtNumberOfContours.Text);
            }
        }

        public int ContourStartValue
        {
            get
            {
                return int.Parse(AnimatorForm.txtContourStartValue.Text);
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
