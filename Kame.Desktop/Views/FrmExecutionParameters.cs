using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Kame.Desktop.Entity;

namespace Kame.Desktop.Views
{
	public partial class FrmExecutionParameters : Form
	{
		public List<string> ExecutionGroups { get; set; }
		public List<string> SelectetExecutionsGroups { get; set; }
		public List<string> RequiredParameters { get; set; }
		public Hashtable ValuedParameters { get; set; }
		public bool ExecutionCanceled { get; set; }

		private List<TextBox> parameterFields = null;

		public FrmExecutionParameters()
		{
			InitializeComponent();
		}

		private void FrmExecutionParameters_Load(object sender, EventArgs e)
		{
			UserConfig userConfig = UserConfig.LoadLocalConfig();

			chkListGroups.Items.Clear();
			foreach (string executionGroup in this.ExecutionGroups)
			{
				chkListGroups.Items.Add(executionGroup, true);
			}

			parameterFields = new List<TextBox>();
			for (int i = 0; i < this.RequiredParameters.Count; i++ )
			{
				Label lblParameter = new Label();
				lblParameter.ForeColor = Color.White;
				lblParameter.Text = RequiredParameters[i].Trim();
				parameterFields.Add(new TextBox());
				ParametersPanel.Controls.Add(parameterFields[i]);
				ParametersPanel.Controls.Add(lblParameter);

				lblParameter.SetBounds(5, 5 + (30 * i), 140, 25);
				parameterFields[i].SetBounds(150, 5 + (30 * i), 150, 25);

				if (userConfig.ExecutionParameters.ContainsKey(RequiredParameters[i].Trim()))
				{
					parameterFields[i].Text = userConfig.ExecutionParameters[RequiredParameters[i].Trim()];
				}
			}
		}

		private void BtnClose_Click(object sender, EventArgs e)
		{
			this.ExecutionCanceled = true;
			this.Close();
		}

		private void BtnIniciarProcessamento_Click(object sender, EventArgs e)
		{
			UserConfig userConfig = UserConfig.LoadLocalConfig();
			this.SelectetExecutionsGroups = new List<string>();

			foreach (string executionGroup in chkListGroups.CheckedItems)
			{
				SelectetExecutionsGroups.Add(executionGroup);
			}

			this.ValuedParameters = new Hashtable();
			userConfig.ExecutionParameters.Clear();
			for (int i = 0; i < this.RequiredParameters.Count; i++ )
			{
				if (!string.IsNullOrEmpty(RequiredParameters[i].Trim()))
				{
					this.ValuedParameters.Add(RequiredParameters[i].Trim(), parameterFields[i].Text);

					if (!userConfig.ExecutionParameters.ContainsKey(RequiredParameters[i].Trim()))
					{
						userConfig.ExecutionParameters.Add(RequiredParameters[i].Trim(), parameterFields[i].Text);
					}
				}
			}
			userConfig.Save();

			this.ExecutionCanceled = false;
			this.Close();
		}
	}
}
