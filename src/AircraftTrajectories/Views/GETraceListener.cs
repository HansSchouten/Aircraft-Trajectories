using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace AircraftTrajectories.Views
{
    public class GETraceListener : TraceListener
    {
        private RichTextBox output;

        public GETraceListener(RichTextBox output)
        {
            this.Name = "Trace";
            this.output = output;
        }

        public override void Write(string message)
        {
            Action append = delegate ()
            {
                output.SelectionColor = System.Drawing.Color.DarkGray;
                output.AppendText(string.Format("[{0}] ", DateTime.Now.ToString()));

                if (message.Contains("error") || message.Contains("Error"))
                {
                    output.SelectionColor = System.Drawing.Color.DarkRed;
                }
                else
                {
                    output.SelectionColor = System.Drawing.Color.DarkBlue;
                }

                output.AppendText(message);
                output.SelectionStart = output.Text.Length;
                output.ScrollToCaret();
            };

            if (output.InvokeRequired)
            {
                output.BeginInvoke(append);
            }
            else
            {
                append();
            }
        }

        public override void WriteLine(string message)
        {
            Write(message + Environment.NewLine);
        }
    }
}
