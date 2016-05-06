namespace AircraftTrajectories.Views
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
            this.pnlControl = new System.Windows.Forms.Panel();
            this.btnAnimate = new System.Windows.Forms.Button();
            this.btnCenterRunway = new System.Windows.Forms.Button();
            this.webBrowser = new System.Windows.Forms.WebBrowser();
            this.tmrAnimationStep = new System.Windows.Forms.Timer(this.components);
            this.tmrFileCheck = new System.Windows.Forms.Timer(this.components);
            this.pnlControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlControl
            // 
            this.pnlControl.Controls.Add(this.btnAnimate);
            this.pnlControl.Controls.Add(this.btnCenterRunway);
            this.pnlControl.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlControl.Location = new System.Drawing.Point(0, 506);
            this.pnlControl.Name = "pnlControl";
            this.pnlControl.Size = new System.Drawing.Size(976, 67);
            this.pnlControl.TabIndex = 0;
            // 
            // btnAnimate
            // 
            this.btnAnimate.Location = new System.Drawing.Point(198, 13);
            this.btnAnimate.Name = "btnAnimate";
            this.btnAnimate.Size = new System.Drawing.Size(150, 40);
            this.btnAnimate.TabIndex = 1;
            this.btnAnimate.Text = "Animate";
            this.btnAnimate.UseVisualStyleBackColor = true;
            this.btnAnimate.Click += new System.EventHandler(this.btnAnimate_Click);
            // 
            // btnCenterRunway
            // 
            this.btnCenterRunway.Location = new System.Drawing.Point(12, 13);
            this.btnCenterRunway.Name = "btnCenterRunway";
            this.btnCenterRunway.Size = new System.Drawing.Size(170, 40);
            this.btnCenterRunway.TabIndex = 0;
            this.btnCenterRunway.Text = "Center RWY 06";
            this.btnCenterRunway.UseVisualStyleBackColor = true;
            this.btnCenterRunway.Click += new System.EventHandler(this.btnCenterRunway_Click);
            // 
            // webBrowser
            // 
            this.webBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser.Location = new System.Drawing.Point(0, 0);
            this.webBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser.Name = "webBrowser";
            this.webBrowser.Size = new System.Drawing.Size(976, 506);
            this.webBrowser.TabIndex = 1;
            // 
            // tmrAnimationStep
            // 
            this.tmrAnimationStep.Interval = 1;
            this.tmrAnimationStep.Tick += new System.EventHandler(this.tmrAnimationStep_Tick);
            // 
            // tmrFileCheck
            // 
            this.tmrFileCheck.Interval = 50;
            this.tmrFileCheck.Tick += new System.EventHandler(this.tmrFileCheck_Tick);
            // 
            // GoogleEarthForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(976, 573);
            this.Controls.Add(this.webBrowser);
            this.Controls.Add(this.pnlControl);
            this.Name = "GoogleEarthForm";
            this.Text = "Google Earth";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.GoogleEarthForm_Load);
            this.pnlControl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlControl;
        private System.Windows.Forms.WebBrowser webBrowser;
        private System.Windows.Forms.Timer tmrAnimationStep;
        private System.Windows.Forms.Button btnCenterRunway;
        private System.Windows.Forms.Button btnAnimate;
        private System.Windows.Forms.Timer tmrFileCheck;
    }
}

