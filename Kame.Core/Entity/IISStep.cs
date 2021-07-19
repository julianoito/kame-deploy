using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using System.DirectoryServices;

using Kame.Core.Entity.Log;

namespace Kame.Core.Entity
{
    public class IISStep : IStepProcessor
    {
        private List<IISApplication> AppList = null;
		private List<IISApplicationSite> SiteList = null;
        private string IISSite = string.Empty;


        public override void Execute(Step step, List<DeployFile> ignoreList, IProjectExecutionLog executionLog, DeployLog log, out string errorMessage)
        {
            errorMessage = string.Empty;
			List<IISExistingSite> exitingSiteList;
			this.LoadIISParameters(step, out exitingSiteList);

            if (this.AppList != null)
            { 
                foreach(IISApplication application in AppList)
                {
                    //Create App Pool
					CreateApplicationPool(application.ApplicationPool, application.FrameWorkVersion, executionLog, ref errorMessage);

                    //Create IIS Application
                    try
                    {
						string applicatinonPath;

						if (application.ApplicationPath.StartsWith(".\\"))
						{
							applicatinonPath = this.workspace;
							if (applicatinonPath[applicatinonPath.Length - 1] != '\\')
							{
								applicatinonPath += "\\";
							}
							applicatinonPath += application.ApplicationPath.Substring(2);
						}
						else
						{
							applicatinonPath = application.ApplicationPath;
						}

						executionLog.SetMessage("IIS: Criando aplicação " + application.ApplicationName, string.Empty);

                        System.Diagnostics.Process p = new System.Diagnostics.Process();
                        p.StartInfo.FileName = @"C:\Windows\System32\inetsrv\appcmd.exe";
						p.StartInfo.Arguments = "add app /site.name:\"" + IISSite + "\" /path:/" + application.ApplicationName + " /physicalpath:\"" + applicatinonPath + "\"";
                        p.StartInfo.Verb = "runas";
                        p.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                        p.StartInfo.RedirectStandardOutput = true;
                        p.StartInfo.UseShellExecute = false;
                        p.Start();
                        while (!p.StandardOutput.EndOfStream)
                        {
                            executionLog.SetMessage(p.StandardOutput.ReadLine(), string.Empty);
                        }
                        p.WaitForExit();
                    }
                    catch (Exception ex)
                    {
                        errorMessage += ex.Message;
                    }

                    //Assign applicatio with App Pool
                    try
                    {

						executionLog.SetMessage("IIS: vinculando pool " + application.ApplicationPool  + " a aplicação " + application.ApplicationName, string.Empty);
                        System.Diagnostics.Process p = new System.Diagnostics.Process();
                        p.StartInfo.FileName = @"C:\Windows\System32\inetsrv\appcmd.exe";
                        p.StartInfo.Arguments = "set app \"" + IISSite + "/" + application.ApplicationName + "\" /applicationpool:" + application.ApplicationPool;
                        p.StartInfo.Verb = "runas";
                        p.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                        p.StartInfo.RedirectStandardOutput = true;
                        p.StartInfo.UseShellExecute = false;
                        p.Start();
                        while (!p.StandardOutput.EndOfStream)
                        {
                            executionLog.SetMessage(p.StandardOutput.ReadLine(), string.Empty);
                        }
                        p.WaitForExit();
                        
                    }
                    catch (Exception ex)
                    {
                        errorMessage += ex.Message;
                    }
                }


				int lastSiteID = 0;
				foreach (IISExistingSite site in exitingSiteList)
				{
					if (site.ID > lastSiteID)
					{
						lastSiteID = site.ID;
					}
				}
				foreach (IISApplicationSite site in SiteList)
				{

					if (!SiteExists(site.SiteName, exitingSiteList))
					{
						lastSiteID++;
						CreateSiteApplication(site, lastSiteID, executionLog, ref errorMessage);
					}
					
				}
            }
        }

		private bool SiteExists(string siteName, List<IISExistingSite> siteList)
		{
			foreach (IISExistingSite site in siteList)
			{
				if (site.Name == siteName)
				{
					return true;
				}
			}

			return false;
		}

		private void CreateSiteApplication(IISApplicationSite site, int siteID, IProjectExecutionLog executionLog, ref string errorMessage)
		{
			CreateApplicationPool(site.ApplicationPool, site.FrameWorkVersion	, executionLog, ref errorMessage);

			//Create IIS Site for Application

			try
			{
				string sitePath;

				if (site.SitePath.StartsWith(".\\"))
				{
					sitePath = this.workspace;
					if (sitePath[sitePath.Length - 1] != '\\')
					{
						sitePath += "\\";
					}
					sitePath += site.SitePath.Substring(2);
				}
				else
				{
					sitePath = site.SitePath;
				}

				executionLog.SetMessage("IIS: Criando Site/aplicação " + site.SiteName, string.Empty);

				System.Diagnostics.Process p = new System.Diagnostics.Process();
				p.StartInfo.FileName = @"C:\Windows\System32\inetsrv\appcmd.exe";
				p.StartInfo.Arguments = "add site /name:\"" + site.SiteName + "\" /id:" + siteID + " /physicalpath:\"" + sitePath + "\" /bindings:\"" + site.Bind + "\"";
				p.StartInfo.Verb = "runas";
				p.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
				p.StartInfo.RedirectStandardOutput = true;
				p.StartInfo.UseShellExecute = false;
				p.Start();
				while (!p.StandardOutput.EndOfStream)
				{
					executionLog.SetMessage(p.StandardOutput.ReadLine(), string.Empty);
				}
				p.WaitForExit();

				p = new System.Diagnostics.Process();
				p.StartInfo.FileName = @"C:\Windows\System32\inetsrv\appcmd.exe";
				p.StartInfo.Arguments = "set site /site.name:\"" + site.SiteName + "\" /[path='/'].applicationPool:\"" + site.ApplicationPool + "\" ";
				p.StartInfo.Verb = "runas";
				p.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
				p.StartInfo.RedirectStandardOutput = true;
				p.StartInfo.UseShellExecute = false;
				p.Start();
				while (!p.StandardOutput.EndOfStream)
				{
					executionLog.SetMessage(p.StandardOutput.ReadLine(), string.Empty);
				}
				p.WaitForExit();
			}
			catch (Exception ex)
			{
				errorMessage += ex.Message;
			}
		}

		private void CreateApplicationPool(string applicationPool, string frameworkVersion, IProjectExecutionLog executionLog, ref string errorMessage)
		{
			try
			{
				executionLog.SetMessage("IIS: Criando pool de aplicação " + applicationPool, string.Empty);

				System.Diagnostics.Process p = new System.Diagnostics.Process();
				p.StartInfo.FileName = @"C:\Windows\System32\inetsrv\appcmd.exe";
				p.StartInfo.Arguments = "add apppool /name:" + applicationPool;
				p.StartInfo.Verb = "runas";
				p.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
				p.StartInfo.RedirectStandardOutput = true;
				p.StartInfo.UseShellExecute = false;
				p.Start();
				while (!p.StandardOutput.EndOfStream)
				{
					executionLog.SetMessage(p.StandardOutput.ReadLine(), string.Empty);
				}
				p.WaitForExit();
			}
			catch (Exception ex)
			{
				errorMessage += ex.Message;
			}

			if (!string.IsNullOrEmpty(frameworkVersion))
			{
				try
				{
					executionLog.SetMessage("IIS: Criando pool de aplicação " + applicationPool, string.Empty);

					System.Diagnostics.Process p = new System.Diagnostics.Process();
					p.StartInfo.FileName = @"C:\Windows\System32\inetsrv\appcmd.exe";
					p.StartInfo.Arguments = "set apppool /apppool.name:" + applicationPool + " /managedRuntimeVersion:" + frameworkVersion;
					p.StartInfo.Verb = "runas";
					p.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
					p.StartInfo.RedirectStandardOutput = true;
					p.StartInfo.UseShellExecute = false;
					p.Start();
					while (!p.StandardOutput.EndOfStream)
					{
						executionLog.SetMessage(p.StandardOutput.ReadLine(), string.Empty);
					}
					p.WaitForExit();
				}
				catch (Exception ex)
				{
					errorMessage += ex.Message;
				}
			}
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
                    "IISSite"
                    , string.Empty
                    , "Site do IIS")
            );

            parameters.Add(
                StepParameter.NewStepParameter(
                    "IISApplications"
                    , string.Empty
                    , "Lista de aplicações do IIS por\";\" com o formato {apppool}|{caminho]|{aplicação}")
            );

            return parameters;
        }

        private void LoadIISParameters(Step step, out List<IISExistingSite> exitingSiteList)
        {
            base.LoadParameters(step);
            AppList = new List<IISApplication>();
			SiteList = new List<IISApplicationSite>();
            Parameter parameter;
			exitingSiteList = new List<IISExistingSite>();

            try
            {
                parameter = step.GetParameter("IISSite");
                IISSite = parameter.ParameterValue;

				List<string> siteNameList = new List<string>();
				System.Diagnostics.Process p = new System.Diagnostics.Process();
				p.StartInfo.FileName = @"C:\Windows\System32\inetsrv\appcmd.exe";
				p.StartInfo.Arguments = "list site";
				p.StartInfo.Verb = "runas";
				p.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
				p.StartInfo.RedirectStandardOutput = true;
				p.StartInfo.UseShellExecute = false;
				p.Start();
				while (!p.StandardOutput.EndOfStream)
				{
					string siteInfoAux = p.StandardOutput.ReadLine();
					exitingSiteList.Add(new IISExistingSite(siteInfoAux));
					siteInfoAux = siteInfoAux.Substring(6);
					siteInfoAux = siteInfoAux.Substring(0, siteInfoAux.IndexOf("\""));
					siteNameList.Add(siteInfoAux);
				}
				p.WaitForExit();

				bool siteExists = false;
				foreach (IISExistingSite site in exitingSiteList)
				{
					if (site.Name == IISSite)
					{
						siteExists = true;
						break;
					}
				}

				if (!siteExists && siteNameList.Count>0)
				{
					IISSite = siteNameList[0];
				}
            }
            catch{}
            
            try
            {
                parameter = step.GetParameter("IISApplications");

                string[] aplications = parameter.ParameterValue.Split(';');
                foreach (string app in aplications)
                {
                    string queuePath = string.Empty, queueLabel = string.Empty;

                    if (app.Trim() != string.Empty && app.Contains('|'))
                    {
                        string[] appData = app.Split('|');
						if (appData.Length >= 3)
                        {
                            IISApplication application = new IISApplication() { ApplicationName = appData[2].Trim(), ApplicationPath = appData[1].Trim(), ApplicationPool = appData[0].Trim() };

							if (appData.Length == 4)
							{
								application.FrameWorkVersion = appData[3].Trim();
							}

                            this.AppList.Add(application);
                        }
                    }
                }
            }
            catch
            {

            }

			try
			{
				parameter = step.GetParameter("IISSiteList");

				string[] sites = parameter.ParameterValue.Split(';');
				foreach (string app in sites)
				{
					string queuePath = string.Empty, queueLabel = string.Empty;

					if (app.Trim() != string.Empty && app.Contains('|'))
					{
						string[] siteData = app.Split('|');
						if (siteData.Length >= 4)
						{
							IISApplicationSite site = new IISApplicationSite() { 
															SiteName = siteData[2].Trim(), 
															SitePath = siteData[1].Trim(), 
															ApplicationPool = siteData[0].Trim(), 
															Bind = siteData[3].Trim() 
														};
							if (siteData.Length == 5)
							{
								site.FrameWorkVersion = siteData[4].Trim();
							}
							this.SiteList.Add(site);
						}
					}
				}
			}
			catch
			{

			}


        }

        private class IISApplication 
        {
            public string ApplicationPool { get; set; }
            public string ApplicationName { get; set; }
            public string ApplicationPath { get; set; }
			public string FrameWorkVersion { get; set; }
        }

		private class IISApplicationSite
		{
			public string ApplicationPool { get; set; }
			public string SiteName { get; set; }
			public string SitePath { get; set; }
			public string Bind { get; set; }
			public string FrameWorkVersion { get; set; }
		}

		private class IISExistingSite
		{
			public int ID;
			public string Name;

			public IISExistingSite(string data)
			{
				string siteInfoAux = data;
				siteInfoAux = siteInfoAux.Substring(6);
				this.Name = siteInfoAux.Substring(0, siteInfoAux.IndexOf("\""));

				try
				{
					siteInfoAux = data.Substring(data.IndexOf(":") + 1);
					ID = int.Parse(siteInfoAux.Substring(0, siteInfoAux.IndexOf(",")));
				}
				catch { }
			}
		}
    }
}
