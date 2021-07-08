using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
//using System.Data.SqlServerCe;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Kame.Core.Entity.Log
{
    public class DeployLog
    {
        private const string localConnectionStringLog = "Data Source=\"{0}\"; Password=\"0000\"";
        private LogSchema projectLogSchema = null;
        private LogSchema stepLogSchema = null;
        private LogSchema stepDetailLogSchema = null;

        [XmlIgnore]
        public DeployProject Projeto { get; set; }
        public int CodigoDeploy { get; set; }
        public int CurrentDeployStepID { get; set; }
        private int DeployStepDetailID {get;set;}
        public DateTime Data { get; set; }
        public List<StepLog> StepLogs{ get; set;}

        private string caminhoArquivo = string.Empty;

        public DeployLog()
        { 
        }

        public DeployLog(DeployProject projeto)
        {
            this.Projeto = projeto;

            this.caminhoArquivo = DeployLog.GetLogFileName(projeto, true);
            
            this.StepLogs = new List<StepLog>();

			/*
            SqlCeEngine sqlEngine = new SqlCeEngine(string.Format(localConnectionStringLog, this.caminhoArquivo));
            if (!File.Exists(caminhoArquivo))
            {
                sqlEngine.CreateDatabase();
            }

            this.projectLogSchema = new LogSchema(){
                TableName = "LogDeploy"
                ,Colunms = new LogSchemaColunm[]{
                    new LogSchemaColunm(){ Name = "DeployID", DataType = "INT" }
                    ,new LogSchemaColunm(){ Name = "ProjectID", DataType = "NVARCHAR(50)" }
                    ,new LogSchemaColunm(){ Name = "startDate", DataType = "DATETIME" }
                    ,new LogSchemaColunm(){ Name = "endDate", DataType = "DATETIME" }
                }
            };

            this.stepLogSchema = new LogSchema()
            {
                TableName = "LogDeployStep"
                ,
                Colunms = new LogSchemaColunm[]{
                    new LogSchemaColunm(){ Name = "DeployID", DataType = "INT" }
                    ,new LogSchemaColunm(){ Name = "DeployStepID", DataType = "INT" }
                    ,new LogSchemaColunm(){ Name = "StepName", DataType = "NVARCHAR(50)" }
                    ,new LogSchemaColunm(){ Name = "startDate", DataType = "DATETIME" }
                    ,new LogSchemaColunm(){ Name = "endDate", DataType = "DATETIME" }
                    ,new LogSchemaColunm(){ Name = "message", DataType = "NVARCHAR(4000)" }
                }
            };

            this.stepDetailLogSchema = new LogSchema()
            {
                TableName = "LogDeployStepDetail"
                ,
                Colunms = new LogSchemaColunm[]{
                    new LogSchemaColunm(){ Name = "DeployID", DataType = "INT" }
                    ,new LogSchemaColunm(){ Name = "DeployStepID", DataType = "INT" }
                    ,new LogSchemaColunm(){ Name = "DeployStepDetailID", DataType = "INT" }
                    ,new LogSchemaColunm(){ Name = "title", DataType = "NVARCHAR(4000)" }
                    ,new LogSchemaColunm(){ Name = "date", DataType = "DATETIME" }
                    ,new LogSchemaColunm(){ Name = "message", DataType = "NVARCHAR(4000)"}
                    ,new LogSchemaColunm(){ Name = "status", DataType = "INT" }
                    ,new LogSchemaColunm(){ Name = "TokenDetail", DataType = "NVARCHAR(50)" }
                }
            };

            SqlCeConnection connection = new SqlCeConnection(string.Format(localConnectionStringLog, caminhoArquivo));
            connection.Open();

            this.projectLogSchema.CheckLoalLogSchema(connection);
            this.stepLogSchema.CheckLoalLogSchema(connection);
            this.stepDetailLogSchema.CheckLoalLogSchema(connection);

            connection.Close();
			*/
        }

        public static string GetLogFileName(DeployProject project, bool creatFolder)
        {
            string filename = project.GetParameter("workspace").ParameterValue;

            if (filename[filename.Length - 1] != '\\')
            {
                filename += "\\";
            }

            if (!Directory.Exists(filename + ".kame") && creatFolder)
            {
                Directory.CreateDirectory(filename + ".kame");
            }

            filename += ".kame\\log_" + project.ProjectID + ".sdf";

            return filename;
        }

        public void StartProjectLog()
        {
            Dictionary<string, string> listaCampos = new Dictionary<string,string>();
			/*
            SqlCeConnection connection = new SqlCeConnection(string.Format(localConnectionStringLog, caminhoArquivo));
            connection.Open();
            
            this.projectLogSchema.CheckLoalLogSchema(connection);

            SqlCeCommand command = new SqlCeCommand("SELECT MAX(DeployID) FROM LogDeploy WHERE ProjectID='" + this.Projeto.ProjectID + "'", connection);
            object ultimoCodigo = command.ExecuteScalar();

            if (ultimoCodigo == DBNull.Value)
            {
                this.CodigoDeploy = 1;
            }
            else
            {
                this.CodigoDeploy = ((int)ultimoCodigo) + 1;
            }

            this.Data = DateTime.Now;
            this.projectLogSchema.InsertCommand(connection, this.CodigoDeploy, this.Projeto.ProjectID, this.Data, null);

            connection.Close();
			*/
        }

        public void EndProjectLog()
        { 
			/*
            SqlCeConnection connection = new SqlCeConnection(string.Format(localConnectionStringLog, caminhoArquivo));
            connection.Open();

            SqlCeCommand command = new SqlCeCommand("UPDATE LogDeploy SET endDate = @p3 WHERE DeployID = @p1 AND ProjectID = @p2", connection);
            command.Parameters.Add("p1", this.CodigoDeploy);
            command.Parameters.Add("p2", this.Projeto.ProjectID);
            command.Parameters.Add("p3", DateTime.Now);
            command.ExecuteNonQuery();

            connection.Close();
			*/
        }

        public void StartStepLog(Step step)
        {
            Dictionary<string, string> listaCampos = new Dictionary<string, string>();
			/*
            SqlCeConnection connection = new SqlCeConnection(string.Format(localConnectionStringLog, caminhoArquivo));
            connection.Open();

            this.stepLogSchema.CheckLoalLogSchema(connection);

            SqlCeCommand command = new SqlCeCommand("SELECT MAX(DeployStepID) FROM LogDeployStep WHERE DeployID='" + this.CodigoDeploy + "'", connection);
            object ultimoCodigo = command.ExecuteScalar();

            if (ultimoCodigo == DBNull.Value)
            {
                CurrentDeployStepID = 1;
            }
            else
            {
                CurrentDeployStepID = ((int)ultimoCodigo) + 1;
            }

            this.stepLogSchema.InsertCommand(connection, this.CodigoDeploy, CurrentDeployStepID, step.Name, this.StepLogs[this.StepLogs.Count-1].StartDate, null, null);
            

            connection.Close();
			*/
			this.StepLogs.Add(new StepLog() { DeployStepID = CurrentDeployStepID, Step = step, StartDate = DateTime.Now });
        }

        public void EndStepLog(Step step, string message)
        {
			/*
            SqlCeConnection connection = new SqlCeConnection(string.Format(localConnectionStringLog, caminhoArquivo));
            connection.Open();
			*/
            if (this.StepLogs.Count > 0)
            {
				string logMessage = message == null ? string.Empty : message;
                this.StepLogs[this.StepLogs.Count - 1].EndDate = DateTime.Now;
				this.StepLogs[this.StepLogs.Count - 1].Message = logMessage;
				/*
                SqlCeCommand command = new SqlCeCommand("UPDATE LogDeployStep SET endDate = @p3, message = @p4  WHERE DeployID = @p1 AND DeployStepID = @p2", connection);
                command.Parameters.Add("p1", this.CodigoDeploy);
                command.Parameters.Add("p2", CurrentDeployStepID);
                command.Parameters.Add("p3", this.StepLogs[this.StepLogs.Count - 1].EndDate);
				command.Parameters.Add("p4", logMessage.Length > 4000 ? logMessage.Substring(0, 4000) : logMessage);
                command.ExecuteNonQuery();
				 * */
            }
			/*
            connection.Close();
			*/
        }

        public void AddStepDetailLog(string title, string token)
        {
			/*
            Dictionary<string, string> listaCampos = new Dictionary<string, string>();

            SqlCeConnection connection = new SqlCeConnection(string.Format(localConnectionStringLog, caminhoArquivo));
            connection.Open();

            this.stepDetailLogSchema.CheckLoalLogSchema(connection);

            SqlCeCommand command = new SqlCeCommand("SELECT MAX(DeployStepDetailID) FROM LogDeployStepDetail WHERE DeployID='" + this.CodigoDeploy + "' AND DeployStepID=" + this.CurrentDeployStepID, connection);
            object ultimoCodigo = command.ExecuteScalar();

            if (ultimoCodigo == DBNull.Value)
            {
                this.DeployStepDetailID = 1;
            }
            else
            {
                this.DeployStepDetailID = ((int)ultimoCodigo) + 1;
            }

            this.stepDetailLogSchema.InsertCommand(connection, this.CodigoDeploy, CurrentDeployStepID, this.DeployStepDetailID, title, stepLog.StepLogDetails[stepLog.StepLogDetails.Count - 1].Date, string.Empty, StepLogDetailStatus.NotExecuted, token);

            connection.Close();
			*/

			StepLog stepLog = this.StepLogs[this.StepLogs.Count - 1];

			if (stepLog.StepLogDetails == null)
			{
				stepLog.StepLogDetails = new List<StepLogDetail>();
			}

			stepLog.StepLogDetails.Add(new StepLogDetail() { Title = title, Date = DateTime.Now, DeployStepDetailID = this.DeployStepDetailID });
        }

        public void EndStepDetailLog(Step step, string message, int status)
        {
			/*
            SqlCeConnection connection = new SqlCeConnection(string.Format(localConnectionStringLog, caminhoArquivo));
            connection.Open();

            SqlCeCommand command = new SqlCeCommand("UPDATE LogDeployStepDetail SET date = @p4, message = @p5, status = @p6  WHERE DeployID = @p1 AND DeployStepID = @p2 AND DeployStepDetailID = @P3", connection);
            command.Parameters.Add("p1", this.CodigoDeploy);
            command.Parameters.Add("p2", CurrentDeployStepID);
            command.Parameters.Add("p3", this.DeployStepDetailID);
            command.Parameters.Add("p4", stepLog.StepLogDetails[stepLog.StepLogDetails.Count - 1].Date);
            command.Parameters.Add("p5", message.Length > 4000 ? message.Substring(0, 4000) : message);
            command.Parameters.Add("p6", status);
            command.ExecuteNonQuery();

            connection.Close();
			*/

			StepLog stepLog = this.StepLogs[this.StepLogs.Count - 1];

			stepLog.StepLogDetails[stepLog.StepLogDetails.Count - 1].Date = DateTime.Now;
			stepLog.StepLogDetails[stepLog.StepLogDetails.Count - 1].Message = message;
			stepLog.StepLogDetails[stepLog.StepLogDetails.Count - 1].Status = status;
        }

		public void DeletePreviousLogDetails()
		{
			/*
			SqlCeConnection connection = new SqlCeConnection(string.Format(localConnectionStringLog, caminhoArquivo));
			connection.Open();
			SqlCeCommand command = new SqlCeCommand("DELETE FROM LogDeployStepDetail WHERE DeployID < @p1", connection);
			command.Parameters.Add("p1", this.CodigoDeploy);
			command.ExecuteNonQuery();

			connection.Close();
			*/
		}

        public StepLogDetail FindDetail(string title)
        {
            StepLogDetail logDetail = null;
			/*
            SqlCeConnection connection = new SqlCeConnection(string.Format(localConnectionStringLog, caminhoArquivo));
            connection.Open();

            SqlCeCommand command = new SqlCeCommand(
                "SELECT * FROM LogDeployStepDetail " +
                "WHERE title like @p2 AND DeployStepID IN (" +
                    "SELECT L.DeployID FROM LogDeploy L " +
                    " WHERE L.ProjectID = @p1) ORDER BY DeployId DESC, DeployStepID DESC, DeployStepDetailID DESC", connection);
            command.Parameters.Add("p1", this.Projeto.ProjectID);
            command.Parameters.Add("p2", title);

            SqlCeDataReader dr = command.ExecuteReader();
            
            if (dr.Read())
            {
                logDetail = new StepLogDetail();
                logDetail.DeployStepDetailID = dr.IsDBNull(dr.GetOrdinal("DeployStepID")) ? int.MinValue : dr.GetInt32(dr.GetOrdinal("DeployStepID"));
                logDetail.Date = dr.IsDBNull(dr.GetOrdinal("date")) ? DateTime.MinValue : dr.GetDateTime(dr.GetOrdinal("date"));
                logDetail.Message = dr.IsDBNull(dr.GetOrdinal("message")) ? string.Empty : dr.GetString(dr.GetOrdinal("message"));
                logDetail.Status = dr.IsDBNull(dr.GetOrdinal("status")) ? int.MinValue : dr.GetInt32(dr.GetOrdinal("status"));
                logDetail.Title = dr.IsDBNull(dr.GetOrdinal("title")) ? string.Empty : dr.GetString(dr.GetOrdinal("title"));
            }

            connection.Close();
			*/
            return logDetail;
        }


        public void ExportXML()
        {
            string xmlFile = this.Projeto.GetParameter("workspace").ParameterValue;

            if (xmlFile[xmlFile.Length - 1] != '\\')
            {
                xmlFile += "\\";
            }

            if (!Directory.Exists(xmlFile + ".kame"))
            {
                Directory.CreateDirectory(xmlFile + ".kame");
            }

            StreamWriter sw = null;
            try
            {
                xmlFile += ".kame\\log_" + this.Projeto.ProjectID + ".xml";
                if (File.Exists(xmlFile))
                {
                    File.Delete(xmlFile);
                }

                DeployLogXML xmlDeploy = new DeployLogXML();
                xmlDeploy.ProjectName = this.Projeto.Name;
                xmlDeploy.DeployCode = this.CodigoDeploy;
                xmlDeploy.Data = this.Data;
                xmlDeploy.StepLogs = new List<DeployStepLogXML>();

                foreach(StepLog stepLog in StepLogs)
                {
                    DeployStepLogXML stepLogXml = new DeployStepLogXML();
                    stepLogXml.StepName = stepLog.Step.Name;
                    stepLogXml.StartDate = stepLog.StartDate;
                    stepLogXml.EndDate = stepLog.EndDate;
                    stepLogXml.Message = stepLog.Message;
                    stepLogXml.StepLogDetails = new List<StepLogDetail>();

                    stepLogXml.StepLogDetails = stepLog.StepLogDetails;

                    xmlDeploy.StepLogs.Add(stepLogXml);
                }

                sw = new StreamWriter(File.OpenWrite(xmlFile));

                XmlSerializer serializer = new XmlSerializer(typeof(DeployLogXML));
                serializer.Serialize(sw, xmlDeploy);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (sw != null)
                {
                    sw.Close();
                }
            }
        }
    }

    public class StepLog
    {
        [XmlIgnore]
        public Step Step { get; set; }
        public int DeployStepID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Message {get; set;}
        public List<StepLogDetail> StepLogDetails { get; set; }
    }

    public class StepLogDetail
    {
        public int DeployStepDetailID { get; set; }
        public DateTime Date { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public int Status { get; set; }
    }

    public class StepLogDetailStatus
    {
        public const int NotExecuted = 0;
        public const int Ok = 1;
        public const int Error = 2;
        public const int Ignored = 3;
    }

    public class DeployLogXML
    {
        public string ProjectName { get; set; }
        public int DeployCode { get; set; }
        public DateTime Data { get; set; }
        public List<DeployStepLogXML> StepLogs { get; set; }
    }
    public class DeployStepLogXML
    {
        public string StepName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Message { get; set; }
        public List<StepLogDetail> StepLogDetails { get; set; }
    }
}
