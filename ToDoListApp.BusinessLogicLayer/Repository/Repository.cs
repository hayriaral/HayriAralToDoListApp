using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoListApp.DataAccessLayer;

namespace ToDoListApp.BusinessLogicLayer.Repository
{
    public abstract class Repository<T>
    {
        public static HayriAralToDoListDBEntities connectedDb = DatabaseConnection.GetConnection();

        public abstract void Insert( T item );
        public abstract void Update( T item );
        public abstract void Delete( T item );
    }
}
