using System;

namespace AircraftTrajectories.Views.Visualisation
{
    public partial class VisualisationCalculateNoiseForm : VisualisationRunForm
    {
        public VisualisationCalculateNoiseForm() : base()
        {
            Message = "Calculating noise";
        }

        private void InitializeComponent()
        {
            throw new NotImplementedException();
        }
        
        protected override void btnCancel_Click(object sender, EventArgs e)
        {
            ((VisualisationForm)this.MdiParent).CancelNoiseClick();
        }
    }
}
