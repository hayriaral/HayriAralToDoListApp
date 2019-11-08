using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoListApp.DataAccessLayer;
using ToDoListApp.BusinessLogicLayer.Interfaces;

namespace ToDoListApp.BusinessLogicLayer.Repository
{
    public class ToDoItemRepository : Repository<ToDoItem>, IGetObjectById<ToDoItem, Guid>, IGetObjectsList<ToDoItem, Guid>, IOrderObjectsByCreateDate<ToDoItem, Guid>, IOrderObjectsByDeadline<ToDoItem, Guid>, IOrderObjectsByName<ToDoItem, Guid>, IOrderObjectsByStatus<ToDoItem, Guid>
    {
        public override void Delete( ToDoItem item )
        {
            ToDoItem toDoItem = GetObjectById(item.itemId);

            //connectedDb.ToDoItems.Remove( toDoItem );
            //Instead of being deleted, it will be ineligible to use.
            toDoItem.isEligible = false;

            connectedDb.SaveChanges();
        }

        public ToDoItem GetObjectById( Guid id )
        {
            ToDoItem toDoItem = connectedDb.ToDoItems.SingleOrDefault(t=>t.itemId==id);

            return toDoItem;
        }

        public List<ToDoItem> GetObjectsList( Guid id )
        {
            List<ToDoItem> toDoItems = connectedDb.ToDoItems.Where(t=>t.isEligible==true && t.listId ==id).OrderByDescending( t => t.itemCreateDate ).ToList();

            return toDoItems;
        }

        public override void Insert( ToDoItem item )
        {
            ToDoItem newToDoItem = connectedDb.ToDoItems.Create();

            newToDoItem.isEligible = true;
            newToDoItem.itemCreateDate = DateTime.Now;
            newToDoItem.itemDeadline = item.itemDeadline;
            newToDoItem.itemDescription = item.itemDescription;
            newToDoItem.itemId = Guid.NewGuid();
            newToDoItem.itemName = item.itemName;
            newToDoItem.itemStatus = item.itemStatus;
            newToDoItem.listId = item.listId;

            connectedDb.ToDoItems.Add( newToDoItem );
            connectedDb.SaveChanges();
        }

        public List<ToDoItem> OrderByCreateDate( Guid id )
        {
            List<ToDoItem> toDoItems = connectedDb.ToDoItems.Where(t=>t.isEligible==true && t.listId ==id).OrderByDescending( t => t.itemCreateDate ).ToList();

            return toDoItems;
        }

        public List<ToDoItem> OrderByDeadline( Guid id )
        {
            List<ToDoItem> toDoItems = connectedDb.ToDoItems.Where(t=>t.isEligible==true && t.listId ==id).OrderByDescending( t => t.itemDeadline ).ToList();

            return toDoItems;
        }

        public List<ToDoItem> OrderByName( Guid id )
        {
            List<ToDoItem> toDoItems = connectedDb.ToDoItems.Where(t=>t.isEligible==true && t.listId ==id).OrderByDescending( t => t.itemName ).ToList();

            return toDoItems;
        }

        public List<ToDoItem> OrderByStatus( Guid id )
        {
            List<ToDoItem> toDoItems = connectedDb.ToDoItems.Where(t=>t.isEligible==true && t.listId ==id).OrderByDescending( t => t.itemStatus ).ToList();

            return toDoItems;
        }

        public override void Update( ToDoItem item )
        {
            ToDoItem toDoItem = GetObjectById(item.itemId);

            toDoItem.itemStatus = item.itemStatus;

            connectedDb.SaveChanges();
        }
    }
}
