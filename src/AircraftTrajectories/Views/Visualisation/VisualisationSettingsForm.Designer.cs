using System.Windows.Forms;

namespace AircraftTrajectories.Views.Visualisation
{
    partial class VisualisationSettingsForm : Form
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
            this.btnCalculateNoise = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.openFile = new System.Windows.Forms.OpenFileDialog();
            this.txtTrajectoryFile = new System.Windows.Forms.TextBox();
            this.btnBrowseTrajectory = new System.Windows.Forms.Button();
            this.radioSingle = new System.Windows.Forms.RadioButton();
            this.radioMultiple = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.radioINM = new System.Windows.Forms.RadioButton();
            this.radioExternal = new System.Windows.Forms.RadioButton();
            this.btnBrowseNoise = new System.Windows.Forms.Button();
            this.txtNoiseFile = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCalculateNoise
            // 
            this.btnCalculateNoise.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCalculateNoise.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.btnCalculateNoise.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCalculateNoise.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCalculateNoise.ForeColor = System.Drawing.Color.White;
            this.btnCalculateNoise.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCalculateNoise.Location = new System.Drawing.Point(12, 429);
            this.btnCalculateNoise.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCalculateNoise.Name = "btnCalculateNoise";
            this.btnCalculateNoise.Size = new System.Drawing.Size(248, 59);
            this.btnCalculateNoise.TabIndex = 139;
            this.btnCalculateNoise.Text = "Visualisation options";
            this.btnCalculateNoise.UseVisualStyleBackColor = false;
            this.btnCalculateNoise.Click += new System.EventHandler(this.btnPrepare_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI Light", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label10.Location = new System.Drawing.Point(12, 13);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(140, 41);
            this.label10.TabIndex = 120;
            this.label10.Text = "Trajectory";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label4.Location = new System.Drawing.Point(15, 207);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(123, 23);
            this.label4.TabIndex = 111;
            this.label4.Text = "Trajectory data";
            // 
            // txtTrajectoryFile
            // 
            this.txtTrajectoryFile.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtTrajectoryFile.Location = new System.Drawing.Point(19, 239);
            this.txtTrajectoryFile.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtTrajectoryFile.Name = "txtTrajectoryFile";
            this.txtTrajectoryFile.Size = new System.Drawing.Size(376, 30);
            this.txtTrajectoryFile.TabIndex = 140;
            // 
            // btnBrowseTrajectory
            // 
            this.btnBrowseTrajectory.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.btnBrowseTrajectory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseTrajectory.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnBrowseTrajectory.ForeColor = System.Drawing.Color.White;
            this.btnBrowseTrajectory.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBrowseTrajectory.Location = new System.Drawing.Point(19, 278);
            this.btnBrowseTrajectory.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnBrowseTrajectory.Name = "btnBrowseTrajectory";
            this.btnBrowseTrajectory.Size = new System.Drawing.Size(133, 42);
            this.btnBrowseTrajectory.TabIndex = 141;
            this.btnBrowseTrajectory.Text = "browse";
            this.btnBrowseTrajectory.UseVisualStyleBackColor = false;
            this.btnBrowseTrajectory.Click += new System.EventHandler(this.btnBrowseTrajectory_Click);
            // 
            // radioSingle
            // 
            this.radioSingle.AutoSize = true;
            this.radioSingle.Checked = true;
            this.radioSingle.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.radioSingle.Location = new System.Drawing.Point(13, 12);
            this.radioSingle.Name = "radioSingle";
            this.radioSingle.Size = new System.Drawing.Size(144, 25);
            this.radioSingle.TabIndex = 143;
            this.radioSingle.TabStop = true;
            this.radioSingle.Text = "Single trajectory";
            this.radioSingle.UseVisualStyleBackColor = true;
            // 
            // radioMultiple
            // 
            this.radioMultiple.AutoSize = true;
            this.radioMultiple.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.radioMultiple.Location = new System.Drawing.Point(13, 43);
            this.radioMultiple.Name = "radioMultiple";
            this.radioMultiple.Size = new System.Drawing.Size(169, 25);
            this.radioMultiple.TabIndex = 144;
            this.radioMultiple.Text = "Multiple trajectories";
            this.radioMultiple.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label1.Location = new System.Drawing.Point(15, 73);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(182, 23);
            this.label1.TabIndex = 145;
            this.label1.Text = "Number of trajectories";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Light", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label2.Location = new System.Drawing.Point(569, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(153, 41);
            this.label2.TabIndex = 146;
            this.label2.Text = "Noise data";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label3.Location = new System.Drawing.Point(572, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(147, 23);
            this.label3.TabIndex = 147;
            this.label3.Text = "Noise data source";
            // 
            // radioINM
            // 
            this.radioINM.AutoSize = true;
            this.radioINM.Checked = true;
            this.radioINM.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.radioINM.Location = new System.Drawing.Point(13, 11);
            this.radioINM.Name = "radioINM";
            this.radioINM.Size = new System.Drawing.Size(212, 25);
            this.radioINM.TabIndex = 148;
            this.radioINM.TabStop = true;
            this.radioINM.Text = "Compute noise using INM";
            this.radioINM.UseVisualStyleBackColor = true;
            this.radioINM.CheckedChanged += new System.EventHandler(this.radioINM_CheckedChanged);
            // 
            // radioExternal
            // 
            this.radioExternal.AutoSize = true;
            this.radioExternal.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.radioExternal.Location = new System.Drawing.Point(13, 42);
            this.radioExternal.Name = "radioExternal";
            this.radioExternal.Size = new System.Drawing.Size(279, 25);
            this.radioExternal.TabIndex = 149;
            this.radioExternal.Text = "Use noise data from external source";
            this.radioExternal.UseVisualStyleBackColor = true;
            this.radioExternal.CheckedChanged += new System.EventHandler(this.radioExternal_CheckedChanged);
            // 
            // btnBrowseNoise
            // 
            this.btnBrowseNoise.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.btnBrowseNoise.Enabled = false;
            this.btnBrowseNoise.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseNoise.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnBrowseNoise.ForeColor = System.Drawing.Color.White;
            this.btnBrowseNoise.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBrowseNoise.Location = new System.Drawing.Point(576, 278);
            this.btnBrowseNoise.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnBrowseNoise.Name = "btnBrowseNoise";
            this.btnBrowseNoise.Size = new System.Drawing.Size(133, 42);
            this.btnBrowseNoise.TabIndex = 152;
            this.btnBrowseNoise.Text = "browse";
            this.btnBrowseNoise.UseVisualStyleBackColor = false;
            this.btnBrowseNoise.Click += new System.EventHandler(this.btnBrowseNoise_Click);
            // 
            // txtNoiseFile
            // 
            this.txtNoiseFile.Enabled = false;
            this.txtNoiseFile.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtNoiseFile.Location = new System.Drawing.Point(576, 239);
            this.txtNoiseFile.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtNoiseFile.Name = "txtNoiseFile";
            this.txtNoiseFile.Size = new System.Drawing.Size(376, 30);
            this.txtNoiseFile.TabIndex = 151;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label5.Location = new System.Drawing.Point(572, 207);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(155, 23);
            this.label5.TabIndex = 150;
            this.label5.Text = "External noise data";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.radioSingle);
            this.panel1.Controls.Add(this.radioMultiple);
            this.panel1.Location = new System.Drawing.Point(6, 99);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(238, 78);
            this.panel1.TabIndex = 153;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.radioINM);
            this.panel2.Controls.Add(this.radioExternal);
            this.panel2.Location = new System.Drawing.Point(564, 99);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(406, 78);
            this.panel2.TabIndex = 154;
            // 
            // VisualisationSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1152, 499);
            this.ControlBox = false;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnBrowseNoise);
            this.Controls.Add(this.txtNoiseFile);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnBrowseTrajectory);
            this.Controls.Add(this.txtTrajectoryFile);
            this.Controls.Add(this.btnCalculateNoise);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label4);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "VisualisationSettingsForm";
            this.Load += new System.EventHandler(this.VisualisationSettingsForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Button btnCalculateNoise;
        private Label label10;
        private Label label4;
        private OpenFileDialog openFile;
        public TextBox txtTrajectoryFile;
        private Button btnBrowseTrajectory;
        public RadioButton radioSingle;
        public RadioButton radioMultiple;
        private Label label1;
        private Label label2;
        private Label label3;
        public RadioButton radioINM;
        public RadioButton radioExternal;
        private Button btnBrowseNoise;
        public TextBox txtNoiseFile;
        private Label label5;
        private Panel panel1;
        private Panel panel2;
    }
}