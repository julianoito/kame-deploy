using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using Kame.Core.Entity.Log;

namespace Kame.Core.Entity
{
    public class TextFileConfig : IStepProcessor
    {
        private string fileTemplate;
        private string destinyFile;
        private string patternType;
        private bool replaceDestinyFile;
        private bool useParameterValues;
        private string splitChar;
        private bool caseSensitive;
        private Dictionary<string, string> replacementTextList;
        private IProjectExecutionLog ExecutionLog;

        public override void Execute(Step step, List<DeployFile> ignoreList, IProjectExecutionLog executionLog, DeployLog log, out string errorMessage)
        {
            errorMessage = string.Empty;
            this.LoadParameters(step);
            this.ExecutionLog = executionLog;
            string fullPathTemplate = this.workspace, fullPathDestetnyFile;

            if (!fullPathTemplate.EndsWith("\\"))
            {
                fullPathTemplate += "\\";
            }
            fullPathDestetnyFile = fullPathTemplate + destinyFile;
            fullPathTemplate += fileTemplate;

            if (!File.Exists(fullPathTemplate))
            {
                throw new ApplicationException("File " + fullPathTemplate + " does not exists.");
            }

            if (!replaceDestinyFile && File.Exists(fullPathDestetnyFile))
            {
                throw new ApplicationException("File " + fullPathDestetnyFile + " does exists and step is set to not replace it.");
            }


            Encoding encoding;
            string fileContent = this.ReadTextFile(fullPathTemplate, out encoding);
            fileContent = fileContent.Replace("\r\n", "\r");
            string[] fileContentList = fileContent.Split('\r');

            for (int i = 0; i < fileContentList.Length; i++)
            {
                string originalContent = caseSensitive ? fileContentList[i].Trim() : fileContentList[i].ToLower().Trim();
                foreach (KeyValuePair<string, string> valueToReplace in this.replacementTextList)
                {
                    string key = caseSensitive ? valueToReplace.Key : valueToReplace.Key.ToLower();
                    if ((patternType == "startswith" && originalContent.StartsWith(key)) || (patternType == "equalto" && originalContent == key))
                    {
                        fileContentList[i] = valueToReplace.Value;
                    }
                }

            }

            if (File.Exists(fullPathDestetnyFile))
            {
                File.Delete(fullPathDestetnyFile);
            }
            File.WriteAllLines(fullPathDestetnyFile, fileContentList, encoding);
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
                    "fileTemplate"
                    , string.Empty
                    , "Relative path to the origin file")
            );

            parameters.Add(
                StepParameter.NewStepParameter(
                    "destinyFile"
                    , string.Empty
                    , "Relative path to the destiny file")
            );

            parameters.Add(
                StepParameter.NewStepParameter(
                    "patternType"
                    , string.Empty
                    , "Pattern search to content replace (startsWith, equalTo)")
            );

            parameters.Add(
                StepParameter.NewStepParameter(
                    "replaceDestinyFile"
                    , string.Empty
                    , "Replace destiny file if exists (true or false)")
            );

            parameters.Add(
                StepParameter.NewStepParameter(
                    "useParameterValues"
                    , string.Empty
                    , "Uses parameters values received on step or any parent step ( {PARAMETER_NAME} )")
            );

            parameters.Add(
                StepParameter.NewStepParameter(
                    "replacementTextList"
                    , string.Empty
                    , "List os texts to replace: Patter1|New Value 1; Patter2|New Value 2")
            );

            parameters.Add(
                StepParameter.NewStepParameter(
                    "splitChar"
                    , string.Empty
                    , "Character used to separete the replacementTextList. Default is \"|\" ")
            );

            parameters.Add(
                StepParameter.NewStepParameter(
                    "caseSensitive"
                    , string.Empty
                    , "Text pattern is case sensitive ")
            );


            return parameters;
        }

        protected virtual void LoadParameters(Step step)
        {
            base.LoadParameters(step);
            Parameter parameter;

            try
            {
                parameter = step.GetParameter("fileTemplate");
                this.fileTemplate = parameter.ParameterValue;
            }
            catch { }

            try
            {
				parameter = step.GetParameter("destinyFile");
                this.destinyFile = parameter.ParameterValue;
            }
            catch{}

            try{
                parameter = step.GetParameter("patternType");
                this.patternType = parameter.ParameterValue.ToLower();
            }
            catch{}
            
            try
            {
                parameter = step.GetParameter("replaceDestinyFile");
                this.replaceDestinyFile = bool.Parse(parameter.ParameterValue);
            }
            catch { }

            try
            {
                parameter = step.GetParameter("useParameterValues");
                this.useParameterValues = bool.Parse(parameter.ParameterValue);
            }
            catch { }

            try
            {
                parameter = step.GetParameter("splitChar");
                this.splitChar = parameter.ParameterValue.Trim()[0].ToString();
            }
            catch { }

            try
            {
                parameter = step.GetParameter("caseSensitive");
                this.caseSensitive = bool.Parse(parameter.ParameterValue);
            }
            catch { }
            

            if (string.IsNullOrEmpty(this.splitChar))
            {
                this.splitChar = "|";
            }

            this.replacementTextList = new Dictionary<string, string>();
            try
            {
                parameter = step.GetParameter("replacementTextList");
                string[] replacementList = parameter.ParameterValue.Split(';');
                foreach (string replaceText in replacementList)
                {
                    if (!string.IsNullOrEmpty(replaceText.Trim()) && replaceText.Contains(this.splitChar[0]))
                    {
                        string[] replacementData = replaceText.Trim().Split(this.splitChar[0]);

                        if (replacementData.Length == 2)
                        {
                            string key = replacementData[0].Trim();
                            string value = replacementData[1].Trim();

                            if (!this.replacementTextList.ContainsKey(key))
                            {
                                if (this.useParameterValues)
                                {
                                    bool parameterNameFound = false;
                                    string parameterName = string.Empty;
                                    List<string> parameterNameList = new List<string>();
                                    for (int i = 0; i < value.Length; i++)
                                    {
                                        if (value[i] == '{')
                                        {
                                            parameterNameFound = true;
                                        }
                                        else if (value[i] == '}')
                                        {
                                            parameterNameFound = false;
                                            if (!string.IsNullOrEmpty(parameterName.Trim()))
                                            {
                                                parameterNameList.Add(parameterName);
                                            }
                                            parameterName = string.Empty;
                                        }
                                        else if (parameterNameFound)
                                        {
                                            parameterName += value[i];
                                        }
                                    }

                                    foreach (string parameterNameAux in parameterNameList)
                                    {
                                        try
                                        {
                                            parameter = step.GetParameter(parameterNameAux);
                                            value = value.Replace("{" + parameterNameAux + "}", parameter.ParameterValue.Trim());
                                        }
                                        catch { }
                                    }
                                }
                                this.replacementTextList.Add(key, value);
                            }
                        }
                        
                    }
                }
            }
            catch { }

            Parameter parametroWorkspace = step.GetParameter("workspace");
        }

    }
}

