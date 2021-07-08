using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kame.Desktop.Entity
{
    public class Profile
    {
        public string Name { get; set; }
		public string ProjectExecutionDescription { get; set; }
        public List<ProjectShortcut> Projects { get; set; }

        public Profile()
        {
            this.Projects = new List<ProjectShortcut>();
        }
    }
}
