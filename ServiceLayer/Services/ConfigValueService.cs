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
                    return (T)Convert.ChangeType(element.Value, typeof(T));
                }
            }

            return default;
        }
    }
}
