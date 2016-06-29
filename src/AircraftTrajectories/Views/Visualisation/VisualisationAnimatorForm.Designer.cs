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
            this.txtHighestContourValue = new System.Windows.Forms.TextBox();
            this.txtLowestContourValue = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
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
            this.cbHeatmap = new System.Windows.Forms.CheckBox();
            this.txtContoursOfInterest = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.txtContourStep = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.cbContourGradient = new System.Windows.Forms.CheckBox();
            this.cbHighlightedContours = new System.Windows.Forms.CheckBox();
            this.pnlHighlightedContours = new System.Windows.Forms.Panel();
            this.pnlContourGradient = new System.Windows.Forms.Panel();
            this.txtPopulationFactor = new System.Windows.Forms.TextBox();
            this.txtPopulationDotSize = new System.Windows.Forms.TextBox();
            this.txtCameraAltitude = new System.Windows.Forms.TextBox();
            this.txtDotFile = new System.Windows.Forms.TextBox();
            this.pnlHighlightedContours.SuspendLayout();
            this.pnlContourGradient.SuspendLayout();
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
            this.btnPrepare.Location = new System.Drawing.Point(11, 402);
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
            // txtHighestContourValue
            // 
            this.txtHighestContourValue.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtHighestContourValue.Location = new System.Drawing.Point(178, 43);
            this.txtHighestContourValue.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtHighestContourValue.Name = "txtHighestContourValue";
            this.txtHighestContourValue.Size = new System.Drawing.Size(120, 25);
            this.txtHighestContourValue.TabIndex = 152;
            this.txtHighestContourValue.Text = "80";
            // 
            // txtLowestContourValue
            // 
            this.txtLowestContourValue.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtLowestContourValue.Location = new System.Drawing.Point(178, 7);
            this.txtLowestContourValue.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtLowestContourValue.Name = "txtLowestContourValue";
            this.txtLowestContourValue.Size = new System.Drawing.Size(120, 25);
            this.txtLowestContourValue.TabIndex = 153;
            this.txtLowestContourValue.Text = "60";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label1.Location = new System.Drawing.Point(4, 7);
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
            this.label3.Location = new System.Drawing.Point(4, 43);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(144, 19);
            this.label3.TabIndex = 150;
            this.label3.Text = "Highest value contour";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI Light", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label6.Location = new System.Drawing.Point(465, 11);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(144, 32);
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
            this.btnBrowseTrajectory.Location = new System.Drawing.Point(716, 81);
            this.btnBrowseTrajectory.Margin = new System.Windows.Forms.Padding(2);
            this.btnBrowseTrajectory.Name = "btnBrowseTrajectory";
            this.btnBrowseTrajectory.Size = new System.Drawing.Size(100, 34);
            this.btnBrowseTrajectory.TabIndex = 161;
            this.btnBrowseTrajectory.Text = "browse";
            this.btnBrowseTrajectory.UseVisualStyleBackColor = false;
            // 
            // txtCustomMapFile
            // 
            this.txtCustomMapFile.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtCustomMapFile.Location = new System.Drawing.Point(470, 87);
            this.txtCustomMapFile.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtCustomMapFile.Name = "txtCustomMapFile";
            this.txtCustomMapFile.Size = new System.Drawing.Size(240, 25);
            this.txtCustomMapFile.TabIndex = 160;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label7.Location = new System.Drawing.Point(467, 61);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(68, 19);
            this.label7.TabIndex = 159;
            this.label7.Text = "Image file";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label15.Location = new System.Drawing.Point(467, 161);
            this.label15.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(59, 19);
            this.label15.TabIndex = 169;
            this.label15.Text = "Latitude";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label13.Location = new System.Drawing.Point(713, 162);
            this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(28, 19);
            this.label13.TabIndex = 179;
            this.label13.Text = "° N";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label14.Location = new System.Drawing.Point(467, 197);
            this.label14.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(71, 19);
            this.label14.TabIndex = 170;
            this.label14.Text = "Longitude";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label9.Location = new System.Drawing.Point(713, 197);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(25, 19);
            this.label9.TabIndex = 178;
            this.label9.Text = "° E";
            // 
            // txtBottomLeftLatitude
            // 
            this.txtBottomLeftLatitude.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtBottomLeftLatitude.Location = new System.Drawing.Point(590, 159);
            this.txtBottomLeftLatitude.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtBottomLeftLatitude.Name = "txtBottomLeftLatitude";
            this.txtBottomLeftLatitude.Size = new System.Drawing.Size(120, 25);
            this.txtBottomLeftLatitude.TabIndex = 171;
            this.txtBottomLeftLatitude.Text = "50.63326389";
            // 
            // txtBottomLeftLongitude
            // 
            this.txtBottomLeftLongitude.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtBottomLeftLongitude.Location = new System.Drawing.Point(590, 196);
            this.txtBottomLeftLongitude.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtBottomLeftLongitude.Name = "txtBottomLeftLongitude";
            this.txtBottomLeftLongitude.Size = new System.Drawing.Size(120, 25);
            this.txtBottomLeftLongitude.TabIndex = 172;
            this.txtBottomLeftLongitude.Text = "2.98075278";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label16.Location = new System.Drawing.Point(469, 128);
            this.label16.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(147, 19);
            this.label16.TabIndex = 181;
            this.label16.Text = "Bottom left coordinate";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label8.Location = new System.Drawing.Point(469, 243);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(149, 19);
            this.label8.TabIndex = 188;
            this.label8.Text = "Upper right coordinate";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label11.Location = new System.Drawing.Point(467, 275);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(59, 19);
            this.label11.TabIndex = 182;
            this.label11.Text = "Latitude";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label12.Location = new System.Drawing.Point(713, 276);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(28, 19);
            this.label12.TabIndex = 187;
            this.label12.Text = "° N";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label17.Location = new System.Drawing.Point(467, 312);
            this.label17.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(71, 19);
            this.label17.TabIndex = 183;
            this.label17.Text = "Longitude";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label18.Location = new System.Drawing.Point(713, 312);
            this.label18.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(25, 19);
            this.label18.TabIndex = 186;
            this.label18.Text = "° E";
            // 
            // txtUpperRightLatitude
            // 
            this.txtUpperRightLatitude.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtUpperRightLatitude.Location = new System.Drawing.Point(590, 274);
            this.txtUpperRightLatitude.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtUpperRightLatitude.Name = "txtUpperRightLatitude";
            this.txtUpperRightLatitude.Size = new System.Drawing.Size(120, 25);
            this.txtUpperRightLatitude.TabIndex = 184;
            this.txtUpperRightLatitude.Text = "53.82599722";
            // 
            // txtUpperRightLongitude
            // 
            this.txtUpperRightLongitude.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtUpperRightLongitude.Location = new System.Drawing.Point(590, 310);
            this.txtUpperRightLongitude.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtUpperRightLongitude.Name = "txtUpperRightLongitude";
            this.txtUpperRightLongitude.Size = new System.Drawing.Size(120, 25);
            this.txtUpperRightLongitude.TabIndex = 185;
            this.txtUpperRightLongitude.Text = "7.61420278";
            // 
            // cbHeatmap
            // 
            this.cbHeatmap.AutoSize = true;
            this.cbHeatmap.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cbHeatmap.Location = new System.Drawing.Point(16, 308);
            this.cbHeatmap.Margin = new System.Windows.Forms.Padding(2);
            this.cbHeatmap.Name = "cbHeatmap";
            this.cbHeatmap.Size = new System.Drawing.Size(198, 23);
            this.cbHeatmap.TabIndex = 189;
            this.cbHeatmap.Text = "Visualise population density";
            this.cbHeatmap.UseVisualStyleBackColor = true;
            // 
            // txtContoursOfInterest
            // 
            this.txtContoursOfInterest.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtContoursOfInterest.Location = new System.Drawing.Point(178, 10);
            this.txtContoursOfInterest.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtContoursOfInterest.Name = "txtContoursOfInterest";
            this.txtContoursOfInterest.Size = new System.Drawing.Size(120, 25);
            this.txtContoursOfInterest.TabIndex = 191;
            this.txtContoursOfInterest.Text = "50, 60, 70, 80";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label19.Location = new System.Drawing.Point(4, 10);
            this.label19.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(132, 19);
            this.label19.TabIndex = 190;
            this.label19.Text = "Contours of interest";
            // 
            // txtContourStep
            // 
            this.txtContourStep.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtContourStep.Location = new System.Drawing.Point(178, 79);
            this.txtContourStep.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtContourStep.Name = "txtContourStep";
            this.txtContourStep.Size = new System.Drawing.Size(120, 25);
            this.txtContourStep.TabIndex = 193;
            this.txtContourStep.Text = "10";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label20.Location = new System.Drawing.Point(4, 79);
            this.label20.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(58, 19);
            this.label20.TabIndex = 192;
            this.label20.Text = "Stepsize";
            // 
            // cbContourGradient
            // 
            this.cbContourGradient.AutoSize = true;
            this.cbContourGradient.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cbContourGradient.Location = new System.Drawing.Point(16, 61);
            this.cbContourGradient.Margin = new System.Windows.Forms.Padding(2);
            this.cbContourGradient.Name = "cbContourGradient";
            this.cbContourGradient.Size = new System.Drawing.Size(134, 23);
            this.cbContourGradient.TabIndex = 196;
            this.cbContourGradient.Text = "Contour gradient";
            this.cbContourGradient.UseVisualStyleBackColor = true;
            this.cbContourGradient.CheckedChanged += new System.EventHandler(this.cbContourGradient_CheckedChanged);
            // 
            // cbHighlightedContours
            // 
            this.cbHighlightedContours.AutoSize = true;
            this.cbHighlightedContours.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cbHighlightedContours.Location = new System.Drawing.Point(16, 215);
            this.cbHighlightedContours.Margin = new System.Windows.Forms.Padding(2);
            this.cbHighlightedContours.Name = "cbHighlightedContours";
            this.cbHighlightedContours.Size = new System.Drawing.Size(157, 23);
            this.cbHighlightedContours.TabIndex = 197;
            this.cbHighlightedContours.Text = "Highlighted contours";
            this.cbHighlightedContours.UseVisualStyleBackColor = true;
            this.cbHighlightedContours.CheckedChanged += new System.EventHandler(this.cbHighlightedContours_CheckedChanged);
            // 
            // pnlHighlightedContours
            // 
            this.pnlHighlightedContours.Controls.Add(this.label19);
            this.pnlHighlightedContours.Controls.Add(this.txtContoursOfInterest);
            this.pnlHighlightedContours.Enabled = false;
            this.pnlHighlightedContours.Location = new System.Drawing.Point(9, 243);
            this.pnlHighlightedContours.Name = "pnlHighlightedContours";
            this.pnlHighlightedContours.Size = new System.Drawing.Size(347, 45);
            this.pnlHighlightedContours.TabIndex = 198;
            // 
            // pnlContourGradient
            // 
            this.pnlContourGradient.Controls.Add(this.label1);
            this.pnlContourGradient.Controls.Add(this.label3);
            this.pnlContourGradient.Controls.Add(this.txtLowestContourValue);
            this.pnlContourGradient.Controls.Add(this.txtHighestContourValue);
            this.pnlContourGradient.Controls.Add(this.txtContourStep);
            this.pnlContourGradient.Controls.Add(this.label20);
            this.pnlContourGradient.Enabled = false;
            this.pnlContourGradient.Location = new System.Drawing.Point(9, 88);
            this.pnlContourGradient.Name = "pnlContourGradient";
            this.pnlContourGradient.Size = new System.Drawing.Size(347, 117);
            this.pnlContourGradient.TabIndex = 199;
            // 
            // txtPopulationFactor
            // 
            this.txtPopulationFactor.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtPopulationFactor.Location = new System.Drawing.Point(397, 416);
            this.txtPopulationFactor.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtPopulationFactor.Name = "txtPopulationFactor";
            this.txtPopulationFactor.Size = new System.Drawing.Size(120, 25);
            this.txtPopulationFactor.TabIndex = 192;
            this.txtPopulationFactor.Text = "0.001";
            this.txtPopulationFactor.Visible = false;
            // 
            // txtPopulationDotSize
            // 
            this.txtPopulationDotSize.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtPopulationDotSize.Location = new System.Drawing.Point(521, 416);
            this.txtPopulationDotSize.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtPopulationDotSize.Name = "txtPopulationDotSize";
            this.txtPopulationDotSize.Size = new System.Drawing.Size(120, 25);
            this.txtPopulationDotSize.TabIndex = 200;
            this.txtPopulationDotSize.Text = "3000";
            this.txtPopulationDotSize.Visible = false;
            // 
            // txtCameraAltitude
            // 
            this.txtCameraAltitude.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtCameraAltitude.Location = new System.Drawing.Point(696, 416);
            this.txtCameraAltitude.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtCameraAltitude.Name = "txtCameraAltitude";
            this.txtCameraAltitude.Size = new System.Drawing.Size(120, 25);
            this.txtCameraAltitude.TabIndex = 201;
            this.txtCameraAltitude.Text = "70000";
            this.txtCameraAltitude.Visible = false;
            // 
            // txtDotFile
            // 
            this.txtDotFile.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtDotFile.Location = new System.Drawing.Point(273, 416);
            this.txtDotFile.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtDotFile.Name = "txtDotFile";
            this.txtDotFile.Size = new System.Drawing.Size(120, 25);
            this.txtDotFile.TabIndex = 202;
            this.txtDotFile.Text = "dot2_30.png";
            this.txtDotFile.Visible = false;
            // 
            // VisualisationAnimatorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(864, 461);
            this.Controls.Add(this.txtDotFile);
            this.Controls.Add(this.txtCameraAltitude);
            this.Controls.Add(this.txtPopulationDotSize);
            this.Controls.Add(this.txtPopulationFactor);
            this.Controls.Add(this.pnlContourGradient);
            this.Controls.Add(this.pnlHighlightedContours);
            this.Controls.Add(this.cbHighlightedContours);
            this.Controls.Add(this.cbContourGradient);
            this.Controls.Add(this.cbHeatmap);
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
            this.Controls.Add(this.label10);
            this.Controls.Add(this.btnPrepare);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "VisualisationAnimatorForm";
            this.Load += new System.EventHandler(this.VisualisationAnimatorForm_Load);
            this.pnlHighlightedContours.ResumeLayout(false);
            this.pnlHighlightedContours.PerformLayout();
            this.pnlContourGradient.ResumeLayout(false);
            this.pnlContourGradient.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnPrepare;
        private System.Windows.Forms.Label label10;
        public System.Windows.Forms.TextBox txtLowestContourValue;
        public System.Windows.Forms.TextBox txtHighestContourValue;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
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
        public System.Windows.Forms.CheckBox cbHeatmap;
        public System.Windows.Forms.TextBox txtContoursOfInterest;
        private System.Windows.Forms.Label label19;
        public System.Windows.Forms.TextBox txtContourStep;
        private System.Windows.Forms.Label label20;
        public System.Windows.Forms.CheckBox cbContourGradient;
        public System.Windows.Forms.CheckBox cbHighlightedContours;
        private System.Windows.Forms.Panel pnlHighlightedContours;
        private System.Windows.Forms.Panel pnlContourGradient;
        public System.Windows.Forms.TextBox txtPopulationFactor;
        public System.Windows.Forms.TextBox txtPopulationDotSize;
        public System.Windows.Forms.TextBox txtCameraAltitude;
        public System.Windows.Forms.TextBox txtDotFile;
    }
}