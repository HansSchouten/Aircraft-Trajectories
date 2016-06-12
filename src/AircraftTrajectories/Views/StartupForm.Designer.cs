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
            this.btnVisualise = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbClose)).BeginInit();
            this.SuspendLayout();
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Segoe UI Light", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.lblName.Location = new System.Drawing.Point(13, 11);
            this.lblName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(153, 41);
            this.lblName.TabIndex = 14;
            this.lblName.Text = "AeroVision";
            // 
            // pbClose
            // 
            this.pbClose.BackColor = System.Drawing.Color.Transparent;
            this.pbClose.Image = global::AircraftTrajectories.Properties.Resources.close;
            this.pbClose.Location = new System.Drawing.Point(544, 11);
            this.pbClose.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pbClose.Name = "pbClose";
            this.pbClose.Size = new System.Drawing.Size(24, 22);
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
            this.btnOptimise.Location = new System.Drawing.Point(45, 110);
            this.btnOptimise.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnOptimise.Name = "btnOptimise";
            this.btnOptimise.Size = new System.Drawing.Size(489, 98);
            this.btnOptimise.TabIndex = 11;
            this.btnOptimise.Text = "Optimise trajectory";
            this.btnOptimise.UseVisualStyleBackColor = false;
            this.btnOptimise.Click += new System.EventHandler(this.btnOptimise_Click);
            // 
            // btnVisualise
            // 
            this.btnVisualise.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnVisualise.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.btnVisualise.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVisualise.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVisualise.ForeColor = System.Drawing.Color.White;
            this.btnVisualise.Image = ((System.Drawing.Image)(resources.GetObject("btnVisualise.Image")));
            this.btnVisualise.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnVisualise.Location = new System.Drawing.Point(45, 255);
            this.btnVisualise.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnVisualise.Name = "btnVisualise";
            this.btnVisualise.Size = new System.Drawing.Size(489, 98);
            this.btnVisualise.TabIndex = 10;
            this.btnVisualise.Text = "Visualise trajectory";
            this.btnVisualise.UseVisualStyleBackColor = false;
            this.btnVisualise.Click += new System.EventHandler(this.btnVisualise_Click);
            // 
            // StartupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(580, 438);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.pbClose);
            this.Controls.Add(this.btnOptimise);
            this.Controls.Add(this.btnVisualise);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
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
        private System.Windows.Forms.Button btnVisualise;
    }
}