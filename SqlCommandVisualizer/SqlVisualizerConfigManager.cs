using System;
using System.IO;
using System.Reflection;
using System.Xml.Serialization;

namespace SqlCommandVisualizer
{
    public class SqlVisualizerConfigManager
    {
        private readonly string _configFileName = "SqlCommandVisualizerConfigSettings.xml";

        public ConfigSettings LoadConfigSettings()
        {
            var configFileFullPath = Path.Combine(GetConfigFilePath(), _configFileName);
            if (File.Exists(configFileFullPath))
            {
                var configSettings = TryToDeserializeConfigSettings(configFileFullPath);
                return configSettings ?? new ConfigSettings();
            }
            else
            {
                return new ConfigSettings();
            }
        }

        private ConfigSettings TryToDeserializeConfigSettings(string configFileFullPath)
        {
            try
            {
                var serializer = new XmlSerializer(typeof(ConfigSettings));
                using (var fs = new FileStream(configFileFullPath, FileMode.OpenOrCreate))
                {
                    return (ConfigSettings)serializer.Deserialize(fs);
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void SaveConfigSettings(ConfigSettings configSettings)
        {
            var configFileFullPath = Path.Combine(GetConfigFilePath(), _configFileName);
            var serializer = new XmlSerializer(typeof(ConfigSettings));
            File.Delete(configFileFullPath);
            using (var fs = new FileStream(configFileFullPath, FileMode.OpenOrCreate))
            {
                serializer.Serialize(fs, configSettings);
            }
        }

        private string GetConfigFilePath()
        {
            return Path.GetDirectoryName(Assembly.GetCallingAssembly().Location);
        }
    }

    [Serializable]
    public class ConfigSettings
    {
        public bool IsMaximized { get; set; }

        public double MainWindowWidth { get; set; }

        public double MainWindowHeight { get; set; }

        public bool IsWordWrap { get; set; }

        public string SSMSFullExePath { get; set; }

        public ConfigSettings()
        {
            IsMaximized = false;
            MainWindowWidth = 600;
            MainWindowHeight = 400;
            IsWordWrap = false;
            SSMSFullExePath = @"C:\Program Files (x86)\Microsoft SQL Server\140\Tools\Binn\ManagementStudio\Ssms.exe";
        }
    }
}
