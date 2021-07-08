namespace Kame.Desktop.Views
{
	partial class FrmConfig
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmConfig));
			this.lblTitle = new System.Windows.Forms.Label();
			this.btnAddProject = new System.Windows.Forms.Button();
			this.BtnClose = new System.Windows.Forms.Button();
			this.txtParameters = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// lblTitle
			// 
			this.lblTitle.BackColor = System.Drawing.Color.DarkGreen;
			this.lblTitle.Font = new System.Drawing.Font("Calibri", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblTitle.ForeColor = System.Drawing.Color.White;
			this.lblTitle.Location = new System.Drawing.Point(0, 2);
			this.lblTitle.Name = "lblTitle";
			this.lblTitle.Size = new System.Drawing.Size(559, 29);
			this.lblTitle.TabIndex = 7;
			this.lblTitle.Text = "Kame Deploy Manager";
			this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// btnAddProject
			// 
			this.btnAddProject.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
			this.btnAddProject.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnAddProject.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnAddProject.ForeColor = System.Drawing.Color.White;
			this.btnAddProject.Location = new System.Drawing.Point(187, 282);
			this.btnAddProject.Name = "btnAddProject";
			this.btnAddProject.Size = new System.Drawing.Size(175, 50);
			this.btnAddProject.TabIndex = 12;
			this.btnAddProject.Text = "Salvar";
			this.btnAddProject.UseVisualStyleBackColor = false;
			// 
			// BtnClose
			// 
			this.BtnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
			this.BtnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.BtnClose.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.BtnClose.ForeColor = System.Drawing.Color.White;
			this.BtnClose.Location = new System.Drawing.Point(368, 282);
			this.BtnClose.Name = "BtnClose";
			this.BtnClose.Size = new System.Drawing.Size(175, 50);
			this.BtnClose.TabIndex = 11;
			this.BtnClose.Text = "Fechar";
			this.BtnClose.UseVisualStyleBackColor = false;
			// 
			// txtParameters
			// 
			this.txtParameters.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtParameters.Location = new System.Drawing.Point(15, 51);
			this.txtParameters.Multiline = true;
			this.txtParameters.Name = "txtParameters";
			this.txtParameters.Size = new System.Drawing.Size(527, 211);
			this.txtParameters.TabIndex = 27;
			// 
			// label3
			// 
			this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
			this.label3.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.ForeColor = System.Drawing.Color.White;
			this.label3.Location = new System.Drawing.Point(11, 28);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(535, 241);
			this.label3.TabIndex = 26;
			this.label3.Text = "Parâmetros de execução";
			// 
			// FrmConfig
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.DarkSlateGray;
			this.ClientSize = new System.Drawing.Size(556, 351);
			this.Controls.Add(this.txtParameters);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.btnAddProject);
			this.Controls.Add(this.BtnClose);
			this.Controls.Add(this.lblTitle);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "FrmConfig";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label lblTitle;
		private System.Windows.Forms.Button btnAddProject;
		private System.Windows.Forms.Button BtnClose;
		private System.Windows.Forms.TextBox txtParameters;
		private System.Windows.Forms.Label label3;
	}
}