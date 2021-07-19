using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using Kame.Core.Entity.Log;

namespace Kame.Core.Entity
{
    public class CVS : IStepProcessor
    {
        private string operation;
        private string user;
        private string server;
        private string branch;
        private List<CVSFolder> folders;
        private IProjectExecutionLog ExecutionLog;

        public override void Execute(Step step, List<DeployFile> ignoreList, IProjectExecutionLog executionLog, DeployLog log, out string errorMessage)
        {
            errorMessage = string.Empty;
            this.LoadParameters(step);
            this.ExecutionLog = executionLog;

            switch (operation)
            {
                case "CheckouOrUpdate":
                    this.Checkout();
                    break;
            }
        }

        public override List<DeployFile> CheckExecution(Step step, DeployLog log)
        {
            return new List<DeployFile>();
        }

        public override List<StepParameter> GetRequiredParameters()
        {
            List<StepParameter> parameters = new List<StepParameter>();
            
            return parameters;
        }

        protected virtual void LoadParameters(Step step)
        {
            base.LoadParameters(step);
            Parameter parameter;

            try{
				parameter = step.GetParameter("CVSUser");
                this.user = parameter.ParameterValue;
            }
            catch{}

            try{
                parameter = step.GetParameter("server");
                this.server = parameter.ParameterValue;
            }
            catch{}

            try{
                parameter = step.GetParameter("branch");
                this.branch = parameter.ParameterValue;
            }
            catch{}
            
            try{
                parameter = step.GetParameter("operation");
                this.operation = parameter.ParameterValue;
            }
            catch{}

            this.folders = new List<CVSFolder>();
            try
            {
                parameter = step.GetParameter("modules");
                string[] modules = parameter.ParameterValue.Split(';');
                foreach (string module in modules)
                {
                    if (module.Trim() != string.Empty && module.Contains('|'))
                    {
                        string[] moduleData = module.Split('|');
                        CVSFolder folder = new CVSFolder() { LocalFolder = moduleData[1].Trim(), RemoteFolder = moduleData[0].Trim() };

                        if (!string.IsNullOrEmpty(folder.LocalFolder) && !string.IsNullOrEmpty(folder.RemoteFolder))
                        {
                            this.folders.Add(folder);
                        }
                    }
                }
            }
            catch{}
            Parameter parametroWorkspace = step.GetParameter("workspace");
        }

        private void Checkout()
        { 
            string CVSROOT = ":sserver:" + this.user + "@" + this.server;
            string logOperation;

            string cvsScriptFile = AppDomain.CurrentDomain.BaseDirectory;
            if (cvsScriptFile[cvsScriptFile.Length - 1] != '\\')
            {
                cvsScriptFile += "\\";
            }

            if (this.folders!=null)
            {
                foreach (CVSFolder cvsFolder in this.folders)
                {
                    string localDirectory = this.workspace, localCVSDirectory;
                    if (localDirectory[localDirectory.Length-1]!='\\')
                    {
                        localDirectory += "\\";
                    }
                    localDirectory += cvsFolder.LocalFolder;
                    localCVSDirectory = localDirectory;
                    if (localDirectory[localDirectory.Length - 1] != '\\')
                    {
                        localDirectory += "\\";
                    }

                    System.Diagnostics.Process p = new System.Diagnostics.Process();
                    p.StartInfo.FileName = "cvs.exe";
                    p.StartInfo.UseShellExecute = false;
                    p.StartInfo.EnvironmentVariables.Add("CVSROOT", CVSROOT);
                    p.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Minimized;
                    p.StartInfo.RedirectStandardOutput = false;

                    if (Directory.Exists(localDirectory) && Directory.Exists(localDirectory + "CVS"))
                    {
                        p.StartInfo.WorkingDirectory = localDirectory;

						if (!string.IsNullOrEmpty(branch))
						{
							p.StartInfo.Arguments = "-Q update -P -r " + branch + " -d -A";
						}
						else
						{
							p.StartInfo.Arguments = "-Q update -P -d -A";
						}
                        logOperation = "CVS UPDATE: ";
                    }
                    else
                    {
						if (!string.IsNullOrEmpty(branch))
						{
							p.StartInfo.Arguments = "-Q checkout -P -r " + branch + " -d " + localDirectory + " " + cvsFolder.RemoteFolder;
						}
						else
						{
							p.StartInfo.Arguments = "-Q checkout -P -d " + localDirectory + " " + cvsFolder.RemoteFolder;
						}
                        logOperation = "CVS CHECKOUT: ";
                    }

					ExecutionLog.SetMessage(logOperation + "Starting module " + cvsFolder.RemoteFolder, string.Empty);
                    p.Start();

					/*
                    while (!p.StandardOutput.EndOfStream)
                    {
                        ExecutionLog.SetMessage(logOperation + p.StandardOutput.ReadLine(), string.Empty);
                    }
					*/
                    p.WaitForExit();
					ExecutionLog.SetMessage(logOperation + "Done", string.Empty);
                }
            }

        }

        private class CVSFolder
        {
            public string RemoteFolder {get;set;}
            public string LocalFolder {get;set;}
        }
    }
}
