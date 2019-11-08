using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoListApp.BusinessLogicLayer.Interfaces
{
    public interface IGetObjectsList<T, M>
    {
        List<T> GetObjectsList( M id );
    }
}
