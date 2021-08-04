using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

using Kame.Core.Entity;

namespace Kame.Management.Desktop
{
    public partial class FrmDeployConfig : Form
    {
        private Dictionary<string, Step> _treeStepMap;
        private Dictionary<string, TreeNode> _treeNodeMap;
        public FrmDeployConfig()
        {
            InitializeComponent();
        }

        private void ListDeployConfig()
        {
            this.lblId.Visible = Config.ApplicationMode != Config.AppMode.File;
            this.txtID.Visible = Config.ApplicationMode != Config.AppMode.File;
            this.txtName.Enabled = Config.ApplicationMode != Config.AppMode.File;

            this.tvSteps.ImageList = this.iconImgList;

            tvSteps.Nodes.Clear();
            _treeStepMap = new Dictionary<string, Step>();
            _treeNodeMap = new Dictionary<string, TreeNode>();

            if (Config.CurrentDeployConfig != null)
            {
                this.txtID.Text = Config.CurrentDeployConfig.Id;
                this.txtName.Text = Config.CurrentDeployConfig.Name;

                if (Config.CurrentDeployConfig.DeployProject.Steps != null)
                {
                    for (int i = 0; i < Config.CurrentDeployConfig.DeployProject.Steps.Count; i++)
                    {
                        AddStepToTreeView(Config.CurrentDeployConfig.DeployProject.Steps[i], this.tvSteps.Nodes);
                    }
                }
            }

            ToolStripMenuItem menu = new ToolStripMenuItem();
            menu.Text = "Adicionar Step";
            menu.Click += btnAddStep_Click;
            this.tvSteps.ContextMenuStrip = new ContextMenuStrip();
            this.tvSteps.ContextMenuStrip.Items.Add(menu);
            this.tvSteps.ExpandAll();

            this.btnSaveAs.Visible = Config.ApplicationMode == Config.AppMode.File;
        }

        private string GetStepDescription(Step step)
        {
            return step.Name + (string.IsNullOrEmpty(step.ExecutionGroup) ? string.Empty : "  (Grupo " + step.ExecutionGroup + ")");
        }

        private int GetStepIconIndex(Step step)
        {
            if (!string.IsNullOrEmpty(step.ProcessClass))
            {
                for (int i = 0; i < Config.ProcessoClassList.Count; i++)
                {
                    if (step.ProcessClass == Config.ProcessoClassList[i].FullClassName && Config.ProcessoClassList[i].IconIndex.HasValue)
                    {
                        return Config.ProcessoClassList[i].IconIndex.Value;
                    }
                }
            }
            return 0;
        }

        private void AddStepToTreeView(Step step, TreeNodeCollection nodeList)
        {
            TreeNode treeNode = new TreeNode();
            treeNode.Name = step.Name;
            treeNode.Text = this.GetStepDescription(step);
            treeNode.Tag = Guid.NewGuid().ToString();
            treeNode.ContextMenuStrip = new ContextMenuStrip();

            treeNode.ImageIndex = GetStepIconIndex(step);
            treeNode.SelectedImageIndex = treeNode.ImageIndex;

            ToolStripMenuItem menu = new ToolStripMenuItem();
            menu.Text = "Editar";
            menu.Click += btnEditStep_Click;
            menu.Tag = treeNode.Tag;
            treeNode.ContextMenuStrip.Items.Add(menu);
            

            menu = new ToolStripMenuItem();
            menu.Text = "Adicionar Step filho";
            menu.Click += btnAddStep_Click;
            menu.Tag = treeNode.Tag;
            treeNode.ContextMenuStrip.Items.Add(menu);

            menu = new ToolStripMenuItem();
            menu.Text = "Excluir";
            menu.Click += btnDeleteStep_Click;
            menu.Tag = treeNode.Tag;
            treeNode.ContextMenuStrip.Items.Add(menu);

            _treeStepMap.Add(treeNode.Tag.ToString(), step);
            _treeNodeMap.Add(treeNode.Tag.ToString(), treeNode);

            nodeList.Add(treeNode);
            if (step.ChildSteps != null)
            {
                for (int i = 0; i < step.ChildSteps.Count; i++)
                {
                    AddStepToTreeView(step.ChildSteps[i], treeNode.Nodes);
                }
            }
        }

        private void SaveFile(string fileName)
        {

            if (string.IsNullOrEmpty(fileName))
            {
                string lastFolder = Config.GetConfig("last-file-config-folder");

                if (Directory.Exists(lastFolder))
                {
                    saveFileDialog.InitialDirectory = lastFolder;
                }

                DialogResult result = saveFileDialog.ShowDialog();
                if (result == DialogResult.Cancel)
                {
                    return;
                }

                fileName = saveFileDialog.FileName;
            }
            FileInfo fileInfo = new FileInfo(fileName);
            Config.SetConfig("last-file-config-folder", fileInfo.Directory.FullName);

            string content = Config.CurrentDeployConfig.DeployProject.Serialize();

            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
            File.WriteAllText(fileName, content, Encoding.Unicode);

            this.txtName.Text = fileInfo.Name;
            this.txtID.Text = fileInfo.FullName;
            this.btnSaveAs.Visible = true;

            MessageBox.Show("Arquivo salvo com sucesso.", "Kame Deploy", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddStep_Click(object sender, EventArgs e)
        {
            if (Config.FrmStep == null)
            {
                Config.FrmStep = new FrmStep();
            }

            Config.FrmStep.Step = new Step();
            Config.FrmStep.ShowDialog();


            if (Config.FrmStep.SaveStep)
            {
                string tag = ((ToolStripMenuItem)sender).Tag as string;
                if (!string.IsNullOrEmpty(tag) && this._treeStepMap.ContainsKey(tag))
                {
                    Step parentStep = this._treeStepMap[tag];
                    if (parentStep.ChildSteps == null)
                    {
                        parentStep.ChildSteps = new List<Step>();
                    }
                    parentStep.ChildSteps.Add(Config.FrmStep.Step);
                    AddStepToTreeView(Config.FrmStep.Step, _treeNodeMap[tag].Nodes);
                    _treeNodeMap[tag].Expand();
                }
                else
                {
                    if (Config.CurrentDeployConfig.DeployProject.Steps == null)
                    {
                        Config.CurrentDeployConfig.DeployProject.Steps = new List<Step>();
                    }

                    Config.CurrentDeployConfig.DeployProject.Steps.Add(Config.FrmStep.Step);
                    AddStepToTreeView(Config.FrmStep.Step, tvSteps.Nodes);
                }
            }
            
        }

        private void FrmDeployConfig_Shown(object sender, EventArgs e)
        {
            ListDeployConfig();
        }

        private void btnEditStep_Click(object sender, EventArgs e)
        {
            string tag = ((ToolStripMenuItem)sender).Tag as string;

            if (Config.FrmStep == null)
            {
                Config.FrmStep = new FrmStep();
            }

            Config.FrmStep.Step = this._treeStepMap[tag];
            Config.FrmStep.ShowDialog();

            if (Config.FrmStep.SaveStep)
            {
                _treeNodeMap[tag].Text = this.GetStepDescription(Config.FrmStep.Step);
                _treeNodeMap[tag].ImageIndex = GetStepIconIndex(Config.FrmStep.Step);
                _treeNodeMap[tag].SelectedImageIndex = _treeNodeMap[tag].ImageIndex;
            }
        }

        private void btnDeleteStep_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            switch (Config.ApplicationMode)
            {
                case Config.AppMode.File:
                    SaveFile(Config.CurrentDeployConfig!=null ? Config.CurrentDeployConfig.Id : string.Empty );
                    break;
            }
        }

        private void btnSaveAs_Click(object sender, EventArgs e)
        {
            switch (Config.ApplicationMode)
            {
                case Config.AppMode.File:
                    SaveFile(string.Empty);
                    break;
            }
        }

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        private void lblTitle_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
