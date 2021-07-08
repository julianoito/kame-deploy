namespace Kame.Desktop.Views
{
    partial class ProjectShortcutButton
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.pnlProject = new System.Windows.Forms.Panel();
			this.imgProjectLogo = new System.Windows.Forms.PictureBox();
			this.lblProjectName = new System.Windows.Forms.Label();
			this.pnlProject.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.imgProjectLogo)).BeginInit();
			this.SuspendLayout();
			// 
			// pnlProject
			// 
			this.pnlProject.BackColor = System.Drawing.Color.DarkBlue;
			this.pnlProject.Controls.Add(this.imgProjectLogo);
			this.pnlProject.Controls.Add(this.lblProjectName);
			this.pnlProject.Location = new System.Drawing.Point(5, 5);
			this.pnlProject.Name = "pnlProject";
			this.pnlProject.Size = new System.Drawing.Size(240, 170);
			this.pnlProject.TabIndex = 0;
			this.pnlProject.Click += new System.EventHandler(this.ProjectShortcutButton_Click);
			// 
			// imgProjectLogo
			// 
			this.imgProjectLogo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.imgProjectLogo.Location = new System.Drawing.Point(20, 26);
			this.imgProjectLogo.Name = "imgProjectLogo";
			this.imgProjectLogo.Size = new System.Drawing.Size(200, 90);
			this.imgProjectLogo.TabIndex = 1;
			this.imgProjectLogo.TabStop = false;
			this.imgProjectLogo.Click += new System.EventHandler(this.ProjectShortcutButton_Click);
			// 
			// lblProjectName
			// 
			this.lblProjectName.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblProjectName.ForeColor = System.Drawing.Color.White;
			this.lblProjectName.Location = new System.Drawing.Point(0, 134);
			this.lblProjectName.Name = "lblProjectName";
			this.lblProjectName.Size = new System.Drawing.Size(240, 34);
			this.lblProjectName.TabIndex = 0;
			this.lblProjectName.Text = "label1";
			this.lblProjectName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.lblProjectName.Click += new System.EventHandler(this.ProjectShortcutButton_Click);
			// 
			// ProjectShortcutButton
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Transparent;
			this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Controls.Add(this.pnlProject);
			this.Margin = new System.Windows.Forms.Padding(0);
			this.Name = "ProjectShortcutButton";
			this.Size = new System.Drawing.Size(250, 180);
			this.Click += new System.EventHandler(this.ProjectShortcutButton_Click);
			this.pnlProject.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.imgProjectLogo)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlProject;
        private System.Windows.Forms.PictureBox imgProjectLogo;
		private System.Windows.Forms.Label lblProjectName;
    }
}
