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
            this.label6 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.radioCoordinateWGS = new System.Windows.Forms.RadioButton();
            this.radioCoordinateRD = new System.Windows.Forms.RadioButton();
            this.radioCoordinateCustom = new System.Windows.Forms.RadioButton();
            this.label7 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtReferenceY = new System.Windows.Forms.TextBox();
            this.txtReferenceX = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.txtReferenceLongitude = new System.Windows.Forms.TextBox();
            this.txtReferenceLatitude = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.panelCoordinateReference = new System.Windows.Forms.Panel();
            this.label14 = new System.Windows.Forms.Label();
            this.panelExternalNoiseFile = new System.Windows.Forms.Panel();
            this.selectNoiseMetric = new System.Windows.Forms.ComboBox();
            this.label16 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panelCoordinateReference.SuspendLayout();
            this.panelExternalNoiseFile.SuspendLayout();
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
            this.btnCalculateNoise.Location = new System.Drawing.Point(11, 388);
            this.btnCalculateNoise.Margin = new System.Windows.Forms.Padding(2);
            this.btnCalculateNoise.Name = "btnCalculateNoise";
            this.btnCalculateNoise.Size = new System.Drawing.Size(186, 48);
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
            this.label10.Location = new System.Drawing.Point(9, 11);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(112, 32);
            this.label10.TabIndex = 120;
            this.label10.Text = "Trajectory";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label4.Location = new System.Drawing.Point(11, 144);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(99, 19);
            this.label4.TabIndex = 111;
            this.label4.Text = "Trajectory data";
            // 
            // txtTrajectoryFile
            // 
            this.txtTrajectoryFile.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtTrajectoryFile.Location = new System.Drawing.Point(14, 170);
            this.txtTrajectoryFile.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtTrajectoryFile.Name = "txtTrajectoryFile";
            this.txtTrajectoryFile.Size = new System.Drawing.Size(240, 25);
            this.txtTrajectoryFile.TabIndex = 140;
            // 
            // btnBrowseTrajectory
            // 
            this.btnBrowseTrajectory.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.btnBrowseTrajectory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseTrajectory.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnBrowseTrajectory.ForeColor = System.Drawing.Color.White;
            this.btnBrowseTrajectory.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBrowseTrajectory.Location = new System.Drawing.Point(260, 164);
            this.btnBrowseTrajectory.Margin = new System.Windows.Forms.Padding(2);
            this.btnBrowseTrajectory.Name = "btnBrowseTrajectory";
            this.btnBrowseTrajectory.Size = new System.Drawing.Size(100, 34);
            this.btnBrowseTrajectory.TabIndex = 141;
            this.btnBrowseTrajectory.Text = "browse";
            this.btnBrowseTrajectory.UseVisualStyleBackColor = false;
            this.btnBrowseTrajectory.Click += new System.EventHandler(this.btnBrowseTrajectory_Click);
            // 
            // radioSingle
            // 
            this.radioSingle.AutoSize = true;
            this.radioSingle.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.radioSingle.Location = new System.Drawing.Point(10, 5);
            this.radioSingle.Margin = new System.Windows.Forms.Padding(2);
            this.radioSingle.Name = "radioSingle";
            this.radioSingle.Size = new System.Drawing.Size(120, 21);
            this.radioSingle.TabIndex = 143;
            this.radioSingle.Text = "Single trajectory";
            this.radioSingle.UseVisualStyleBackColor = true;
            this.radioSingle.CheckedChanged += new System.EventHandler(this.radioSingle_CheckedChanged);
            // 
            // radioMultiple
            // 
            this.radioMultiple.AutoSize = true;
            this.radioMultiple.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.radioMultiple.Location = new System.Drawing.Point(10, 30);
            this.radioMultiple.Margin = new System.Windows.Forms.Padding(2);
            this.radioMultiple.Name = "radioMultiple";
            this.radioMultiple.Size = new System.Drawing.Size(142, 21);
            this.radioMultiple.TabIndex = 144;
            this.radioMultiple.Text = "Multiple trajectories";
            this.radioMultiple.UseVisualStyleBackColor = true;
            this.radioMultiple.CheckedChanged += new System.EventHandler(this.radioMultiple_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label1.Location = new System.Drawing.Point(11, 59);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(146, 19);
            this.label1.TabIndex = 145;
            this.label1.Text = "Number of trajectories";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Light", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label2.Location = new System.Drawing.Point(402, 11);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(122, 32);
            this.label2.TabIndex = 146;
            this.label2.Text = "Noise data";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label3.Location = new System.Drawing.Point(404, 59);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(118, 19);
            this.label3.TabIndex = 147;
            this.label3.Text = "Noise data source";
            // 
            // radioINM
            // 
            this.radioINM.AutoSize = true;
            this.radioINM.Checked = true;
            this.radioINM.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.radioINM.Location = new System.Drawing.Point(10, 4);
            this.radioINM.Margin = new System.Windows.Forms.Padding(2);
            this.radioINM.Name = "radioINM";
            this.radioINM.Size = new System.Drawing.Size(178, 21);
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
            this.radioExternal.Location = new System.Drawing.Point(10, 29);
            this.radioExternal.Margin = new System.Windows.Forms.Padding(2);
            this.radioExternal.Name = "radioExternal";
            this.radioExternal.Size = new System.Drawing.Size(238, 21);
            this.radioExternal.TabIndex = 149;
            this.radioExternal.Text = "Use noise data from external source";
            this.radioExternal.UseVisualStyleBackColor = true;
            this.radioExternal.CheckedChanged += new System.EventHandler(this.radioExternal_CheckedChanged);
            // 
            // btnBrowseNoise
            // 
            this.btnBrowseNoise.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.btnBrowseNoise.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseNoise.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnBrowseNoise.ForeColor = System.Drawing.Color.White;
            this.btnBrowseNoise.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBrowseNoise.Location = new System.Drawing.Point(264, 28);
            this.btnBrowseNoise.Margin = new System.Windows.Forms.Padding(2);
            this.btnBrowseNoise.Name = "btnBrowseNoise";
            this.btnBrowseNoise.Size = new System.Drawing.Size(100, 34);
            this.btnBrowseNoise.TabIndex = 152;
            this.btnBrowseNoise.Text = "browse";
            this.btnBrowseNoise.UseVisualStyleBackColor = false;
            this.btnBrowseNoise.Click += new System.EventHandler(this.btnBrowseNoise_Click);
            // 
            // txtNoiseFile
            // 
            this.txtNoiseFile.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtNoiseFile.Location = new System.Drawing.Point(19, 33);
            this.txtNoiseFile.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtNoiseFile.Name = "txtNoiseFile";
            this.txtNoiseFile.Size = new System.Drawing.Size(240, 25);
            this.txtNoiseFile.TabIndex = 151;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label5.Location = new System.Drawing.Point(16, 7);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(124, 19);
            this.label5.TabIndex = 150;
            this.label5.Text = "External noise data";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.radioSingle);
            this.panel1.Controls.Add(this.radioMultiple);
            this.panel1.Location = new System.Drawing.Point(4, 80);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(178, 63);
            this.panel1.TabIndex = 153;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.radioINM);
            this.panel2.Controls.Add(this.radioExternal);
            this.panel2.Location = new System.Drawing.Point(398, 80);
            this.panel2.Margin = new System.Windows.Forms.Padding(2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(250, 63);
            this.panel2.TabIndex = 154;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI Light", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label6.Location = new System.Drawing.Point(9, 222);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(180, 32);
            this.label6.TabIndex = 155;
            this.label6.Text = "Coordinate units";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.radioCoordinateWGS);
            this.panel3.Controls.Add(this.radioCoordinateRD);
            this.panel3.Controls.Add(this.radioCoordinateCustom);
            this.panel3.Location = new System.Drawing.Point(4, 257);
            this.panel3.Margin = new System.Windows.Forms.Padding(2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(178, 93);
            this.panel3.TabIndex = 154;
            // 
            // radioCoordinateWGS
            // 
            this.radioCoordinateWGS.AutoSize = true;
            this.radioCoordinateWGS.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.radioCoordinateWGS.Location = new System.Drawing.Point(10, 58);
            this.radioCoordinateWGS.Margin = new System.Windows.Forms.Padding(2);
            this.radioCoordinateWGS.Name = "radioCoordinateWGS";
            this.radioCoordinateWGS.Size = new System.Drawing.Size(68, 21);
            this.radioCoordinateWGS.TabIndex = 145;
            this.radioCoordinateWGS.Text = "WGS84";
            this.radioCoordinateWGS.UseVisualStyleBackColor = true;
            // 
            // radioCoordinateRD
            // 
            this.radioCoordinateRD.AutoSize = true;
            this.radioCoordinateRD.Checked = true;
            this.radioCoordinateRD.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.radioCoordinateRD.Location = new System.Drawing.Point(10, 6);
            this.radioCoordinateRD.Margin = new System.Windows.Forms.Padding(2);
            this.radioCoordinateRD.Name = "radioCoordinateRD";
            this.radioCoordinateRD.Size = new System.Drawing.Size(176, 21);
            this.radioCoordinateRD.TabIndex = 143;
            this.radioCoordinateRD.TabStop = true;
            this.radioCoordinateRD.Text = "Rijksdriehoek coordinates";
            this.radioCoordinateRD.UseVisualStyleBackColor = true;
            // 
            // radioCoordinateCustom
            // 
            this.radioCoordinateCustom.AutoSize = true;
            this.radioCoordinateCustom.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.radioCoordinateCustom.Location = new System.Drawing.Point(10, 32);
            this.radioCoordinateCustom.Margin = new System.Windows.Forms.Padding(2);
            this.radioCoordinateCustom.Name = "radioCoordinateCustom";
            this.radioCoordinateCustom.Size = new System.Drawing.Size(129, 21);
            this.radioCoordinateCustom.TabIndex = 144;
            this.radioCoordinateCustom.Text = "Custom reference";
            this.radioCoordinateCustom.UseVisualStyleBackColor = true;
            this.radioCoordinateCustom.CheckedChanged += new System.EventHandler(this.radioCoordinateCustom_CheckedChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI Light", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label7.Location = new System.Drawing.Point(14, 20);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(233, 32);
            this.label7.TabIndex = 156;
            this.label7.Text = "Coordinate Reference";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label17.Location = new System.Drawing.Point(398, 100);
            this.label17.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(21, 19);
            this.label17.TabIndex = 168;
            this.label17.Text = "m";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label13.Location = new System.Drawing.Point(168, 62);
            this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(28, 19);
            this.label13.TabIndex = 167;
            this.label13.Text = "° N";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label9.Location = new System.Drawing.Point(398, 63);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(25, 19);
            this.label9.TabIndex = 166;
            this.label9.Text = "° E";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label8.Location = new System.Drawing.Point(168, 98);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(21, 19);
            this.label8.TabIndex = 165;
            this.label8.Text = "m";
            // 
            // txtReferenceY
            // 
            this.txtReferenceY.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtReferenceY.Location = new System.Drawing.Point(316, 98);
            this.txtReferenceY.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtReferenceY.Name = "txtReferenceY";
            this.txtReferenceY.Size = new System.Drawing.Size(78, 25);
            this.txtReferenceY.TabIndex = 164;
            this.txtReferenceY.Text = "0";
            // 
            // txtReferenceX
            // 
            this.txtReferenceX.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtReferenceX.Location = new System.Drawing.Point(87, 96);
            this.txtReferenceX.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtReferenceX.Name = "txtReferenceX";
            this.txtReferenceX.Size = new System.Drawing.Size(78, 25);
            this.txtReferenceX.TabIndex = 162;
            this.txtReferenceX.Text = "0";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label11.Location = new System.Drawing.Point(237, 100);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(17, 19);
            this.label11.TabIndex = 163;
            this.label11.Text = "Y";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label12.Location = new System.Drawing.Point(16, 101);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(17, 19);
            this.label12.TabIndex = 161;
            this.label12.Text = "X";
            // 
            // txtReferenceLongitude
            // 
            this.txtReferenceLongitude.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtReferenceLongitude.Location = new System.Drawing.Point(316, 62);
            this.txtReferenceLongitude.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtReferenceLongitude.Name = "txtReferenceLongitude";
            this.txtReferenceLongitude.Size = new System.Drawing.Size(78, 25);
            this.txtReferenceLongitude.TabIndex = 160;
            this.txtReferenceLongitude.Text = "4.7602168";
            // 
            // txtReferenceLatitude
            // 
            this.txtReferenceLatitude.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtReferenceLatitude.Location = new System.Drawing.Point(87, 59);
            this.txtReferenceLatitude.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtReferenceLatitude.Name = "txtReferenceLatitude";
            this.txtReferenceLatitude.Size = new System.Drawing.Size(78, 25);
            this.txtReferenceLatitude.TabIndex = 159;
            this.txtReferenceLatitude.Text = "52.3075183";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label15.Location = new System.Drawing.Point(16, 64);
            this.label15.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(59, 19);
            this.label15.TabIndex = 157;
            this.label15.Text = "Latitude";
            // 
            // panelCoordinateReference
            // 
            this.panelCoordinateReference.Controls.Add(this.label7);
            this.panelCoordinateReference.Controls.Add(this.label17);
            this.panelCoordinateReference.Controls.Add(this.label15);
            this.panelCoordinateReference.Controls.Add(this.label13);
            this.panelCoordinateReference.Controls.Add(this.label14);
            this.panelCoordinateReference.Controls.Add(this.label9);
            this.panelCoordinateReference.Controls.Add(this.txtReferenceLatitude);
            this.panelCoordinateReference.Controls.Add(this.label8);
            this.panelCoordinateReference.Controls.Add(this.txtReferenceLongitude);
            this.panelCoordinateReference.Controls.Add(this.txtReferenceY);
            this.panelCoordinateReference.Controls.Add(this.label12);
            this.panelCoordinateReference.Controls.Add(this.txtReferenceX);
            this.panelCoordinateReference.Controls.Add(this.label11);
            this.panelCoordinateReference.Location = new System.Drawing.Point(388, 202);
            this.panelCoordinateReference.Margin = new System.Windows.Forms.Padding(2);
            this.panelCoordinateReference.Name = "panelCoordinateReference";
            this.panelCoordinateReference.Size = new System.Drawing.Size(442, 160);
            this.panelCoordinateReference.TabIndex = 169;
            this.panelCoordinateReference.Visible = false;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label14.Location = new System.Drawing.Point(237, 64);
            this.label14.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(71, 19);
            this.label14.TabIndex = 158;
            this.label14.Text = "Longitude";
            // 
            // panelExternalNoiseFile
            // 
            this.panelExternalNoiseFile.Controls.Add(this.label5);
            this.panelExternalNoiseFile.Controls.Add(this.txtNoiseFile);
            this.panelExternalNoiseFile.Controls.Add(this.btnBrowseNoise);
            this.panelExternalNoiseFile.Location = new System.Drawing.Point(388, 136);
            this.panelExternalNoiseFile.Margin = new System.Windows.Forms.Padding(2);
            this.panelExternalNoiseFile.Name = "panelExternalNoiseFile";
            this.panelExternalNoiseFile.Size = new System.Drawing.Size(370, 74);
            this.panelExternalNoiseFile.TabIndex = 170;
            this.panelExternalNoiseFile.Visible = false;
            // 
            // selectNoiseMetric
            // 
            this.selectNoiseMetric.AutoCompleteCustomSource.AddRange(new string[] {
            "Follow aircraft"});
            this.selectNoiseMetric.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.selectNoiseMetric.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.selectNoiseMetric.Location = new System.Drawing.Point(700, 84);
            this.selectNoiseMetric.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.selectNoiseMetric.Name = "selectNoiseMetric";
            this.selectNoiseMetric.Size = new System.Drawing.Size(130, 25);
            this.selectNoiseMetric.TabIndex = 172;
            this.selectNoiseMetric.SelectedIndexChanged += new System.EventHandler(this.selectNoiseMetric_SelectedIndexChanged);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label16.Location = new System.Drawing.Point(697, 59);
            this.label16.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(91, 19);
            this.label16.TabIndex = 171;
            this.label16.Text = "Noise metrics";
            // 
            // VisualisationSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(864, 446);
            this.ControlBox = false;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.selectNoiseMetric);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.panelExternalNoiseFile);
            this.Controls.Add(this.panelCoordinateReference);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnBrowseTrajectory);
            this.Controls.Add(this.txtTrajectoryFile);
            this.Controls.Add(this.btnCalculateNoise);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label4);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "VisualisationSettingsForm";
            this.Load += new System.EventHandler(this.VisualisationSettingsForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panelCoordinateReference.ResumeLayout(false);
            this.panelCoordinateReference.PerformLayout();
            this.panelExternalNoiseFile.ResumeLayout(false);
            this.panelExternalNoiseFile.PerformLayout();
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
        private Label label6;
        private Panel panel3;
        public RadioButton radioCoordinateRD;
        public RadioButton radioCoordinateCustom;
        public RadioButton radioCoordinateWGS;
        private Label label7;
        private Label label17;
        private Label label13;
        private Label label9;
        private Label label8;
        public TextBox txtReferenceY;
        public TextBox txtReferenceX;
        private Label label11;
        private Label label12;
        public TextBox txtReferenceLongitude;
        public TextBox txtReferenceLatitude;
        private Label label15;
        private Panel panelCoordinateReference;
        private Panel panelExternalNoiseFile;
        private Label label14;
        public ComboBox selectNoiseMetric;
        private Label label16;
    }
}