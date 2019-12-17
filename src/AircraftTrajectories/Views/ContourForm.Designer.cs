namespace AircraftTrajectories.Views
{
	partial class ContourForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.lbVisibleContours = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnGenerateKMLFile = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Visualise contours:";
            // 
            // lbVisibleContours
            // 
            this.lbVisibleContours.FormattingEnabled = true;
            this.lbVisibleContours.Location = new System.Drawing.Point(16, 39);
            this.lbVisibleContours.Name = "lbVisibleContours";
            this.lbVisibleContours.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbVisibleContours.Size = new System.Drawing.Size(154, 251);
            this.lbVisibleContours.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lbVisibleContours);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(613, 307);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Configuration";
            // 
            // btnGenerateKMLFile
            // 
            this.btnGenerateKMLFile.Location = new System.Drawing.Point(12, 325);
            this.btnGenerateKMLFile.Name = "btnGenerateKMLFile";
            this.btnGenerateKMLFile.Size = new System.Drawing.Size(148, 23);
            this.btnGenerateKMLFile.TabIndex = 5;
            this.btnGenerateKMLFile.Text = "Generate KML file";
            this.btnGenerateKMLFile.UseVisualStyleBackColor = true;
            this.btnGenerateKMLFile.Click += new System.EventHandler(this.btnGenerateKMLFile_Click);
            // 
            // ContourForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(637, 360);
            this.Controls.Add(this.btnGenerateKMLFile);
            this.Controls.Add(this.groupBox1);
            this.Name = "ContourForm";
            this.Text = "Schiphol Noise Contours";
            this.Load += new System.EventHandler(this.ContourForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ListBox lbVisibleContours;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnGenerateKMLFile;
    }
}