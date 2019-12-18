namespace AircraftTrajectories.Views
{
    partial class StartupForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StartupForm));
			this.lblName = new System.Windows.Forms.Label();
			this.pbClose = new System.Windows.Forms.PictureBox();
			this.btnOptimise = new System.Windows.Forms.Button();
			this.btnVisualiseTrajectory = new System.Windows.Forms.Button();
			this.btnVisualiseNoise = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.pbClose)).BeginInit();
			this.SuspendLayout();
			// 
			// lblName
			// 
			this.lblName.AutoSize = true;
			this.lblName.Font = new System.Drawing.Font("Segoe UI Light", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblName.ForeColor = System.Drawing.Color.DeepSkyBlue;
			this.lblName.Location = new System.Drawing.Point(10, 9);
			this.lblName.Name = "lblName";
			this.lblName.Size = new System.Drawing.Size(122, 32);
			this.lblName.TabIndex = 14;
			this.lblName.Text = "AeroVision";
			// 
			// pbClose
			// 
			this.pbClose.BackColor = System.Drawing.Color.Transparent;
			this.pbClose.Image = global::AircraftTrajectories.Properties.Resources.close;
			this.pbClose.Location = new System.Drawing.Point(435, 9);
			this.pbClose.Name = "pbClose";
			this.pbClose.Size = new System.Drawing.Size(18, 18);
			this.pbClose.TabIndex = 13;
			this.pbClose.TabStop = false;
			this.pbClose.Click += new System.EventHandler(this.pbClose_Click);
			this.pbClose.MouseEnter += new System.EventHandler(this.pbClose_MouseEnter);
			this.pbClose.MouseLeave += new System.EventHandler(this.pbClose_MouseLeave);
			// 
			// btnOptimise
			// 
			this.btnOptimise.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.btnOptimise.BackColor = System.Drawing.Color.DeepSkyBlue;
			this.btnOptimise.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnOptimise.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnOptimise.ForeColor = System.Drawing.Color.White;
			this.btnOptimise.Image = ((System.Drawing.Image)(resources.GetObject("btnOptimise.Image")));
			this.btnOptimise.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnOptimise.Location = new System.Drawing.Point(32, 104);
			this.btnOptimise.Name = "btnOptimise";
			this.btnOptimise.Size = new System.Drawing.Size(396, 80);
			this.btnOptimise.TabIndex = 11;
			this.btnOptimise.Text = "Optimise Trajectory";
			this.btnOptimise.UseVisualStyleBackColor = false;
			this.btnOptimise.Click += new System.EventHandler(this.btnOptimise_Click);
			// 
			// btnVisualiseTrajectory
			// 
			this.btnVisualiseTrajectory.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.btnVisualiseTrajectory.BackColor = System.Drawing.Color.DeepSkyBlue;
			this.btnVisualiseTrajectory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnVisualiseTrajectory.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnVisualiseTrajectory.ForeColor = System.Drawing.Color.White;
			this.btnVisualiseTrajectory.Image = ((System.Drawing.Image)(resources.GetObject("btnVisualiseTrajectory.Image")));
			this.btnVisualiseTrajectory.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnVisualiseTrajectory.Location = new System.Drawing.Point(32, 210);
			this.btnVisualiseTrajectory.Name = "btnVisualiseTrajectory";
			this.btnVisualiseTrajectory.Size = new System.Drawing.Size(396, 80);
			this.btnVisualiseTrajectory.TabIndex = 10;
			this.btnVisualiseTrajectory.Text = "Visualise Trajectory";
			this.btnVisualiseTrajectory.UseVisualStyleBackColor = false;
			this.btnVisualiseTrajectory.Click += new System.EventHandler(this.btnVisualise_Click);
			// 
			// btnVisualiseNoise
			// 
			this.btnVisualiseNoise.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.btnVisualiseNoise.BackColor = System.Drawing.Color.DeepSkyBlue;
			this.btnVisualiseNoise.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnVisualiseNoise.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnVisualiseNoise.ForeColor = System.Drawing.Color.White;
			this.btnVisualiseNoise.Image = global::AircraftTrajectories.Properties.Resources.appbar_location_circle;
			this.btnVisualiseNoise.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnVisualiseNoise.Location = new System.Drawing.Point(32, 316);
			this.btnVisualiseNoise.Name = "btnVisualiseNoise";
			this.btnVisualiseNoise.Size = new System.Drawing.Size(396, 80);
			this.btnVisualiseNoise.TabIndex = 15;
			this.btnVisualiseNoise.Text = "Visualise Noise Contours";
			this.btnVisualiseNoise.UseVisualStyleBackColor = false;
			this.btnVisualiseNoise.Click += new System.EventHandler(this.btnVisualiseNoise_Click);
			// 
			// StartupForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(465, 493);
			this.Controls.Add(this.btnVisualiseNoise);
			this.Controls.Add(this.lblName);
			this.Controls.Add(this.pbClose);
			this.Controls.Add(this.btnOptimise);
			this.Controls.Add(this.btnVisualiseTrajectory);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(2);
			this.Name = "StartupForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "StartupForm";
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form_Paint);
			((System.ComponentModel.ISupportInitialize)(this.pbClose)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblName;
        internal System.Windows.Forms.PictureBox pbClose;
        private System.Windows.Forms.Button btnOptimise;
        private System.Windows.Forms.Button btnVisualiseTrajectory;
		private System.Windows.Forms.Button btnVisualiseNoise;
	}
}