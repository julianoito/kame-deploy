using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

using Kame.Core.Entity;

namespace Kame.Desktop.Entity
{
    public class ProjectShortcut
    {
        public string Filename { get; set; }
        public string Workspace { get; set; }
        public string LogFileToReplace { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
		public string ProjectExecutionDescription { get; set; }
		public List<string> RequiredParameters { get; set; }

        [XmlIgnore]
        public System.Drawing.Color BackgroudColor
        {
            get {
                return System.Drawing.Color.FromArgb(ARGBBackgroundColor);
            }
            set 
            {
                this.ARGBBackgroundColor = value.ToArgb();
            }
        }

        public int ARGBBackgroundColor {get; set;}

        public string GetProjectName()
        {
            string projectName = string.Empty;
            DeployProject deployProject = null;

            if (!File.Exists(this.Filename))
            {
                throw new ApplicationException("Arquivo não encontrado");
            }

            try
            {
                deployProject = DeployProject.LoadDeployProject(Filename, null);
            }
            catch{
                throw new ApplicationException("O Arquivo não e um arquivo de projeto válido");
            }

            return deployProject.Name;
        }

		public string GetProjectDescription()
		{ 
			string projectDEscripton = string.Empty;
			try{
				projectDEscripton = File.ReadAllText(this.ProjectExecutionDescription.Trim());
			}
			catch{}

			return projectDEscripton;
		}
    }
}
