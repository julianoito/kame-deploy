using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Security;

using Kame.Desktop.Views;
using Kame.Desktop.Entity;

namespace Kame.Desktop
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            KameDesktopConfig.LoadConfigurations( Application.ExecutablePath );

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
			bool newUpdate = false;

			if (args != null)
			{
				foreach (string argument in args)
				{
					if (argument == "updated")
					{
						newUpdate = true;
						break;
					}
				}
			}

			FrmMain frmMain = new FrmMain();
			if (newUpdate)
			{
				try
				{
					frmMain.NewVersion = KameDesktopConfig.GetVersion(Application.ExecutablePath);
				}
				catch { }
			}

			Application.Run(frmMain);
        }
    }
}
