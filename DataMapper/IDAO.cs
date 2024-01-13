using Library.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMapper
{
    public interface IDAO<T>
    {
        T GetById(int id);
        void Add(T t);
        void Update(T t);
        void Delete(T t);
        List<T> GetAll();
    }
}
