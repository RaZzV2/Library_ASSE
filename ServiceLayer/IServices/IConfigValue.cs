using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.IServices
{
    public interface IConfigValue
    {
         void LoadConfiguration(string filePath);
         T GetValue<T>(string key);
    }
}
