using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using Kame.Core.Entity.Log;

namespace Kame.Core.Entity
{
    public class GitClonePull : IStepProcessor
    {
        private string url;
        private string branch;
        private string folderName;
        private string depth;
        private string relativeWorkspacePath;
        private IProjectExecutionLog ExecutionLog;

        public override void Execute(Step step, List<DeployFile> ignoreList, IProjectExecutionLog executionLog, DeployLog log, out string errorMessage)
        {
            errorMessage = string.Empty;
            this.LoadParameters(step);
            this.ExecutionLog = executionLog;

            this.CloneOrPull(out errorMessage);
        }

        public override List<DeployFile> CheckExecution(Step step, DeployLog log)
        {
            return new List<DeployFile>();
        }

        public override List<StepParameter> GetRequiredParameters()
        {
            List<StepParameter> parameters = new List<StepParameter>();

            parameters.Add(
                StepParameter.NewStepParameter(
                    "url"
                    , string.Empty
                    , "Repository URL")
            );

            return parameters;
        }

        protected virtual void LoadParameters(Step step)
        {
            base.LoadParameters(step);
            Parameter parameter;

            try
            {
                parameter = step.GetParameter("url");
                this.url = parameter.ParameterValue;
            }
            catch { }

            try
            {
				parameter = step.GetParameter("branch");
                this.branch = parameter.ParameterValue;
            }
            catch{}

            try{
                parameter = step.GetParameter("folderName");
                this.folderName = parameter.ParameterValue;
            }
            catch{}
            
            try
            {
                parameter = step.GetParameter("depth");
                this.depth = parameter.ParameterValue;
            }
            catch { }

            try
            {
                parameter = step.GetParameter("relativeworkspacepath");
                this.relativeWorkspacePath = parameter.ParameterValue;
            }
            catch { }

            

            Parameter parametroWorkspace = step.GetParameter("workspace");
        }

        private void CloneOrPull(out string errorMessage)
        {
            string parameters = "clone " + url;
            string localPath = this.workspace;

            if (!string.IsNullOrEmpty(this.relativeWorkspacePath))
            {
                if (!localPath.EndsWith("\\"))
                {
                    localPath += "\\";
                }

                if (this.relativeWorkspacePath.StartsWith("\\"))
                {
                    this.relativeWorkspacePath = this.relativeWorkspacePath.Substring(1);
                }
                localPath += this.relativeWorkspacePath;
            }

            if (!string.IsNullOrEmpty(branch) && !string.IsNullOrEmpty(branch.Trim()))
            {
                parameters += " --branch " + branch;
            }

            if (!string.IsNullOrEmpty(depth) && !string.IsNullOrEmpty(branch.Trim()))
            {
                parameters += " --depth " + depth;
            }

            if (!string.IsNullOrEmpty(folderName))
            {
                parameters += " " + folderName;
            }

            errorMessage = string.Empty;
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.FileName = "git";

            p.StartInfo.WorkingDirectory = localPath;
            p.StartInfo.Arguments = parameters;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = false;

            try
            {
                p.Start();

                p.WaitForExit();
            }
            catch
            {
            }

            string repositoriName = string.Empty;

            if (string.IsNullOrEmpty(folderName))
            {
                string[] urlSplit = url.Split('/');
                repositoriName = urlSplit[urlSplit.Length - 1];
                if (repositoriName.ToLower().EndsWith(".git"))
                {
                    repositoriName = repositoriName.Substring(0, repositoriName.Length - 4);
                }
            }
            else
            {
                repositoriName = folderName;
            }

            if (!localPath.EndsWith("\\"))
            {
                localPath += "\\";
            }
            localPath += repositoriName;
            if (!localPath.EndsWith("\\"))
            {
                localPath += "\\";
            }


            p.StartInfo.Arguments = "pull";
            p.StartInfo.WorkingDirectory = localPath;

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

            if (!string.IsNullOrEmpty(branch) && !string.IsNullOrEmpty(branch.Trim()))
            {
                p.StartInfo.Arguments = "checkout " + branch;

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


        }

    }
}
