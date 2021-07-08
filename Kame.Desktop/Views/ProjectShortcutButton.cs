using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Kame.Desktop.Entity;

namespace Kame.Desktop.Views
{
    public delegate void OnEdit(ProjectShortcut project);
    public delegate void OnExecute(ProjectShortcut project, ExecutionMode executionMode, string executionGroup);

    public partial class ProjectShortcutButton : UserControl
    {
        public OnEdit OnEdit { get; set; }
        public OnExecute OnExecute { get; set; }

        private ProjectShortcut Project = null;

        public ProjectShortcutButton()
        {
            InitializeComponent();
        }

        public void SetProjectShortcut(ProjectShortcut project)
        {
            this.Project = project;
            this.lblProjectName.Text = this.Project.Name;
            if (!String.IsNullOrEmpty(this.Project.ImagePath))
            {
                imgProjectLogo.ImageLocation = this.Project.ImagePath;
            }

            this.pnlProject.BackColor = this.Project.BackgroudColor;

        }

		private void ProjectShortcutButton_Click(object sender, EventArgs e)
		{
			this.OnExecute(Project, ExecutionMode.Normal, string.Empty);
		}
    }
}
