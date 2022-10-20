using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataManipulatorDAL
{
    public interface IDAL<T> where T : class
    {
        Task Create(T item);
    }
}
