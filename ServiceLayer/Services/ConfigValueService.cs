// <copyright file="ConfigValueService.cs" company="Transilvania University of Brasov">
// Dragomir Razvan
// </copyright>

namespace ServiceLayer.Services
{
    using System;
    using System.IO;
    using System.Xml.Linq;
    using log4net;
    using ServiceLayer.IServices;

    /// <summary>
    /// Service class for managing configuration values from an XML file.
    /// </summary>
    public class ConfigValueService : IConfigValue
    {
        /// <summary>
        /// Represents the static readonly log instance for logging in the ConfigValueService class.
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(typeof(ConfigValueService));

        /// <summary>
        /// Represents XML data used by the ConfigValueService.
        /// </summary>
        private XElement xmlData;

        /// <summary>
        /// Loads the configuration data from the specified XML file.
        /// </summary>
        /// <param name="filePath">The path to the XML file containing configuration data.</param>
        public void LoadConfiguration(string filePath)
        {
            string xmlText = File.ReadAllText(filePath);
            this.xmlData = XElement.Parse(xmlText);
        }

        /// <summary>
        /// Retrieves a configuration value of type T associated with the specified key.
        /// </summary>
        /// <typeparam name="T">The type of the configuration value to retrieve.</typeparam>
        /// <param name="key">The key associated with the desired configuration value.</param>
        /// <returns>The configuration value of type T.</returns>
        public T GetValue<T>(string key)
        {
            if (this.xmlData != null)
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
