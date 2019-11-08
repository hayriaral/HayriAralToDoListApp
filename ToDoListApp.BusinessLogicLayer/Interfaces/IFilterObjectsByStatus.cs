using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoListApp.BusinessLogicLayer.Interfaces
{
    public interface IFilterObjectsByStatus<T, M>
    {
        List<T> FilterByStatus( M id , string status );
    }
}
