using Kame.Management.Core.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kame.Management.Desktop
{
    public partial class FrmDatabaseConnect : Form
    {
        public FrmDatabaseConnect()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
            Config.FrmSelectMode.Show();
        }

        private void FrmDatabaseConnect_Load(object sender, EventArgs e)
        {
            this.cmbDatabaseType.Items.Clear();
            string type = Config.GetConfig("connectionstring-type");
            this.cmbDatabaseType.Items.Add("Selecione");
            this.cmbDatabaseType.SelectedIndex = 0;
            for (int i=0; i< Config.DataBaseTypes.List.Length; i++)
            {
                this.cmbDatabaseType.Items.Add(Config.DataBaseTypes.List[i]);
                if (Config.DataBaseTypes.List[i] == type)
                {
                    this.cmbDatabaseType.SelectedIndex = i + 1;
                }
            }

            LoadConnectionString();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (this.cmbDatabaseType.SelectedIndex < 0)
            {
                MessageBox.Show("Nenhum tipo de base de dados selecionado", "Erro ao conectar ao servidor", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            switch(this.cmbDatabaseType.Items[this.cmbDatabaseType.SelectedIndex].ToString())
            {
                case Config.DataBaseTypes.MongoDB:
                    
                    MongoDbContext.ConnectionString = this.txtConnectionString.Text;
                    MongoDbContext.IsSSL = this.chkIsSSL.Checked;
                    MongoDbContext.DatabaseName = MongoDbContext.DefaultDatabaseName;

                    try
                    {
                        MongoDbContext dbcontext = new MongoDbContext();

                        Config.DbContext = dbcontext;
                        Config.ApplicationMode = Config.AppMode.DataBase;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Erro ao conectar ao servidor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    break;
            }

            

            Config.SetConfig("connectionstring-type", this.cmbDatabaseType.Items[this.cmbDatabaseType.SelectedIndex].ToString());

            if (this.cmbDatabaseType.SelectedIndex >= 0 && this.cmbDatabaseType.Items[this.cmbDatabaseType.SelectedIndex].ToString() == Config.DataBaseTypes.MongoDB)
            {
                Config.SetConfig("mongodb-connectionstring", this.txtConnectionString.Text);
            }

            Config.SetConfig("connectionstring-ssl", this.chkIsSSL.Checked.ToString());

            Config.Save();

            this.Hide();

            FrmKameManager frmKameManager = new FrmKameManager();
            frmKameManager.Show();

        }

        private void cmbDatabaseType_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadConnectionString();
        }

        private void LoadConnectionString()
        {
            if (this.cmbDatabaseType.SelectedIndex>=0 && this.cmbDatabaseType.Items[this.cmbDatabaseType.SelectedIndex].ToString() == Config.DataBaseTypes.MongoDB)
            {
                this.txtConnectionString.Text = Config.GetConfig("mongodb-connectionstring");
            }

            string isSSl = Config.GetConfig("connectionstring-ssl");

            if (!string.IsNullOrEmpty(isSSl))
            {
                bool checado;
                if (bool.TryParse(isSSl, out checado))
                {
                    this.chkIsSSL.Checked = checado;
                }
            }
            
        }

    }
}
