namespace AircraftTrajectories.Views.Visualisation
{
    partial class VisualisationAnimatorForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VisualisationAnimatorForm));
            this.btnPrepare = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtNumberOfContours = new System.Windows.Forms.TextBox();
            this.txtContourStartValue = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.selectCameraType = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.selectValueConversion = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnBrowseTrajectory = new System.Windows.Forms.Button();
            this.txtCustomMapFile = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtBottomLeftLatitude = new System.Windows.Forms.TextBox();
            this.txtBottomLeftLongitude = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.txtUpperRightLatitude = new System.Windows.Forms.TextBox();
            this.txtUpperRightLongitude = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnPrepare
            // 
            this.btnPrepare.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPrepare.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.btnPrepare.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrepare.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrepare.ForeColor = System.Drawing.Color.White;
            this.btnPrepare.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPrepare.Location = new System.Drawing.Point(15, 426);
            this.btnPrepare.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnPrepare.Name = "btnPrepare";
            this.btnPrepare.Size = new System.Drawing.Size(248, 59);
            this.btnPrepare.TabIndex = 140;
            this.btnPrepare.Text = "Prepare visualisation";
            this.btnPrepare.UseVisualStyleBackColor = false;
            this.btnPrepare.Click += new System.EventHandler(this.btnPrepare_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI Light", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label10.Location = new System.Drawing.Point(13, 14);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(134, 41);
            this.label10.TabIndex = 146;
            this.label10.Text = "Contours";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Light", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label2.Location = new System.Drawing.Point(13, 213);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(117, 41);
            this.label2.TabIndex = 149;
            this.label2.Text = "Camera";
            // 
            // txtNumberOfContours
            // 
            this.txtNumberOfContours.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtNumberOfContours.Location = new System.Drawing.Point(248, 68);
            this.txtNumberOfContours.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtNumberOfContours.Name = "txtNumberOfContours";
            this.txtNumberOfContours.Size = new System.Drawing.Size(159, 30);
            this.txtNumberOfContours.TabIndex = 152;
            this.txtNumberOfContours.Text = "20";
            // 
            // txtContourStartValue
            // 
            this.txtContourStartValue.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtContourStartValue.Location = new System.Drawing.Point(248, 112);
            this.txtContourStartValue.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtContourStartValue.Name = "txtContourStartValue";
            this.txtContourStartValue.Size = new System.Drawing.Size(159, 30);
            this.txtContourStartValue.TabIndex = 153;
            this.txtContourStartValue.Text = "55";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label1.Location = new System.Drawing.Point(16, 112);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(172, 23);
            this.label1.TabIndex = 151;
            this.label1.Text = "Lowest value contour";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label3.Location = new System.Drawing.Point(16, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(165, 23);
            this.label3.TabIndex = 150;
            this.label3.Text = "Number of contours";
            // 
            // selectCameraType
            // 
            this.selectCameraType.AutoCompleteCustomSource.AddRange(new string[] {
            "Follow aircraft"});
            this.selectCameraType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.selectCameraType.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.selectCameraType.Items.AddRange(new object[] {
            "Topview",
            "Follow aircraft"});
            this.selectCameraType.Location = new System.Drawing.Point(248, 264);
            this.selectCameraType.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.selectCameraType.Name = "selectCameraType";
            this.selectCameraType.Size = new System.Drawing.Size(159, 31);
            this.selectCameraType.TabIndex = 155;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label4.Location = new System.Drawing.Point(16, 267);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(107, 23);
            this.label4.TabIndex = 154;
            this.label4.Text = "Camera type";
            // 
            // selectValueConversion
            // 
            this.selectValueConversion.AutoCompleteCustomSource.AddRange(new string[] {
            "Follow aircraft"});
            this.selectValueConversion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.selectValueConversion.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.selectValueConversion.Items.AddRange(new object[] {
            "None",
            "Max"});
            this.selectValueConversion.Location = new System.Drawing.Point(248, 157);
            this.selectValueConversion.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.selectValueConversion.Name = "selectValueConversion";
            this.selectValueConversion.Size = new System.Drawing.Size(159, 31);
            this.selectValueConversion.TabIndex = 157;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label5.Location = new System.Drawing.Point(16, 160);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(139, 23);
            this.label5.TabIndex = 156;
            this.label5.Text = "Value conversion";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI Light", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label6.Location = new System.Drawing.Point(620, 14);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(181, 41);
            this.label6.TabIndex = 158;
            this.label6.Text = "Custom Map";
            // 
            // btnBrowseTrajectory
            // 
            this.btnBrowseTrajectory.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.btnBrowseTrajectory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseTrajectory.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnBrowseTrajectory.ForeColor = System.Drawing.Color.White;
            this.btnBrowseTrajectory.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBrowseTrajectory.Location = new System.Drawing.Point(954, 100);
            this.btnBrowseTrajectory.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnBrowseTrajectory.Name = "btnBrowseTrajectory";
            this.btnBrowseTrajectory.Size = new System.Drawing.Size(133, 42);
            this.btnBrowseTrajectory.TabIndex = 161;
            this.btnBrowseTrajectory.Text = "browse";
            this.btnBrowseTrajectory.UseVisualStyleBackColor = false;
            // 
            // txtCustomMapFile
            // 
            this.txtCustomMapFile.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtCustomMapFile.Location = new System.Drawing.Point(627, 107);
            this.txtCustomMapFile.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtCustomMapFile.Name = "txtCustomMapFile";
            this.txtCustomMapFile.Size = new System.Drawing.Size(318, 30);
            this.txtCustomMapFile.TabIndex = 160;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label7.Location = new System.Drawing.Point(623, 75);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(85, 23);
            this.label7.TabIndex = 159;
            this.label7.Text = "Image file";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label15.Location = new System.Drawing.Point(623, 198);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(72, 23);
            this.label15.TabIndex = 169;
            this.label15.Text = "Latitude";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label13.Location = new System.Drawing.Point(951, 199);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(34, 23);
            this.label13.TabIndex = 179;
            this.label13.Text = "° N";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label14.Location = new System.Drawing.Point(623, 243);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(87, 23);
            this.label14.TabIndex = 170;
            this.label14.Text = "Longitude";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label9.Location = new System.Drawing.Point(951, 243);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(30, 23);
            this.label9.TabIndex = 178;
            this.label9.Text = "° E";
            // 
            // txtBottomLeftLatitude
            // 
            this.txtBottomLeftLatitude.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtBottomLeftLatitude.Location = new System.Drawing.Point(786, 196);
            this.txtBottomLeftLatitude.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtBottomLeftLatitude.Name = "txtBottomLeftLatitude";
            this.txtBottomLeftLatitude.Size = new System.Drawing.Size(159, 30);
            this.txtBottomLeftLatitude.TabIndex = 171;
            this.txtBottomLeftLatitude.Text = "52.1664473";
            // 
            // txtBottomLeftLongitude
            // 
            this.txtBottomLeftLongitude.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtBottomLeftLongitude.Location = new System.Drawing.Point(786, 241);
            this.txtBottomLeftLongitude.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtBottomLeftLongitude.Name = "txtBottomLeftLongitude";
            this.txtBottomLeftLongitude.Size = new System.Drawing.Size(159, 30);
            this.txtBottomLeftLongitude.TabIndex = 172;
            this.txtBottomLeftLongitude.Text = "4.4924063";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label16.Location = new System.Drawing.Point(625, 158);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(183, 23);
            this.label16.TabIndex = 181;
            this.label16.Text = "Bottom left coordinate";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label8.Location = new System.Drawing.Point(625, 299);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(185, 23);
            this.label8.TabIndex = 188;
            this.label8.Text = "Upper right coordinate";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label11.Location = new System.Drawing.Point(623, 339);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(72, 23);
            this.label11.TabIndex = 182;
            this.label11.Text = "Latitude";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label12.Location = new System.Drawing.Point(951, 340);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(34, 23);
            this.label12.TabIndex = 187;
            this.label12.Text = "° N";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label17.Location = new System.Drawing.Point(623, 384);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(87, 23);
            this.label17.TabIndex = 183;
            this.label17.Text = "Longitude";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label18.Location = new System.Drawing.Point(951, 384);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(30, 23);
            this.label18.TabIndex = 186;
            this.label18.Text = "° E";
            // 
            // txtUpperRightLatitude
            // 
            this.txtUpperRightLatitude.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtUpperRightLatitude.Location = new System.Drawing.Point(786, 337);
            this.txtUpperRightLatitude.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtUpperRightLatitude.Name = "txtUpperRightLatitude";
            this.txtUpperRightLatitude.Size = new System.Drawing.Size(159, 30);
            this.txtUpperRightLatitude.TabIndex = 184;
            this.txtUpperRightLatitude.Text = "52.4803652";
            // 
            // txtUpperRightLongitude
            // 
            this.txtUpperRightLongitude.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtUpperRightLongitude.Location = new System.Drawing.Point(786, 382);
            this.txtUpperRightLongitude.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtUpperRightLongitude.Name = "txtUpperRightLongitude";
            this.txtUpperRightLongitude.Size = new System.Drawing.Size(159, 30);
            this.txtUpperRightLongitude.TabIndex = 185;
            this.txtUpperRightLongitude.Text = "5.0171003";
            // 
            // VisualisationAnimatorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1152, 498);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.txtUpperRightLatitude);
            this.Controls.Add(this.txtUpperRightLongitude);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtBottomLeftLatitude);
            this.Controls.Add(this.txtBottomLeftLongitude);
            this.Controls.Add(this.btnBrowseTrajectory);
            this.Controls.Add(this.txtCustomMapFile);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.selectValueConversion);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.selectCameraType);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtNumberOfContours);
            this.Controls.Add(this.txtContourStartValue);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.btnPrepare);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "VisualisationAnimatorForm";
            this.Load += new System.EventHandler(this.VisualisationAnimatorForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnPrepare;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox txtContourStartValue;
        public System.Windows.Forms.TextBox txtNumberOfContours;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.ComboBox selectCameraType;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.ComboBox selectValueConversion;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnBrowseTrajectory;
        public System.Windows.Forms.TextBox txtCustomMapFile;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label9;
        public System.Windows.Forms.TextBox txtBottomLeftLatitude;
        public System.Windows.Forms.TextBox txtBottomLeftLongitude;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        public System.Windows.Forms.TextBox txtUpperRightLatitude;
        public System.Windows.Forms.TextBox txtUpperRightLongitude;
    }
}