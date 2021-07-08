using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

using Kame.Core.Entity.Log;

namespace Kame.Core.Entity
{
	public class SQL2005AdmStep : ISQL2005Step
    {
        private string operation = string.Empty;
        private string connectionString;
        private string databaseName;
        private string backupPath;
        private bool ignoreErrors;
        private string stepName;
		private bool deletePreviousLogDetails;
		private string SQLServerAlias;
		private List<string> logins = new List<string>();
        private IProjectExecutionLog ExecutionLog;

        public override void Execute(Step step, List<DeployFile> ignoreList, IProjectExecutionLog executionLog, DeployLog log, out string errorMessage)
        {
            this.stepName = step.Name;
            this.ExecutionLog = executionLog;
            errorMessage = string.Empty;
            this.LoadParameters(step);

            switch (this.operation)
            {
                case "RESTORE":
                    RestoreDatabase(executionLog, out errorMessage);
                    break;
            }

			if (deletePreviousLogDetails)
			{
				log.DeletePreviousLogDetails();
			}
        }
        public override List<DeployFile> CheckExecution(Step step, DeployLog log)
        {
            return null;
        }
        public override List<StepParameter> ListarParametrosUtilizados()
        {
            return null;
        }


        private new void LoadParameters(Step step)
        {
            Parameter parametro;

            try
            {
                parametro = step.GetParameter("operation");

                this.operation = parametro.ParameterValue;
            }
            catch
            {
                this.ThrowAplicationException("O parametro operação informado em um formata inválido");
            }

            try
            {
                parametro = step.GetParameter("connectionString");

                this.connectionString = parametro.ParameterValue;
            }
            catch
            {
                this.ThrowAplicationException("O parametro connectionString informado em um formata inválido");
            }

            try
            {
                parametro = step.GetParameter("databaseName");

                this.databaseName = parametro.ParameterValue;
            }
            catch
            {
                this.ThrowAplicationException("O parametro databaseName informado em um formata inválido");
            }

            try
            {
                parametro = step.GetParameter("backupPath");

                this.backupPath = parametro.ParameterValue;
            }
            catch
            {
                this.ThrowAplicationException("O parametro backupPath informado em um formata inválido");
            }

			try
            {
				parametro = step.GetParameter("SQLServerAlias");
				this.SQLServerAlias = parametro.ParameterValue;
            }
            catch
            {
            }

			try
            {
				parametro = step.GetParameter("createLogin");
				this.logins.Add(parametro.ParameterValue);
            }
            catch
            {
            }

			try
			{
				parametro = step.GetParameter("createMultipleLogins");

				string[] loginsList = parametro.ParameterValue.Split('|');
				foreach (string login in loginsList)
				{
					if (!string.IsNullOrEmpty(login))
					{
						this.logins.Add(login.Trim());
					}
				}
			}
			catch
			{
			}

            try
            {
                parametro = step.GetParameter("ignoreErrors");

                this.ignoreErrors = bool.Parse(parametro.ParameterValue);
            }
            catch
            {
                this.ThrowAplicationException("O parametro ignoreErrors informado em um formata inválido");
            }

			deletePreviousLogDetails = false;
			try
			{
				parametro = step.GetParameter("deletePreviousLogDetails");

				this.deletePreviousLogDetails = bool.Parse(parametro.ParameterValue);
			}
			catch
			{
				deletePreviousLogDetails = false;
			}
        }

        private bool RestoreDatabase(IProjectExecutionLog executionLog, out string errorMessage)
        {
            errorMessage = string.Empty;
			bool tempFile = false;
			string backupFilePath = this.backupPath;

            SqlConnection connection = null;
            SqlCommand comando;

            try
            {
				if (this.backupPath.StartsWith("\\\\")) //network file
				{
					backupFilePath = AppDomain.CurrentDomain.BaseDirectory;
					if (!backupFilePath.EndsWith("\\"))
					{
						backupFilePath += "\\";
					}
					backupFilePath += "backup_" + Guid.NewGuid().ToString() + ".bak";

					this.CopyFile(this.backupPath, backupFilePath, executionLog);
					tempFile = true;
				}

				if (string.IsNullOrEmpty(this.SQLServerAlias))
				{
					connection = new SqlConnection(this.connectionString);
				}
				else
				{
					connection = new SqlConnection(ChangeConnectionStringServer(this.connectionString, this.SQLServerAlias));
				}
                connection.Open();
                connection.FireInfoMessageEventOnUserErrors = true;
                connection.InfoMessage += new SqlInfoMessageEventHandler(OnInfoMessage);


                //Gets the file location
                comando = new SqlCommand(@"
                    SELECT SUBSTRING(physical_name, 1,
                        CHARINDEX(N'master.mdf',
                        LOWER(physical_name)) - 1) DataFileLocation
                    FROM master.sys.master_files
                    WHERE database_id = 1 AND FILE_ID = 1", connection);
                comando.CommandTimeout = 50000;
                string path = (string)comando.ExecuteScalar();
                if (path[path.Length - 1] != '\\')
                {
                    path += "\\";
                }


                //Get ths logical name from files
                comando = new SqlCommand(@"RESTORE filelistonly FROM DISK = '" + backupFilePath + "'", connection);
                SqlDataReader dr = comando.ExecuteReader();

                string datafile = string.Empty, logfile = string.Empty;
                while (dr.Read())
                {
                    if (dr["Type"].ToString() == "D")
                    {
                        datafile = dr["LogicalName"].ToString();
                    }
                    else if (dr["Type"].ToString() == "L")
                    {
                        logfile = dr["LogicalName"].ToString();
                    }
                }
                dr.Close();

				//set existing database to restrict user
				comando = new SqlCommand(
					"IF EXISTS(SELECT * FROM sys.databases where name = '" + this.databaseName.Replace("'", "") + "')\r\n" +
					"ALTER DATABASE " + this.databaseName + " SET RESTRICTED_USER WITH ROLLBACK IMMEDIATE"
				, connection);
				comando.CommandTimeout = 500000;
				comando.ExecuteNonQuery();

                //Restore database
                comando = new SqlCommand(
                    "RESTORE DATABASE " + this.databaseName +
                    " FROM DISK = '" + backupFilePath + "' WITH REPLACE, STATS," +
                    " MOVE '" + datafile + "' TO '" + path + this.databaseName +  "_Data.mdf', MOVE '" + logfile + "' TO '" + path + this.databaseName +  "_Log.ldf'"
				, connection);
                comando.CommandTimeout = 500000;
                comando.ExecuteNonQuery();

				//set database to multiuser
				comando = new SqlCommand(
					"ALTER DATABASE " + this.databaseName + " SET MULTI_USER"
				, connection);
				comando.CommandTimeout = 500000;
				comando.ExecuteNonQuery();

				if (logins != null)
				{
					foreach (string login in logins)
					{
						if (!string.IsNullOrEmpty(login))
						{
							List<string> listaComandosUsuario = new List<string>();


							listaComandosUsuario.Add(
								"IF NOT EXISTS(SELECT * FROM syslogins where name = '" + login + "')\r\n"
								+ "CREATE LOGIN [" + login + "] FROM WINDOWS");

							listaComandosUsuario.Add(
								"use " + this.databaseName + "\r\n"
								+ "IF NOT EXISTS(SELECT * FROM sysusers where name = '" + login + "')\r\n"
								+ "CREATE USER [" + login + "]");

							listaComandosUsuario.Add("use " + this.databaseName + " \r\n exec sp_addrolemember 'db_datareader', '" + login + "'");
							listaComandosUsuario.Add("use " + this.databaseName + " \r\n exec sp_addrolemember 'db_datawriter', '" + login + "'");

							try
							{
								foreach (string comandoUsuario in listaComandosUsuario)
								{
									comando = new SqlCommand(comandoUsuario, connection);
									comando.CommandTimeout = 500000;
									comando.ExecuteNonQuery();
								}
							}
							catch (Exception exLogin)
							{
								errorMessage = exLogin.Message;
							}
						}
					}
				}

				if (tempFile)
				{
					System.IO.File.Delete(backupFilePath);
				}


            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;

                if (!this.ignoreErrors)
                {
                    return false;
                }
            }

            return true;
        }

        protected void OnInfoMessage(object sender, SqlInfoMessageEventArgs args)
        {
            if (this.ExecutionLog == null)
            {
                return;
            }
            if (!string.IsNullOrEmpty(args.Message))
            {
                ExecutionLog.SetMessage(this.stepName + " - Restoring " + this.databaseName, args.Message);
            }
        }
    }
}
