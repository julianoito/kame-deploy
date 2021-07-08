using System;
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
    public partial class FrmMain : Form
    {
		private Goku goku = null;
		private PictureBox picGoku = null;
        private FrmProjectData frmAddProject = null;
        private FrmExecution frmExecution = null;
		private string newVersion = string.Empty;

		public string NewVersion
		{
			set { this.newVersion = value; }
		}

        public FrmMain()
        {
            InitializeComponent();
        }

        #region MinimizedIcon

        const int WS_MINIMIZEBOX = 0x20000;
        const int CS_DBLCLKS = 0x8;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.Style |= WS_MINIMIZEBOX;
                cp.ClassStyle |= CS_DBLCLKS;
                return cp;
            }
        }

        #endregion

        #region Events

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmMain_Shown(object sender, EventArgs e)
        {
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
			Application.DoEvents();
			this.Refresh();
            cmdProfile.Items.Clear();
            foreach (Profile profile in KameDesktopConfig.Current.Profiles)
            {
                cmdProfile.Items.Add(profile.Name);
            }
            cmdProfile.SelectedIndex = 0;

			if (!string.IsNullOrEmpty(this.newVersion))
			{
				this.lblUpdatedVersion.Text = "Kame atualizado para a versão " + this.newVersion;
				this.pnlUpdate.Visible = true;
			}
			Application.DoEvents();
			this.Refresh();
        }

        private void cmdProfile_SelectedIndexChanged(object sender, EventArgs e)
        {
            KameDesktopConfig.Current.CurrentProfile = KameDesktopConfig.Current.Profiles[cmdProfile.SelectedIndex];
            this.ShowProfile();
        }

        private void EditProject(ProjectShortcut project)
        {
            if (frmAddProject == null)
            {
                frmAddProject = new FrmProjectData();
            }

            frmAddProject.EditProjectShortcut(project);

            this.ShowProfile();
        }

        private void ExecuteProject(ProjectShortcut project, ExecutionMode executionMode, string executionGroup)
        {
            if (this.frmExecution==null)
            {
                this.frmExecution = new FrmExecution();
            }

			this.Hide();
            frmExecution.ExecuteProject(project, executionMode);
			this.Show();
        }

        #endregion

        #region Private Methods

        private void ShowProfile()
        {
            pnlProjects.Controls.Clear();

            int positionX = 1, positionY = 1, painelWidth = 245, painelHeight = 180;
            foreach (ProjectShortcut project in KameDesktopConfig.Current.CurrentProfile.Projects)
            {
                ProjectShortcutButton projectButton = new ProjectShortcutButton();
                projectButton.SetProjectShortcut(project);
                pnlProjects.Controls.Add(projectButton);
                projectButton.SetBounds(positionX, positionY, painelWidth, painelHeight);
                projectButton.OnEdit = new OnEdit(EditProject);
                projectButton.OnExecute = new OnExecute(ExecuteProject);

                positionX += painelWidth;
                if (positionX >= 980)
                {
                    positionX = 1;
                    positionY += painelHeight;
                }
            }
        }

        #endregion

		private void lblGoku_Click(object sender, EventArgs e)
		{
			if ((Control.ModifierKeys & Keys.Shift) != Keys.None)
			{
				if (this.picGoku == null)
				{
					this.picGoku = new PictureBox();
					this.Controls.Add(picGoku);
					picGoku.SetBounds(0, 255, 1024, 90);
					picGoku.BringToFront();
				}
				if (this.goku == null)
				{
					this.goku = new Goku(this.picGoku);
				}
				this.goku.Start();
			}
		}

		private void btnCloseUpdate_Click(object sender, EventArgs e)
		{
			this.pnlUpdate.Visible = false;
		}

		private void FrmMain_Activated(object sender, EventArgs e)
		{
			this.Refresh();
		}
    }
}
