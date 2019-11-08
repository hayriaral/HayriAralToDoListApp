using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoListApp.DataAccessLayer;
using ToDoListApp.BusinessLogicLayer.Interfaces;

namespace ToDoListApp.BusinessLogicLayer.Repository
{
    public class ToDoListRepository : Repository<ToDoList>, IGetObjectById<ToDoList, Guid>, IGetObjectsList<ToDoList,Guid>
    {
        public override void Delete( ToDoList item )
        {
            ToDoList toDoList = GetObjectById(item.listId);

            //connectedDb.ToDoLists.Remove( toDoList );
            //Instead of being deleted, it will be ineligible to use.
            toDoList.isEligible = false;

            connectedDb.SaveChanges();
        }

        public ToDoList GetObjectById( Guid id )
        {
            ToDoList toDoList = connectedDb.ToDoLists.SingleOrDefault(t=>t.listId==id);

            return toDoList;
        }

        public List<ToDoList> GetObjectsList( Guid id )
        {
            List<ToDoList> lists = connectedDb.ToDoLists.Where(t=>t.isEligible==true && t.userId==id).OrderByDescending(t=>t.listCreateDate).ToList();

            return lists;
        }

        public override void Insert( ToDoList item )
        {
            ToDoList newToDoList = connectedDb.ToDoLists.Create();

            newToDoList.isEligible = true;
            newToDoList.listCreateDate = DateTime.Now;
            newToDoList.listId = Guid.NewGuid();
            newToDoList.listName = item.listName;
            newToDoList.userId = item.userId;

            connectedDb.ToDoLists.Add( newToDoList );
            connectedDb.SaveChanges();
        }

        public override void Update( ToDoList item )
        {
            //The program will not allow user to update the exist lists.
        }
    }
}
