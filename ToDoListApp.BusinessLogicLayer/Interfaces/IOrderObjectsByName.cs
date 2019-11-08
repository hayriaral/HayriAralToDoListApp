using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoListApp.BusinessLogicLayer.Interfaces
{
    public interface IOrderObjectsByName<T, M>
    {
        List<T> OrderByName( M id );
    }
}
