using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Kame.Core.Entity;

namespace Kame.Management.Desktop
{
    public class ucParameter
    {
        public Panel MainPanel { get; }
        private TextBox _txtParameterName;
        private TextBox _txtParameterValue;
        private Button _btnDeleteParameter;
        private StepParameter _stepParameter;

        public ucParameter(StepParameter stepParameter)
        {
            this._stepParameter = stepParameter;

            MainPanel = new Panel();
            MainPanel.BackColor = Color.LightGray;

            Label lblParName = new Label();
            lblParName.Text = "Nome:";
            MainPanel.Controls.Add(lblParName);
            lblParName.SetBounds(3, 5, 55, 25);

            Label lblParValor = new Label();
            lblParValor.Text = "Valor:";
            MainPanel.Controls.Add(lblParValor);
            lblParValor.SetBounds(3, 35, 55, 25); 

            _txtParameterName = new TextBox();
            _txtParameterValue = new TextBox();

            this.MainPanel.Controls.Add(_txtParameterName);
            _txtParameterName.SetBounds(61, 3, 800, 25);

            this.MainPanel.Controls.Add(_txtParameterValue);
            _txtParameterValue.SetBounds(61, 31, 870, 100);
            _txtParameterValue.Multiline = true;
            _txtParameterValue.ScrollBars = ScrollBars.Both;

            _btnDeleteParameter = new Button();
            _btnDeleteParameter.Text = "Excluir Parametro";
            this.MainPanel.Controls.Add(_btnDeleteParameter);
            _btnDeleteParameter.SetBounds(870,2,60,25);
            _btnDeleteParameter.BackColor = Color.LightGray;

            _txtParameterName.Text = this._stepParameter.ParameterKey;

            string parameterValue = this._stepParameter.ParameterValue;
            if (!string.IsNullOrEmpty(parameterValue) && parameterValue.Contains('\n') && !parameterValue.Contains('\r'))
            {
                parameterValue = parameterValue.Replace("\n", "\r\n");
            }
            _txtParameterValue.Text = parameterValue;

        }

        public int Index
        {
            set { this._btnDeleteParameter.Tag = value.ToString(); }
        }

        public string ParameterKey
        {
            get { return this._txtParameterName.Text; }
        }
        public string ParameterValue
        {
            get { return this._txtParameterValue.Text; }
        }

        public EventHandler RemEvent_Client
        {
            set { this._btnDeleteParameter.Click += value; }
        }
    }
}
