namespace AircraftTrajectories.Views
{
    partial class GoogleEarthServerForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.splitTreeBrowser = new System.Windows.Forms.SplitContainer();
            this.kmlTreeView = new FC.GEPluginCtrls.KmlTreeView();
            this.geWebBrowser = new FC.GEPluginCtrls.GEWebBrowser();
            this.splitBrowserTextbox = new System.Windows.Forms.SplitContainer();
            this.richTextBox = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitTreeBrowser)).BeginInit();
            this.splitTreeBrowser.Panel1.SuspendLayout();
            this.splitTreeBrowser.Panel2.SuspendLayout();
            this.splitTreeBrowser.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitBrowserTextbox)).BeginInit();
            this.splitBrowserTextbox.Panel1.SuspendLayout();
            this.splitBrowserTextbox.Panel2.SuspendLayout();
            this.splitBrowserTextbox.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitTreeBrowser
            // 
            this.splitTreeBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitTreeBrowser.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitTreeBrowser.Location = new System.Drawing.Point(0, 0);
            this.splitTreeBrowser.Name = "splitTreeBrowser";
            // 
            // splitTreeBrowser.Panel1
            // 
            this.splitTreeBrowser.Panel1.Controls.Add(this.kmlTreeView);
            // 
            // splitTreeBrowser.Panel2
            // 
            this.splitTreeBrowser.Panel2.Controls.Add(this.splitBrowserTextbox);
            this.splitTreeBrowser.Size = new System.Drawing.Size(841, 585);
            this.splitTreeBrowser.SplitterDistance = 269;
            this.splitTreeBrowser.TabIndex = 0;
            // 
            // kmlTreeView
            // 
            this.kmlTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kmlTreeView.ImageKey = "ge";
            this.kmlTreeView.Location = new System.Drawing.Point(0, 0);
            this.kmlTreeView.Name = "kmlTreeView";
            this.kmlTreeView.SelectedImageIndex = 0;
            this.kmlTreeView.Size = new System.Drawing.Size(269, 585);
            this.kmlTreeView.TabIndex = 0;
            // 
            // geWebBrowser
            // 
            this.geWebBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.geWebBrowser.ImageryBase = FC.GEPluginCtrls.ImageryBase.Earth;
            this.geWebBrowser.IsWebBrowserContextMenuEnabled = false;
            this.geWebBrowser.Location = new System.Drawing.Point(0, 0);
            this.geWebBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.geWebBrowser.Name = "geWebBrowser";
            this.geWebBrowser.RedirectLinksToSystemBrowser = false;
            this.geWebBrowser.ScrollBarsEnabled = false;
            this.geWebBrowser.Size = new System.Drawing.Size(568, 395);
            this.geWebBrowser.TabIndex = 0;
            this.geWebBrowser.WebBrowserShortcutsEnabled = false;
            // 
            // splitBrowserTextbox
            // 
            this.splitBrowserTextbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitBrowserTextbox.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitBrowserTextbox.Location = new System.Drawing.Point(0, 0);
            this.splitBrowserTextbox.Name = "splitBrowserTextbox";
            this.splitBrowserTextbox.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitBrowserTextbox.Panel1
            // 
            this.splitBrowserTextbox.Panel1.Controls.Add(this.geWebBrowser);
            // 
            // splitBrowserTextbox.Panel2
            // 
            this.splitBrowserTextbox.Panel2.Controls.Add(this.richTextBox);
            this.splitBrowserTextbox.Size = new System.Drawing.Size(568, 585);
            this.splitBrowserTextbox.SplitterDistance = 395;
            this.splitBrowserTextbox.TabIndex = 0;
            // 
            // richTextBox
            // 
            this.richTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox.Location = new System.Drawing.Point(0, 0);
            this.richTextBox.Name = "richTextBox";
            this.richTextBox.Size = new System.Drawing.Size(568, 186);
            this.richTextBox.TabIndex = 0;
            this.richTextBox.Text = "";
            // 
            // GoogleEarthServerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(841, 585);
            this.Controls.Add(this.splitTreeBrowser);
            this.Name = "GoogleEarthServerForm";
            this.Text = "GoogleEarthServerForm";
            this.splitTreeBrowser.Panel1.ResumeLayout(false);
            this.splitTreeBrowser.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitTreeBrowser)).EndInit();
            this.splitTreeBrowser.ResumeLayout(false);
            this.splitBrowserTextbox.Panel1.ResumeLayout(false);
            this.splitBrowserTextbox.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitBrowserTextbox)).EndInit();
            this.splitBrowserTextbox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitTreeBrowser;
        private FC.GEPluginCtrls.GEWebBrowser geWebBrowser;
        private FC.GEPluginCtrls.KmlTreeView kmlTreeView;
        private System.Windows.Forms.SplitContainer splitBrowserTextbox;
        private System.Windows.Forms.RichTextBox richTextBox;
    }
}