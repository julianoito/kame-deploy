namespace Kame.Desktop.Views
{
    partial class FrmExecution
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmExecution));
			this.btnExecute = new System.Windows.Forms.Button();
			this.BtnClose = new System.Windows.Forms.Button();
			this.lblTitle = new System.Windows.Forms.Label();
			this.lblProjectName = new System.Windows.Forms.Label();
			this.imgProjectLogo = new System.Windows.Forms.PictureBox();
			this.txtExecutionLog = new System.Windows.Forms.TextBox();
			this.btnShowLog = new System.Windows.Forms.Button();
			this.webBrowser = new System.Windows.Forms.WebBrowser();
			((System.ComponentModel.ISupportInitialize)(this.imgProjectLogo)).BeginInit();
			this.SuspendLayout();
			// 
			// btnExecute
			// 
			this.btnExecute.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
			this.btnExecute.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnExecute.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnExecute.ForeColor = System.Drawing.Color.White;
			this.btnExecute.Location = new System.Drawing.Point(656, 538);
			this.btnExecute.Name = "btnExecute";
			this.btnExecute.Size = new System.Drawing.Size(175, 50);
			this.btnExecute.TabIndex = 12;
			this.btnExecute.Text = "Executar Projeto";
			this.btnExecute.UseVisualStyleBackColor = false;
			this.btnExecute.Click += new System.EventHandler(this.btnExecute_Click);
			// 
			// BtnClose
			// 
			this.BtnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
			this.BtnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.BtnClose.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.BtnClose.ForeColor = System.Drawing.Color.White;
			this.BtnClose.Location = new System.Drawing.Point(837, 538);
			this.BtnClose.Name = "BtnClose";
			this.BtnClose.Size = new System.Drawing.Size(175, 50);
			this.BtnClose.TabIndex = 11;
			this.BtnClose.Text = "Fechar";
			this.BtnClose.UseVisualStyleBackColor = false;
			this.BtnClose.Click += new System.EventHandler(this.BtnClose_Click);
			// 
			// lblTitle
			// 
			this.lblTitle.BackColor = System.Drawing.Color.DarkGreen;
			this.lblTitle.Font = new System.Drawing.Font("Calibri", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblTitle.ForeColor = System.Drawing.Color.White;
			this.lblTitle.Location = new System.Drawing.Point(-2, -1);
			this.lblTitle.Name = "lblTitle";
			this.lblTitle.Size = new System.Drawing.Size(1027, 29);
			this.lblTitle.TabIndex = 13;
			this.lblTitle.Text = "Kame Deploy Manager";
			this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblProjectName
			// 
			this.lblProjectName.BackColor = System.Drawing.Color.Transparent;
			this.lblProjectName.Font = new System.Drawing.Font("Calibri", 20.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblProjectName.ForeColor = System.Drawing.Color.White;
			this.lblProjectName.Location = new System.Drawing.Point(218, 31);
			this.lblProjectName.Name = "lblProjectName";
			this.lblProjectName.Size = new System.Drawing.Size(585, 50);
			this.lblProjectName.TabIndex = 14;
			this.lblProjectName.Text = "Project Name";
			this.lblProjectName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// imgProjectLogo
			// 
			this.imgProjectLogo.Location = new System.Drawing.Point(12, 31);
			this.imgProjectLogo.Name = "imgProjectLogo";
			this.imgProjectLogo.Size = new System.Drawing.Size(200, 90);
			this.imgProjectLogo.TabIndex = 15;
			this.imgProjectLogo.TabStop = false;
			// 
			// txtExecutionLog
			// 
			this.txtExecutionLog.BackColor = System.Drawing.Color.Honeydew;
			this.txtExecutionLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.txtExecutionLog.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtExecutionLog.Location = new System.Drawing.Point(679, 127);
			this.txtExecutionLog.Multiline = true;
			this.txtExecutionLog.Name = "txtExecutionLog";
			this.txtExecutionLog.ReadOnly = true;
			this.txtExecutionLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtExecutionLog.Size = new System.Drawing.Size(333, 254);
			this.txtExecutionLog.TabIndex = 17;
			this.txtExecutionLog.Visible = false;
			// 
			// btnShowLog
			// 
			this.btnShowLog.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
			this.btnShowLog.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnShowLog.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnShowLog.ForeColor = System.Drawing.Color.White;
			this.btnShowLog.Location = new System.Drawing.Point(475, 538);
			this.btnShowLog.Name = "btnShowLog";
			this.btnShowLog.Size = new System.Drawing.Size(175, 50);
			this.btnShowLog.TabIndex = 18;
			this.btnShowLog.Text = "Exibir Log";
			this.btnShowLog.UseVisualStyleBackColor = false;
			this.btnShowLog.Visible = false;
			this.btnShowLog.Click += new System.EventHandler(this.btnShowLog_Click);
			// 
			// webBrowser
			// 
			this.webBrowser.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.webBrowser.Location = new System.Drawing.Point(56, 188);
			this.webBrowser.MinimumSize = new System.Drawing.Size(20, 20);
			this.webBrowser.Name = "webBrowser";
			this.webBrowser.ScriptErrorsSuppressed = true;
			this.webBrowser.Size = new System.Drawing.Size(485, 265);
			this.webBrowser.TabIndex = 19;
			this.webBrowser.WebBrowserShortcutsEnabled = false;
			// 
			// FrmExecution
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.DarkSlateGray;
			this.ClientSize = new System.Drawing.Size(1024, 600);
			this.Controls.Add(this.webBrowser);
			this.Controls.Add(this.btnShowLog);
			this.Controls.Add(this.txtExecutionLog);
			this.Controls.Add(this.imgProjectLogo);
			this.Controls.Add(this.lblProjectName);
			this.Controls.Add(this.lblTitle);
			this.Controls.Add(this.btnExecute);
			this.Controls.Add(this.BtnClose);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "FrmExecution";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Kame Deploy Manager";
			((System.ComponentModel.ISupportInitialize)(this.imgProjectLogo)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnExecute;
        private System.Windows.Forms.Button BtnClose;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblProjectName;
		private System.Windows.Forms.PictureBox imgProjectLogo;
        private System.Windows.Forms.TextBox txtExecutionLog;
        private System.Windows.Forms.Button btnShowLog;
		private System.Windows.Forms.WebBrowser webBrowser;
    }
}