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
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.txtExecutionGroup = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtName
            // 
            this.txtName.BackColor = System.Drawing.Color.Gainsboro;
            this.txtName.Location = new System.Drawing.Point(179, 44);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(630, 23);
            this.txtName.TabIndex = 0;
            // 
            // lnlName
            // 
            this.lnlName.AutoSize = true;
            this.lnlName.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lnlName.ForeColor = System.Drawing.Color.White;
            this.lnlName.Location = new System.Drawing.Point(12, 47);
            this.lnlName.Name = "lnlName";
            this.lnlName.Size = new System.Drawing.Size(45, 17);
            this.lnlName.TabIndex = 1;
            this.lnlName.Text = "Nome";
            // 
            // lblProcessClass
            // 
            this.lblProcessClass.AutoSize = true;
            this.lblProcessClass.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblProcessClass.ForeColor = System.Drawing.Color.White;
            this.lblProcessClass.Location = new System.Drawing.Point(12, 76);
            this.lblProcessClass.Name = "lblProcessClass";
            this.lblProcessClass.Size = new System.Drawing.Size(162, 17);
            this.lblProcessClass.TabIndex = 1;
            this.lblProcessClass.Text = "Classe de processamento";
            // 
            // cmbProcessorClass
            // 
            this.cmbProcessorClass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProcessorClass.FormattingEnabled = true;
            this.cmbProcessorClass.Location = new System.Drawing.Point(179, 73);
            this.cmbProcessorClass.Name = "cmbProcessorClass";
            this.cmbProcessorClass.Size = new System.Drawing.Size(630, 23);
            this.cmbProcessorClass.TabIndex = 1;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.DimGray;
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(916, 591);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(83, 35);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Cancelar";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.DimGray;
            this.btnSave.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(827, 591);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(83, 35);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "OK";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnAddParameter
            // 
            this.btnAddParameter.BackColor = System.Drawing.Color.DimGray;
            this.btnAddParameter.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.btnAddParameter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddParameter.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnAddParameter.ForeColor = System.Drawing.Color.White;
            this.btnAddParameter.Location = new System.Drawing.Point(12, 146);
            this.btnAddParameter.Name = "btnAddParameter";
            this.btnAddParameter.Size = new System.Drawing.Size(202, 35);
            this.btnAddParameter.TabIndex = 3;
            this.btnAddParameter.Text = "Adiciconar Parametro";
            this.btnAddParameter.UseVisualStyleBackColor = false;
            this.btnAddParameter.Click += new System.EventHandler(this.btnAddParameter_Click);
            // 
            // pnlParameters
            // 
            this.pnlParameters.AutoScroll = true;
            this.pnlParameters.BackColor = System.Drawing.Color.Gainsboro;
            this.pnlParameters.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlParameters.Location = new System.Drawing.Point(12, 193);
            this.pnlParameters.Name = "pnlParameters";
            this.pnlParameters.Size = new System.Drawing.Size(990, 383);
            this.pnlParameters.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(989, 25);
            this.label1.TabIndex = 3;
            this.label1.Text = "Kame Management  - Step Details ";
            this.label1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblTitle_MouseDown);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.button1.ForeColor = System.Drawing.Color.Black;
            this.button1.Location = new System.Drawing.Point(985, -1);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(31, 26);
            this.button1.TabIndex = 5;
            this.button1.TabStop = false;
            this.button1.Text = "X";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // txtExecutionGroup
            // 
            this.txtExecutionGroup.BackColor = System.Drawing.Color.Gainsboro;
            this.txtExecutionGroup.Location = new System.Drawing.Point(179, 102);
            this.txtExecutionGroup.Name = "txtExecutionGroup";
            this.txtExecutionGroup.Size = new System.Drawing.Size(630, 23);
            this.txtExecutionGroup.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(12, 102);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(125, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Grupo de execução";
            // 
            // FrmStep
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.ClientSize = new System.Drawing.Size(1014, 644);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtExecutionGroup);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pnlParameters);
            this.Controls.Add(this.btnAddParameter);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.cmbProcessorClass);
            this.Controls.Add(this.lblProcessClass);
            this.Controls.Add(this.lnlName);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmStep";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Detalhes do Step";
            this.Load += new System.EventHandler(this.FrmStep_Load);
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
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtExecutionGroup;
        private System.Windows.Forms.Label label2;
    }
}