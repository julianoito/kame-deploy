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
    public partial class KameMessageDialog : Form
    {
        private static KameMessageDialog messageDialogInstance = null;
        public static bool ShowDialog(string title, string message)
        {
            if (messageDialogInstance == null)
            {
                messageDialogInstance = new KameMessageDialog();

                messageDialogInstance.Title = title;
                messageDialogInstance.Message = message;
            }

            messageDialogInstance.SetMessagePosition();
            messageDialogInstance.ShowDialog();

            return (messageDialogInstance.DialogResult == DialogResult.OK || messageDialogInstance.DialogResult == DialogResult.Yes);
        }

        protected string Title
        {
            set { this.lblTitle.Text = value; }
        }

        protected string Message
        {
            set { this.lblMessage.Text = value; }
        }

        protected void SetMessagePosition()
        {
            this.pnlContent.SetBounds((messageDialogInstance.Width / 2) - 210, 0, 420, 248);
        }

        public KameMessageDialog()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            messageDialogInstance.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
