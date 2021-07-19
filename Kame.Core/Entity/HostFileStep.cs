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
	public class HostFileStep : IStepProcessor
	{
		private List<string> HostEntries;
		private string HostStartMark;
		private string HostEndMark;

		public override void Execute(Step step, List<DeployFile> ignoreList, IProjectExecutionLog executionLog, DeployLog log, out string errorMessage)
		{
			errorMessage = string.Empty;
			this.LoadParameters(step);
			string hostFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "drivers/etc/hosts");

			List<string> conteudoHosts = new List<string>();
			List<string> conteudoHostsDev = new List<string>();
			StringBuilder outputFile = new StringBuilder();

			string conteudoOriginal = this.ReadTextFile(hostFilePath);
			string[] listaConteudoOriginal = conteudoOriginal.Split('\n');

			bool editableHostPart = false;

			foreach (string line in listaConteudoOriginal)
			{
				if (!editableHostPart && line.Trim() == HostStartMark)
				{
					editableHostPart = true;
				}

				if (!editableHostPart)
				{
					conteudoHosts.Add(line.Trim());
				}
				else
				{
					if (line.Trim() != HostStartMark && line.Trim() != HostEndMark)
					{
						conteudoHostsDev.Add(line.Trim());
					}
				}

				if (editableHostPart && line.Trim() == HostEndMark)
				{
					editableHostPart = false;
				}
			}

			while (conteudoHosts.Count > 0 && string.IsNullOrEmpty(conteudoHosts[conteudoHosts.Count - 1]))
			{
				conteudoHosts.RemoveAt(conteudoHosts.Count - 1);
			}
			foreach (string line in conteudoHosts)
			{
				outputFile.Append(line + "\r\n");
			}

			outputFile.Append(HostStartMark + "\r\n");

			//Keep current host entries
			foreach (string line in conteudoHostsDev)
			{
				outputFile.Append(line + "\r\n");
			}

			//Add new entries (if needed)
			foreach (string host in HostEntries)
			{
				if (!conteudoHostsDev.Contains(host) && !string.IsNullOrEmpty(host))
				{
					outputFile.Append(host + "\r\n");	
				}
			}

			outputFile.Append("\r\n" + HostEndMark + "\r\n");

			StreamWriter sw = null;
			try 
			{
				if (outputFile != null)
				{
					bool encodeDetected;
					Encoding encoding = GetEncode(hostFilePath, out encodeDetected);
					sw = new StreamWriter(hostFilePath, false, encoding);
					sw.Write(outputFile.ToString());
				}
			}
			catch { }
			finally 
			{
				if (sw != null)
				{
					sw.Close();
				}
			}
		}

		protected virtual void LoadParameters(Step step)
		{ 
			base.LoadParameters(step);
			this.HostEntries = new List<string>();
			Parameter parameter;

			try 
			{
				parameter = step.GetParameter("HostEntries");
				string[] hostEntriesAux = parameter.ParameterValue.Split(';');
				foreach (string host in hostEntriesAux)
				{
					HostEntries.Add(host.Trim());
				}
			}
			catch { }

			try 
			{
				parameter = step.GetParameter("HostStartMark");
				this.HostStartMark = parameter.ParameterValue.Trim();
			}
			catch { }

			try 
			{
				parameter = step.GetParameter("HostEndMark");
				this.HostEndMark = parameter.ParameterValue.Trim();
			}
			catch { }

			
			

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
					"HostEntries"
					, string.Empty
					, "Lista de hosts a ser adicionado no arquivo de hosts")
			);

			parameters.Add(
				StepParameter.NewStepParameter(
					"HostStartMark"
					, string.Empty
					, "Linha que indica o início dos hosts alterados dinamicamente no arquivo de hosts")
			);

			parameters.Add(
				StepParameter.NewStepParameter(
					"HostStartMark"
					, string.Empty
					, "Linha que indica o fim dos hosts alterados dinamicamente no arquivo de hosts")
			);

			return parameters;
		}
	}
}
