using System;
using System.Collections.Generic;
using System.Text;

namespace BeMyAngel.Persistance.Generics
{
    public interface IRepository<T> where T: IDto
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        int Insert(T dto);
        void Update(T dto);
        void Delete(T dto);
    }
}
