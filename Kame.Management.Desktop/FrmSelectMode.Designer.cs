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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnEditFile = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnDatabaseConnect
            // 
            this.btnDatabaseConnect.Location = new System.Drawing.Point(25, 30);
            this.btnDatabaseConnect.Name = "btnDatabaseConnect";
            this.btnDatabaseConnect.Size = new System.Drawing.Size(200, 120);
            this.btnDatabaseConnect.TabIndex = 0;
            this.btnDatabaseConnect.Text = "Conectar a uma base de dados";
            this.btnDatabaseConnect.UseVisualStyleBackColor = true;
            this.btnDatabaseConnect.Click += new System.EventHandler(this.btnDatabaseConnect_Click);
            // 
            // btnApiConnect
            // 
            this.btnApiConnect.Location = new System.Drawing.Point(25, 30);
            this.btnApiConnect.Name = "btnApiConnect";
            this.btnApiConnect.Size = new System.Drawing.Size(200, 120);
            this.btnApiConnect.TabIndex = 0;
            this.btnApiConnect.Text = "Conectar a uma API";
            this.btnApiConnect.UseVisualStyleBackColor = true;
            // 
            // btnNewFile
            // 
            this.btnNewFile.Location = new System.Drawing.Point(25, 30);
            this.btnNewFile.Name = "btnNewFile";
            this.btnNewFile.Size = new System.Drawing.Size(200, 46);
            this.btnNewFile.TabIndex = 0;
            this.btnNewFile.Text = "Novo arquivo de configuração";
            this.btnNewFile.UseVisualStyleBackColor = true;
            this.btnNewFile.Click += new System.EventHandler(this.btnNewFile_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnDatabaseConnect);
            this.groupBox1.Location = new System.Drawing.Point(12, 64);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(245, 170);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Base de dados";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnApiConnect);
            this.groupBox2.Location = new System.Drawing.Point(281, 64);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(245, 170);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "API";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnEditFile);
            this.groupBox3.Controls.Add(this.btnNewFile);
            this.groupBox3.Location = new System.Drawing.Point(551, 64);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(245, 170);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Edição de arquivos";
            // 
            // btnEditFile
            // 
            this.btnEditFile.Location = new System.Drawing.Point(25, 104);
            this.btnEditFile.Name = "btnEditFile";
            this.btnEditFile.Size = new System.Drawing.Size(200, 46);
            this.btnEditFile.TabIndex = 0;
            this.btnEditFile.Text = "Editar arquivo de configuração";
            this.btnEditFile.UseVisualStyleBackColor = true;
            this.btnEditFile.Click += new System.EventHandler(this.btnEditFile_Click);
            // 
            // FrmSelectMode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(809, 250);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "FrmSelectMode";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Kame Management";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnDatabaseConnect;
        private System.Windows.Forms.Button btnApiConnect;
        private System.Windows.Forms.Button btnNewFile;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnEditFile;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
    }
}

