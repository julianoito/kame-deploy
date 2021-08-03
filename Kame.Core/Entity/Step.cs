using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Xml.Serialization;
using System.Text;

using Kame.Core.Entity.Log;

namespace Kame.Core.Entity
{
    [Serializable]
    public class Step : BaseEntity
    {
        #region Atributos

        public string StepID { get; set; }
        public string Name { get; set; }

        [XmlIgnore]
        public DeployProject Project { get; set; }

        [XmlIgnore]
        public Step ParentStep {get;set;}

        public List<StepParameter> Parameters { get; set; }
        public List<Step> ChildSteps { get; set; }
        public string ProcessClass { get; set; }
        public int Sequence { get; set; }
        public string ParentStepID { get; set; }
        public string ParentProjectID { get; set; }

        private IStepProcessor _processador = null;

        protected IStepProcessor Processador
        {
            get 
            {
                if (this._processador == null)
                {
                    if (string.IsNullOrEmpty(this.ProcessClass))
                    {
                        return null;
                    }

                    try
                    {
                        this._processador = (IStepProcessor)Activator.CreateInstance(Type.GetType(this.ProcessClass));
                    }
                    catch
                    {
                        return null;
                    }
                }

                return this._processador;
            }
        }

        #endregion

        #region Constructor

        public Step() :base()
        {
        
        }

        public static Step NewStep(string name, string processClass, DeployProject project, Step parentStep)
        {
            return new Step() { 
                StepID = Guid.NewGuid().ToString()
                ,Name = name
                ,ProcessClass = processClass
                ,Project = project
                ,ParentProjectID = (project == null ? string.Empty : project.ProjectID)
                ,ParentStep = parentStep
                ,ParentStepID = (parentStep == null ? string.Empty : parentStep.StepID) };
        }

        #endregion

        #region Parameters

        public string ExecutionGroup {get;set;}

        public Parameter GetParameter(string key)
        {
            return this.GetParameter(key, false);
        }
        public Parameter GetParameter(string key, bool ignoreParentParameters)
        {
            Parameter parameter = null;
            if (this.Parameters != null)
            {
                var query = this.Parameters.Where(p => p.ParameterKey == key);
                if (query.Count<StepParameter>() > 0)
                {
                    parameter = this.Parameters.Where(p => p.ParameterKey == key).First<StepParameter>();
                    if (parameter != null)
                    {
                        return parameter;
                    }
                }
            }

            if (!ignoreParentParameters)
            {
                if (parameter == null && this.ParentStep != null)
                {
                    parameter = ParentStep.GetParameter(key, ignoreParentParameters);
                }

                if (parameter == null && this.Project != null)
                {
                    parameter = Project.GetParameter(key);
                }
            }

            return parameter;
        }

        public void AddParameter(string parameterKey)
        {
            if (this.Parameters == null)
            {
                this.Parameters = new List<StepParameter>();
            }

            if (this.GetParameter(parameterKey, true) == null)
            {
                this.Parameters.Add(StepParameter.NewStepParameter(parameterKey, string.Empty, string.Empty) );
            }
        }

        public void AddParentStepParameter(string parameterKey, string value)
        {
            if (this.ParentStep != null && this.ParentStep.Parameters!=null)
            {
                if (this.ParentStep.GetParameter(parameterKey, true) == null)
                {
                    this.ParentStep.Parameters.Add(StepParameter.NewStepParameter(parameterKey, value, string.Empty));
                }
            }
            else if (this.Project != null)
            {
                this.Project.AddProjectParameter(parameterKey, value);
            }
        }

        public Step AddNewStep()
        {
            Step step = Step.NewStep("Novo Step", string.Empty, this.Project, this);

            if (this.ChildSteps == null)
            {
                this.ChildSteps = new List<Step>();
            }
            this.ChildSteps.Add(step);
            step.Sequence = this.ChildSteps.Count;

            return step;
        }

        private bool CheckExecutionGroup(List<string> executionGroups)
        {
            if (executionGroups == null || executionGroups.Count == 0 || string.IsNullOrEmpty(ExecutionGroup))
            {
                return true;
            }

            foreach (string executiogGroup in executionGroups)
            {
                if (executiogGroup == this.ExecutionGroup)
                {
                    return true;
                }
            }

            return false;
        }

		public List<string> GetExecutiosGroups()
		{
			List<string> executionGroups = new List<string>();

			if (!String.IsNullOrEmpty(this.ExecutionGroup))
			{
				executionGroups.Add(this.ExecutionGroup);
			}

			foreach (Step step in this.ChildSteps)
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

        public bool Processar(IProjectExecutionLog executionLog, DeployLog log, List<string> executionGroups, out string errorMessage)
        {
            string mensagemLog = null;
            bool stepExecuted = false;

            errorMessage = string.Empty;
            try
            {
                if (!CheckExecutionGroup(executionGroups))
                {
                    return true;
                }

                if (executionLog != null)
                {
                    executionLog.SetMessage("Processing step: " + this.Name, string.Empty);
                }
                log.StartStepLog(this);
                if (this.Processador != null)
                {
                    this.Processador.Execute(this, null, executionLog, log, out errorMessage);

                    if (!string.IsNullOrEmpty(errorMessage))
                    {
                        throw new ApplicationException(errorMessage);
                    }

                    stepExecuted = true;
                }
                else
                {
                    stepExecuted = true;
                }

                foreach (Step step in this.ChildSteps)
                {
                    step.Processar(executionLog, log, executionGroups, out errorMessage);

                    if (!string.IsNullOrEmpty(errorMessage))
                    {
                        throw new ApplicationException(errorMessage);
                    }
                }


                mensagemLog = "Step executed";
            }
            catch(Exception ex)
            {
                stepExecuted = false;
                mensagemLog = ex.Message;
            }
            finally
            {
                log.EndStepLog(this, mensagemLog);
            }
            return stepExecuted;
        }

        private static Hashtable StepTypes = new Hashtable();
        private static Dictionary<string, string> StepTypesClasses = null;
        public static Dictionary<string, string> GetStepTypes()
        {

            if (StepTypesClasses != null)
            {
                return StepTypesClasses;
            }
            StepTypesClasses = new Dictionary<string, string>();

            StepTypesClasses.Add("Default", "");
            StepTypesClasses.Add("SQL2005 Adm", "Kame.Core.Entity.SQL2005AdmStep, Kame.Core");
            StepTypesClasses.Add("SQL2005 Script", "Kame.Core.Entity.SQL2005ScriptStep, Kame.Core");
            StepTypesClasses.Add("Internet Inf. Services", "Kame.Core.Entity.IISStep, Kame.Core");
            StepTypesClasses.Add("MS Message Queue", "Kame.Core.Entity.MSMessageQueueStep, Kame.Core");
            StepTypesClasses.Add("CVS", "Kame.Core.Entity.CVS, Kame.Core");
            StepTypesClasses.Add("Leitor SVD.Properties", "Kame.ClSoftware.LeitorParametrosProjeto, Kame.ClSoftware");
            

            return StepTypesClasses;
        }

        public static IStepProcessor GetStepProcess(string name)
        {
            if (StepTypes.ContainsKey(name))
            {
                return (IStepProcessor)StepTypes[name];
            }
            else
            {
                Type processorType = Type.GetType(name);
                if (processorType != null)
                {
                    StepTypes.Add(name, Activator.CreateInstance(processorType));
                    return (IStepProcessor)StepTypes[name];
                }
            }

            return null;
        }

        //Method to redefine step parents from deserialization
        public void SetParentStep(Step parentStep)
        {
            this.ParentStep = parentStep;
            this.ParentStepID = (parentStep == null) ? string.Empty : parentStep.StepID;

            if (this.ChildSteps != null)
            {
                foreach (Step childStep in this.ChildSteps)
                {
                    childStep.SetParentStep(this);
                }
            }
        }

        public void DeleteStep(string stepID, ref List<Step> deletedSteps)
        {
            if (this.ChildSteps != null)
            {
                foreach (Step childStep in this.ChildSteps)
                {
                    if (childStep.StepID == stepID)
                    {
                        this.ChildSteps.Remove(childStep);
                        deletedSteps.Add(childStep);
                        break;
                    }
                    else
                    {
                        childStep.DeleteStep(stepID, ref deletedSteps);
                    }

                }
            }
        }

        #endregion

        #region Metodos

        public Step GetStepByID(string stepID)
        {
            Step targetStep = null;
            if (this.ChildSteps != null)
            {
                foreach (Step step in this.ChildSteps)
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

       


        #endregion
    }
}
