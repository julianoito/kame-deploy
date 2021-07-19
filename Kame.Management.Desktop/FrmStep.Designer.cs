namespace Kame.Management.Desktop
{
    partial class FrmStep
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
            this.txtName = new System.Windows.Forms.TextBox();
            this.lnlName = new System.Windows.Forms.Label();
            this.lblProcessClass = new System.Windows.Forms.Label();
            this.cmbProcessorClass = new System.Windows.Forms.ComboBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnAddParameter = new System.Windows.Forms.Button();
            this.pnlParameters = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(158, 6);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(630, 23);
            this.txtName.TabIndex = 0;
            // 
            // lnlName
            // 
            this.lnlName.AutoSize = true;
            this.lnlName.Location = new System.Drawing.Point(12, 9);
            this.lnlName.Name = "lnlName";
            this.lnlName.Size = new System.Drawing.Size(40, 15);
            this.lnlName.TabIndex = 1;
            this.lnlName.Text = "Nome";
            // 
            // lblProcessClass
            // 
            this.lblProcessClass.AutoSize = true;
            this.lblProcessClass.Location = new System.Drawing.Point(12, 38);
            this.lblProcessClass.Name = "lblProcessClass";
            this.lblProcessClass.Size = new System.Drawing.Size(140, 15);
            this.lblProcessClass.TabIndex = 1;
            this.lblProcessClass.Text = "Classe de processamento";
            // 
            // cmbProcessorClass
            // 
            this.cmbProcessorClass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProcessorClass.FormattingEnabled = true;
            this.cmbProcessorClass.Location = new System.Drawing.Point(158, 35);
            this.cmbProcessorClass.Name = "cmbProcessorClass";
            this.cmbProcessorClass.Size = new System.Drawing.Size(630, 23);
            this.cmbProcessorClass.TabIndex = 2;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(916, 520);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(83, 35);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancelar";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(827, 520);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(83, 35);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "OK";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnAddParameter
            // 
            this.btnAddParameter.Location = new System.Drawing.Point(12, 75);
            this.btnAddParameter.Name = "btnAddParameter";
            this.btnAddParameter.Size = new System.Drawing.Size(202, 35);
            this.btnAddParameter.TabIndex = 3;
            this.btnAddParameter.Text = "Adiciconar Parametro";
            this.btnAddParameter.UseVisualStyleBackColor = true;
            this.btnAddParameter.Click += new System.EventHandler(this.btnAddParameter_Click);
            // 
            // pnlParameters
            // 
            this.pnlParameters.AutoScroll = true;
            this.pnlParameters.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlParameters.Location = new System.Drawing.Point(12, 116);
            this.pnlParameters.Name = "pnlParameters";
            this.pnlParameters.Size = new System.Drawing.Size(990, 398);
            this.pnlParameters.TabIndex = 4;
            // 
            // FrmStep
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1014, 567);
            this.Controls.Add(this.pnlParameters);
            this.Controls.Add(this.btnAddParameter);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.cmbProcessorClass);
            this.Controls.Add(this.lblProcessClass);
            this.Controls.Add(this.lnlName);
            this.Controls.Add(this.txtName);
            this.Name = "FrmStep";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Detalhes do Step";
            this.Shown += new System.EventHandler(this.FrmStep_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lnlName;
        private System.Windows.Forms.Label lblProcessClass;
        private System.Windows.Forms.ComboBox cmbProcessorClass;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnAddParameter;
        private System.Windows.Forms.Panel pnlParameters;
    }
}