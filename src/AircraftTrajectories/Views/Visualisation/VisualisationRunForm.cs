using System;
using System.Windows.Forms;

namespace AircraftTrajectories.Views.Visualisation
{
    public partial class VisualisationRunForm : Form
    {
        public VisualisationRunForm()
        {
            InitializeComponent();
        }

        protected string _message;
        public string Message
        {
            get
            {
                return _message;
            }
            set
            {
                _message = value;
                lblPercentage.Text = _message;
            }
        }

        public Action CancelCallback { get; set; }

        public string CancelButtonText
        {
            set
            {
                btnCancel.Text = value;
            }
        }

        protected void VisualisationRunForm_Load(object sender, EventArgs e)
        {
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            ControlBox = false;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            CancelCallback();
        }
    }
}
