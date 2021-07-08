using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Kame.Desktop.Views
{
    public partial class FrmLog : Form
    {
        public FrmLog()
        {
            InitializeComponent();
        }

        public void ShowLog(string filename)
        {
            string fileUrl = "file:///" + filename.Replace("\\", "/");
            this.webBrowser.Navigate(fileUrl);
            this.ShowDialog();
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
