using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoListApp.DataAccessLayer
{
    public class DatabaseConnection
    {

        public static HayriAralToDoListDBEntities database = null;

        public static HayriAralToDoListDBEntities GetConnection()
        {

            if ( database == null )
            {

                database = new HayriAralToDoListDBEntities();
            }

            return database;
        }
    }
}
