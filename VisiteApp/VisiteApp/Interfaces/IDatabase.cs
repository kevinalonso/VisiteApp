using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisiteApp.Interfaces
{
    public interface IDatabase<T>
    {
        IEnumerable<T> getAll();
        T get(int id);
        void delete(int id);
        int add(T t);
    }
}
