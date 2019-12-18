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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ContourForm));
			this.label1 = new System.Windows.Forms.Label();
			this.lbVisibleContours = new System.Windows.Forms.ListBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.btnGenerateKMLFile = new System.Windows.Forms.Button();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.label7 = new System.Windows.Forms.Label();
			this.nudSkipLinesCount = new System.Windows.Forms.NumericUpDown();
			this.label5 = new System.Windows.Forms.Label();
			this.nudLowerLeftY = new System.Windows.Forms.NumericUpDown();
			this.label6 = new System.Windows.Forms.Label();
			this.nudLowerLeftX = new System.Windows.Forms.NumericUpDown();
			this.tbInputFile = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.nudGridWidth = new System.Windows.Forms.NumericUpDown();
			this.label2 = new System.Windows.Forms.Label();
			this.nudCellSize = new System.Windows.Forms.NumericUpDown();
			this.ofdNoiseFile = new System.Windows.Forms.OpenFileDialog();
			this.btnReadNoiseFile = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudSkipLinesCount)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nudLowerLeftY)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nudLowerLeftX)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nudGridWidth)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nudCellSize)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(13, 23);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(92, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Visualise contours";
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
			this.groupBox1.Location = new System.Drawing.Point(308, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(317, 307);
			this.groupBox1.TabIndex = 4;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "KML Visualisation";
			// 
			// btnGenerateKMLFile
			// 
			this.btnGenerateKMLFile.Location = new System.Drawing.Point(308, 325);
			this.btnGenerateKMLFile.Name = "btnGenerateKMLFile";
			this.btnGenerateKMLFile.Size = new System.Drawing.Size(148, 23);
			this.btnGenerateKMLFile.TabIndex = 5;
			this.btnGenerateKMLFile.Text = "Generate KML file";
			this.btnGenerateKMLFile.UseVisualStyleBackColor = true;
			this.btnGenerateKMLFile.Click += new System.EventHandler(this.btnGenerateKMLFile_Click);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.label7);
			this.groupBox2.Controls.Add(this.nudSkipLinesCount);
			this.groupBox2.Controls.Add(this.label5);
			this.groupBox2.Controls.Add(this.nudLowerLeftY);
			this.groupBox2.Controls.Add(this.label6);
			this.groupBox2.Controls.Add(this.nudLowerLeftX);
			this.groupBox2.Controls.Add(this.tbInputFile);
			this.groupBox2.Controls.Add(this.label4);
			this.groupBox2.Controls.Add(this.label3);
			this.groupBox2.Controls.Add(this.nudGridWidth);
			this.groupBox2.Controls.Add(this.label2);
			this.groupBox2.Controls.Add(this.nudCellSize);
			this.groupBox2.Location = new System.Drawing.Point(12, 12);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(290, 307);
			this.groupBox2.TabIndex = 6;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Noise data";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(12, 67);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(66, 13);
			this.label7.TabIndex = 16;
			this.label7.Text = "Lines to skip";
			// 
			// nudSkipLinesCount
			// 
			this.nudSkipLinesCount.Location = new System.Drawing.Point(15, 83);
			this.nudSkipLinesCount.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
			this.nudSkipLinesCount.Name = "nudSkipLinesCount";
			this.nudSkipLinesCount.Size = new System.Drawing.Size(147, 20);
			this.nudSkipLinesCount.TabIndex = 15;
			this.nudSkipLinesCount.Value = new decimal(new int[] {
            23,
            0,
            0,
            0});
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(12, 243);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(121, 13);
			this.label5.TabIndex = 14;
			this.label5.Text = "Lower left corner Y [RD]";
			// 
			// nudLowerLeftY
			// 
			this.nudLowerLeftY.Location = new System.Drawing.Point(15, 259);
			this.nudLowerLeftY.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
			this.nudLowerLeftY.Name = "nudLowerLeftY";
			this.nudLowerLeftY.Size = new System.Drawing.Size(147, 20);
			this.nudLowerLeftY.TabIndex = 13;
			this.nudLowerLeftY.Value = new decimal(new int[] {
            450000,
            0,
            0,
            0});
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(12, 199);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(121, 13);
			this.label6.TabIndex = 12;
			this.label6.Text = "Lower left corner X [RD]";
			// 
			// nudLowerLeftX
			// 
			this.nudLowerLeftX.Location = new System.Drawing.Point(15, 215);
			this.nudLowerLeftX.Maximum = new decimal(new int[] {
            5000000,
            0,
            0,
            0});
			this.nudLowerLeftX.Name = "nudLowerLeftX";
			this.nudLowerLeftX.Size = new System.Drawing.Size(147, 20);
			this.nudLowerLeftX.TabIndex = 11;
			this.nudLowerLeftX.Value = new decimal(new int[] {
            84000,
            0,
            0,
            0});
			// 
			// tbInputFile
			// 
			this.tbInputFile.Location = new System.Drawing.Point(15, 39);
			this.tbInputFile.Name = "tbInputFile";
			this.tbInputFile.Size = new System.Drawing.Size(147, 20);
			this.tbInputFile.TabIndex = 10;
			this.tbInputFile.Click += new System.EventHandler(this.tbInputFile_Click);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(12, 23);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(47, 13);
			this.label4.TabIndex = 9;
			this.label4.Text = "Input file";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(12, 155);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(134, 13);
			this.label3.TabIndex = 8;
			this.label3.Text = "Grid width [number of cells]";
			// 
			// nudGridWidth
			// 
			this.nudGridWidth.Location = new System.Drawing.Point(15, 171);
			this.nudGridWidth.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.nudGridWidth.Name = "nudGridWidth";
			this.nudGridWidth.Size = new System.Drawing.Size(147, 20);
			this.nudGridWidth.TabIndex = 7;
			this.nudGridWidth.Value = new decimal(new int[] {
            285,
            0,
            0,
            0});
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 111);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(83, 13);
			this.label2.TabIndex = 6;
			this.label2.Text = "Grid cell size [m]";
			// 
			// nudCellSize
			// 
			this.nudCellSize.Location = new System.Drawing.Point(15, 127);
			this.nudCellSize.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
			this.nudCellSize.Name = "nudCellSize";
			this.nudCellSize.Size = new System.Drawing.Size(147, 20);
			this.nudCellSize.TabIndex = 5;
			this.nudCellSize.Value = new decimal(new int[] {
            250,
            0,
            0,
            0});
			// 
			// btnReadNoiseFile
			// 
			this.btnReadNoiseFile.Location = new System.Drawing.Point(12, 325);
			this.btnReadNoiseFile.Name = "btnReadNoiseFile";
			this.btnReadNoiseFile.Size = new System.Drawing.Size(146, 23);
			this.btnReadNoiseFile.TabIndex = 7;
			this.btnReadNoiseFile.Text = "Read Noise Data";
			this.btnReadNoiseFile.UseVisualStyleBackColor = true;
			this.btnReadNoiseFile.Click += new System.EventHandler(this.btnReadNoiseFile_Click);
			// 
			// ContourForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(637, 360);
			this.Controls.Add(this.btnReadNoiseFile);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.btnGenerateKMLFile);
			this.Controls.Add(this.groupBox1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "ContourForm";
			this.Text = "Noise Contour Visualiser";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ContourForm_FormClosing);
			this.Load += new System.EventHandler(this.ContourForm_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudSkipLinesCount)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nudLowerLeftY)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nudLowerLeftX)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nudGridWidth)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nudCellSize)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ListBox lbVisibleContours;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnGenerateKMLFile;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nudCellSize;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.NumericUpDown nudGridWidth;
		private System.Windows.Forms.OpenFileDialog ofdNoiseFile;
		private System.Windows.Forms.Button btnReadNoiseFile;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox tbInputFile;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.NumericUpDown nudLowerLeftY;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.NumericUpDown nudLowerLeftX;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.NumericUpDown nudSkipLinesCount;
	}
}