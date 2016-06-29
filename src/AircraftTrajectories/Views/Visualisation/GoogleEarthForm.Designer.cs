namespace AircraftTrajectories.Views.Visualisation
{
    partial class GoogleEarthForm
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
            this.splitBrowserTextbox = new System.Windows.Forms.SplitContainer();
            this.geWebBrowser = new FC.GEPluginCtrls.GEWebBrowser();
            this.richTextBox = new System.Windows.Forms.RichTextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.splitTreeBrowser)).BeginInit();
            this.splitTreeBrowser.Panel1.SuspendLayout();
            this.splitTreeBrowser.Panel2.SuspendLayout();
            this.splitTreeBrowser.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitBrowserTextbox)).BeginInit();
            this.splitBrowserTextbox.Panel1.SuspendLayout();
            this.splitBrowserTextbox.Panel2.SuspendLayout();
            this.splitBrowserTextbox.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitTreeBrowser
            // 
            this.splitTreeBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitTreeBrowser.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitTreeBrowser.Location = new System.Drawing.Point(0, 0);
            this.splitTreeBrowser.Margin = new System.Windows.Forms.Padding(4);
            this.splitTreeBrowser.Name = "splitTreeBrowser";
            // 
            // splitTreeBrowser.Panel1
            // 
            this.splitTreeBrowser.Panel1.Controls.Add(this.kmlTreeView);
            // 
            // splitTreeBrowser.Panel2
            // 
            this.splitTreeBrowser.Panel2.Controls.Add(this.splitBrowserTextbox);
            this.splitTreeBrowser.Size = new System.Drawing.Size(1144, 641);
            this.splitTreeBrowser.SplitterDistance = 220;
            this.splitTreeBrowser.SplitterWidth = 5;
            this.splitTreeBrowser.TabIndex = 1;
            // 
            // kmlTreeView
            // 
            this.kmlTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kmlTreeView.ImageKey = "ge";
            this.kmlTreeView.Location = new System.Drawing.Point(0, 0);
            this.kmlTreeView.Margin = new System.Windows.Forms.Padding(4);
            this.kmlTreeView.Name = "kmlTreeView";
            this.kmlTreeView.SelectedImageIndex = 0;
            this.kmlTreeView.Size = new System.Drawing.Size(220, 641);
            this.kmlTreeView.TabIndex = 0;
            // 
            // splitBrowserTextbox
            // 
            this.splitBrowserTextbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitBrowserTextbox.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitBrowserTextbox.Location = new System.Drawing.Point(0, 0);
            this.splitBrowserTextbox.Margin = new System.Windows.Forms.Padding(4);
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
            this.splitBrowserTextbox.Size = new System.Drawing.Size(919, 641);
            this.splitBrowserTextbox.SplitterDistance = 449;
            this.splitBrowserTextbox.SplitterWidth = 5;
            this.splitBrowserTextbox.TabIndex = 0;
            // 
            // geWebBrowser
            // 
            this.geWebBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.geWebBrowser.ImageryBase = FC.GEPluginCtrls.ImageryBase.Earth;
            this.geWebBrowser.IsWebBrowserContextMenuEnabled = false;
            this.geWebBrowser.Location = new System.Drawing.Point(0, 0);
            this.geWebBrowser.Margin = new System.Windows.Forms.Padding(4);
            this.geWebBrowser.MinimumSize = new System.Drawing.Size(27, 25);
            this.geWebBrowser.Name = "geWebBrowser";
            this.geWebBrowser.RedirectLinksToSystemBrowser = false;
            this.geWebBrowser.ScrollBarsEnabled = false;
            this.geWebBrowser.Size = new System.Drawing.Size(919, 449);
            this.geWebBrowser.TabIndex = 0;
            this.geWebBrowser.WebBrowserShortcutsEnabled = false;
            // 
            // richTextBox
            // 
            this.richTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox.Location = new System.Drawing.Point(0, 0);
            this.richTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.richTextBox.Name = "richTextBox";
            this.richTextBox.Size = new System.Drawing.Size(919, 187);
            this.richTextBox.TabIndex = 0;
            this.richTextBox.Text = "";
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.statusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 614);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1144, 27);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            this.statusStrip1.Visible = false;
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(52, 22);
            this.toolStripStatusLabel1.Text = "Status:";
            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(34, 22);
            this.statusLabel.Text = "idle";
            // 
            // GoogleEarthForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1144, 641);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.splitTreeBrowser);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "GoogleEarthForm";
            this.Load += new System.EventHandler(this.GoogleEarthForm_Load);
            this.splitTreeBrowser.Panel1.ResumeLayout(false);
            this.splitTreeBrowser.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitTreeBrowser)).EndInit();
            this.splitTreeBrowser.ResumeLayout(false);
            this.splitBrowserTextbox.Panel1.ResumeLayout(false);
            this.splitBrowserTextbox.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitBrowserTextbox)).EndInit();
            this.splitBrowserTextbox.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitTreeBrowser;
        private FC.GEPluginCtrls.KmlTreeView kmlTreeView;
        private System.Windows.Forms.SplitContainer splitBrowserTextbox;
        public FC.GEPluginCtrls.GEWebBrowser geWebBrowser;
        private System.Windows.Forms.RichTextBox richTextBox;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
    }
}