using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;

using Kame.Core.Entity;

namespace Kame.Prompt
{
    class Program
    {
		protected static DeployProject projeto;

        static int Main(string[] args)
        {
            string caminhoTemplate = string.Empty;
            List<string> executionGroupList = new List<string>();
            bool restoreMode = false;

            List<ProjectParameter> parametros = new List<ProjectParameter>();
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.WriteLine("  Kame Deploy Manager - v" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version + "  \r\n");

            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;

            if (args != null && args.Length > 0)
            {
                if (args.Length == 1 && args[0] == "/?")
                {
                    Console.WriteLine("kame.prompt /template=\"\" [/workspace=\"\"] ");
                    Console.WriteLine("Parameters:");
                    Console.WriteLine("template\ttemplate path");
                    Console.WriteLine("workspace\tWorkspace path");
                    Console.WriteLine("restoremode\tDefine Kame in retore mode");
                    Console.WriteLine("ExecutionGroups\tFilter execution steps when supplied, use \",\" to separete parameters");

                    Console.ReadKey();
                    return -1;
                }
                else
                {
                    foreach (string strParametro in args)
                    {
                        string[] parametro = strParametro.Split('=');

                        if (parametro.Length == 2 && !string.IsNullOrEmpty(parametro[0]) && !string.IsNullOrEmpty(parametro[1]) && parametro[0][0]=='/')
                        {
                            if (parametro[0].Substring(1).ToLower() == "executiongroups")
                            {
                                string[] executionGroupsListAux = parametro[1].Split(',');
                                foreach (string executionGroup in executionGroupsListAux)
                                {
                                    if (!string.IsNullOrEmpty(executionGroup.Trim()))
                                    {
                                        executionGroupList.Add(executionGroup.Trim());
                                    }
                                }
                            }
                            else
                            {
                                parametros.Add(ProjectParameter.NewProjectParameter(parametro[0].Substring(1), parametro[1], string.Empty, null));

                                if (parametro[0].Substring(1).ToLower() == "template")
                                {
                                    caminhoTemplate = parametro[1];
                                }
                                else if (parametro[0].Substring(1).ToLower() == "restoremode" && parametro[1].ToLower().Trim() == "true")
                                {
                                    restoreMode = true;
                                }
                            }
                        }
                    }                    
                }

            }
            else
            {
                Console.WriteLine("Template not provided. Use /? for more information.");
            }

            if (caminhoTemplate != string.Empty)
            {
                if (!File.Exists(caminhoTemplate))
                {
                    caminhoTemplate = AppDomain.CurrentDomain.BaseDirectory + caminhoTemplate;
                }
                if (!File.Exists(caminhoTemplate))
                {
                    Console.WriteLine("Template not found: '" + caminhoTemplate + "'.");
                    return -1;
                }
            }

            projeto = DeployProject.LoadDeployProject(caminhoTemplate, parametros);

            if (projeto != null)
            {
				Console.CancelKeyPress += delegate(object sender, ConsoleCancelEventArgs e)
				{
					if (projeto != null && !projeto.LogSaved)
					{
						projeto.ExportLog();
					}
				};
				AppDomain.CurrentDomain.ProcessExit += delegate(object sender, EventArgs e)
				{
					if (projeto != null && !projeto.LogSaved)
					{
						projeto.ExportLog();
					}
				};

                string errorMessage;
                projeto.Processar(new ConsoleLog(), executionGroupList, restoreMode, out errorMessage);

                if (!string.IsNullOrEmpty(errorMessage))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(errorMessage);
                    return -1;
                }
            }
            
            return 0;
        }
    }

    class ConsoleLog : IProjectExecutionLog
    {
        public void SetMessage(string step, string stepDetail)
        {
            Console.WriteLine(step + (string.IsNullOrEmpty(stepDetail) ? string.Empty : (" - " + stepDetail)));
        }

		public void SetMessageFixedLine(string message, string messagePrefix, string messageDetail)
		{
			this.SetMessage(message, messageDetail);
		}
    }
}
