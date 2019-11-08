using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoListApp.BusinessLogicLayer.Repository;
using ToDoListApp.DataAccessLayer;
using System.Security.Cryptography;

namespace ToDoListApp.BusinessLogicLayer
{
    public class Process
    {
        public User currentUser { get; set; }
        public ToDoList selectedList { get; set; }
        public List<ToDoItem> orderedItemsList { get; set; }
        public FilterSelection filterSelection { get; set; }
        public Process()
        {
            this.orderedItemsList = new List<ToDoItem>();
            this.filterSelection = FilterSelection.Default;
        }

        public enum Status
        {
            Completed,
            NotCompleted,
            Expired
        }

        public enum OrderBySelection
        {
            CreateDate,
            Deadline,
            Name,
            Status
        }

        public enum FilterSelection
        {
            Default,
            Completed,
            NotCompleted,
            Expired
        }

        public List<ToDoItem> FilterItemsByStatus( FilterSelection filter , List<ToDoItem> items )
        {
            List<ToDoItem> filteredList = items.ToList();

            switch ( filter )
            {
                case FilterSelection.Completed:
                    filterSelection = FilterSelection.Completed;
                    foreach ( ToDoItem item in items )
                    {
                        if ( item.itemStatus != Status.Completed.ToString() )
                        {
                            filteredList.Remove( item );
                        }
                    }
                    break;
                case FilterSelection.NotCompleted:
                    filterSelection = FilterSelection.NotCompleted;
                    foreach ( ToDoItem item in items )
                    {
                        if ( item.itemStatus != Status.NotCompleted.ToString() )
                        {
                            filteredList.Remove( item );
                        }
                    }
                    break;
                case FilterSelection.Expired:
                    filterSelection = FilterSelection.Expired;
                    foreach ( ToDoItem item in items )
                    {
                        if ( item.itemStatus != Status.Expired.ToString() )
                        {
                            filteredList.Remove( item );
                        }
                    }
                    break;
                case FilterSelection.Default:
                    filterSelection = FilterSelection.Default;
                    filteredList = GetItems( selectedList );
                    orderedItemsList.Clear();
                    break;
                default:
                    break;
            }
            return filteredList;
        }

        public List<ToDoItem> OrderItems( OrderBySelection orderBySelection )
        {
            List<ToDoItem> orderedItems = new List<ToDoItem>();
            ToDoItemRepository toDoItemRepository = new ToDoItemRepository();

            switch ( orderBySelection )
            {
                case OrderBySelection.CreateDate:
                    orderedItems = toDoItemRepository.OrderByCreateDate( selectedList.listId );
                    break;
                case OrderBySelection.Deadline:
                    orderedItems = toDoItemRepository.OrderByDeadline( selectedList.listId );
                    break;
                case OrderBySelection.Name:
                    orderedItems = toDoItemRepository.OrderByName( selectedList.listId );
                    break;
                case OrderBySelection.Status:
                    orderedItems = toDoItemRepository.OrderByStatus( selectedList.listId );
                    break;
                default:
                    break;
            }
            return orderedItems;
        }

        public void RemoveToDoItem( ToDoItem deletedItem )
        {
            ToDoItemRepository toDoItemRepository = new ToDoItemRepository();
            toDoItemRepository.Delete( deletedItem );
        }

        public void UpdateItem( ToDoItem updatedItem )
        {
            ToDoItemRepository toDoItemRepository = new ToDoItemRepository();
            updatedItem.itemStatus = Status.Completed.ToString();
            toDoItemRepository.Update( updatedItem );
        }

        public List<ToDoItem> GetItems( ToDoList list )
        {
            ToDoItemRepository toDoItemRepository = new ToDoItemRepository();
            List<ToDoItem> items = toDoItemRepository.GetObjectsList(list.listId);
            return items;
        }

        public void CreateToDoItem( string itemName, string itemDescription, DateTime itemDeadline )
        {
            ToDoItemRepository toDoItemRepository = new ToDoItemRepository();

            ToDoItem newItem = new ToDoItem();
            newItem.itemDeadline = itemDeadline;
            newItem.itemDescription = itemDescription;
            newItem.itemName = itemName;
            newItem.itemStatus = Status.NotCompleted.ToString();
            newItem.listId = selectedList.listId;

            toDoItemRepository.Insert( newItem );
        }

        public List<ToDoList> GetLists()
        {
            ToDoListRepository toDoListRepository = new ToDoListRepository();
            List<ToDoList> lists = toDoListRepository.GetObjectsList(currentUser.userId);
            return lists;
        }

        public void RemoveToDoList( ToDoList deletedList )
        {
            ToDoListRepository toDoListRepository = new ToDoListRepository();

            toDoListRepository.Delete( deletedList );

            //Items also will be deleted from related list.
            ToDoItemRepository toDoItemRepository = new ToDoItemRepository();
            List<ToDoItem> toDoItems = GetItems(deletedList);
            foreach ( ToDoItem item in toDoItems )
            {
                toDoItemRepository.Delete( item );
            }
        }

        public void CreateToDoList( string listName )
        {
            ToDoListRepository toDoListRepository = new ToDoListRepository();

            ToDoList newList = new ToDoList();
            newList.listName = listName;
            newList.userId = currentUser.userId;

            toDoListRepository.Insert( newList );
        }

        public string SignIn( string username, string password )
        {
            string message = null;
            UserRepository userRepository = new UserRepository();

            if ( username != null && username != "" && password != null && password != "" )
            {
                string encryptedPassword = ComputeSha256Hash(password);
                currentUser = UserRepository.connectedDb.Users.SingleOrDefault( t => t.userName == username && t.userPassword == encryptedPassword );
                //If an object returns, the username already exist.
                if ( currentUser == null )
                {
                    message = "Username or password is incorrect!";
                    return message;
                }
                else
                {
                    return message;
                }
            }
            else
            {
                message = "Username or password is invalid!";
                return message;
            }
        }

        public string SignUp( string username, string password )
        {
            string message = null;
            UserRepository userRepository = new UserRepository();

            if ( username != null && username != "" && password != null && password != "" )
            {
                User user = UserRepository.connectedDb.Users.SingleOrDefault(t => t.userName == username);
                if ( user == null )
                {
                    User newUser = new User();
                    newUser.userName = username;
                    newUser.userPassword = ComputeSha256Hash( password );
                    userRepository.Insert( newUser );
                    message = "The account created successfully!";

                    return message;
                }
                else
                {
                    message = "This username already exist.";
                    return message;
                }
            }
            else
            {
                message = "Username and password are required fields.";
                return message;
            }
        }

        private static string ComputeSha256Hash( string rawData )
        {
            // Create a SHA256
            using ( SHA256 sha256Hash = SHA256.Create() )
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder stringBuilder = new StringBuilder();
                for ( int i = 0; i < bytes.Length; i++ )
                {
                    stringBuilder.Append( bytes[i].ToString( "x2" ) );
                }
                return stringBuilder.ToString();
            }
        }
    }
}
