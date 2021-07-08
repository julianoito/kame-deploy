namespace Kame.Desktop.Views
{
    partial class FrmMain
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
			this.btnClose = new System.Windows.Forms.PictureBox();
			this.pnlProjects = new System.Windows.Forms.Panel();
			this.lblTitle = new System.Windows.Forms.Label();
			this.pnlMenu = new System.Windows.Forms.Panel();
			this.lblProfile = new System.Windows.Forms.Label();
			this.cmdProfile = new System.Windows.Forms.ComboBox();
			this.lblMenuBackgroud = new System.Windows.Forms.Label();
			this.lblGoku = new System.Windows.Forms.Label();
			this.pnlUpdate = new System.Windows.Forms.Panel();
			this.lblUpdatedVersion = new System.Windows.Forms.Label();
			this.lblTItuloAtualizacao = new System.Windows.Forms.Label();
			this.btnCloseUpdate = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.btnClose)).BeginInit();
			this.pnlMenu.SuspendLayout();
			this.pnlUpdate.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnClose
			// 
			this.btnClose.Image = global::Kame.Desktop.Properties.Resources.windowclose;
			this.btnClose.Location = new System.Drawing.Point(979, -1);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(45, 20);
			this.btnClose.TabIndex = 3;
			this.btnClose.TabStop = false;
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// pnlProjects
			// 
			this.pnlProjects.AutoScroll = true;
			this.pnlProjects.BackColor = System.Drawing.Color.Transparent;
			this.pnlProjects.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.pnlProjects.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pnlProjects.Location = new System.Drawing.Point(12, 93);
			this.pnlProjects.Name = "pnlProjects";
			this.pnlProjects.Size = new System.Drawing.Size(1000, 495);
			this.pnlProjects.TabIndex = 4;
			// 
			// lblTitle
			// 
			this.lblTitle.BackColor = System.Drawing.Color.DarkGreen;
			this.lblTitle.Font = new System.Drawing.Font("Calibri", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblTitle.ForeColor = System.Drawing.Color.White;
			this.lblTitle.Location = new System.Drawing.Point(-1, -1);
			this.lblTitle.Name = "lblTitle";
			this.lblTitle.Size = new System.Drawing.Size(1025, 29);
			this.lblTitle.TabIndex = 5;
			this.lblTitle.Text = "Kame Deploy Manager";
			this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// pnlMenu
			// 
			this.pnlMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
			this.pnlMenu.Controls.Add(this.lblProfile);
			this.pnlMenu.Controls.Add(this.cmdProfile);
			this.pnlMenu.Location = new System.Drawing.Point(2, 31);
			this.pnlMenu.Name = "pnlMenu";
			this.pnlMenu.Size = new System.Drawing.Size(1025, 56);
			this.pnlMenu.TabIndex = 9;
			// 
			// lblProfile
			// 
			this.lblProfile.AutoSize = true;
			this.lblProfile.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblProfile.ForeColor = System.Drawing.Color.White;
			this.lblProfile.Location = new System.Drawing.Point(3, 3);
			this.lblProfile.Name = "lblProfile";
			this.lblProfile.Size = new System.Drawing.Size(54, 19);
			this.lblProfile.TabIndex = 9;
			this.lblProfile.Text = "Profile";
			// 
			// cmdProfile
			// 
			this.cmdProfile.BackColor = System.Drawing.Color.White;
			this.cmdProfile.Cursor = System.Windows.Forms.Cursors.Hand;
			this.cmdProfile.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmdProfile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.cmdProfile.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cmdProfile.FormattingEnabled = true;
			this.cmdProfile.Location = new System.Drawing.Point(7, 24);
			this.cmdProfile.Name = "cmdProfile";
			this.cmdProfile.Size = new System.Drawing.Size(207, 27);
			this.cmdProfile.TabIndex = 8;
			this.cmdProfile.SelectedIndexChanged += new System.EventHandler(this.cmdProfile_SelectedIndexChanged);
			// 
			// lblMenuBackgroud
			// 
			this.lblMenuBackgroud.BackColor = System.Drawing.Color.DarkSlateGray;
			this.lblMenuBackgroud.Location = new System.Drawing.Point(0, 27);
			this.lblMenuBackgroud.Name = "lblMenuBackgroud";
			this.lblMenuBackgroud.Size = new System.Drawing.Size(1024, 63);
			this.lblMenuBackgroud.TabIndex = 10;
			// 
			// lblGoku
			// 
			this.lblGoku.AutoSize = true;
			this.lblGoku.BackColor = System.Drawing.Color.Transparent;
			this.lblGoku.Location = new System.Drawing.Point(994, 587);
			this.lblGoku.Name = "lblGoku";
			this.lblGoku.Size = new System.Drawing.Size(33, 13);
			this.lblGoku.TabIndex = 1;
			this.lblGoku.Text = "Goku";
			this.lblGoku.Click += new System.EventHandler(this.lblGoku_Click);
			// 
			// pnlUpdate
			// 
			this.pnlUpdate.BackColor = System.Drawing.Color.MintCream;
			this.pnlUpdate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pnlUpdate.Controls.Add(this.lblUpdatedVersion);
			this.pnlUpdate.Controls.Add(this.lblTItuloAtualizacao);
			this.pnlUpdate.Controls.Add(this.btnCloseUpdate);
			this.pnlUpdate.Location = new System.Drawing.Point(819, 518);
			this.pnlUpdate.Name = "pnlUpdate";
			this.pnlUpdate.Size = new System.Drawing.Size(200, 68);
			this.pnlUpdate.TabIndex = 0;
			this.pnlUpdate.Visible = false;
			// 
			// lblUpdatedVersion
			// 
			this.lblUpdatedVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblUpdatedVersion.ForeColor = System.Drawing.Color.SeaGreen;
			this.lblUpdatedVersion.Location = new System.Drawing.Point(3, 18);
			this.lblUpdatedVersion.Name = "lblUpdatedVersion";
			this.lblUpdatedVersion.Size = new System.Drawing.Size(157, 39);
			this.lblUpdatedVersion.TabIndex = 2;
			this.lblUpdatedVersion.Text = "Atualização";
			// 
			// lblTItuloAtualizacao
			// 
			this.lblTItuloAtualizacao.AutoSize = true;
			this.lblTItuloAtualizacao.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblTItuloAtualizacao.ForeColor = System.Drawing.Color.SeaGreen;
			this.lblTItuloAtualizacao.Location = new System.Drawing.Point(3, 3);
			this.lblTItuloAtualizacao.Name = "lblTItuloAtualizacao";
			this.lblTItuloAtualizacao.Size = new System.Drawing.Size(81, 15);
			this.lblTItuloAtualizacao.TabIndex = 1;
			this.lblTItuloAtualizacao.Text = "Atualização";
			// 
			// btnCloseUpdate
			// 
			this.btnCloseUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnCloseUpdate.Location = new System.Drawing.Point(166, 3);
			this.btnCloseUpdate.Name = "btnCloseUpdate";
			this.btnCloseUpdate.Size = new System.Drawing.Size(29, 23);
			this.btnCloseUpdate.TabIndex = 0;
			this.btnCloseUpdate.Text = "X";
			this.btnCloseUpdate.UseVisualStyleBackColor = true;
			this.btnCloseUpdate.Click += new System.EventHandler(this.btnCloseUpdate_Click);
			// 
			// FrmMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackgroundImage = global::Kame.Desktop.Properties.Resources.background;
			this.ClientSize = new System.Drawing.Size(1024, 600);
			this.Controls.Add(this.pnlUpdate);
			this.Controls.Add(this.lblGoku);
			this.Controls.Add(this.pnlMenu);
			this.Controls.Add(this.pnlProjects);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.lblMenuBackgroud);
			this.Controls.Add(this.lblTitle);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "FrmMain";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Kame";
			this.Activated += new System.EventHandler(this.FrmMain_Activated);
			this.Load += new System.EventHandler(this.FrmMain_Load);
			this.Shown += new System.EventHandler(this.FrmMain_Shown);
			((System.ComponentModel.ISupportInitialize)(this.btnClose)).EndInit();
			this.pnlMenu.ResumeLayout(false);
			this.pnlMenu.PerformLayout();
			this.pnlUpdate.ResumeLayout(false);
			this.pnlUpdate.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox btnClose;
        private System.Windows.Forms.Panel pnlProjects;
		private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlMenu;
        private System.Windows.Forms.ComboBox cmdProfile;
        private System.Windows.Forms.Label lblProfile;
		private System.Windows.Forms.Label lblMenuBackgroud;
		private System.Windows.Forms.Label lblGoku;
		private System.Windows.Forms.Panel pnlUpdate;
		private System.Windows.Forms.Label lblUpdatedVersion;
		private System.Windows.Forms.Label lblTItuloAtualizacao;
		private System.Windows.Forms.Button btnCloseUpdate;

    }
}

