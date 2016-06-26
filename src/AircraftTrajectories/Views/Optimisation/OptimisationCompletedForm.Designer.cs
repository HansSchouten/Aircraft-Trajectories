namespace AircraftTrajectories.Views.Optimisation
{
    partial class OptimisationCompletedForm
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnVisualise = new System.Windows.Forms.Button();
            this.chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.btnAltitudeTime = new System.Windows.Forms.Button();
            this.btnGroundpath = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.btnAltitudeDistance = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.chart)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSave.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(9, 392);
            this.btnSave.Margin = new System.Windows.Forms.Padding(2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(186, 48);
            this.btnSave.TabIndex = 105;
            this.btnSave.Text = "Save trajectory to file";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnVisualise
            // 
            this.btnVisualise.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnVisualise.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.btnVisualise.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVisualise.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVisualise.ForeColor = System.Drawing.Color.White;
            this.btnVisualise.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnVisualise.Location = new System.Drawing.Point(211, 392);
            this.btnVisualise.Margin = new System.Windows.Forms.Padding(2);
            this.btnVisualise.Name = "btnVisualise";
            this.btnVisualise.Size = new System.Drawing.Size(186, 48);
            this.btnVisualise.TabIndex = 106;
            this.btnVisualise.Text = "Visualise trajectory";
            this.btnVisualise.UseVisualStyleBackColor = false;
            this.btnVisualise.Click += new System.EventHandler(this.btnVisualise_Click);
            // 
            // chart
            // 
            chartArea3.Name = "ChartArea1";
            this.chart.ChartAreas.Add(chartArea3);
            legend3.Name = "Legend1";
            this.chart.Legends.Add(legend3);
            this.chart.Location = new System.Drawing.Point(12, 44);
            this.chart.Name = "chart";
            series3.ChartArea = "ChartArea1";
            series3.Legend = "Legend1";
            series3.Name = "Altitude [m]";
            this.chart.Series.Add(series3);
            this.chart.Size = new System.Drawing.Size(636, 343);
            this.chart.TabIndex = 115;
            // 
            // btnAltitudeTime
            // 
            this.btnAltitudeTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAltitudeTime.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.btnAltitudeTime.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAltitudeTime.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAltitudeTime.ForeColor = System.Drawing.Color.White;
            this.btnAltitudeTime.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAltitudeTime.Location = new System.Drawing.Point(653, 54);
            this.btnAltitudeTime.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.btnAltitudeTime.Name = "btnAltitudeTime";
            this.btnAltitudeTime.Size = new System.Drawing.Size(186, 41);
            this.btnAltitudeTime.TabIndex = 116;
            this.btnAltitudeTime.Text = "Altitude vs Time";
            this.btnAltitudeTime.UseVisualStyleBackColor = false;
            this.btnAltitudeTime.Click += new System.EventHandler(this.btnAltitudeTime_Click);
            // 
            // btnGroundpath
            // 
            this.btnGroundpath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnGroundpath.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.btnGroundpath.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGroundpath.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGroundpath.ForeColor = System.Drawing.Color.White;
            this.btnGroundpath.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGroundpath.Location = new System.Drawing.Point(653, 182);
            this.btnGroundpath.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.btnGroundpath.Name = "btnGroundpath";
            this.btnGroundpath.Size = new System.Drawing.Size(186, 41);
            this.btnGroundpath.TabIndex = 117;
            this.btnGroundpath.Text = "Groundpath";
            this.btnGroundpath.UseVisualStyleBackColor = false;
            this.btnGroundpath.Click += new System.EventHandler(this.btnGroundpath_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI Light", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label10.Location = new System.Drawing.Point(11, 9);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(202, 32);
            this.label10.TabIndex = 118;
            this.label10.Text = "Optimisation result";
            // 
            // btnAltitudeDistance
            // 
            this.btnAltitudeDistance.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAltitudeDistance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.btnAltitudeDistance.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAltitudeDistance.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAltitudeDistance.ForeColor = System.Drawing.Color.White;
            this.btnAltitudeDistance.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAltitudeDistance.Location = new System.Drawing.Point(653, 117);
            this.btnAltitudeDistance.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.btnAltitudeDistance.Name = "btnAltitudeDistance";
            this.btnAltitudeDistance.Size = new System.Drawing.Size(186, 41);
            this.btnAltitudeDistance.TabIndex = 119;
            this.btnAltitudeDistance.Text = "Altitude vs Distance";
            this.btnAltitudeDistance.UseVisualStyleBackColor = false;
            this.btnAltitudeDistance.Click += new System.EventHandler(this.btnAltitudeDistance_Click);
            // 
            // OptimisationCompletedForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(864, 448);
            this.ControlBox = false;
            this.Controls.Add(this.btnAltitudeDistance);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.btnGroundpath);
            this.Controls.Add(this.btnAltitudeTime);
            this.Controls.Add(this.chart);
            this.Controls.Add(this.btnVisualise);
            this.Controls.Add(this.btnSave);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "OptimisationCompletedForm";
            this.Load += new System.EventHandler(this.OptimisationCompletedForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chart)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnVisualise;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart;
        private System.Windows.Forms.Button btnAltitudeTime;
        private System.Windows.Forms.Button btnGroundpath;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnAltitudeDistance;
    }
}