using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Kame.Desktop.Entity
{
	public class UserConfig
	{
		private enum ConfigurationSection
		{ 
			ExecutionParameters = 1
		}

		public Dictionary<string, string> ExecutionParameters { get; set; }
		private static string ConfigurationFilePath {
			get {
				string filename = AppDomain.CurrentDomain.BaseDirectory;
				if (!filename.EndsWith("\\"))
				{
					filename += "\\";
				}
				filename += "usr.cfg";

				return filename;
			}
		}
		public static UserConfig LoadLocalConfig()
		{
			UserConfig userConfig = new UserConfig();
			userConfig.ExecutionParameters = new Dictionary<string, string>();

			if (File.Exists(ConfigurationFilePath))
			{
				StreamReader sr = null;
				try
				{
					sr = new StreamReader(File.OpenRead(ConfigurationFilePath));
					ConfigurationSection section = 0;
					while (!sr.EndOfStream)
					{
						string linha = sr.ReadLine();

						if (linha.StartsWith("["))
						{
							if (linha.Trim() == "[ExecutionParameters]")
							{
								section = ConfigurationSection.ExecutionParameters;
							}
						}
						else
						{
							string[] keyValue = linha.Split('=');
							switch (section)
							{
								case ConfigurationSection.ExecutionParameters:
									if (keyValue.Length == 2 && !userConfig.ExecutionParameters.ContainsKey(keyValue[0]) && !string.IsNullOrEmpty(keyValue[0]) && !string.IsNullOrEmpty(keyValue[1]))
									{
										userConfig.ExecutionParameters.Add(keyValue[0], keyValue[1]);
									}
									break;
							}
						}
					}
				}
				catch
				{
				}
				finally 
				{
					if (sr != null)
					{
						sr.Close();
					}
				}
			}

			return userConfig;
		}

		public void Save()
		{
			if (File.Exists(ConfigurationFilePath))
			{
				File.Delete(ConfigurationFilePath);
			}

			StreamWriter sw = null;

			try
			{
				sw = File.CreateText(ConfigurationFilePath);

				if (this.ExecutionParameters.Count > 0)
				{
					sw.WriteLine("[ExecutionParameters]");
					foreach (KeyValuePair<string, string> parameter in this.ExecutionParameters)
					{
						if (!string.IsNullOrEmpty(parameter.Value))
						{
							sw.WriteLine(parameter.Key.ToString().Trim() + "=" + parameter.Value.ToString().Trim());
						}
					}
				}
			}
			catch { }
			finally
			{
				if (sw != null)
				{
					sw.Close();
				}
			}
		}
	}
}
