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
    public partial class FrmUser : Form
    {
        public FrmUser()
        {
            InitializeComponent();
        }

        public User User;


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
            this.Hide();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtName.Text.Trim()))
            {
                MessageBox.Show("O nome usuário não preenchido", "Erro na validação do Usuário", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if ((this.User==null || this.chkChangePassword.Checked) && string.IsNullOrEmpty(this.txtPassword.Text.Trim()))
            {
                MessageBox.Show("A senha do usuário não preenchido", "Erro na validação do Usuário", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (this.cmbProfile.SelectedIndex<0)
            {
                MessageBox.Show("Perfil do usuário não selecionado", "Erro na validação do Usuário", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (this.User == null)
            {
                this.User = new User();
            }

            this.User.Name = this.txtName.Text;
            if (this.chkChangePassword.Checked || string.IsNullOrEmpty(this.User.Id))
            {
                this.User.Password = User.CreateSha512Password(this.txtPassword.Text);
            }
            this.User.Profile = this.cmbProfile.SelectedIndex;

            Config.DbContext.SaveUser(this.User);

            this.Hide();
        }

        private void FrmUser_Shown(object sender, EventArgs e)
        {
            this.txtPassword.Text = string.Empty;
            if (this.User != null)
            {
                this.txtName.Text = this.User.Name;
                this.cmbProfile.SelectedIndex = this.User.Profile;
                this.chkChangePassword.Visible = true;
                this.chkChangePassword.Checked = false;
                this.txtPassword.Visible = false;
                this.txtPassword.SetBounds(295, 73, 363, 23); 
            }
            else
            {
                this.txtName.Text = string.Empty;
                this.cmbProfile.SelectedIndex = 0;
                this.chkChangePassword.Visible = false;
                this.txtPassword.SetBounds(179, 73, 479, 23);
            }
        }

        private void chkChangePassword_CheckedChanged(object sender, EventArgs e)
        {
            this.txtPassword.Visible = this.chkChangePassword.Checked;
        }
    }
}
