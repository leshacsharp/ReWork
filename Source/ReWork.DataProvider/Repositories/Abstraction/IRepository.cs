using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ReWork.DataProvider.Repositories.Abstraction
{
    public interface IRepository<T> where T : class
    {
        void Create(T item);
        void Delete(T item);
        void Update(T item);
    }
}
