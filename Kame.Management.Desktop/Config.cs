using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Diagnostics;
using Kame.Management.Core.Services;
using Kame.Management.Core.Entity;

namespace Kame.Management.Desktop
{
    public class Config
    {
        public static IKameDbContext DbContext { get; set; }
        public static FrmSelectMode FrmSelectMode { get; set; }
        public static FrmDatabaseConnect FrmDatabaseConnect { get; set; }
        public static FrmDeployConfig FrmDeployConfig { get; set; }
        public static FrmStep FrmStep { get; set; }

        public static FrmUser FrmUser { get; set; }
        public static byte ApplicationMode { get; set; }

        public static DeployConfig CurrentDeployConfig { get; set; }

        private static string EncryptionKey = "4A9E02BF-F6CD-4DDB-B073-0E4CDE521569-DD9A043F-75CD-4752-8C36-CEED590392CE";
        private static byte[] sal = { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x23, 0x56, 0x12, 0x65, 0x76 };
        private static string[] ConfigurationKeys = { "connectionstring-type",  "mongodb-connectionstring", "connectionstring-ssl", "last-file-config-folder" };
        private static Dictionary<string, string> _configurations = new Dictionary<string, string>();
        private static Dictionary<byte, List<string>> _constants = new Dictionary<byte, List<string>>();
        private static List<ProcessorClass> _processoClassList = null;

        public static List<ProcessorClass> ProcessoClassList
        {
            get
            {
                if (_processoClassList == null)
                {
                    _processoClassList = new List<ProcessorClass>();
                    List<string> processorClassConfigList = Config.GetConstant(Config.ConstantTypes.ProcessorClasses);

                    foreach (string processorConfig in processorClassConfigList)
                    {
                        if (processorConfig.Contains("|"))
                        {
                            string[] processorData = processorConfig.Split('|');
                            ProcessorClass processorClass = new ProcessorClass();
                            processorClass.Name = processorData[0];
                            processorClass.FullClassName = processorData[1];

                            if (processorData.Length > 2)
                            {
                                byte icon;
                                if (byte.TryParse(processorData[2], out icon))
                                {
                                    processorClass.IconIndex = icon;
                                }
                                else
                                {
                                    processorClass.IconIndex = null;
                                }
                            }

                            _processoClassList.Add(processorClass);
                        }
                    }
                }

                return _processoClassList;
            }
        }

        public static string GetConfig(string key)
        {
            if (_configurations.ContainsKey(key))
            {
                return _configurations[key];
            }
            return string.Empty;
        }

        public static void SetConfig(string key, string value)
        {
            if (_configurations.ContainsKey(key))
            {
                _configurations[key] = value;
            }
            else
            {
                _configurations.Add(key, value);
            }
        }

        public static List<string> GetConstant(byte constantType)
        {
            if (Config._constants.ContainsKey(constantType))
            {
                return Config._constants[constantType];
            }
            else
            {
                return new List<string>();
            }
        }


        public static void Load()
        {
            string userConfigFile, configFile;
            GetFileName(out userConfigFile, out configFile);
            string configContet = string.Empty;
            _configurations.Clear();

            if (File.Exists(userConfigFile))
            {
                configContet = File.ReadAllText(userConfigFile);
            }

            if (!string.IsNullOrEmpty(configContet))
            {
                configContet = configContet.Replace(" ", "+");
                byte[] cipherBytes = Convert.FromBase64String(configContet);
                using (Aes encryptor = Aes.Create())
                {
                    Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, sal);
                    encryptor.Key = pdb.GetBytes(32);
                    encryptor.IV = pdb.GetBytes(16);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(cipherBytes, 0, cipherBytes.Length);
                            cs.Close();
                        }
                        configContet = Encoding.Unicode.GetString(ms.ToArray());
                    }
                }

                string[] configList = configContet.Split('\r');
                for (int i = 0; i < configList.Length; i++)
                {
                    for (int k = 0; k < ConfigurationKeys.Length; k++)
                    { 
                        if (configList[i].StartsWith(ConfigurationKeys[k] + "="))
                        {
                            string value = configList[i].Replace(ConfigurationKeys[k] + "=", string.Empty);
                            SetConfig(ConfigurationKeys[k], value);
                        }
                    }
                }
            }

            Config._constants.Clear();
            if (File.Exists(configFile))
            {
                string[] configLineList = File.ReadAllLines(configFile);
                byte currentConstantType = byte.MaxValue;
                foreach (string configLine in configLineList)
                {
                    switch (configLine.ToUpper().Trim())
                    {
                        case "[KAME PROCESSOR CLASS]":
                            currentConstantType = Config.ConstantTypes.ProcessorClasses;
                            break;
                        default:
                            if (!Config._constants.ContainsKey(currentConstantType))
                            {
                                Config._constants.Add(currentConstantType, new List<string>());
                            }
                            Config._constants[currentConstantType].Add(configLine.Trim());
                            break;
                    }
                }
            }

        }

        public static void Save()
        {
            string userConfigFile, configFile;
            GetFileName(out userConfigFile, out configFile);
            string configuration = string.Empty;

            for (int i = 0; i < ConfigurationKeys.Length; i++)
            {
                configuration += ConfigurationKeys[i] + "=" + GetConfig(ConfigurationKeys[i]) + "\r";
            }

            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, sal);
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                byte[] clearBytes = Encoding.Unicode.GetBytes(configuration);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    configuration = Convert.ToBase64String(ms.ToArray());
                }
            }

            if (File.Exists(userConfigFile))
            {
                File.Delete(userConfigFile);
            }
            File.WriteAllText(userConfigFile, configuration);
        }

        private static void GetFileName(out string userConfigFile, out string configFile)
        {
            userConfigFile = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
            if (!userConfigFile.EndsWith("\\"))
            {
                userConfigFile += "\\";
            }
            configFile = userConfigFile + "consts.ini";
            userConfigFile += "config.ini";
        }

        public class DataBaseTypes
        {
            public const string MongoDB = "Mongo DB";

            public static string[] List
            {
                get 
                { 
                    return new string[] { DataBaseTypes.MongoDB };
                }
            }
        }

        public class AppMode
        {
            public const byte DataBase = 1;
            public const byte API = 2;
            public const byte File = 3;
        }

        public class ConstantTypes
        {
            public const byte ProcessorClasses = 1;
        }

        public class ProcessorClass
        { 
            public string Name { get; set; }
            public string FullClassName { get; set; }
            public byte? IconIndex { get; set; }
        }
    }

    
}
