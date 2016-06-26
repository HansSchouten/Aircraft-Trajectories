namespace AircraftTrajectories.Views
{
    partial class Test
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chartAltitude = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartGroundPath = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            ((System.ComponentModel.ISupportInitialize)(this.chartAltitude)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartGroundPath)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // chartAltitude
            // 
            chartArea1.Name = "ChartArea1";
            this.chartAltitude.ChartAreas.Add(chartArea1);
            this.chartAltitude.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.chartAltitude.Legends.Add(legend1);
            this.chartAltitude.Location = new System.Drawing.Point(3, 3);
            this.chartAltitude.Name = "chartAltitude";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Altitude [m]";
            this.chartAltitude.Series.Add(series1);
            this.chartAltitude.Size = new System.Drawing.Size(670, 469);
            this.chartAltitude.TabIndex = 0;
            this.chartAltitude.Text = "chartAltitude";
            // 
            // chartGroundPath
            // 
            chartArea2.Name = "ChartArea1";
            this.chartGroundPath.ChartAreas.Add(chartArea2);
            this.chartGroundPath.Dock = System.Windows.Forms.DockStyle.Fill;
            legend2.Name = "Legend1";
            this.chartGroundPath.Legends.Add(legend2);
            this.chartGroundPath.Location = new System.Drawing.Point(3, 3);
            this.chartGroundPath.Name = "chartGroundPath";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Groundpath";
            this.chartGroundPath.Series.Add(series2);
            this.chartGroundPath.Size = new System.Drawing.Size(670, 469);
            this.chartGroundPath.TabIndex = 1;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(684, 501);
            this.tabControl1.TabIndex = 117;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.chartAltitude);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(676, 475);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Altitude";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.chartGroundPath);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(676, 475);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Groundpath";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // Test
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(700, 514);
            this.Controls.Add(this.tabControl1);
            this.Name = "Test";
            this.Text = "Test";
            this.Load += new System.EventHandler(this.Test_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chartAltitude)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartGroundPath)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chartAltitude;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartGroundPath;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
    }
}