using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kame.Core.Entity
{
    [Serializable]
    public class ProjectParameter : Parameter
    {
        public virtual string ProjectID { get; set; }
        public virtual DeployProject Project { get; set; }

        protected ProjectParameter() : base()
        {
        }

        public static ProjectParameter NewProjectParameter()
        {
            return new ProjectParameter() {  ParameterID = Guid.NewGuid().ToString() };
        }

        public static ProjectParameter NewProjectParameter(string parameterKey, string parameterValue, string parameterDescription, DeployProject project)
        {
            return new ProjectParameter() { 
                ParameterID = Guid.NewGuid().ToString()
                ,ParameterKey = parameterKey
                ,ParameterValue = parameterValue
                ,ParameterDescription = parameterDescription
                ,Project = project
                ,ProjectID = (project!=null ? project.ProjectID : string.Empty)
            };
        }
    }
}
