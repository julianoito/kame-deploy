using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kame.Core.Entity.Log;

namespace Kame.Core.Entity
{
    public class PromptComand : IStepProcessor
    {
        
        public override void Execute(Step step, List<DeployFile> ignoreList, IProjectExecutionLog executionLog, DeployLog log, out string errorMessage)
        {
            errorMessage = string.Empty;
            string command = string.Empty, arguments = string.Empty, startDirectory = string.Empty;
			string[] argumentList = null;
			bool executeWithoutParameters = false;

            #region Load parameters

            base.LoadParameters(step);
            Parameter parameter;
            try
            {
                parameter = step.GetParameter("promptCommand");
                command = parameter.ParameterValue;
            }
            catch { }

            try
            {
                parameter = step.GetParameter("arguments");
                arguments = parameter.ParameterValue;
            }
            catch { }

			try
			{
				parameter = step.GetParameter("argumentList");
				argumentList = parameter.ParameterValue.Split(';');
			}
			catch { }

            try
            {
                parameter = step.GetParameter("startDirectory");
                startDirectory = parameter.ParameterValue;
            }
            catch { }

			try
            {
				parameter = step.GetParameter("executeWithoutParameters");
				executeWithoutParameters = bool.Parse(parameter.ParameterValue);
            }
            catch { }
			

            #endregion

			if (executeWithoutParameters)
			{
				System.Diagnostics.Process.Start(command);
			}
			else
			{
				if (!string.IsNullOrEmpty(arguments) || (string.IsNullOrEmpty(arguments) && argumentList == null))
				{
					this.ExecutePrompt(command, arguments, startDirectory, out errorMessage);
				}

				if (argumentList != null)
				{
					foreach (string argument in argumentList)
					{
						if (!string.IsNullOrEmpty(argument) && argument.Trim() != string.Empty)
						{
							this.ExecutePrompt(command, argument.Trim(), startDirectory, out errorMessage);
						}
					}
				}
			}
        }
		private void ExecutePrompt(string command, string arguments, string startDirectory, out string errorMessage)
		{
			errorMessage = string.Empty; 

			System.Diagnostics.Process p = new System.Diagnostics.Process();
			p.StartInfo.FileName = command;
			p.StartInfo.UseShellExecute = false;
			//p.StartInfo.RedirectStandardOutput = true;s

			p.StartInfo.CreateNoWindow = false;

			if (!string.IsNullOrEmpty(startDirectory))
			{
				p.StartInfo.WorkingDirectory = startDirectory;
			}
			else
			{
				p.StartInfo.WorkingDirectory = this.workspace;
			}
			p.StartInfo.Arguments = arguments;
			p.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;

			try
			{
				p.Start();

				p.WaitForExit();
			}
			catch (Exception ex)
			{
				errorMessage = ex.Message;
				throw;
			}
		}

        public override List<DeployFile> CheckExecution(Step step, DeployLog log)
        {
            return null;
        }
        public override List<StepParameter> GetRequiredParameters()
        {
            return null;
        }
    }
}
