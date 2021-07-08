using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

using Kame.Core.Entity.Log;
using Kame.Core.Data;

namespace Kame.Core.Entity
{
    [Serializable]
    public class DeployProject : BaseEntity
    {
        #region properties

        public string ProjectID { get; set; }
        public string Name { get; set; }
        public int UserID { get; set; }

		[NotMapped]
		[XmlIgnore]
		public bool LogSaved { get; set; }

		[XmlIgnore]
		private DeployLog log;

        [XmlElement(typeof(ProjectParameter))]
        public List<ProjectParameter> Parameters { get; set; }
        [NotMapped]
        [XmlElement(typeof(Step))]
        public virtual List<Step> Steps { get; set; }

        [NotMapped]
        [XmlElement(typeof(Step))]
        public virtual List<Step> RestoreSteps { get; set; }

        [NotMapped]
        [XmlIgnore]
        public virtual List<Step> DeletedSteps { get; set; }

        #endregion

        #region construtores

        private DeployProject()
        {
            this.EntityState = System.Data.EntityState.Modified;
        }
        
        public static DeployProject NewDeployProject(KameUser user)
        {
            return new DeployProject() { EntityState = System.Data.EntityState.Added, ProjectID = Guid.NewGuid().ToString(), UserID = user.UserID  };
        }

        public static DeployProject LoadDeployProject(string template, List<ProjectParameter> parametros)
        {
            DeployProject project = null;

            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(DeployProject), new Type[] { typeof(ProjectParameter), typeof(Parameter), typeof(Step) });
            FileStream configFile = null;

            try
            {
                configFile = new FileStream(template, FileMode.Open, FileAccess.Read);
                project = (DeployProject)serializer.Deserialize(configFile);

                if (project.Parameters == null)
                {
                    project.Parameters = new List<ProjectParameter>();
                }

                if (parametros != null)
                {
                    foreach (ProjectParameter parametro in parametros)
                    {
                        ProjectParameter parametroExistente = null;
                        var consulta = project.Parameters.Where<ProjectParameter>(f => f.ParameterKey == parametro.ParameterKey);
                        if (consulta.Count<ProjectParameter>() > 0)
                        {
                            parametroExistente = consulta.First<ProjectParameter>();
                        }

                        if (parametroExistente != null)
                        {
                            parametroExistente.ParameterKey = parametro.ParameterKey;
                        }
                        else
                        {
                            project.Parameters.Add(parametro);
                        }
                    }
                }

                if (project.Steps == null)
                {
                    project.Steps = new List<Step>();
                }
                else
                {
                    foreach (Step step in project.Steps)
                    {
                        step.SetParentStep(null);
                        step.Project = project;
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally {
                if (configFile != null)
                {
                    configFile.Close();
                }
            }

            return project;
        }

        #endregion

		

        public void Processar(IProjectExecutionLog executionLog, List<string> executionGroups, bool restoreMode, out string errorMessage)
        {
			this.LogSaved = false;
            if (restoreMode)
            {
                string logFile = DeployLog.GetLogFileName(this, false);

                if (File.Exists(logFile))
                {
                    File.Delete(logFile);
                }

                Parameter replaceLogParameter = this.GetParameter("replaceLog");
                if (replaceLogParameter != null && File.Exists(replaceLogParameter.ParameterValue))
                {
                    File.Copy(replaceLogParameter.ParameterValue, logFile);
                }
            }

            this.log = new DeployLog(this);
            errorMessage = string.Empty;
			this.log.StartProjectLog();

            if (restoreMode && this.RestoreSteps != null)
            {
                executionLog.SetMessage("RestoreMode", string.Empty);
                for (int i = 0; i < this.RestoreSteps.Count; i++)
                {
					if (!this.RestoreSteps[i].Processar(executionLog, this.log, executionGroups, out errorMessage))
                    {
                        break;
                    }
                }
            }
            
            for (int i = 0; i < this.Steps.Count; i++)
            {
				if (!this.Steps[i].Processar(executionLog, this.log, executionGroups, out errorMessage))
                {
                    break;
                }
            }

			ExportLog();
        }

		public void ExportLog()
		{
			if (this.log != null)
			{
				log.EndProjectLog();
				log.ExportXML();
				this.LogSaved = true;
			}
		}

        public Parameter GetParameter(string parameterKey)
        {
            if (this.Parameters != null)
            {
                var query = this.Parameters.Where(p => p.ParameterKey == parameterKey);
                if (query.Count<ProjectParameter>() > 0)
                {
                    Parameter parametro = (Parameter)this.Parameters.Where(p => p.ParameterKey == parameterKey).First<ProjectParameter>();
                    if (parametro != null)
                    {
                        return parametro;
                    }
                }
            }

            return null;
        }

        public static List<DeployProject> GetProjects(DeployProject projectFilter, KameUser user)
        {
            KameDbContext dbContext = new KameDbContext();

            IQueryable<DeployProject> query = dbContext.Set<DeployProject>();
            

            if (projectFilter != null)
            {
                if (!string.IsNullOrEmpty(projectFilter.ProjectID))
                {
                    query = query.Where(p => p.ProjectID == projectFilter.ProjectID);
                }
            }

            if (user != null)
            {
                IQueryable<KameUser> queryUser = dbContext.Set<KameUser>();
                queryUser = queryUser.Where(u => u.UserID == user.UserID && u.Roles.Any(r => r.Administrator));

                if (queryUser.ToList<KameUser>().Count == 0)
                {
                    query = query.Where(p => p.UserID == user.UserID);
                }
            }

            query = query.OrderBy(p => p.Name);

            List<DeployProject> projectList = query.ToList<DeployProject>();
            dbContext.Dispose();
            return projectList;
        }

        public static DeployProject GetProjectById(string projectId, KameUser user)
        {
            DeployProject project = new DeployProject() { ProjectID = projectId };
            List<DeployProject> list = DeployProject.GetProjects(project, user);

            if (list.Count > 0)
            {
                project = list[0];
                project.LoadDetails();

                return project;
            }
            return null;
        }

        public void Save()
        {
            KameDbContext dbContext = new KameDbContext();

            dbContext.Save(this);
            
            if (this.Parameters != null)
            {
                foreach (ProjectParameter parameter in this.Parameters)
                {
                    dbContext.Save(parameter);
                }
            }

            if (this.Steps != null)
            {
                foreach (Step step in this.Steps)
                {
                    step.SaveStep(dbContext);
                }
            }

            if (this.DeletedSteps != null)
            {
                for (int i = this.DeletedSteps.Count - 1; i >= 0; i--)
                { 
                    Step stepToDelete = this.DeletedSteps[i];
                    stepToDelete.DeleteStep(dbContext);

                }
            }

            dbContext.SaveChanges();
            this.DeletedSteps = null;
        }

        /// <summary>
        /// 
        /// </summary>
        private void LoadDetails()
        {
            KameDbContext dbContext = new KameDbContext();

            IQueryable<ProjectParameter> queryProjectParameters = dbContext.Set<ProjectParameter>();
            queryProjectParameters = queryProjectParameters.Where<ProjectParameter>(p => p.ProjectID == this.ProjectID);
            this.Parameters = queryProjectParameters.ToList<ProjectParameter>();

            IQueryable<Step> querySteps = dbContext.Set<Step>();
            querySteps = querySteps.Where<Step>(s => s.ParentProjectID == this.ProjectID && string.IsNullOrEmpty(s.ParentStepID));
            querySteps = querySteps.OrderBy(s => s.Sequence);
            this.Steps = querySteps.ToList<Step>();

            if (this.Steps != null)
            {
                foreach (Step step in this.Steps)
                {
                    step.LoadStepDetails(dbContext);
                }
            }
        }

        public void AddProjectParameter(string parameterKey)
        {
            this.AddProjectParameter(parameterKey, string.Empty);
        }
        public void AddProjectParameter(string parameterKey, string value)
        {
            if (this.Parameters == null)
            {
                this.Parameters = new List<ProjectParameter>();
            }

            if (this.GetParameter(parameterKey) == null)
            {
                this.Parameters.Add(ProjectParameter.NewProjectParameter(parameterKey, value, string.Empty, this));
            }
        }

        
        public void DeleteStep(string stepID)
        {
            if (this.DeletedSteps == null)
            {
                this.DeletedSteps = new List<Step>();
            }
            List<Step> deletedSteps = this.DeletedSteps;
            if (this.Steps != null)
            {
                foreach (Step step in this.Steps)
                {
                    if (step.StepID == stepID)
                    {
                        this.Steps.Remove(step);
                        this.DeletedSteps.Add(step);
                        break;
                    }
                    else
                    {

                        step.DeleteStep(stepID, ref deletedSteps);
                    }
                }
            }

            this.DeletedSteps = deletedSteps;
        }

        public Step GetStepByID(string stepID)
        {
            Step targetStep = null;

            if (this.Steps != null)
            {
                foreach (Step step in this.Steps)
                {
                    if (step.StepID == stepID)
                    {
                        targetStep = step;
                        break;
                    }
                    else
                    {
                        targetStep = step.GetStepByID(stepID);
                        if (targetStep != null)
                        {
                            break;
                        }
                    }
                }
            }

            return targetStep;
        }

        public Step AddNewStep()
        {
            Step step = Step.NewStep("Novo Step", string.Empty, this, null);

            if (this.Steps == null)
            {
                this.Steps = new List<Step>();
            }
            this.Steps.Add(step);
            step.Sequence = this.Steps.Count;

            return step;
        }

		public List<string> GetExecutiosGroups()
		{
			List<string> executionGroups = new List<string>();
			foreach (Step step in this.Steps)
			{
				List<string> executionGroupsAux = step.GetExecutiosGroups();
				foreach (string executionGroup in executionGroupsAux)
				{
					if (!executionGroups.Contains(executionGroup))
					{
						executionGroups.Add(executionGroup);
					}
				}
			}

			return executionGroups;
		}

        public byte[] Serialize()
        {
            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(DeployProject), new Type[]{ typeof(ProjectParameter), typeof(Parameter), typeof(Step) });
            MemoryStream stream = new MemoryStream();
            serializer.Serialize(stream, this);

            return stream.ToArray();
        }
    }
}
