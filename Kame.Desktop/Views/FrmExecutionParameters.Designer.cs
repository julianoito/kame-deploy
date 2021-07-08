namespace Kame.Desktop.Views
{
	partial class FrmExecutionParameters
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmExecutionParameters));
			this.lblTitle = new System.Windows.Forms.Label();
			this.chkListGroups = new System.Windows.Forms.CheckedListBox();
			this.BtnClose = new System.Windows.Forms.Button();
			this.btnIniciarProcessamento = new System.Windows.Forms.Button();
			this.ParametersPanel = new System.Windows.Forms.Panel();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// lblTitle
			// 
			this.lblTitle.BackColor = System.Drawing.Color.DarkGreen;
			this.lblTitle.Font = new System.Drawing.Font("Calibri", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblTitle.ForeColor = System.Drawing.Color.White;
			this.lblTitle.Location = new System.Drawing.Point(-1, -2);
			this.lblTitle.Name = "lblTitle";
			this.lblTitle.Size = new System.Drawing.Size(480, 29);
			this.lblTitle.TabIndex = 27;
			this.lblTitle.Text = "Kame Deploy Manager";
			this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// chkListGroups
			// 
			this.chkListGroups.CheckOnClick = true;
			this.chkListGroups.FormattingEnabled = true;
			this.chkListGroups.Location = new System.Drawing.Point(12, 50);
			this.chkListGroups.Name = "chkListGroups";
			this.chkListGroups.Size = new System.Drawing.Size(456, 94);
			this.chkListGroups.TabIndex = 28;
			// 
			// BtnClose
			// 
			this.BtnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
			this.BtnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.BtnClose.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.BtnClose.ForeColor = System.Drawing.Color.White;
			this.BtnClose.Location = new System.Drawing.Point(293, 332);
			this.BtnClose.Name = "BtnClose";
			this.BtnClose.Size = new System.Drawing.Size(175, 50);
			this.BtnClose.TabIndex = 29;
			this.BtnClose.Text = "Cancelar";
			this.BtnClose.UseVisualStyleBackColor = false;
			this.BtnClose.Click += new System.EventHandler(this.BtnClose_Click);
			// 
			// btnIniciarProcessamento
			// 
			this.btnIniciarProcessamento.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
			this.btnIniciarProcessamento.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnIniciarProcessamento.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnIniciarProcessamento.ForeColor = System.Drawing.Color.White;
			this.btnIniciarProcessamento.Location = new System.Drawing.Point(12, 332);
			this.btnIniciarProcessamento.Name = "btnIniciarProcessamento";
			this.btnIniciarProcessamento.Size = new System.Drawing.Size(275, 50);
			this.btnIniciarProcessamento.TabIndex = 30;
			this.btnIniciarProcessamento.Text = "Iniciar Procesamento";
			this.btnIniciarProcessamento.UseVisualStyleBackColor = false;
			this.btnIniciarProcessamento.Click += new System.EventHandler(this.BtnIniciarProcessamento_Click);
			// 
			// ParametersPanel
			// 
			this.ParametersPanel.AutoScroll = true;
			this.ParametersPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.ParametersPanel.Location = new System.Drawing.Point(12, 183);
			this.ParametersPanel.Name = "ParametersPanel";
			this.ParametersPanel.Size = new System.Drawing.Size(456, 143);
			this.ParametersPanel.TabIndex = 31;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.ForeColor = System.Drawing.Color.White;
			this.label1.Location = new System.Drawing.Point(12, 27);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(186, 15);
			this.label1.TabIndex = 32;
			this.label1.Text = "Passos a serem executados";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.ForeColor = System.Drawing.Color.White;
			this.label2.Location = new System.Drawing.Point(12, 165);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(81, 15);
			this.label2.TabIndex = 33;
			this.label2.Text = "Parâmetros";
			// 
			// FrmExecutionParameters
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.DarkSlateGray;
			this.ClientSize = new System.Drawing.Size(480, 394);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.ParametersPanel);
			this.Controls.Add(this.btnIniciarProcessamento);
			this.Controls.Add(this.BtnClose);
			this.Controls.Add(this.chkListGroups);
			this.Controls.Add(this.lblTitle);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "FrmExecutionParameters";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "FrmExecutionParameters";
			this.Load += new System.EventHandler(this.FrmExecutionParameters_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label lblTitle;
		private System.Windows.Forms.CheckedListBox chkListGroups;
		private System.Windows.Forms.Button BtnClose;
		private System.Windows.Forms.Button btnIniciarProcessamento;
		private System.Windows.Forms.Panel ParametersPanel;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
	}
}