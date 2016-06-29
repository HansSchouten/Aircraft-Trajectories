using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using FC.GEPluginCtrls;
using FC.GEPluginCtrls.Geo;
using FC.GEPluginCtrls.HttpServer;
using System.Diagnostics;

namespace AircraftTrajectories.Views.Visualisation
{
    public partial class GoogleEarthForm : Form
    {
        private dynamic ge = null;
        public GEServer server = null;

        public GoogleEarthForm()
        {
            InitializeComponent();

            //GETraceListener trace = new GETraceListener(richTextBox);
            richTextBox.LinkClicked += (o, e) => { Process.Start(e.LinkText); };
            //Debug.Listeners.Add(trace);

            server = new GEServer(System.Net.IPAddress.Loopback, 8080, "webroot");
            server.Start();
            statusLabel.Text = "Loading Google Earth..";

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
            
            if (wait)
            {
                this.geWebBrowser.FetchKml("http://localhost:8080/visualisation.kml");        //DEBUG
                wait = false;
            }

            statusLabel.Text = "Loading KML..";
        }

        void geWebBrowser_KmlLoaded(object sender, GEEventArgs e)
        {
            var kml = e.ApiObject;
            kmlTreeView.ParseKmlObject(kml);
            kmlTreeView.Nodes[0].Expand();
            geWebBrowser.ParseKmlObject(kml);
            statusLabel.Text = "KML loaded";
        }

        private void GoogleEarthForm_Load(object sender, EventArgs e)
        {
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            ControlBox = false;

            splitTreeBrowser.Panel1Collapsed = true;
            splitTreeBrowser.Panel1.Hide();
            splitBrowserTextbox.Panel2Collapsed = true;
            splitBrowserTextbox.Panel2.Hide();

            splitTreeBrowser.Panel1Collapsed = false;
            splitTreeBrowser.Panel1.Show();
        }

        public bool wait = false;
        public void Visualise(string kml)
        {;
            this.MdiParent.WindowState = FormWindowState.Maximized;
            //kmlTreeView.Nodes.Clear();
            if (geWebBrowser.PluginIsReady)
            {
                this.geWebBrowser.FetchKml("http://localhost:8080/" + kml);       //DEBUG
            } else
            {
                wait = true;
            }
        }
    }
}
