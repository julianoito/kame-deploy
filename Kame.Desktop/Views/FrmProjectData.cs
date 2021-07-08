using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Kame.Desktop.Entity;

namespace Kame.Desktop.Views
{
    public partial class FrmProjectData : Form
    {
        public ProjectShortcut ProjectShortcut {get; set;}

        public FrmProjectData()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.ProjectShortcut = null;
            this.Close();
        }

        private void btnAddProject_Click(object sender, EventArgs e)
        {
			this.SaveParameterFile(this.ProjectShortcut, this.txtParameters.Text);

            this.Close();
        }

        public void EditProjectShortcut(ProjectShortcut project)
        {
            this.ProjectShortcut = project;

			this.txtParameters.Text = OpenParameterFile(project);

            this.btnAddProject.Text = "Salvar";

            this.ShowDialog();
        }

		private string OpenParameterFile(ProjectShortcut project)
		{
			string baseDirectory, filename;
			GetParameterFilename(project, out baseDirectory, out filename);

			if (File.Exists(filename))
			{
				return File.ReadAllText(filename, Encoding.UTF8);
			}
			return string.Empty;
		}

		private void SaveParameterFile(ProjectShortcut project, string parameters)
		{
			string baseDirectory, filename;
			GetParameterFilename(project, out baseDirectory, out filename);

			if (File.Exists(filename))
			{
				File.Delete(filename);
			}

			if (!Directory.Exists(baseDirectory + "localdata"))
			{
				Directory.CreateDirectory(baseDirectory + "localdata");
			}

			StreamWriter sw = File.CreateText(filename);
			sw.Write(parameters);
			sw.Close();
		}

		private void GetParameterFilename(ProjectShortcut project, out string baseDirectory, out string filename)
		{
			FileInfo fileInfo = new FileInfo(Application.ExecutablePath);
			baseDirectory = fileInfo.DirectoryName;

			if (baseDirectory[baseDirectory.Length - 1] != '\\')
			{
				baseDirectory += "\\";
			}
			filename =baseDirectory + "localdata\\" + project.Name.Replace(" ", "_") + "_parameters.txt";
		}

    }
}
