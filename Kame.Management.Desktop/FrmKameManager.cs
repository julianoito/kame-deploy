using Kame.Management.Core.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Kame.Management.Desktop
{
    public partial class FrmKameManager : Form
    {
        private Label[] _menuList;
        private List<User> _userList;
        private List<DeployConfig> _depoloyList;
        private string _currentMenu;
        public FrmKameManager()
        {
            InitializeComponent();
            _menuList = new Label[] { lblMenuDeploys, lblMenuUsers };
        }

        private void FrmKameManager_Load(object sender, EventArgs e)
        {
            MenuSelect(0);
        }

        private void btnDeploys_Click(object sender, EventArgs e)
        {
            MenuSelect(0);
        }

        private void lblMenuDeploys_Click(object sender, EventArgs e)
        {
            MenuSelect(0);
        }

        private void btnUsers_Click(object sender, EventArgs e)
        {
            MenuSelect(1);
        }

        private void lblMenuUsers_Click(object sender, EventArgs e)
        {
            MenuSelect(1);
        }

        private void MenuSelect(int menu)
        {
            _currentMenu = string.Empty;
            for (int i = 0;  i < _menuList.Length; i++)
            {
                if (i == menu)
                {
                    _menuList[i].Font = new Font(_menuList[i].Font.Name, _menuList[i].Font.SizeInPoints, FontStyle.Bold | FontStyle.Underline);
                    _currentMenu = _menuList[i].Tag.ToString();
                }
                else
                {
                    _menuList[i].Font = new Font(_menuList[i].Font.Name, _menuList[i].Font.SizeInPoints, FontStyle.Regular);
                    

                }
            }

            switch (_currentMenu)
            {
                case "Deploys":
                    ListDeploys();
                    break;
                case "Users":
                    ListUsers();
                    break;
            }
        }

        private void ListDeploys()
        {
            lstData.Items.Clear();

            switch (Config.ApplicationMode)
            {
                case Config.AppMode.DataBase:
                    if (!Config.DbContext.CheckDeployConfigTable(false))
                    {
                        DialogResult diagResult = MessageBox.Show("A tabela de configuração de deploys não existe. Deseja cria-la?", "Erro ao se conectar ao banco", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (diagResult == DialogResult.Yes)
                        {
                            Config.DbContext.CheckDeployConfigTable(true);
                        }
                    }

                    try
                    {
                        _depoloyList = Config.DbContext.GetDeployConfigs();
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show("Erro ao obter as configurações de deploy: \r\n" + ex.Message, "Erro ao se conectar ao banco", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;
            }
        }

        private void ListUsers()
        {
            lstData.Items.Clear();
            _userList = null;
            switch (Config.ApplicationMode)
            {
                case Config.AppMode.DataBase:
                    if (!Config.DbContext.CheckUserTable(false))
                    {
                        DialogResult diagResult = MessageBox.Show("A tabela de usuários não existe. Deseja cria-la?", "Erro ao se conectar ao banco", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (diagResult == DialogResult.Yes)
                        {
                            Config.DbContext.CheckUserTable(true);
                        }
                    }

                    try
                    {
                        _userList = Config.DbContext.GetUsers();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro ao obter os usuários: \r\n" + ex.Message, "Erro ao se conectar ao banco", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    
                    break;
            }

            if (_userList.Count == 0)
            {
                lstData.Items.Add("Nenhum usuário encontrado" );
                lstData.Enabled = false;
            }
            else
            {
                foreach (User user in _userList)
                {
                    lstData.Items.Add(user.Name);
                }
                lstData.Enabled = true;
            }
        }

        

        private void btnNewRecord_Click(object sender, EventArgs e)
        {
            switch (_currentMenu)
            {
                case "Deploys":
                    Config.CurrentDeployConfig = new DeployConfig();
                    Config.CurrentDeployConfig.DeployProject = new Kame.Core.Entity.DeployProject();

                    if (Config.FrmDeployConfig == null)
                    {
                        Config.FrmDeployConfig = new FrmDeployConfig();
                    }
                    Config.FrmDeployConfig.ShowDialog();
                    break;
                case "Users":
                    ListUsers();
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
            Config.FrmDatabaseConnect.Hide();
            Config.FrmSelectMode.Show();
        }


    }
}
