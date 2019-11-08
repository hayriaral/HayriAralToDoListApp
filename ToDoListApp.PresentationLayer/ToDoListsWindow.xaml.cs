using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ToDoListApp.DataAccessLayer;
using ToDoListApp.BusinessLogicLayer;

namespace ToDoListApp.PresentationLayer
{
    /// <summary>
    /// Interaction logic for ToDoListsWindow.xaml
    /// </summary>
    public partial class ToDoListsWindow : Window
    {
        //public LoginWindow loginWindow { get; set; }
        public Process process { get; set; }
        public ToDoListsWindow(Process _process)
        {
            this.process = _process;
            InitializeComponent();
            FillTitle();
            RefreshLists();
        }

        private void FillTitle()
        {
            Label titleLabel = new Label()
            {
                Content=process.currentUser.userName.ToString() +" 's To-Do Lists",
                Style = (Style)FindResource("titleLabelStyle")
            };
            this.titleStackPanel.Children.Add( titleLabel );

            Separator titleSeparator = new Separator();
            this.titleStackPanel.Children.Add( titleSeparator );
        }

        private void FillBody()
        {
            List<ToDoList> lists = new List<ToDoList>();
            lists = process.GetLists();

            if ( lists.Count > 0 )
            {
                foreach ( ToDoList list in lists )
                {
                    Label listNameLabel = new Label()
                    {
                        Content=list.listName.ToString(),
                        Style = (Style)FindResource("nameLabelStyle"),
                        Tag = list
                    };
                    this.listNameStackPanel.Children.Add( listNameLabel );

                    Label xLabel = new Label()
                    {
                        Style = (Style)FindResource("deleteLabelStyle"),
                        Tag=list
                    };
                    this.deleteIconStackPanel.Children.Add( xLabel );

                    xLabel.MouseDown += XLabel_MouseDown;
                    listNameLabel.MouseDown += ListNameLabel_MouseDown;
                }
            }
        }

        private void ListNameLabel_MouseDown( object sender, MouseButtonEventArgs e )
        {
            Label selectedLabel = (Label)sender;
            process.selectedList = (ToDoList)selectedLabel.Tag;
            ToDoItemsWindow itemWindow = new ToDoItemsWindow(process);
            itemWindow.Show();
            this.Close();
        }

        private void XLabel_MouseDown( object sender, MouseButtonEventArgs e )
        {
            Label selectedLabel = (Label)sender;
            ToDoList deletedList = (ToDoList)selectedLabel.Tag;
            process.RemoveToDoList( deletedList );
            MessageBox.Show( "The list deleted successfully!" );
            RefreshLists();
        }

        private void RefreshLists()
        {
            listNameStackPanel.Children.Clear();
            deleteIconStackPanel.Children.Clear();
            FillBody();
        }

        private void CreateToDoListButton_Click( object sender, RoutedEventArgs e )
        {
            CreateToDoListWindow createListWindow = new CreateToDoListWindow(process);
            createListWindow.Show();
            this.Close();
        }

        private void LogoutButton_Click( object sender, RoutedEventArgs e )
        {
            LoginWindow newLoginWindow = new LoginWindow();
            newLoginWindow.Show();
            this.Close();
        }
    }
}
