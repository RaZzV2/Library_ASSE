using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.IServices
{
    public interface IDAOService<T>
    {
        T GetById(int id);
        void Add(T t);
        void Update(T t);
        void Delete(T t);
    }
}
