using ServiceLayer.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ServiceLayer.Services
{
    public class ConfigValueService : IConfigValue
    {
        public int Value(string key, string filePath)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(filePath);

                XmlNode node = doc.SelectSingleNode($"//appSettings/add[@key='{key}']");
                if (node != null)
                {
                    if (int.TryParse(node.Attributes["value"].Value, out int result))
                    {
                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            return 0;
        }
    }
}
