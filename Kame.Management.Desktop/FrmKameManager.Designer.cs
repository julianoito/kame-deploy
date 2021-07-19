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
            this.btnUsers = new System.Windows.Forms.Button();
            this.btnDeploys = new System.Windows.Forms.Button();
            this.lstData = new System.Windows.Forms.ListBox();
            this.btnNewRecord = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnUsers
            // 
            this.btnUsers.Location = new System.Drawing.Point(12, 137);
            this.btnUsers.Name = "btnUsers";
            this.btnUsers.Size = new System.Drawing.Size(180, 73);
            this.btnUsers.TabIndex = 0;
            this.btnUsers.Tag = "Users";
            this.btnUsers.Text = "Usuários";
            this.btnUsers.UseVisualStyleBackColor = true;
            this.btnUsers.Click += new System.EventHandler(this.btnUsers_Click);
            // 
            // btnDeploys
            // 
            this.btnDeploys.Location = new System.Drawing.Point(12, 58);
            this.btnDeploys.Name = "btnDeploys";
            this.btnDeploys.Size = new System.Drawing.Size(180, 73);
            this.btnDeploys.TabIndex = 0;
            this.btnDeploys.Tag = "Deploys";
            this.btnDeploys.Text = "Configurações de Deploy";
            this.btnDeploys.UseVisualStyleBackColor = true;
            this.btnDeploys.Click += new System.EventHandler(this.btnDeploys_Click);
            // 
            // lstData
            // 
            this.lstData.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lstData.FormattingEnabled = true;
            this.lstData.ItemHeight = 19;
            this.lstData.Location = new System.Drawing.Point(234, 58);
            this.lstData.Name = "lstData";
            this.lstData.Size = new System.Drawing.Size(861, 536);
            this.lstData.TabIndex = 1;
            // 
            // btnNewRecord
            // 
            this.btnNewRecord.Location = new System.Drawing.Point(1008, 12);
            this.btnNewRecord.Name = "btnNewRecord";
            this.btnNewRecord.Size = new System.Drawing.Size(87, 35);
            this.btnNewRecord.TabIndex = 2;
            this.btnNewRecord.Text = "Novo";
            this.btnNewRecord.UseVisualStyleBackColor = true;
            this.btnNewRecord.Click += new System.EventHandler(this.btnNewRecord_Click);
            // 
            // FrmKameManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1129, 610);
            this.Controls.Add(this.btnNewRecord);
            this.Controls.Add(this.lstData);
            this.Controls.Add(this.btnDeploys);
            this.Controls.Add(this.btnUsers);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "FrmKameManager";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Kame Management";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmKameManager_FormClosing);
            this.Load += new System.EventHandler(this.FrmKameManager_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnUsers;
        private System.Windows.Forms.Button btnDeploys;
        private System.Windows.Forms.ListBox lstData;
        private System.Windows.Forms.Button btnNewRecord;
    }
}