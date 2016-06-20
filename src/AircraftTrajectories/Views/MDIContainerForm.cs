using System.Windows.Forms;
using System.Drawing;
using System.Linq;

namespace AircraftTrajectories.Views
{
    public class MDIContainerForm : Form
    {
        protected void MDIContainerForm_Load()
        {
            RemoveMDIContainerBorder();
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

    }
}
