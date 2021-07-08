using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

using System.Reflection;

namespace Kame.Desktop.Entity
{
    public class KameDesktopConfig
    {
        public static KameDesktopConfig currentConfig;
        public static string currentConfigPath;
        public static KameDesktopConfig Current { get { return currentConfig; } }
        
        public List<Profile> Profiles { get; set; }
        public Profile CurrentProfile { get; set; }

        public static void LoadConfigurations(string executionPath)
        {

			if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["ConfigurationFile"]))
			{
				currentConfigPath = ConfigurationManager.AppSettings["ConfigurationFile"];
			}
			else
			{

				FileInfo fileInfo = new FileInfo(executionPath);
				currentConfigPath = fileInfo.DirectoryName;
				if (currentConfigPath[currentConfigPath.Length - 1] != '\\')
				{
					currentConfigPath += "\\";
				}
				currentConfigPath += "KameDesktopConfig.configuration";
			}

            if (File.Exists(currentConfigPath))
            {
                FileStream fs = null;
                try
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(KameDesktopConfig));
                    fs = File.OpenRead(currentConfigPath);
                    KameDesktopConfig config = (KameDesktopConfig)xmlSerializer.Deserialize(fs);

                    currentConfig = config;

                }
                catch(Exception ex)
                {

                }
                finally
                {
                    if (fs != null)
                    {
                        fs.Close();
                    }
                }
            }

            if (currentConfig == null)
            {
                currentConfig = new KameDesktopConfig();
            }
            if (currentConfig.Profiles == null)
            {
                currentConfig.Profiles = new List<Profile>();
            }
            if (currentConfig.Profiles.Count == 0)
            {
                currentConfig.Profiles.Add(new Profile() { Name = "Default" });
            }
        }

        public static void SaveConfigurations()
        {
            FileStream fw = null;
            try
            {
                if (File.Exists(currentConfigPath))
                {
                    File.Delete(currentConfigPath);
                }
                fw = File.Create(currentConfigPath);

                currentConfig.CurrentProfile = null;
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(KameDesktopConfig));
                xmlSerializer.Serialize(fw, currentConfig);

            }
            catch { }
            finally {
                if (fw != null)
                {
                    fw.Close();
                }
            }
        }

		public static string GetVersion(string kameExecutable)
		{
			FileInfo fileInfo = new FileInfo(kameExecutable);
			string versionFile = fileInfo.Directory.FullName;

			if (File.Exists(kameExecutable))
			{
				return AssemblyName.GetAssemblyName(kameExecutable).Version.ToString();
			}
			

			return string.Empty;
		}
    }
}
