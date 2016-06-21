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
            this.label25 = new System.Windows.Forms.Label();
            this.txtNumberOfGenerations = new System.Windows.Forms.TextBox();
            this.txtPopulationSize = new System.Windows.Forms.TextBox();
            this.label28 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.btnOptimise = new System.Windows.Forms.Button();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.txtNumberOfSegments = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtEndLongitude = new System.Windows.Forms.TextBox();
            this.txtEndLatitude = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.txtTakeoffSpeed = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.txtTakeoffHeading = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtTotalAircraftMass = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtStartLongitude = new System.Windows.Forms.TextBox();
            this.txtTakeoffLatitude = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("Segoe UI Light", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label25.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label25.Location = new System.Drawing.Point(397, 10);
            this.label25.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(317, 32);
            this.label25.TabIndex = 144;
            this.label25.Text = "Genetic Algorithm Parameters";
            // 
            // txtNumberOfGenerations
            // 
            this.txtNumberOfGenerations.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtNumberOfGenerations.Location = new System.Drawing.Point(565, 97);
            this.txtNumberOfGenerations.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtNumberOfGenerations.Name = "txtNumberOfGenerations";
            this.txtNumberOfGenerations.Size = new System.Drawing.Size(120, 25);
            this.txtNumberOfGenerations.TabIndex = 143;
            this.txtNumberOfGenerations.Text = "100";
            // 
            // txtPopulationSize
            // 
            this.txtPopulationSize.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtPopulationSize.Location = new System.Drawing.Point(565, 60);
            this.txtPopulationSize.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtPopulationSize.Name = "txtPopulationSize";
            this.txtPopulationSize.Size = new System.Drawing.Size(120, 25);
            this.txtPopulationSize.TabIndex = 142;
            this.txtPopulationSize.Text = "70";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label28.Location = new System.Drawing.Point(399, 98);
            this.label28.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(151, 19);
            this.label28.TabIndex = 141;
            this.label28.Text = "Number of generations";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label29.Location = new System.Drawing.Point(399, 61);
            this.label29.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(101, 19);
            this.label29.TabIndex = 140;
            this.label29.Text = "Population size";
            // 
            // btnOptimise
            // 
            this.btnOptimise.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOptimise.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.btnOptimise.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOptimise.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOptimise.ForeColor = System.Drawing.Color.White;
            this.btnOptimise.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOptimise.Location = new System.Drawing.Point(15, 355);
            this.btnOptimise.Margin = new System.Windows.Forms.Padding(2);
            this.btnOptimise.Name = "btnOptimise";
            this.btnOptimise.Size = new System.Drawing.Size(186, 48);
            this.btnOptimise.TabIndex = 139;
            this.btnOptimise.Text = "Run optimisation";
            this.btnOptimise.UseVisualStyleBackColor = false;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label19.Location = new System.Drawing.Point(687, 268);
            this.label19.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(28, 19);
            this.label19.TabIndex = 138;
            this.label19.Text = "° N";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label20.Location = new System.Drawing.Point(687, 302);
            this.label20.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(25, 19);
            this.label20.TabIndex = 137;
            this.label20.Text = "° E";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label18.Location = new System.Drawing.Point(278, 97);
            this.label18.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(24, 19);
            this.label18.TabIndex = 136;
            this.label18.Text = "kg";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label17.Location = new System.Drawing.Point(278, 309);
            this.label17.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(27, 19);
            this.label17.TabIndex = 135;
            this.label17.Text = "kts";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label13.Location = new System.Drawing.Point(278, 200);
            this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(28, 19);
            this.label13.TabIndex = 134;
            this.label13.Text = "° N";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label9.Location = new System.Drawing.Point(278, 236);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(25, 19);
            this.label9.TabIndex = 133;
            this.label9.Text = "° E";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label8.Location = new System.Drawing.Point(278, 272);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(14, 19);
            this.label8.TabIndex = 132;
            this.label8.Text = "°";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label6.Location = new System.Drawing.Point(398, 238);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(452, 19);
            this.label6.TabIndex = 131;
            this.label6.Text = "Specify the location that marks the end of the trajectory being optimised";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Segoe UI Light", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label12.Location = new System.Drawing.Point(397, 149);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(114, 32);
            this.label12.TabIndex = 130;
            this.label12.Text = "Trajectory";
            // 
            // txtNumberOfSegments
            // 
            this.txtNumberOfSegments.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtNumberOfSegments.Location = new System.Drawing.Point(563, 198);
            this.txtNumberOfSegments.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtNumberOfSegments.Name = "txtNumberOfSegments";
            this.txtNumberOfSegments.Size = new System.Drawing.Size(120, 25);
            this.txtNumberOfSegments.TabIndex = 129;
            this.txtNumberOfSegments.Text = "3";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label14.Location = new System.Drawing.Point(398, 199);
            this.label14.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(138, 19);
            this.label14.TabIndex = 128;
            this.label14.Text = "Number of segments";
            // 
            // txtEndLongitude
            // 
            this.txtEndLongitude.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtEndLongitude.Location = new System.Drawing.Point(563, 300);
            this.txtEndLongitude.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtEndLongitude.Name = "txtEndLongitude";
            this.txtEndLongitude.Size = new System.Drawing.Size(120, 25);
            this.txtEndLongitude.TabIndex = 127;
            this.txtEndLongitude.Text = "4.15";
            // 
            // txtEndLatitude
            // 
            this.txtEndLatitude.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtEndLatitude.Location = new System.Drawing.Point(563, 266);
            this.txtEndLatitude.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtEndLatitude.Name = "txtEndLatitude";
            this.txtEndLatitude.Size = new System.Drawing.Size(120, 25);
            this.txtEndLatitude.TabIndex = 126;
            this.txtEndLatitude.Text = "52.35";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label15.Location = new System.Drawing.Point(398, 300);
            this.label15.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(71, 19);
            this.label15.TabIndex = 125;
            this.label15.Text = "Longitude";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label16.Location = new System.Drawing.Point(398, 267);
            this.label16.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(59, 19);
            this.label16.TabIndex = 124;
            this.label16.Text = "Latitude";
            // 
            // txtTakeoffSpeed
            // 
            this.txtTakeoffSpeed.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtTakeoffSpeed.Location = new System.Drawing.Point(154, 307);
            this.txtTakeoffSpeed.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtTakeoffSpeed.Name = "txtTakeoffSpeed";
            this.txtTakeoffSpeed.Size = new System.Drawing.Size(120, 25);
            this.txtTakeoffSpeed.TabIndex = 123;
            this.txtTakeoffSpeed.Text = "160";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Segoe UI Light", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label11.Location = new System.Drawing.Point(14, 149);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(89, 32);
            this.label11.TabIndex = 122;
            this.label11.Text = "Takeoff";
            // 
            // comboBox1
            // 
            this.comboBox1.AutoCompleteCustomSource.AddRange(new string[] {
            "Boeing 747-400"});
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.comboBox1.Location = new System.Drawing.Point(154, 56);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(120, 25);
            this.comboBox1.TabIndex = 112;
            // 
            // txtTakeoffHeading
            // 
            this.txtTakeoffHeading.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtTakeoffHeading.Location = new System.Drawing.Point(154, 270);
            this.txtTakeoffHeading.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtTakeoffHeading.Name = "txtTakeoffHeading";
            this.txtTakeoffHeading.Size = new System.Drawing.Size(120, 25);
            this.txtTakeoffHeading.TabIndex = 117;
            this.txtTakeoffHeading.Text = "90";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label5.Location = new System.Drawing.Point(17, 307);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(93, 19);
            this.label5.TabIndex = 121;
            this.label5.Text = "Takeoff speed";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI Light", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label10.Location = new System.Drawing.Point(15, 10);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(88, 32);
            this.label10.TabIndex = 120;
            this.label10.Text = "Aircraft";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label4.Location = new System.Drawing.Point(17, 56);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(95, 19);
            this.label4.TabIndex = 111;
            this.label4.Text = "Aircraft model";
            // 
            // txtTotalAircraftMass
            // 
            this.txtTotalAircraftMass.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtTotalAircraftMass.Location = new System.Drawing.Point(154, 94);
            this.txtTotalAircraftMass.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtTotalAircraftMass.Name = "txtTotalAircraftMass";
            this.txtTotalAircraftMass.Size = new System.Drawing.Size(120, 25);
            this.txtTotalAircraftMass.TabIndex = 119;
            this.txtTotalAircraftMass.Text = "200000";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label3.Location = new System.Drawing.Point(17, 270);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 19);
            this.label3.TabIndex = 116;
            this.label3.Text = "Heading";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label7.Location = new System.Drawing.Point(17, 94);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(120, 19);
            this.label7.TabIndex = 118;
            this.label7.Text = "Total aircraft mass";
            // 
            // txtStartLongitude
            // 
            this.txtStartLongitude.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtStartLongitude.Location = new System.Drawing.Point(154, 234);
            this.txtStartLongitude.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtStartLongitude.Name = "txtStartLongitude";
            this.txtStartLongitude.Size = new System.Drawing.Size(120, 25);
            this.txtStartLongitude.TabIndex = 115;
            this.txtStartLongitude.Text = "4.1";
            // 
            // txtTakeoffLatitude
            // 
            this.txtTakeoffLatitude.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtTakeoffLatitude.Location = new System.Drawing.Point(154, 198);
            this.txtTakeoffLatitude.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtTakeoffLatitude.Name = "txtTakeoffLatitude";
            this.txtTakeoffLatitude.Size = new System.Drawing.Size(120, 25);
            this.txtTakeoffLatitude.TabIndex = 114;
            this.txtTakeoffLatitude.Text = "52.3";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label2.Location = new System.Drawing.Point(17, 234);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 19);
            this.label2.TabIndex = 113;
            this.label2.Text = "Longitude";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label1.Location = new System.Drawing.Point(17, 198);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 19);
            this.label1.TabIndex = 110;
            this.label1.Text = "Latitude";
            // 
            // VisualisationSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(864, 413);
            this.ControlBox = false;
            this.Controls.Add(this.label25);
            this.Controls.Add(this.txtNumberOfGenerations);
            this.Controls.Add(this.txtPopulationSize);
            this.Controls.Add(this.label28);
            this.Controls.Add(this.label29);
            this.Controls.Add(this.btnOptimise);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txtNumberOfSegments);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.txtEndLongitude);
            this.Controls.Add(this.txtEndLatitude);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.txtTakeoffSpeed);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.txtTakeoffHeading);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtTotalAircraftMass);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtStartLongitude);
            this.Controls.Add(this.txtTakeoffLatitude);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "VisualisationSettingsForm";
            this.Load += new System.EventHandler(this.VisualisationSettingsForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label label25;
        public TextBox txtNumberOfGenerations;
        public TextBox txtPopulationSize;
        private Label label28;
        private Label label29;
        private Button btnOptimise;
        private Label label19;
        private Label label20;
        private Label label18;
        private Label label17;
        private Label label13;
        private Label label9;
        private Label label8;
        private Label label6;
        private Label label12;
        public TextBox txtNumberOfSegments;
        private Label label14;
        public TextBox txtEndLongitude;
        public TextBox txtEndLatitude;
        private Label label15;
        private Label label16;
        public TextBox txtTakeoffSpeed;
        private Label label11;
        public ComboBox comboBox1;
        public TextBox txtTakeoffHeading;
        private Label label5;
        private Label label10;
        private Label label4;
        public TextBox txtTotalAircraftMass;
        private Label label3;
        private Label label7;
        public TextBox txtStartLongitude;
        public TextBox txtTakeoffLatitude;
        private Label label2;
        private Label label1;
    }
}