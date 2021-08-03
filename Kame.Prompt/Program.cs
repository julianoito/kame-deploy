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
            List<string> executionGroups = new List<string>();
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
                    Console.WriteLine("Parametros esperados:");
                    Console.WriteLine("template\tCaminho do template a ser utilizado");
                    Console.WriteLine("workspace\tCaminho do workspace do deply");
                    Console.WriteLine("restoremode\tDefine se o Kame está em retore mode");
                    Console.WriteLine("replaceLog\tCaminho do log do Kame que susbtituirá o logo atual em um restore");
                    
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
            else
            {
                Console.WriteLine("Template não informado. Utilize o parâmetro /? para mais informações.");
            }

            if (caminhoTemplate != string.Empty)
            {
                if (!File.Exists(caminhoTemplate))
                {
                    caminhoTemplate = AppDomain.CurrentDomain.BaseDirectory + caminhoTemplate;
                }
                if (!File.Exists(caminhoTemplate))
                {
                    Console.WriteLine("Template não encontrado em '" + caminhoTemplate + "'.");
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
                projeto.Processar(new ConsoleLog(), executionGroups, restoreMode, out errorMessage);

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
