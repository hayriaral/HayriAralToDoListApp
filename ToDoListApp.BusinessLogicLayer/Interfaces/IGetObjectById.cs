using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoListApp.BusinessLogicLayer.Interfaces
{
    public interface IGetObjectById<T, M>
    {
        T GetObjectById( M id );
    }
}
