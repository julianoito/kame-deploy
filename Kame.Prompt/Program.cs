using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;

using Kame.Core.Entity;
using System.Net;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Xml;

namespace Kame.Prompt
{
    class Program
    {
		protected static DeployProject projeto;

        static int Main(string[] args)
        {
            string caminhoTemplate = string.Empty, kamemode = "file", apiurl = string.Empty, apiuser = string.Empty, apipassword = string.Empty, apiprojectid = string.Empty, apiprojectname = string.Empty;
            List<string> executionGroupList = new List<string>();
            bool restoreMode = false, apilog = false;
            string apiToken = string.Empty;

            List<ProjectParameter> parametros = new List<ProjectParameter>();
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.WriteLine("  Kame Deploy Manager - v" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version + "  ");

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
                    Console.WriteLine("kamemode\tfile or api");
                    Console.WriteLine("apiurl\tKame API URL");
                    Console.WriteLine("apiuser\tuser to access Kame API");
                    Console.WriteLine("apipassword\tuser to access Kame API");
                    Console.WriteLine("apiprojectid\tproject id to execute");
                    Console.WriteLine("apiprojectname\tproject id to execute");
                    Console.WriteLine("apilog\ttrue to post execution log to API");

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

                                switch (parametro[0].Substring(1).ToLower())
                                {
                                    case "template":
                                        caminhoTemplate = parametro[1];
                                        break;
                                    case "kamemode":
                                        kamemode = parametro[1];
                                        break;
                                    case "restoremode":
                                        if (parametro[1].ToLower().Trim() == "true")
                                        {
                                            restoreMode = true;
                                        }
                                        break;
                                    case "apiurl":
                                        apiurl = parametro[1];
                                        break;
                                    case "apiuser":
                                        apiuser = parametro[1];
                                        break;
                                    case "apipassword":
                                        apipassword = parametro[1];
                                        break;
                                    case "apilog":
                                        if (parametro[1].ToLower().Trim() == "true")
                                        {
                                            apilog = true;
                                        }
                                        break;
                                    case "apiprojectid":
                                        apiprojectid = parametro[1];
                                        break;
                                    case "apiprojectname":
                                        apiprojectname = parametro[1];
                                        break;


                                        
                                            
                                }
                            }
                        }
                    }                    
                }

            }
            else
            {
                Console.WriteLine("\r\nTemplate not provided. Use /? for more information.");
            }

            Console.WriteLine("Mode:" + kamemode + " \r\n");

            switch (kamemode)
            {
                case "file":
                    if (caminhoTemplate != string.Empty)
                    {
                        if (!File.Exists(caminhoTemplate))
                        {
                            caminhoTemplate = AppDomain.CurrentDomain.BaseDirectory + caminhoTemplate;
                        }
                        if (!File.Exists(caminhoTemplate))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Template not found: '" + caminhoTemplate + "'.");
                            return -1;
                        }
                    }

                    projeto = DeployProject.LoadDeployProject(caminhoTemplate, parametros);
                    break;
                case "api":
                    if (string.IsNullOrEmpty(apiurl))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("API URL not provided.");
                        Console.ForegroundColor = ConsoleColor.White;
                        return -1;
                    }

                    if (string.IsNullOrEmpty(apiuser))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("API user not provided.");
                        Console.ForegroundColor = ConsoleColor.White;
                        return -1;
                    }

                    if (string.IsNullOrEmpty(apipassword))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("API password not provided.");
                        Console.ForegroundColor = ConsoleColor.White;
                        return -1;
                    }

                    if (string.IsNullOrEmpty(apiprojectid) && string.IsNullOrEmpty(apiprojectname))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("API projetc id or name not provided.");
                        Console.ForegroundColor = ConsoleColor.White;
                        return -1;
                    }
                        
                    try
                    {
                        if (!apiurl.EndsWith("/"))
                        {
                            apiurl += "/";
                        }

                        string requestBody = "{\"name\":\"" + apiuser + "\", \"Password\":\"" + apipassword + "\" }";
                        byte[] byteArray = Encoding.UTF8.GetBytes(requestBody);
                        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiurl + "api/auth");
                        request.Method = "POST";
                        request.ContentType = "application/json";
                        request.ContentLength = byteArray.Length;
                        Stream strem = request.GetRequestStream();
                        strem.Write(byteArray, 0, byteArray.Length);

                        WebResponse response = request.GetResponse();

                        using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                        {
                            dynamic apiReturn = JObject.Parse(reader.ReadToEnd());
                            apiToken = apiReturn.token;
                        }

                        if (!string.IsNullOrEmpty(apiprojectid))
                        {
                            request = (HttpWebRequest)WebRequest.Create(apiurl + "api/deployproject/getbyid/" + apiprojectid);
                        }
                        else
                        {
                            request = (HttpWebRequest)WebRequest.Create(apiurl + "api/deployproject/getbyname/" + apiprojectname);
                        }
                        request.Method = "GET";
                        request.ContentType = "application/json";
                        request.Headers.Add("Authorization", "Beared " + apiToken);
                        response = request.GetResponse();

                        using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                        {
                            string retorno = reader.ReadToEnd();
                            projeto = JsonConvert.DeserializeObject<DeployProject>(retorno);
                            projeto.AddProjectParameters(parametros);
                        }


                    }
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(ex.Message);
                        Console.ForegroundColor = ConsoleColor.White;
                        return 0;
                    }

                    
                    break;
            }



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

                if (apilog)
                {
                    try
                    {
                        string requestBody = JsonConvert.SerializeObject(projeto.GetExecutionLog());
                        byte[] byteArray = Encoding.UTF8.GetBytes(requestBody);
                        HttpWebRequest request = null;
                        if (!string.IsNullOrEmpty(apiprojectid))
                        {
                            request = (HttpWebRequest)WebRequest.Create(apiurl + "api/deployproject/executionLogById/" + apiprojectid);
                        }
                        else
                        {
                            request = (HttpWebRequest)WebRequest.Create(apiurl + "api/deployproject/executionLogByName/" + apiprojectname);
                        }

                        request.Method = "POST";
                        request.ContentType = "application/json";
                        request.Headers.Add("Authorization", "Beared " + apiToken);
                        request.ContentLength = byteArray.Length;
                        Stream strem = request.GetRequestStream();
                        strem.Write(byteArray, 0, byteArray.Length);
                    }
                    catch (Exception ex)
                    {
                        Console.Write("Error on posting execution log: " + ex.Message);
                    }
                }

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
