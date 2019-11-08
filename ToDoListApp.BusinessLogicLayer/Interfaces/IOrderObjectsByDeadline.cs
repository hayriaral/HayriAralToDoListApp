using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoListApp.BusinessLogicLayer.Interfaces
{
    public interface IOrderObjectsByDeadline<T, M>
    {
        List<T> OrderByDeadline( M id );
    }
}
