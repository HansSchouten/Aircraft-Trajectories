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
            this.btnPrepare.Location = new System.Drawing.Point(11, 346);
            this.btnPrepare.Margin = new System.Windows.Forms.Padding(2);
            this.btnPrepare.Name = "btnPrepare";
            this.btnPrepare.Size = new System.Drawing.Size(186, 48);
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
            this.label10.Location = new System.Drawing.Point(10, 11);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(106, 32);
            this.label10.TabIndex = 146;
            this.label10.Text = "Contours";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Light", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label2.Location = new System.Drawing.Point(464, 11);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 32);
            this.label2.TabIndex = 149;
            this.label2.Text = "Camera";
            // 
            // txtNumberOfContours
            // 
            this.txtNumberOfContours.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtNumberOfContours.Location = new System.Drawing.Point(186, 55);
            this.txtNumberOfContours.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtNumberOfContours.Name = "txtNumberOfContours";
            this.txtNumberOfContours.Size = new System.Drawing.Size(120, 25);
            this.txtNumberOfContours.TabIndex = 152;
            this.txtNumberOfContours.Text = "20";
            // 
            // txtContourStartValue
            // 
            this.txtContourStartValue.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtContourStartValue.Location = new System.Drawing.Point(186, 91);
            this.txtContourStartValue.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtContourStartValue.Name = "txtContourStartValue";
            this.txtContourStartValue.Size = new System.Drawing.Size(120, 25);
            this.txtContourStartValue.TabIndex = 153;
            this.txtContourStartValue.Text = "55";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label1.Location = new System.Drawing.Point(12, 91);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(140, 19);
            this.label1.TabIndex = 151;
            this.label1.Text = "Lowest value contour";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label3.Location = new System.Drawing.Point(12, 55);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(133, 19);
            this.label3.TabIndex = 150;
            this.label3.Text = "Number of contours";
            // 
            // selectCameraType
            // 
            this.selectCameraType.AutoCompleteCustomSource.AddRange(new string[] {
            "Follow aircraft"});
            this.selectCameraType.Enabled = false;
            this.selectCameraType.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.selectCameraType.Location = new System.Drawing.Point(600, 55);
            this.selectCameraType.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.selectCameraType.Name = "selectCameraType";
            this.selectCameraType.Size = new System.Drawing.Size(120, 25);
            this.selectCameraType.TabIndex = 155;
            this.selectCameraType.Text = "Follow aircraft";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label4.Location = new System.Drawing.Point(463, 55);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(87, 19);
            this.label4.TabIndex = 154;
            this.label4.Text = "Camera type";
            // 
            // VisualisationAnimatorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(864, 405);
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
    }
}