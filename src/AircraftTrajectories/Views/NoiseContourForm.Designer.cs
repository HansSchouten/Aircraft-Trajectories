namespace AircraftTrajectories.Views
{
	partial class NoiseContourForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NoiseContourForm));
			this.label1 = new System.Windows.Forms.Label();
			this.lbVisibleContours = new System.Windows.Forms.ListBox();
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
			this.btnGenerateKMLFile = new System.Windows.Forms.Button();
			this.label25 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
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
			this.label1.Location = new System.Drawing.Point(308, 51);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(81, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Visible contours";
			// 
			// lbVisibleContours
			// 
			this.lbVisibleContours.FormattingEnabled = true;
			this.lbVisibleContours.Location = new System.Drawing.Point(308, 68);
			this.lbVisibleContours.Name = "lbVisibleContours";
			this.lbVisibleContours.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.lbVisibleContours.Size = new System.Drawing.Size(168, 238);
			this.lbVisibleContours.TabIndex = 2;
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(14, 96);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(66, 13);
			this.label7.TabIndex = 16;
			this.label7.Text = "Lines to skip";
			// 
			// nudSkipLinesCount
			// 
			this.nudSkipLinesCount.Location = new System.Drawing.Point(17, 112);
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
			this.label5.Location = new System.Drawing.Point(14, 272);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(121, 13);
			this.label5.TabIndex = 14;
			this.label5.Text = "Lower left corner Y [RD]";
			// 
			// nudLowerLeftY
			// 
			this.nudLowerLeftY.Location = new System.Drawing.Point(17, 288);
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
			this.label6.Location = new System.Drawing.Point(14, 228);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(121, 13);
			this.label6.TabIndex = 12;
			this.label6.Text = "Lower left corner X [RD]";
			// 
			// nudLowerLeftX
			// 
			this.nudLowerLeftX.Location = new System.Drawing.Point(17, 244);
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
			this.tbInputFile.Location = new System.Drawing.Point(17, 68);
			this.tbInputFile.Name = "tbInputFile";
			this.tbInputFile.Size = new System.Drawing.Size(147, 20);
			this.tbInputFile.TabIndex = 10;
			this.tbInputFile.Click += new System.EventHandler(this.tbInputFile_Click);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(14, 52);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(47, 13);
			this.label4.TabIndex = 9;
			this.label4.Text = "Input file";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(14, 184);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(134, 13);
			this.label3.TabIndex = 8;
			this.label3.Text = "Grid width [number of cells]";
			// 
			// nudGridWidth
			// 
			this.nudGridWidth.Location = new System.Drawing.Point(17, 200);
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
			this.label2.Location = new System.Drawing.Point(14, 140);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(83, 13);
			this.label2.TabIndex = 6;
			this.label2.Text = "Grid cell size [m]";
			// 
			// nudCellSize
			// 
			this.nudCellSize.Location = new System.Drawing.Point(17, 156);
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
			this.btnReadNoiseFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnReadNoiseFile.BackColor = System.Drawing.Color.DeepSkyBlue;
			this.btnReadNoiseFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnReadNoiseFile.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnReadNoiseFile.ForeColor = System.Drawing.Color.White;
			this.btnReadNoiseFile.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnReadNoiseFile.Location = new System.Drawing.Point(11, 328);
			this.btnReadNoiseFile.Margin = new System.Windows.Forms.Padding(2);
			this.btnReadNoiseFile.Name = "btnReadNoiseFile";
			this.btnReadNoiseFile.Size = new System.Drawing.Size(186, 36);
			this.btnReadNoiseFile.TabIndex = 105;
			this.btnReadNoiseFile.Text = "Read Noise File";
			this.btnReadNoiseFile.UseVisualStyleBackColor = false;
			this.btnReadNoiseFile.Click += new System.EventHandler(this.btnReadNoiseFile_Click);
			// 
			// btnGenerateKMLFile
			// 
			this.btnGenerateKMLFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnGenerateKMLFile.BackColor = System.Drawing.Color.DeepSkyBlue;
			this.btnGenerateKMLFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnGenerateKMLFile.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnGenerateKMLFile.ForeColor = System.Drawing.Color.White;
			this.btnGenerateKMLFile.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnGenerateKMLFile.Location = new System.Drawing.Point(308, 328);
			this.btnGenerateKMLFile.Margin = new System.Windows.Forms.Padding(2);
			this.btnGenerateKMLFile.Name = "btnGenerateKMLFile";
			this.btnGenerateKMLFile.Size = new System.Drawing.Size(186, 36);
			this.btnGenerateKMLFile.TabIndex = 106;
			this.btnGenerateKMLFile.Text = "Generate KML File";
			this.btnGenerateKMLFile.UseVisualStyleBackColor = false;
			this.btnGenerateKMLFile.Click += new System.EventHandler(this.btnGenerateKMLFile_Click);
			// 
			// label25
			// 
			this.label25.AutoSize = true;
			this.label25.Font = new System.Drawing.Font("Segoe UI Light", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label25.ForeColor = System.Drawing.Color.DodgerBlue;
			this.label25.Location = new System.Drawing.Point(307, 12);
			this.label25.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label25.Name = "label25";
			this.label25.Size = new System.Drawing.Size(188, 32);
			this.label25.TabIndex = 110;
			this.label25.Text = "KML Visualisation";
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Font = new System.Drawing.Font("Segoe UI Light", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label8.ForeColor = System.Drawing.Color.DodgerBlue;
			this.label8.Location = new System.Drawing.Point(11, 12);
			this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(169, 32);
			this.label8.TabIndex = 111;
			this.label8.Text = "Noise Input File";
			// 
			// NoiseContourForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(592, 375);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.label25);
			this.Controls.Add(this.nudSkipLinesCount);
			this.Controls.Add(this.lbVisibleContours);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.nudLowerLeftY);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.btnGenerateKMLFile);
			this.Controls.Add(this.nudLowerLeftX);
			this.Controls.Add(this.btnReadNoiseFile);
			this.Controls.Add(this.tbInputFile);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.nudCellSize);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.nudGridWidth);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "NoiseContourForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Noise Contour Visualiser";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ContourForm_FormClosing);
			this.Load += new System.EventHandler(this.ContourForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.nudSkipLinesCount)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nudLowerLeftY)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nudLowerLeftX)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nudGridWidth)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nudCellSize)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ListBox lbVisibleContours;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nudCellSize;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.NumericUpDown nudGridWidth;
		private System.Windows.Forms.OpenFileDialog ofdNoiseFile;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox tbInputFile;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.NumericUpDown nudLowerLeftY;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.NumericUpDown nudLowerLeftX;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.NumericUpDown nudSkipLinesCount;
		private System.Windows.Forms.Button btnReadNoiseFile;
		private System.Windows.Forms.Button btnGenerateKMLFile;
		private System.Windows.Forms.Label label25;
		private System.Windows.Forms.Label label8;
	}
}