using AircraftTrajectories.Models.Space3D;
using AircraftTrajectories.Models.Trajectory;
using AircraftTrajectories.Presenters;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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
        public StartupForm StartupForm;


        /// <summary>
        /// Construct the VisualisationForm
        /// </summary>
        public VisualisationForm(StartupForm startupForm)
        {
            InitializeComponent();
            StartupForm = startupForm;
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

        private void menuItemGoBack_Click(object sender, EventArgs e)
        {
            Form activeChild = this.ActiveMdiChild;
            if (activeChild == SettingsForm)
            {
                StartupForm.Show();
                this.Hide();
            } else if(activeChild == RunForm && RunForm.lblPercentage.Text.Contains("noise"))
            {
                CancelNoiseClick();
            }
            else if (activeChild == RunForm && RunForm.lblPercentage.Text.Contains("preparing"))
            {
                CancelPreparation();
            }
            else if (activeChild == AnimatorForm)
            {
                SettingsForm.BringToFront();
            }
            else if (activeChild == GoogleEarthForm)
            {
                try
                {
                    GoogleEarthForm.Dispose();
                } catch(Exception ex)
                {
                }
                GoogleEarthForm = new GoogleEarthForm();
                GoogleEarthForm.MdiParent = this;
                GoogleEarthForm.Show();

                this.WindowState = FormWindowState.Normal;
                AnimatorForm.BringToFront();
            }
        }
        private void menuitemScreenshot_Click(object sender, EventArgs e)
        {
            Bitmap bmp = GoogleEarthForm.geWebBrowser.ScreenGrab();
            bmp.Save("screenshot.png");
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
            GoogleEarthForm.Dispose();
            Application.Exit();
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

        public List<int> ContoursOfInterest
        {
            get
            {
                return AnimatorForm.txtContoursOfInterest.Text.Split(',').Select(x => int.Parse(x)).ToList();
            }
        }

        public int NoiseMetric
        {
            get
            {
                return SettingsForm.NoiseMetric;
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
                string message = (value <= 1) ? "completed in 1 minute" : value + " minutes remaining";
                RunForm.lblTimeLeft.Text = message;
            }
        }
        
        public double LowestContourValue
        {
            get
            {
                return double.Parse(AnimatorForm.txtLowestContourValue.Text);
            }
        }

        public double HighestContourValue
        {
            get
            {
                return double.Parse(AnimatorForm.txtHighestContourValue.Text);
            }
        }

        public double ContourValueStep
        {
            get
            {
                return double.Parse(AnimatorForm.txtContourStep.Text);
            }
        }

        public bool VisualiseGradient
        {
            get
            {
                return AnimatorForm.cbContourGradient.Checked;
            }
        }

        public bool VisualiseContoursOfInterest
        {
            get
            {
                return AnimatorForm.cbHighlightedContours.Checked;
            }
        }

        public double PopulationFactor
        {
            get
            {
                return double.Parse(AnimatorForm.txtPopulationFactor.Text);
            }
        }

        public int PopulationDotSize
        {
            get
            {
                return int.Parse(AnimatorForm.txtPopulationDotSize.Text);
            }
        }

        public int CameraAltitude
        {
            get
            {
                return int.Parse(AnimatorForm.txtCameraAltitude.Text);
            }
        }

        public string PopulationDotFile
        {
            get
            {
                return AnimatorForm.txtDotFile.Text;
            }
        }

        public void Invoke(MethodInvoker methodInvoker)
        {
            base.Invoke(methodInvoker);
        }

        #endregion

    }
}
