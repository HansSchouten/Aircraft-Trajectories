using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using FC.GEPluginCtrls;
using FC.GEPluginCtrls.Geo;
using FC.GEPluginCtrls.HttpServer;
using System.Diagnostics;

namespace AircraftTrajectories.Views
{
    public partial class GoogleEarthServerForm : Form
    {
        private dynamic ge = null;
        private GEServer server = null;

        public GoogleEarthServerForm()
        {
            InitializeComponent();

            GETraceListener trace = new GETraceListener(richTextBox);
            richTextBox.LinkClicked += (o, e) => { Process.Start(e.LinkText); };            
            Debug.Listeners.Add(trace);

            server = new GEServer(System.Net.IPAddress.Loopback, 8080, "webroot");
            server.Start();

            this.geWebBrowser.PluginReady += geWebBrowser_PluginReady;
            this.geWebBrowser.KmlLoaded += geWebBrowser_KmlLoaded;
            this.geWebBrowser.LoadEmbeddedPlugin();
            this.geWebBrowser.ScriptError += (o, ea) =>
            {
                Debug.WriteLine(ea.Message + ": " + ea.Data, "Script-Error");
            };
        }

        void geWebBrowser_PluginReady(object sender, GEEventArgs e)
        {
            this.ge = e.ApiObject;

            //this.geToolStrip1.SetBrowserInstance(this.geWebBrowser);
            //this.geStatusStrip1.SetBrowserInstance(this.geWebBrowser);
            this.kmlTreeView.SetBrowserInstance(this.geWebBrowser);

            //this.geWebBrowser.FetchKml("http://localhost:8080/B733_Fuel_Animation.kml");
            this.geWebBrowser.FetchKml("http://localhost:8080/animation.kml");
            //this.geWebBrowser.FetchKml("http://localhost:8080/CRW_5km_Product_Suite.kmz");
        }

        void geWebBrowser_KmlLoaded(object sender, GEEventArgs e)
        {
            var kml = e.ApiObject;
            kmlTreeView.ParseKmlObject(kml);
            geWebBrowser.ParseKmlObject(kml);
        }

        private void toolStripMenuItemClear_Click(object sender, EventArgs e)
        {
            this.richTextBox.Clear();
        }
    }













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
            Action append = delegate()
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
