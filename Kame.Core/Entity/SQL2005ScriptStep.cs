using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Mozilla.CharDet;


using Kame.Core.Entity.Log;

namespace Kame.Core.Entity
{
	public class SQL2005ScriptStep : ISQL2005Step
    {

        #region Parametros

        private string connectionString;
        private List<string> baseDirectories;
        private bool executarTodosScripts;
        private bool controleExecucaoScripts = false;
        private List<string> palavrasChaveIgnorar;
        private List<string> mensagensErroIgnorar;
		private List<string> comandosRemover;
		private List<string> arquivosIgnorarTransacao;
        private List<string> listaArquivosFixo;
        private string termosSubstituir;
        private DateTime minFileDate;
		private bool removeSQLTransactions = true;
		private string SQLServerAlias;

        #endregion

        public override List<StepParameter> ListarParametrosUtilizados()
        {
            List<StepParameter> listaParametros = new List<StepParameter>();


            listaParametros.Add(
                StepParameter.NewStepParameter(
                    "connectionString"
                    ,string.Empty
                    ,"Connection string utilizada na execução de scripts SQL")
            );

            listaParametros.Add(
                StepParameter.NewStepParameter(
                    "listaPastasExecucao"
                    ,string.Empty
                    ,"Lista das pastas de arquivos dentro do workspace de onde serão lidos os arquivos SQL. O nome das pastas devem ser separadas por ponto e virgula (;)"
                )
            );

            listaParametros.Add(
                StepParameter.NewStepParameter(
                    "executarTodosScripts"
                    ,string.Empty
                    ,"Parametro boolean que indica se o step deve processar todos os scripts SQL encontrados ou se deve controlar a execução dos scripts, executando-os apenas uma vez."
                )
            );

            listaParametros.Add(
                StepParameter.NewStepParameter(
                    "controleExecucaoScripts"
                    ,string.Empty
                    ,"Parametro boolean que indica se o step deve tentar reexecutar scripts que apresentaram algum erro em uma ordem diferente, colocando-os no final da fila de execução."
                )
            );

            listaParametros.Add(
                StepParameter.NewStepParameter(
                    "palavrasChaveIgnorar"
                    ,string.Empty
                    ,"Lista de palavras chave serão utilizados para ignorar a execução de arquivos. Caso o nome do arquivo seja contituido por uma dessas chaves o arquivo é ignorado. As palavras chave devem ser separadas por ponto e virgula (;)."
                )
            );

            listaParametros.Add(
                StepParameter.NewStepParameter(
                    "mensagensErroIgnorar"
                    ,string.Empty
                    ,"Lista de mensagens de erro a serem ignoradas pelo step. Caso um erro gerado na execução do script contenha uma das mensagens informadas o erro é ignorado e o sciprt considerado como executado. As mensagens de erro devem ser separadas por ponto e virgula (;)"
                )
            );

            listaParametros.Add(
                StepParameter.NewStepParameter(
                    "termosSubstituir"
                    ,string.Empty
                    ,"Lista de termos a serem substituidos nos arquivos antes da execução do script. Os termos devem ser separadas por ponto e virgula (;), no seguinte formato: palavra1|palavra2; onde palavra1 será subsituido por palavra2"
                )
            );
            
            listaParametros.Add(
                StepParameter.NewStepParameter(
                    "minFileDate"
                    ,string.Empty
                    ,"Data minima de alteração do arquivo a ser executado"
                )
            );

            listaParametros.Add(
                StepParameter.NewStepParameter(
                    "minMonthsFileDate"
                    , string.Empty
                    , "Limite em meses da alteração do arquivo a ser executado em relação a data atual"
                )
            );

			listaParametros.Add(
                StepParameter.NewStepParameter(
					"arquivosIgnorarTransacao"
                    , string.Empty
                    , "Lista de arquivos (ou parte de nomes de arquivos) que serão executados sem transação"
                )
            );

            listaParametros.Add(
                StepParameter.NewStepParameter(
                    "listaArquivosFixo"
                    , string.Empty
                    , "Lista fixa de arquivos que serão executados. Arquivos não listados não serão executados. E a execução dos arquivos seguirá a ordem da lista"
                )
            );
			

            return listaParametros;
        }

        public override void Execute(Step step, List<DeployFile> ignoreList, IProjectExecutionLog executionLog, DeployLog log, out string errorMessage)
        {
            if (executionLog != null)
            {
                executionLog.SetMessage("Executando step " + step.Name + ": carregando parametros", string.Empty);
            }
            this.LoadParameters(step);
            errorMessage = string.Empty;

            if (executionLog != null)
            {
                executionLog.SetMessage("Executando step " + step.Name + ": gerando lista de execução de arquivos", string.Empty);
            }
            List<DeployFile> fileList = this.ListFiles(step, log, ignoreList, executionLog);
            List<DeployFile> fileListError = new List<DeployFile>();

            foreach (DeployFile file in fileList)
            {
                if (executionLog != null)
                {
                    executionLog.SetMessage(step.Name + ": processando arquivo " + file.Name, string.Empty);
                }

                if (!this.ExecuteSQLFile(file, step, log, out errorMessage))
                {
                    if (this.controleExecucaoScripts)
                    {
                        fileListError.Add(file);
                    }
                    else
                    {
                        return;
                    }
                }
            }

            while (fileListError.Count > 0)
            {
                int fileCount = fileListError.Count;
                List<DeployFile> fileToRemove = new List<DeployFile>();

                errorMessage = string.Empty;
                foreach (DeployFile file in fileListError)
                {
                    if (executionLog != null)
                    {
                        executionLog.SetMessage(step.Name , file.Name);
                    }
                    if (this.ExecuteSQLFile(file, step, log, out errorMessage))
                    {
                        fileToRemove.Add(file);
                    }
                }

                foreach (DeployFile file in fileToRemove)
                {
                    fileListError.Remove(file);
                }

                if (fileCount == fileListError.Count)
                {
                    break;
                }
            }
        }

        public override List<DeployFile> CheckExecution(Step step, DeployLog log)
        {
            this.LoadParameters(step);

            return this.ListFiles(step, log, null, null);
        }
        
     
   
        #region metodos privados

        private bool ExecuteSQLFile(DeployFile file, Step step, DeployLog log, out string errorMessage)
        {
            errorMessage = string.Empty;

            SqlConnection connection = null;
            SqlTransaction sqlTransaction = null;
            SqlCommand comando;

            bool fileExecuted = false;
			bool useTransaction = CheckUseTransaction(file.Name);
            try
            {
				if (string.IsNullOrEmpty(this.SQLServerAlias))
				{
					connection = new SqlConnection(this.connectionString);
				}
				else
				{
					connection = new SqlConnection(ChangeConnectionStringServer(this.connectionString, this.SQLServerAlias));
				}

                connection.Open();
				if (useTransaction)
				{
					sqlTransaction = connection.BeginTransaction();
				}
            }
            catch (Exception ex)
            {
                log.AddStepDetailLog("connectionString", string.Empty);
                log.EndStepDetailLog(step, ex.Message, StepLogDetailStatus.Error);

                if (connection != null)
                {
                    connection.Close();
                }

                errorMessage = ex.Message;

                throw ex;
            }

            try
            {
                List<string> listSQLCommands = this.OpenSqlScript(file.FullName);
                SQLTextHelper sqlTextHelper = new SQLTextHelper(this.termosSubstituir);

                log.AddStepDetailLog(file.FullName, file.Token);

                foreach (string comandoSQL in listSQLCommands)
                {
                    if (!string.IsNullOrEmpty(comandoSQL))
                    {
                        comando = new SqlCommand( sqlTextHelper.ReplaceSQLText(comandoSQL), connection);
                        comando.CommandTimeout = 50000;
						if (useTransaction)
						{
							comando.Transaction = sqlTransaction;
						}
                        comando.ExecuteNonQuery();
                    }
                }

				if (useTransaction)
				{
					sqlTransaction.Commit();
				}
                log.EndStepDetailLog(step, string.Empty, StepLogDetailStatus.Ok);

                fileExecuted = true;
            }
            catch (System.Exception exception)
            {
                bool errorIgnored = false;
				errorMessage = exception.Message;

				try
				{
					if (useTransaction && sqlTransaction != null)
					{
						sqlTransaction.Rollback();
					}
				}
				catch (Exception exRollback)
				{
					errorMessage += exRollback.Message;
				}

                if (mensagensErroIgnorar != null)
                {
                    foreach (string ignoredError in mensagensErroIgnorar)
                    {
                        if (!string.IsNullOrEmpty(ignoredError) && ignoredError.Trim()!=string.Empty && exception.Message.Contains(ignoredError.Trim()))
                        {
							log.EndStepDetailLog(step, errorMessage, StepLogDetailStatus.Ignored);
                            errorIgnored = true;
							errorMessage = string.Empty;
                            break;
                        }
                    }
                }
                if (!errorIgnored)
                {
                    log.EndStepDetailLog(step, exception.Message, StepLogDetailStatus.Error);
                    errorMessage = "Error executing file " + file.FullName + " - " + exception.Message;
                }
                else
                {
                    log.EndStepDetailLog(step, string.Empty, StepLogDetailStatus.Ok);
                    fileExecuted = true;
                }
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }
            }

            return fileExecuted;
        }

        protected virtual void LoadParameters(Step step)
        {
            Parameter parametro;

            try
            {
                parametro = step.GetParameter("connectionString");
                if (parametro == null)
                {
                    this.ThrowAplicationException("O parametro connectionString não informado.");
                }
                this.connectionString = parametro.ParameterValue;
            }
            catch {
                this.ThrowAplicationException("O parametro connectionString informado em um formata inválido");
            }

            try
            {
                parametro = step.GetParameter("workspace");
                if (parametro == null)
                {
                    this.ThrowAplicationException("O parametro workspace não informado.");
                }
                this.workspace = parametro.ParameterValue;
                if (!this.workspace.EndsWith("\\"))
                {
                    this.workspace += "\\";
                }
            }
            catch {
                this.ThrowAplicationException("O parametro workspace informado em um formata inválido");
            }
            

            try
            {
                parametro = step.GetParameter("listaPastasExecucao");
                this.baseDirectories = null;
                if (parametro != null)
                {
                    this.baseDirectories = new List<string>(parametro.ParameterValue.Replace("\r", "").Replace("\n", "").Split(';'));
                }
                else
                {
                    this.ThrowAplicationException("O parametro listaPastasExecucao não informado");
                }
            }
            catch {
                this.ThrowAplicationException("O parametro listaPastasExecucao informado em um formata inválido");
            }

            try
            {
                parametro = step.GetParameter("controleExecucaoScripts");
                if (parametro == null)
                {
                    this.ThrowAplicationException("O parametro controleExecucaoScripts não informado");
                }
                this.controleExecucaoScripts = bool.Parse(parametro.ParameterValue);
            }
            catch
            {
                this.ThrowAplicationException("O parametro listaPastasExecucao informado em um formata inválido");
            }

			try
            {
				parametro = step.GetParameter("removeSQLTransactions");
                if (parametro != null)
                {
					this.removeSQLTransactions = bool.Parse(parametro.ParameterValue);
				}
            }
            catch
            {
            }

			

            try
            {
                parametro = step.GetParameter("executarTodosScripts");

                if (parametro == null)
                {
                    this.ThrowAplicationException("O parametro executarTodosScripts não informado");
                }
                this.executarTodosScripts = parametro == null ? false : bool.Parse(parametro.ParameterValue);
            }
            catch
            {
                this.ThrowAplicationException("O parametro executarTodosScripts informado em um formata inválido");
            }


            parametro = step.GetParameter("palavrasChaveIgnorar");
            if (parametro != null)
            {
                try
                {
                    this.palavrasChaveIgnorar = new List<string>(parametro.ParameterValue.Replace("\r", "").Replace("\n", "").Split(';'));
                }
                catch
                {
                    this.ThrowAplicationException("O parametro palavrasChaveIgnorar informado em um formata inválido");
                }
            }
            else
            {
                this.palavrasChaveIgnorar = new List<string>();
            }

			parametro = step.GetParameter("comandosRemover");
			if (parametro != null)
			{
				try
				{
					this.comandosRemover = new List<string>(parametro.ParameterValue.Replace("\r", "").Replace("\n", "").Split(';'));
				}
				catch
				{
					this.ThrowAplicationException("O parametro comandosRemover informado em um formata inválido");
				}
			}
			else
			{
				this.comandosRemover = new List<string>();
			}
			
            parametro = step.GetParameter("mensagensErroIgnorar");
            if (parametro == null)
            {
                this.mensagensErroIgnorar = new List<string>();                
            }
            else
            {
                try
                {
                    this.mensagensErroIgnorar = new List<string>(parametro.ParameterValue.Replace("\r", "").Replace("\n", "").Split(';'));
                }
                catch
                {
                    this.ThrowAplicationException("O parametro mensagensErroIgnorar informado em um formata inválido");
                }
            }


            try
            {
                parametro = step.GetParameter("termosSubstituir");
                this.termosSubstituir = parametro.ParameterValue;
            }
            catch
            {
                
            }

            parametro = step.GetParameter("minFileDate");
			if (parametro == null || string.IsNullOrEmpty(parametro.ParameterValue))
            {
                this.minFileDate = DateTime.MinValue;
            }
            else
            {
                try
                {
                    this.minFileDate = DateTime.ParseExact(parametro.ParameterValue, "yyyy-MM-dd", null); // new List<string>(parametro.ParameterValue.Replace("\r", "").Replace("\n", "").Split(';'));                    
                }
                catch
                {
                    this.ThrowAplicationException("O parametro minFileDate informado em um formata inválido");
                }
            }

            parametro = step.GetParameter("minMonthsFileDate");
            if (parametro != null)
            {
                try
                {
                    this.minFileDate = DateTime.Now.Date.AddMonths(int.Parse(parametro.ParameterValue)); // new List<string>(parametro.ParameterValue.Replace("\r", "").Replace("\n", "").Split(';'));                    
                }
                catch
                {
                    this.ThrowAplicationException("O parametro minFileDate informado em um formata inválido");
                }
            }

			try
			{
				parametro = step.GetParameter("SQLServerAlias");
				this.SQLServerAlias = parametro.ParameterValue;
			}
			catch
			{
			}

			arquivosIgnorarTransacao = new List<string>();
			parametro = step.GetParameter("arquivosIgnorarTransacao");
			if (parametro != null)
			{
				try
				{
					this.arquivosIgnorarTransacao = new List<string>(parametro.ParameterValue.Replace("\r", "").Replace("\n", "").Split(';'));
				}
				catch
				{
					this.ThrowAplicationException("O parametro arquivosIgnorarTransacao informado em um formata inválido");
				}
			}
			else
			{
				this.arquivosIgnorarTransacao = new List<string>();
			}

            listaArquivosFixo = new List<string>();
            parametro = step.GetParameter("listaArquivosFixo");
            if (parametro != null)
            {
                try
                {
                    this.listaArquivosFixo = new List<string>(parametro.ParameterValue.Replace("\r", "").Replace("\n", "").Split(';'));
                }
                catch
                {
                    this.ThrowAplicationException("O parametro listaArquivosFixo informado em um formata inválido");
                }
            }
            
        }

		private List<DeployFile> ListFiles(Step step, DeployLog log, List<DeployFile> ignoreList, IProjectExecutionLog executionLog)
        {
            List<DeployFile> fileList = new List<DeployFile>();

            if (listaArquivosFixo != null && listaArquivosFixo.Count > 0)
            {
                bool listaArquivosFixoUtilizada = false;
                for (int i = 0; i < listaArquivosFixo.Count; i++)
                {
                    if (!string.IsNullOrEmpty(listaArquivosFixo[i].Trim()))
                    {
                        listaArquivosFixoUtilizada = true;


                        foreach (string directoryPath in baseDirectories)
                        {
				            if (Directory.Exists(this.workspace + directoryPath.Trim()))
				            {
                                string caminhoCompletoArquivo = this.workspace + directoryPath.Trim();
                                if (caminhoCompletoArquivo.Length > 0 && caminhoCompletoArquivo[caminhoCompletoArquivo.Length - 1] != '\\')
                                {
                                    caminhoCompletoArquivo += "\\";
                                }
                                caminhoCompletoArquivo += listaArquivosFixo[i].Trim();

                                if (File.Exists(caminhoCompletoArquivo))
                                {
                                    FileInfo file = new FileInfo(caminhoCompletoArquivo);
                                    fileList.Add(new DeployFile() { Name = file.Name, FullName = file.FullName });
                                }
                            }
                        }
                        
                    }
                }

                if (listaArquivosFixoUtilizada)
                {
                    return fileList;
                }
            }

            foreach (string directoryPath in baseDirectories)
            {
				if (Directory.Exists(this.workspace + directoryPath.Trim()))
				{
					DirectoryInfo directory = new DirectoryInfo(this.workspace + directoryPath.Trim());
					FileInfo[] sqlFiles = directory.EnumerateFiles("*.sql", SearchOption.TopDirectoryOnly).ToArray<FileInfo>();
					foreach (FileInfo file in sqlFiles)
					{
						bool ignoreFile = false;

						if (file.LastWriteTime < this.minFileDate)
						{
							ignoreFile = true;
						}

						if (!ignoreFile && this.palavrasChaveIgnorar != null)
						{
							foreach (string ignoreKeyword in this.palavrasChaveIgnorar)
							{
								if (!string.IsNullOrEmpty(ignoreKeyword.Trim()) && file.Name.Contains(ignoreKeyword.Trim()))
								{
									ignoreFile = true;
									break;
								}
							}
						}

						if (!ignoreFile)
						{
							if (ignoreList == null || ignoreList.Single<DeployFile>(f => f.FullName == file.FullName) == null)
							{
								StepLogDetail logDetail = null;
								if (!this.executarTodosScripts)
								{
									//logDetail = log.FindDetail("%" + file.FullName.Replace(this.workspace, string.Empty));
								}
								if (this.executarTodosScripts || logDetail == null || logDetail.Status == StepLogDetailStatus.Error)
								{
									fileList.Add(new DeployFile() { Name = file.Name, FullName = file.FullName });
								}
							}
						}
					}

					if (executionLog != null)
					{
						executionLog.SetMessage("Diretório " + this.workspace + directoryPath + ": " + sqlFiles.Length + " arquivos, " + fileList.Count + " utilizados", string.Empty);
					}
				}
			}

            return fileList.OrderBy(f => f.Name).ToList<DeployFile>();
        }

		public bool CheckUseTransaction(string filename)
		{
			if (arquivosIgnorarTransacao == null || arquivosIgnorarTransacao.Count == 0)
			{
				return true;
			}

			foreach (string ignoreKeyword in this.arquivosIgnorarTransacao)
			{
				if (!string.IsNullOrEmpty(ignoreKeyword.Trim()) && filename.Contains(ignoreKeyword.Trim()))
				{
					return false;
				}
			}

			return true;
		}

        public List<string> OpenSqlScript(string caminhoArquivo)
        {
            List<string> listaComandosSQL = new List<string>();
            string conteudoSQL = this.ReadTextFile(caminhoArquivo);

            //var re = @"(@(?:""[^""]*"")+|""(?:[^""\n\\]+|\\.)*""|'(?:[^'\n\\]+|\\.)*')|//.*|/\*(?s:.*?)\*/";
            //var re = @"/\*(?>(?:(?!\*/|/\*).)*)(?>(?:/\*(?>(?:(?!\*/|/\*).)*)\*/(?>(?:(?!\*/|/\*).)*))*).*?\*/|--.*?\r?[\n]";

            //var re = @"/\*([^*]|[\r\n]|(\*+([^*/]|[\r\n])))*\*+/";
            //conteudoSQL = Regex.Replace(conteudoSQL, re, "\r\n");

			//remove os comandos configurados no step
			if (comandosRemover != null)
			{
				foreach (string expressaoRegular in comandosRemover)
				{
					if (!string.IsNullOrEmpty(expressaoRegular))
					{
						conteudoSQL = Regex.Replace(conteudoSQL, expressaoRegular.Trim(), "");
					}
				}
			}

            //Remove comentários em bloco, ignorando comentários de bloco que estejam dentro de uma strign SQL
            int indice = 0;
            bool blocoString = false;
            int[] blocoComentario = new int[2];
            while (indice < conteudoSQL.Length-1)
            {
                if (conteudoSQL[indice] == '\'')
                {
                    blocoString = !blocoString;
                }

                if (!blocoString)
                {
                    if (conteudoSQL.Substring(indice, 2) == "/*")
                    {
                        blocoComentario[0] = indice;
                    }
                    else if (conteudoSQL.Substring(indice, 2) == "*/")
                    {
                        blocoComentario[1] = indice;

                        conteudoSQL = conteudoSQL.Remove(blocoComentario[0], blocoComentario[1] - blocoComentario[0] + 2);
                        conteudoSQL.Insert(blocoComentario[0], "\r\n");

                        indice = -1; // Reinicia a verificação do arquivo
                    }
                }

                indice++;
            }

			if (removeSQLTransactions)
			{
				conteudoSQL = conteudoSQL.Replace("BEGIN TRANSACTION", "print('')");
				conteudoSQL = conteudoSQL.Replace("COMMIT TRANSACTION", "print('')");
				conteudoSQL = conteudoSQL.Replace("ROLLBACK TRANSACTION", "print('')");
				conteudoSQL = conteudoSQL.Replace("ROLLBACK TRAN", "print('')");
				conteudoSQL = conteudoSQL.Replace("BEGIN TRAN A", "print('')");
				conteudoSQL = conteudoSQL.Replace("BEGIN TRAN", "print('')");
				conteudoSQL = conteudoSQL.Replace("COMMIT TRAN", "print('')");

				//Tranforma o readumcommitedd trocando uma letra para que a remoação do termo commit não estrague a view
				conteudoSQL = conteudoSQL.Replace("READUNCOMMITTED", "READUNCOM_ITTED");
				conteudoSQL = conteudoSQL.Replace("READ UNCOMMITTED", "READ UNCOMMI_TED");
				conteudoSQL = conteudoSQL.Replace("COMMIT", "print('')");
				conteudoSQL = conteudoSQL.Replace("READUNCOM_ITTED", "READUNCOMMITTED");
				conteudoSQL = conteudoSQL.Replace("READ UNCOMMI_TED", "READ UNCOMMITTED");
			}

            string[] linhasArquivo = conteudoSQL.Split(new char[] { '\n' });

            listaComandosSQL.Add(string.Empty);

            for (int i = 0; i < linhasArquivo.Length; i++)
            {
                string linha = linhasArquivo[i];
                
                if (linha.Contains("--"))
                {
					bool quotationMark = false;
					int posComentario = int.MinValue;
					for (int c = 0; c < linha.Length - 1; c++)
					{
						if (linha[c] == '\'')
						{
							quotationMark = !quotationMark;
						}
						else 
						{
							if (linha[c] == '-' && linha[c + 1] == '-' && !quotationMark)
							{
								posComentario = c;
								break;
							}
						}
					}
					if (posComentario != int.MinValue)
					{
						linha = linha.Substring(0, posComentario);
					}
                }

                if (linha.Replace("\t", "").Trim().Length >= 2 && linha.Replace("\t", "").Trim().Substring(0, 2).ToUpper() == "GO"
					&& (
						linha.Replace("\t", "").Trim().Length == 2 ||
						linha.Replace("\t", "").Trim()[2] == '\r' ||
						linha.Replace("\t", "").Trim()[2] == ' ' ||
						linha.Replace("\t", "").Trim()[2] == ';' 
					)
				)
                {
                    listaComandosSQL.Add(string.Empty);
                    if (linha.Replace("\t", "").Trim().Length > 3 && linha.Replace("\t", "").Trim().Substring(0, 3).ToUpper() == "GO;")
                    {
                        listaComandosSQL[listaComandosSQL.Count - 1] += linha.Replace("\t", "").Replace("\r", "").Trim().Substring(3) + "\r\n";
                    }
                }
                else
                {
                    listaComandosSQL[listaComandosSQL.Count - 1] += linha.Replace("\r", "") + "\r\n";
                }
            }


            return listaComandosSQL;
        }

        
        #endregion

        private class SQLTextHelper
        {
            private Dictionary<string, string> dicionarioTextos;

            public SQLTextHelper(string texstosSubstituir)
            {
                this.dicionarioTextos = new Dictionary<string, string>();
                if (string.IsNullOrEmpty(texstosSubstituir))
                {
                    return;
                }

                string[] listaTextos = texstosSubstituir.Split(';');

                foreach (string textoSubstituir in listaTextos)
                { 
                    string[] chaveValor = textoSubstituir.Trim().Split('|');

                    if (chaveValor!=null && chaveValor.Length == 2)
                    {
                        dicionarioTextos.Add(chaveValor[0], chaveValor[1]);
                    }
                }
            }

            public string ReplaceSQLText(string sqlCommand)
            {
                foreach (KeyValuePair<string, string> textToReplace in dicionarioTextos)
                {
                    sqlCommand = sqlCommand.Replace(textToReplace.Key, textToReplace.Value);
                }

                return sqlCommand;
            }
        }
    }

    
}
