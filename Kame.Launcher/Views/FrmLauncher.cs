using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Reflection;
using System.IO;
using System.IO.Compression;
using System.Configuration;

using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;

namespace Kame.Launcher.Views
{
	public partial class FrmLauncher : Form
	{
		public FrmLauncher()
		{
			InitializeComponent();
		}

		private void FrmLauncher_Load(object sender, EventArgs e)
		{
			Application.DoEvents();

			FileInfo fileInfo = new FileInfo(Application.ExecutablePath);

			string filePath, directoryPath = fileInfo.Directory.FullName;
			if (directoryPath.Length > 0 && directoryPath[directoryPath.Length - 1] != '\\')
			{
				directoryPath += "\\";
			}
			filePath = directoryPath + "Kame.Desktop.exe";

			bool updated = false;
			try
			{
				updated = UpdateVersion(directoryPath, filePath);
			}
			catch (Exception ex){
				MessageBox.Show(ex.Message);
			}

			System.Diagnostics.Process p = new System.Diagnostics.Process();
			p.StartInfo.FileName = filePath;
			if (updated)
			{
				p.StartInfo.Arguments = "updated";
			}
			p.StartInfo.Verb = "runas";
			p.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
			p.StartInfo.RedirectStandardOutput = false;
			p.StartInfo.UseShellExecute = false;
			p.Start();

			Application.Exit();
		}

		private bool UpdateVersion(string directoryPath, string filePath)
		{
			string currentVersion = null;
			bool updated = false;

			if (File.Exists(filePath))
			{
				this.Text = AssemblyName.GetAssemblyName(filePath).Version.Revision.ToString();
				currentVersion = AssemblyName.GetAssemblyName(filePath).Version.ToString();

				string updatePath = ConfigurationManager.AppSettings["UpdatePath"];
				if (Directory.Exists(updatePath))
				{
					DirectoryInfo updateDirectory = new DirectoryInfo(updatePath);
					FileInfo[] updateFileList = updateDirectory.GetFiles();
					FileInfo update = null;

					foreach (FileInfo updateFile in updateFileList)
					{
						if (updateFile.Name.StartsWith("v_") && updateFile.Extension.ToUpper() == ".ZIP")
						{
							string updateVersion = updateFile.Name.Substring(2).ToUpper().Replace(".ZIP", string.Empty);

							if (CompareVersion(updateVersion, currentVersion) > 0)
							{
								currentVersion = updateVersion;
								update = updateFile;
							}
						}
					}

					if (update != null)
					{
						File.Copy(update.FullName, directoryPath + update.Name, true);

						FileStream fs = File.OpenRead(directoryPath + update.Name);
						ZipFile zf = new ZipFile(fs);

						foreach (ZipEntry zipEntry in zf)
						{
							if (!zipEntry.IsFile)
							{
								continue;           // Ignore directories
							}
							String entryFileName = zipEntry.Name;
							// to remove the folder from the entry:- entryFileName = Path.GetFileName(entryFileName);
							// Optionally match entrynames against a selection list here to skip as desired.
							// The unpacked length is available in the zipEntry.Size property.

							byte[] buffer = new byte[4096];     // 4K is optimum
							Stream zipStream = zf.GetInputStream(zipEntry);

							// Manipulate the output filename here as desired.
							String fullZipToPath = Path.Combine(directoryPath, entryFileName);
							string directoryName = Path.GetDirectoryName(fullZipToPath);
							if (directoryName.Length > 0)
								Directory.CreateDirectory(directoryName);

							// Unzip file in buffered chunks. This is just as fast as unpacking to a buffer the full size
							// of the file, but does not waste memory.
							// The "using" will close the stream even if an exception occurs.
							using (FileStream streamWriter = File.Create(fullZipToPath))
							{
								StreamUtils.Copy(zipStream, streamWriter, buffer);
							}
						}

						fs.Close();
						File.Delete(directoryPath + update.Name);
						updated = true;
					}
				}
			}

			return updated;
		}

		private int CompareVersion(string versionA, string versionB)
		{
			string[] subservionsA = versionA.Split('.'), subservionsB = versionB.Split('.');
			int compare = 0;

			for (int i = 0; i < subservionsA.Length; i++)
			{
				if (versionB.Length > i)
				{
					int subversionA, subversionB;
					if (int.TryParse(subservionsA[i], out subversionA) && int.TryParse(subservionsB[i], out subversionB))
					{
						if (subversionA > subversionB)
						{
							compare = 1;
							break;
						}
						else if (subversionA < subversionB)
						{
							compare = -1;
							break;
						}
					}
				}
			}

			return compare;

		}
	}
}
