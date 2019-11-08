using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoListApp.BusinessLogicLayer.Interfaces
{
    public interface IOrderObjectsByCreateDate<T, M>
    {
        List<T> OrderByCreateDate( M id );
    }
}
