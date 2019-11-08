using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoListApp.DataAccessLayer;
using ToDoListApp.BusinessLogicLayer.Interfaces;

namespace ToDoListApp.BusinessLogicLayer.Repository
{
    public class UserRepository : Repository<User>, IGetObjectById<User, Guid>
    {
        public override void Delete( User item )
        {
            //The program will not allow user to delete its account.
        }

        public User GetObjectById( Guid id )
        {
            User user = connectedDb.Users.SingleOrDefault(t=>t.userId==id);

            return user;
        }

        public override void Insert( User item )
        {
            User newUser = connectedDb.Users.Create();

            newUser.userId = Guid.NewGuid();
            newUser.userName = item.userName;
            newUser.userPassword = item.userPassword;

            connectedDb.Users.Add( newUser );
            connectedDb.SaveChanges();
        }

        public override void Update( User item )
        {
            //The program will not allow user to update its account details.
        }
    }
}
