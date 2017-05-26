using System.Collections.Generic;
using System.Linq;

namespace DBModel.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        void Save(T entity);

        IEnumerable<T> GetAll();
    }
}
