using log4net;
using ServiceLayer.IServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace ServiceLayer.Services
{
    public class ConfigValueService : IConfigValue
    {
        private XElement xmlData;
        private static readonly ILog Log = LogManager.GetLogger(typeof(ConfigValueService));
        public void LoadConfiguration(string filePath)
        {
            string xmlText = File.ReadAllText(filePath);
            xmlData = XElement.Parse(xmlText);
        }

        public T GetValue<T>(string key)
        {
            if (xmlData != null)
            {
                XElement element = xmlData.Element(key);
                if (element != null)
                {
                    Log.Debug("Configuration loaded successfully.");
                    return (T)Convert.ChangeType(element.Value, typeof(T));
                }
            }
            Log.Warn("Xml is null!");
            return default;
        }
    }
}
