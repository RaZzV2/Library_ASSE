namespace ServiceLayer.Services
{
    using System;
    using System.IO;
    using System.Xml.Linq;
    using log4net;
    using ServiceLayer.IServices;

    public class ConfigValueService : IConfigValue
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(ConfigValueService));
        private XElement xmlData;

        public void LoadConfiguration(string filePath)
        {
            string xmlText = File.ReadAllText(filePath);
            this.xmlData = XElement.Parse(xmlText);
        }

        public T GetValue<T>(string key)
        {
            if (xmlData != null)
            {
                XElement element = this.xmlData.Element(key);
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
