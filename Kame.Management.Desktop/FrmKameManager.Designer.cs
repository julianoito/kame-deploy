namespace Kame.Management.Desktop
{
    partial class FrmKameManager
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
            this.lstData = new System.Windows.Forms.ListBox();
            this.btnNewRecord = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblMenuDeploys = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblMenuUsers = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lstData
            // 
            this.lstData.BackColor = System.Drawing.Color.Gainsboro;
            this.lstData.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstData.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lstData.FormattingEnabled = true;
            this.lstData.ItemHeight = 19;
            this.lstData.Location = new System.Drawing.Point(35, 58);
            this.lstData.Name = "lstData";
            this.lstData.Size = new System.Drawing.Size(1060, 532);
            this.lstData.TabIndex = 1;
            // 
            // btnNewRecord
            // 
            this.btnNewRecord.BackColor = System.Drawing.Color.DimGray;
            this.btnNewRecord.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.btnNewRecord.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNewRecord.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnNewRecord.ForeColor = System.Drawing.Color.Snow;
            this.btnNewRecord.Location = new System.Drawing.Point(993, 71);
            this.btnNewRecord.Name = "btnNewRecord";
            this.btnNewRecord.Size = new System.Drawing.Size(87, 35);
            this.btnNewRecord.TabIndex = 2;
            this.btnNewRecord.Text = "Novo";
            this.btnNewRecord.UseVisualStyleBackColor = false;
            this.btnNewRecord.Click += new System.EventHandler(this.btnNewRecord_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnClose.ForeColor = System.Drawing.Color.Black;
            this.btnClose.Location = new System.Drawing.Point(1101, -1);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(31, 26);
            this.btnClose.TabIndex = 5;
            this.btnClose.Text = "X";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblTitle.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTitle.ForeColor = System.Drawing.Color.Black;
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(1132, 25);
            this.lblTitle.TabIndex = 3;
            this.lblTitle.Text = "Kame Management";
            this.lblTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblTitle_MouseDown);
            // 
            // lblMenuDeploys
            // 
            this.lblMenuDeploys.AutoSize = true;
            this.lblMenuDeploys.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblMenuDeploys.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblMenuDeploys.ForeColor = System.Drawing.Color.Snow;
            this.lblMenuDeploys.Location = new System.Drawing.Point(12, 0);
            this.lblMenuDeploys.Name = "lblMenuDeploys";
            this.lblMenuDeploys.Size = new System.Drawing.Size(177, 20);
            this.lblMenuDeploys.TabIndex = 6;
            this.lblMenuDeploys.Tag = "Deploys";
            this.lblMenuDeploys.Text = "Configuração de Deploys";
            this.lblMenuDeploys.Click += new System.EventHandler(this.lblMenuDeploys_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.Controls.Add(this.lblMenuUsers);
            this.panel1.Controls.Add(this.lblMenuDeploys);
            this.panel1.Location = new System.Drawing.Point(0, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1132, 22);
            this.panel1.TabIndex = 7;
            // 
            // lblMenuUsers
            // 
            this.lblMenuUsers.AutoSize = true;
            this.lblMenuUsers.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblMenuUsers.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblMenuUsers.ForeColor = System.Drawing.Color.Snow;
            this.lblMenuUsers.Location = new System.Drawing.Point(210, 1);
            this.lblMenuUsers.Name = "lblMenuUsers";
            this.lblMenuUsers.Size = new System.Drawing.Size(65, 20);
            this.lblMenuUsers.TabIndex = 6;
            this.lblMenuUsers.Tag = "Users";
            this.lblMenuUsers.Text = "Usuários";
            this.lblMenuUsers.Click += new System.EventHandler(this.lblMenuUsers_Click);
            // 
            // FrmKameManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.ClientSize = new System.Drawing.Size(1129, 610);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnNewRecord);
            this.Controls.Add(this.lstData);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "FrmKameManager";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Kame Management";
            this.Load += new System.EventHandler(this.FrmKameManager_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ListBox lstData;
        private System.Windows.Forms.Button btnNewRecord;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblMenuDeploys;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblMenuUsers;
    }
}