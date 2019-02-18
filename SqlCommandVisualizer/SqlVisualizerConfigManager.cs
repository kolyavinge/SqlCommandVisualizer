using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SqlCommandVisualizer
{
    public class SqlVisualizerConfigManager
    {
        private string _configFileName = "SqlCommandVisualizerConfigSettings.xml";

        public ConfigSettings LoadConfigSettings()
        {
            var configFileFullPath = Path.Combine(GetConfigFilePath(), _configFileName);
            if (File.Exists(configFileFullPath))
            {
                var serializer = new XmlSerializer(typeof(ConfigSettings));
                using (var fs = new FileStream(configFileFullPath, FileMode.OpenOrCreate))
                {
                    return (ConfigSettings)serializer.Deserialize(fs);
                }
            }
            else
            {
                return new ConfigSettings();
            }
        }

        public void SaveConfigSettings(ConfigSettings configSettings)
        {
            var configFileFullPath = Path.Combine(GetConfigFilePath(), _configFileName);
            var serializer = new XmlSerializer(typeof(ConfigSettings));
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

        public ConfigSettings()
        {
            IsMaximized = false;
            MainWindowWidth = 600;
            MainWindowHeight = 400;
            IsWordWrap = false;
        }
    }
}
