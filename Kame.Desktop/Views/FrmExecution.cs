using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Kame.Desktop.Entity;
using Kame.Core.Entity;

namespace Kame.Desktop.Views
{
    public partial class FrmExecution : Form, IProjectExecutionLog
    {
        private ProjectShortcut CurrentProject = null;
		private DeployProject Project = null;
		private List<string> ExecutionGroups = null;
        private ExecutionMode ExecutionMode = null;
        private FrmLog frmLog = null;
		private FrmExecutionParameters frmExecutionParameters = null;
        private System.Threading.Timer timer = null;

        public FrmExecution()
        {
            InitializeComponent();
        }


        public void ExecuteProject(ProjectShortcut project, ExecutionMode executionMode)
        {
            this.CurrentProject = project;
            this.ExecutionMode = executionMode;

            this.lblProjectName.Text = project.Name;

            if (!String.IsNullOrEmpty(this.CurrentProject.ImagePath))
            {
                imgProjectLogo.ImageLocation = this.CurrentProject.ImagePath;
            }

            this.btnExecute.Enabled = true;
            this.btnShowLog.Visible = false;
            this.txtExecutionLog.Text = string.Empty;

			this.txtExecutionLog.Visible = false;
			this.ShowExecutiosDescription();

            this.ShowDialog();
        
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnExecute_Click(object sender, EventArgs e)
        {

			List<ProjectParameter> parametros = new List<ProjectParameter>();
			parametros.Add(ProjectParameter.NewProjectParameter("workspace", this.CurrentProject.Workspace, string.Empty, null));
			parametros.Add(ProjectParameter.NewProjectParameter("replaceLog", this.CurrentProject.LogFileToReplace, string.Empty, null));

			this.Project = DeployProject.LoadDeployProject(this.CurrentProject.Filename, parametros);

			frmExecutionParameters = new FrmExecutionParameters();
			frmExecutionParameters.ExecutionGroups = this.Project.GetExecutiosGroups();
			frmExecutionParameters.RequiredParameters = this.CurrentProject.RequiredParameters;
			frmExecutionParameters.ShowDialog();


			if (!frmExecutionParameters.ExecutionCanceled)
			{
				this.btnExecute.Enabled = false;
				this.BtnClose.Enabled = false;

				this.webBrowser.Visible = false;
				this.txtExecutionLog.Visible = true;
				this.txtExecutionLog.Width = 1000;
				this.txtExecutionLog.Height = 405;
				this.txtExecutionLog.Left = 12;
				this.txtExecutionLog.Top = 127;
				

				this.ExecutionGroups = frmExecutionParameters.SelectetExecutionsGroups;

				foreach (DictionaryEntry parameter in frmExecutionParameters.ValuedParameters)
				{
					this.Project.AddProjectParameter(parameter.Key.ToString(), parameter.Value.ToString());
				}

				System.Threading.Thread tread = new System.Threading.Thread(new System.Threading.ThreadStart(ExecuteBackgroundProcess));
				tread.Start();
			}
        }

        private void ExecuteBackgroundProcess()
        {

			if (this.Project != null)
            {
                string errorMessage;
				this.Project.Processar(this, this.ExecutionGroups, this.ExecutionMode.ExecutionCode == ExecutionMode.Restore.ExecutionCode, out errorMessage);

                if (!string.IsNullOrEmpty(errorMessage))
                {
                    this.SetMessage("Erro encontrado durante a execução:\r\n" + errorMessage, string.Empty);
                }
                else 
                {
                    this.SetMessage("Execução do projeto finalizada", string.Empty);
                }
                this.EnableButtons();
            }
            else 
            {
                this.SetMessage("Projeto não carregado", string.Empty);
            }

        }


        private delegate void SetThreadEnableButtons();
        private delegate void SetThreadMessage(string propertyName, bool fixedLine, string messagePrefix);

        public void SetMessage(string message, string messageDeatil)
        {
            this.txtExecutionLog.Invoke(new SetThreadMessage(SetMessageThread), message + (string.IsNullOrEmpty(message)? string.Empty: "\r\n") + messageDeatil, false, string.Empty);
        }

		public void SetMessageFixedLine(string message, string messagePrefix, string messageDeatil)
		{
			this.txtExecutionLog.Invoke(new SetThreadMessage(SetMessageThread), message + (string.IsNullOrEmpty(message) ? string.Empty : "\r\n") + messageDeatil, true, messagePrefix);
		}

        public void SetMessageThread(string message, bool fixedLine, string messagePrefix)
        {
			if (fixedLine && !string.IsNullOrEmpty(this.txtExecutionLog.Text))
			{
				string[] linhas = this.txtExecutionLog.Text.Replace("\r\n", "\r").Split('\r');
				if (linhas != null && linhas.Length > 0)
				{
					this.txtExecutionLog.Text = string.Empty;
					for (int i = 0; i < linhas.Length - 2; i++)
					{
						this.txtExecutionLog.Text += linhas[i] + "\r\n";
					}

					if (!linhas[linhas.Length - 2].StartsWith(messagePrefix))
					{
						this.txtExecutionLog.Text += linhas[linhas.Length - 2] + "\r\n";
					}
				}
			}

			this.txtExecutionLog.Text += message;
            this.txtExecutionLog.SelectionStart = this.txtExecutionLog.Text.Length;
            this.txtExecutionLog.ScrollToCaret();
        }

        public void EnableButtons()
        {
            this.Invoke(new SetThreadEnableButtons(EnableButtonsThread));
        }

        public void EnableButtonsThread()
        {
            this.btnShowLog.Visible = true;
			this.BtnClose.Enabled = true;
        }

        private void btnShowLog_Click(object sender, EventArgs e)
        {
            DeployProject projeto = DeployProject.LoadDeployProject(this.CurrentProject.Filename, null);
            if (this.frmLog == null)
            {
                this.frmLog = new FrmLog();
            }
            string logFile = this.CurrentProject.Workspace;
            if (logFile[logFile.Length - 1] != '\\')
            {
                logFile += "\\";
            }
            logFile += ".kame\\log_" + projeto.ProjectID + ".xml";

            this.frmLog.ShowLog(logFile);
        }

		private void ShowExecutiosDescription()
		{
			string deployInstructions = string.Empty;
			string filename = AppDomain.CurrentDomain.BaseDirectory;
			if (!filename.EndsWith("\\"))
			{
				filename += "\\";
			}
			filename += "temp.hml";

			if (File.Exists(filename))
			{
				File.Delete(filename);
			}

			deployInstructions = Properties.Resources.HtmlTemplate;
			deployInstructions = deployInstructions.Replace("{ExecutionDescription}", KameDesktopConfig.Current.CurrentProfile.ProjectExecutionDescription);
			deployInstructions = deployInstructions.Replace("{ProjectExecutionDescription}", this.CurrentProject.GetProjectDescription());

			File.WriteAllText(filename, deployInstructions);

			this.webBrowser.Visible = true;
			this.webBrowser.Width = 1000;
			this.webBrowser.Height = 405;
			this.webBrowser.Left = 12;
			this.webBrowser.Top = 127;
			this.webBrowser.Navigate("file:///" + filename.Replace("\\", "/"));
		}
    }
}
