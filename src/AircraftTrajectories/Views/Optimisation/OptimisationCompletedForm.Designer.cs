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
            this.btnSave = new System.Windows.Forms.Button();
            this.btnVisualise = new System.Windows.Forms.Button();
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
            this.btnSave.Location = new System.Drawing.Point(12, 482);
            this.btnSave.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(248, 59);
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
            this.btnVisualise.Location = new System.Drawing.Point(281, 482);
            this.btnVisualise.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnVisualise.Name = "btnVisualise";
            this.btnVisualise.Size = new System.Drawing.Size(248, 59);
            this.btnVisualise.TabIndex = 106;
            this.btnVisualise.Text = "Visualise trajectory";
            this.btnVisualise.UseVisualStyleBackColor = false;
            this.btnVisualise.Click += new System.EventHandler(this.btnVisualise_Click);
            // 
            // OptimisationCompletedForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1152, 552);
            this.ControlBox = false;
            this.Controls.Add(this.btnVisualise);
            this.Controls.Add(this.btnSave);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "OptimisationCompletedForm";
            this.Load += new System.EventHandler(this.OptimisationCompletedForm_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnVisualise;
        private System.Windows.Forms.Button btnSave;
    }
}