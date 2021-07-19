using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Kame.Core.Entity;

namespace Kame.Management.Desktop
{
    public partial class FrmStep : Form
    {
        public Step Step { get; set; }
        public bool SaveStep { get; set; }
        private int _verticalParapeterPosition;
        private List<ucParameter> _parameters;

        private void ShowStepData()
        {
            this.SaveStep = false;
            this.txtName.Text = this.Step.Name;

            this.cmbProcessorClass.SelectedIndex = 0;
            if (!string.IsNullOrEmpty(this.Step.ProcessClass))
            {
                for (int i = 0; i < Config.ProcessoClassList.Count; i++)
                {
                    if (this.Step.ProcessClass == Config.ProcessoClassList[i].FullClassName)
                    {
                        this.cmbProcessorClass.SelectedIndex = i+1;
                        break;
                    }
                    
                }
            }

            _parameters = new List<ucParameter>();

            if (this.Step.Parameters != null)
            {
                foreach (StepParameter parameter in this.Step.Parameters)
                {
                    _parameters.Add(
                        new ucParameter(
                            new StepParameter()
                            {
                                ParameterKey = parameter.ParameterKey,
                                ParameterValue = parameter.ParameterValue
                            }
                        )
                    );
                    _parameters[_parameters.Count - 1].RemEvent_Client = btnRemParameter_Click;
                }
            }

            ListParameters();
        }

        private void ListParameters()
        {
            this._verticalParapeterPosition = 1;
            this.pnlParameters.Controls.Clear();
            if (this._parameters != null)
            {
                for (int i=0; i<this._parameters.Count; i++)
                {
                    ListParameter(i);
                }
            }
        }

        private void ListParameter(int index)
        {
            ucParameter ucParameter = this._parameters[index];
            this.pnlParameters.Controls.Add(ucParameter.MainPanel);
            ucParameter.MainPanel.SetBounds(1, _verticalParapeterPosition, 970, 140);
            ucParameter.Index = index;

            _verticalParapeterPosition += 144;
        }

        public FrmStep()
        {
            InitializeComponent();
            List<string> processorClassList = Config.GetConstant(Config.ConstantTypes.ProcessorClasses);

            this.cmbProcessorClass.Items.Add("Nenhum");
            for (int i=0; i< Config.ProcessoClassList.Count; i++)
            {
                this.cmbProcessorClass.Items.Add(Config.ProcessoClassList[i].Name);
            }
            this.cmbProcessorClass.SelectedIndex = 0;
        }

        private void FrmStep_Shown(object sender, EventArgs e)
        {
            ShowStepData();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (this.txtName.Text.Trim() == "")
            {
                MessageBox.Show("O nome do step não foi preenchido", "Erro na validação do Step", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            List<StepParameter> parameterList = new List<StepParameter>();
            foreach (ucParameter ucParameter in this._parameters)
            {
                if (string.IsNullOrEmpty(ucParameter.ParameterKey.Trim()))
                {
                    MessageBox.Show("Existe um parâmetro configurado sem nomne definido", "Erro na validação do Step", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                parameterList.Add(
                    new StepParameter()
                    {
                        ParameterKey = ucParameter.ParameterKey,
                        ParameterValue = ucParameter.ParameterValue
                    }
                );
            }

            this.SaveStep = true;
            this.Step.Name = this.txtName.Text;
            if (cmbProcessorClass.SelectedIndex == 0)
            {
                this.Step.ProcessClass = string.Empty;
            }
            else
            {
                this.Step.ProcessClass = Config.ProcessoClassList[cmbProcessorClass.SelectedIndex - 1].FullClassName;
            }
            this.Step.Parameters = parameterList;

            this.Hide();
        }

        private void btnAddParameter_Click(object sender, EventArgs e)
        {
            _parameters.Add(new ucParameter(new StepParameter()));
            pnlParameters.VerticalScroll.Value = 0;
            ListParameter(_parameters.Count - 1);

            pnlParameters.VerticalScroll.Value = pnlParameters.VerticalScroll.Maximum;
        }

        private void btnRemParameter_Click(object sender, EventArgs e)
        {
            int index = int.Parse(((Control)sender).Tag.ToString());
            this._parameters.RemoveAt(index);
            ListParameters();
        }
    }
}
