using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;

using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;

namespace Kame.UpdateDeploy
{
	class Program
	{
		static void Main(string[] args)
		{
			string fileToCheck = string.Empty, localFolder = string.Empty, updateFolder = string.Empty;
			if (args != null)
			{
				foreach (string argument in args)
				{
					if (argument.Contains('='))
					{ 
						string[] data =argument.Split('=');
						if (data.Length == 2)
						{
							switch (data[0])
							{ 
								case "fileToCheck":
									fileToCheck = data[1];
									break;
								case "localFolder":
									localFolder = data[1];
									break;
								case "updateFolder":
									updateFolder = data[1];
									break;
							}
						}
					}
				}
			}

			if (string.IsNullOrEmpty(fileToCheck) || string.IsNullOrEmpty(localFolder) || string.IsNullOrEmpty(updateFolder))
			{
				Console.WriteLine("Parâmetros não passados");
				Console.WriteLine("fileToCheck:" + fileToCheck);
				Console.WriteLine("localFolder:" + localFolder);
				Console.WriteLine("updateFolder:" + updateFolder);
				return;
			}

			if (!localFolder.EndsWith("\\"))
			{
				localFolder += "\\";
			}

			if (!Directory.Exists(updateFolder))
			{
				Console.WriteLine("Caminho dos arquivos de atualização não existe");
				return;
			}
			if (!updateFolder.EndsWith("\\"))
			{
				updateFolder += "\\";
			}

			if (!File.Exists(localFolder + fileToCheck))
			{
				Console.WriteLine("Arquivo de validação não existe: " + localFolder + fileToCheck);
				return;
			}

			Assembly kameAssembly = Assembly.LoadFile(localFolder + fileToCheck);
			if (kameAssembly == null)
			{
				Console.WriteLine("Assembly não encontrado");
				return;
				
			}

			string currentVersion = kameAssembly.GetName().Version.ToString();
			DirectoryInfo updateDirectory = new DirectoryInfo(updateFolder);
			FileInfo[] updateFileList = updateDirectory.GetFiles();
			bool updateExists = false;

			foreach (FileInfo updateFile in updateFileList)
			{
				if (updateFile.Name.StartsWith("v_") && updateFile.Extension.ToUpper() == ".ZIP")
				{
					string updateVersion = updateFile.Name.Substring(2).ToUpper().Replace(".ZIP", string.Empty);

					if (updateVersion == currentVersion)
					{
						updateExists = true;
					}
				}
			}

			if (!updateExists)
			{
				FileStream fs = File.Create(updateFolder + "v_" + currentVersion  + ".zip");
				ZipOutputStream zipStream = new ZipOutputStream(fs);

				string[] files = Directory.GetFiles(localFolder);

				foreach (string filename in files)
				{
					FileInfo fi = new FileInfo(filename);

					if (fi.Extension.ToUpper() == ".EXE" || fi.Extension.ToUpper() == ".DLL" || fi.Extension.ToUpper() == ".MANIFEST" || fi.Extension.ToUpper() == ".CONFIG")
					{
						string entryName = fi.Name;
						entryName = ZipEntry.CleanName(entryName); // Removes drive from name and fixes slash direction
						ZipEntry newEntry = new ZipEntry(entryName);
						newEntry.DateTime = fi.LastWriteTime; // Note the zip format stores 2 second granularity
						newEntry.Size = fi.Length;

						zipStream.PutNextEntry(newEntry);

						// Zip the file in buffered chunks
						// the "using" will close the stream even if an exception occurs
						byte[] buffer = new byte[4096];
						using (FileStream streamReader = File.OpenRead(filename))
						{
							StreamUtils.Copy(streamReader, zipStream, buffer);
						}
						zipStream.CloseEntry();
					}
				}

				zipStream.IsStreamOwner = true;
				zipStream.Close();
				Console.WriteLine("Arquivo v_" + currentVersion + ".zip gerado");
			}
		}
	}
}
