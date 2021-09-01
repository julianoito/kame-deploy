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
        [XmlIgnore]
        public DeployProject Projeto { get; set; }
        public int CodigoDeploy { get; set; }
        public int CurrentDeployStepID { get; set; }
        private int DeployStepDetailID {get;set;}
        public DateTime Data { get; set; }
        public List<StepLog> StepLogs{ get; set;}

        public DeployLog()
        { 
        }

        public DeployLog(DeployProject projeto)
        {
            this.Projeto = projeto;

            this.StepLogs = new List<StepLog>();
        }

        public void StartStepLog(Step step)
        {
            Dictionary<string, string> listaCampos = new Dictionary<string, string>();
			
			this.StepLogs.Add(new StepLog() { DeployStepID = CurrentDeployStepID, Step = step, StartDate = DateTime.Now });
        }

        public void EndStepLog(Step step, string message)
        {
            if (this.StepLogs.Count > 0)
            {
				string logMessage = message == null ? string.Empty : message;
                this.StepLogs[this.StepLogs.Count - 1].EndDate = DateTime.Now;
				this.StepLogs[this.StepLogs.Count - 1].Message = logMessage;
            }
        }

        public void AddStepDetailLog(string title, string token)
        {
			StepLog stepLog = this.StepLogs[this.StepLogs.Count - 1];

			if (stepLog.StepLogDetails == null)
			{
				stepLog.StepLogDetails = new List<StepLogDetail>();
			}

			stepLog.StepLogDetails.Add(new StepLogDetail() { Title = title, Date = DateTime.Now, DeployStepDetailID = this.DeployStepDetailID });
        }

        public void EndStepDetailLog(Step step, string message, int status)
        {
			StepLog stepLog = this.StepLogs[this.StepLogs.Count - 1];

			stepLog.StepLogDetails[stepLog.StepLogDetails.Count - 1].Date = DateTime.Now;
			stepLog.StepLogDetails[stepLog.StepLogDetails.Count - 1].Message = message;
			stepLog.StepLogDetails[stepLog.StepLogDetails.Count - 1].Status = status;
        }

		public void DeletePreviousLogDetails()
		{
		}

        public StepLogDetail FindDetail(string title)
        {
            StepLogDetail logDetail = null;
			
            return logDetail;
        }


        public void ExportXML()
        {
            string xmlFile = this.Projeto.GetParameter("workspace").ParameterValue;

            if (!xmlFile.EndsWith("\\"))
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
                xmlFile += ".kame\\log_" + ( string.IsNullOrEmpty(this.Projeto.ProjectID) ? this.Projeto.Name.Replace(" ","") : this.Projeto.ProjectID) + ".xml";
                if (File.Exists(xmlFile))
                {
                    File.Delete(xmlFile);
                }

                /*DeployLogXML xmlDeploy = new DeployLogXML();
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
                }*/

                sw = new StreamWriter(File.OpenWrite(xmlFile));

                XmlSerializer serializer = new XmlSerializer(typeof(DeployLogXML));
                serializer.Serialize(sw, GetXMlObject());

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

        public DeployLogXML GetXMlObject()
        {
            DeployLogXML xmlDeploy = new DeployLogXML();
            xmlDeploy.ProjectName = this.Projeto.Name;
            xmlDeploy.DeployCode = this.CodigoDeploy;
            xmlDeploy.Data = this.Data;
            xmlDeploy.StepLogs = new List<DeployStepLogXML>();

            foreach (StepLog stepLog in StepLogs)
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
            
            return xmlDeploy;
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
        public string ProjectId { get; set; }
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
