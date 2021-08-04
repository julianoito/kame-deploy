namespace Kame.Management.Desktop
{
    partial class FrmSelectMode
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnDatabaseConnect = new System.Windows.Forms.Button();
            this.btnApiConnect = new System.Windows.Forms.Button();
            this.btnNewFile = new System.Windows.Forms.Button();
            this.btnEditFile = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lnlTitleDatabase = new System.Windows.Forms.Label();
            this.lblTileFileEdit = new System.Windows.Forms.Label();
            this.lbTitleAPI = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnDatabaseConnect
            // 
            this.btnDatabaseConnect.BackColor = System.Drawing.Color.DimGray;
            this.btnDatabaseConnect.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnDatabaseConnect.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.btnDatabaseConnect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDatabaseConnect.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnDatabaseConnect.ForeColor = System.Drawing.Color.Snow;
            this.btnDatabaseConnect.Location = new System.Drawing.Point(12, 77);
            this.btnDatabaseConnect.Name = "btnDatabaseConnect";
            this.btnDatabaseConnect.Size = new System.Drawing.Size(200, 120);
            this.btnDatabaseConnect.TabIndex = 0;
            this.btnDatabaseConnect.TabStop = false;
            this.btnDatabaseConnect.Text = "Conectar a uma base de dados";
            this.btnDatabaseConnect.UseVisualStyleBackColor = false;
            this.btnDatabaseConnect.Click += new System.EventHandler(this.btnDatabaseConnect_Click);
            // 
            // btnApiConnect
            // 
            this.btnApiConnect.BackColor = System.Drawing.Color.DimGray;
            this.btnApiConnect.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.btnApiConnect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnApiConnect.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnApiConnect.ForeColor = System.Drawing.Color.Snow;
            this.btnApiConnect.Location = new System.Drawing.Point(281, 77);
            this.btnApiConnect.Name = "btnApiConnect";
            this.btnApiConnect.Size = new System.Drawing.Size(200, 120);
            this.btnApiConnect.TabIndex = 1;
            this.btnApiConnect.TabStop = false;
            this.btnApiConnect.Text = "Conectar a uma API";
            this.btnApiConnect.UseVisualStyleBackColor = false;
            // 
            // btnNewFile
            // 
            this.btnNewFile.BackColor = System.Drawing.Color.DimGray;
            this.btnNewFile.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.btnNewFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNewFile.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnNewFile.ForeColor = System.Drawing.Color.Snow;
            this.btnNewFile.Location = new System.Drawing.Point(551, 77);
            this.btnNewFile.Name = "btnNewFile";
            this.btnNewFile.Size = new System.Drawing.Size(200, 46);
            this.btnNewFile.TabIndex = 2;
            this.btnNewFile.TabStop = false;
            this.btnNewFile.Text = "Novo arquivo de configuração";
            this.btnNewFile.UseVisualStyleBackColor = false;
            this.btnNewFile.Click += new System.EventHandler(this.btnNewFile_Click);
            // 
            // btnEditFile
            // 
            this.btnEditFile.BackColor = System.Drawing.Color.DimGray;
            this.btnEditFile.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.btnEditFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditFile.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnEditFile.ForeColor = System.Drawing.Color.Snow;
            this.btnEditFile.Location = new System.Drawing.Point(551, 151);
            this.btnEditFile.Name = "btnEditFile";
            this.btnEditFile.Size = new System.Drawing.Size(200, 46);
            this.btnEditFile.TabIndex = 3;
            this.btnEditFile.TabStop = false;
            this.btnEditFile.Text = "Editar arquivo de configuração";
            this.btnEditFile.UseVisualStyleBackColor = false;
            this.btnEditFile.Click += new System.EventHandler(this.btnEditFile_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblTitle.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTitle.ForeColor = System.Drawing.Color.Black;
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(812, 25);
            this.lblTitle.TabIndex = 3;
            this.lblTitle.Text = "Kame Management";
            this.lblTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblTitle_MouseDown);
            // 
            // lnlTitleDatabase
            // 
            this.lnlTitleDatabase.AutoSize = true;
            this.lnlTitleDatabase.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lnlTitleDatabase.Location = new System.Drawing.Point(12, 40);
            this.lnlTitleDatabase.Name = "lnlTitleDatabase";
            this.lnlTitleDatabase.Size = new System.Drawing.Size(118, 21);
            this.lnlTitleDatabase.TabIndex = 4;
            this.lnlTitleDatabase.Text = "Base de dados";
            // 
            // lblTileFileEdit
            // 
            this.lblTileFileEdit.AutoSize = true;
            this.lblTileFileEdit.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTileFileEdit.Location = new System.Drawing.Point(551, 40);
            this.lblTileFileEdit.Name = "lblTileFileEdit";
            this.lblTileFileEdit.Size = new System.Drawing.Size(154, 21);
            this.lblTileFileEdit.TabIndex = 4;
            this.lblTileFileEdit.Text = "Edição de arquivos";
            // 
            // lbTitleAPI
            // 
            this.lbTitleAPI.AutoSize = true;
            this.lbTitleAPI.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lbTitleAPI.Location = new System.Drawing.Point(281, 40);
            this.lbTitleAPI.Name = "lbTitleAPI";
            this.lbTitleAPI.Size = new System.Drawing.Size(36, 21);
            this.lbTitleAPI.TabIndex = 4;
            this.lbTitleAPI.Text = "API";
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnClose.ForeColor = System.Drawing.Color.Black;
            this.btnClose.Location = new System.Drawing.Point(781, -1);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(31, 26);
            this.btnClose.TabIndex = 5;
            this.btnClose.Text = "X";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // FrmSelectMode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.ClientSize = new System.Drawing.Size(809, 250);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnEditFile);
            this.Controls.Add(this.btnApiConnect);
            this.Controls.Add(this.btnNewFile);
            this.Controls.Add(this.btnDatabaseConnect);
            this.Controls.Add(this.lbTitleAPI);
            this.Controls.Add(this.lblTileFileEdit);
            this.Controls.Add(this.lnlTitleDatabase);
            this.Controls.Add(this.lblTitle);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "FrmSelectMode";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Base de Dados";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnDatabaseConnect;
        private System.Windows.Forms.Button btnApiConnect;
        private System.Windows.Forms.Button btnNewFile;
        private System.Windows.Forms.Button btnEditFile;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lnlTitleDatabase;
        private System.Windows.Forms.Label lblTileFileEdit;
        private System.Windows.Forms.Label lbTitleAPI;
        private System.Windows.Forms.Button btnClose;
    }
}

