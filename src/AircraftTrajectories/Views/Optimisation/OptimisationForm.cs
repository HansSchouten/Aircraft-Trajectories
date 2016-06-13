using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace AircraftTrajectories.Views.Optimisation
{
    public partial class OptimisationForm : Form
    {
        public OptimisationForm()
        {
            InitializeComponent();
        }

        OptimisationSettingsForm settingsForm = new OptimisationSettingsForm();
        OptimisationRunForm runForm = new OptimisationRunForm();
        private void OptimisationForm_Load(object sender, EventArgs e)
        {
            RemoveMDIContainerBorder();
            settingsForm.MdiParent = this;
            settingsForm.Show();
        }

        private void RemoveMDIContainerBorder()
        {
            var mdiclient = this.Controls.OfType<MdiClient>().Single();
            SuspendLayout();
            mdiclient.SuspendLayout();
            var hdiff = mdiclient.Size.Width - mdiclient.ClientSize.Width;
            var vdiff = mdiclient.Size.Height - mdiclient.ClientSize.Height;
            var size = new Size(mdiclient.Width + hdiff, mdiclient.Height + vdiff);
            var location = new Point(mdiclient.Left - (hdiff / 2), mdiclient.Top - (vdiff / 2));
            mdiclient.Dock = DockStyle.None;
            mdiclient.Size = size;
            mdiclient.Location = location;
            mdiclient.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;
            mdiclient.ResumeLayout(true);
            ResumeLayout(true);
        }

        private void OptimisationForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        public void RunOptimisation()
        {
            settingsForm.Hide();
            runForm = new OptimisationRunForm();
            runForm.MdiParent = this;
            runForm.Show();
        }

        public void CancelOptimisation()
        {
            runForm.Hide();
            settingsForm = new OptimisationSettingsForm();
            settingsForm.MdiParent = this;
            settingsForm.Show();
        }
    }
}
