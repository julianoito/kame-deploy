using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Kame.Core.Entity;
using Kame.Management.Core.Entity;

namespace Kame.Management.Desktop
{
    public partial class FrmSelectMode : Form
    {
        public FrmSelectMode()
        {
            InitializeComponent();
            Config.FrmSelectMode = this;
        }

        private void btnDatabaseConnect_Click(object sender, EventArgs e)
        {
            if (Config.FrmDatabaseConnect == null)
            { 
                Config.FrmDatabaseConnect = new FrmDatabaseConnect();
            }
            this.Hide();
            Config.FrmDatabaseConnect.Show();
        }

        private void btnEditFile_Click(object sender, EventArgs e)
        {
            string lastFolder = Config.GetConfig("last-file-config-folder");
            if (Directory.Exists(lastFolder))
            {
                openFileDialog.InitialDirectory = lastFolder;
            }

            openFileDialog.ShowDialog();

            if (File.Exists(openFileDialog.FileName))
            {
                FileInfo file = new FileInfo(openFileDialog.FileName);

                Config.SetConfig("last-file-config-folder", file.Directory.FullName);
                Config.Save();

                try
                {
                    DeployConfig deployConfig = new DeployConfig();

                    deployConfig.DeployProject = DeployProject.LoadDeployProject(file.FullName, null);
                    deployConfig.Name = file.Name;
                    deployConfig.Id = file.FullName;

                    Config.ApplicationMode = Config.AppMode.File;
                    Config.CurrentDeployConfig = deployConfig;

                    if (Config.FrmDeployConfig == null)
                    {
                        Config.FrmDeployConfig = new FrmDeployConfig();
                    }
                }
                catch
                {
                    MessageBox.Show("Não foi possível abrir o arquivo selecionado", "Erro ao abrir arquivo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Config.FrmDeployConfig.ShowDialog();
            }
            
        }

        private void btnNewFile_Click(object sender, EventArgs e)
        {
            DeployConfig deployConfig = new DeployConfig();

            deployConfig.DeployProject = new DeployProject();
            deployConfig.Name = "Novo arquivo";
            deployConfig.Id = string.Empty;

            Config.ApplicationMode = Config.AppMode.File;
            Config.CurrentDeployConfig = deployConfig;

            if (Config.FrmDeployConfig == null)
            {
                Config.FrmDeployConfig = new FrmDeployConfig();
            }

            Config.FrmDeployConfig.ShowDialog();
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
