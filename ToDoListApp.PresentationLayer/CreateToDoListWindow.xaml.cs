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
using ToDoListApp.BusinessLogicLayer;

namespace ToDoListApp.PresentationLayer
{
    /// <summary>
    /// Interaction logic for CreateToDoListWindow.xaml
    /// </summary>
    public partial class CreateToDoListWindow : Window
    {
        public Process process { get; set; }
        public CreateToDoListWindow(Process _process)
        {
            this.process = _process;
            InitializeComponent();
        }

        private void SubmitButton_Click( object sender, RoutedEventArgs e )
        {
            if ( listNameTextBox.Text != null && listNameTextBox.Text != "" )
            {
                process.CreateToDoList( listNameTextBox.Text.ToString() );
                MessageBox.Show( "The list created successfully!" );

                ToDoListsWindow newToDoListsWindow = new ToDoListsWindow(process);
                newToDoListsWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show( "List name is required field." );
            }
        }

        private void BackButton_Click( object sender, RoutedEventArgs e )
        {
            ToDoListsWindow newToDoListsWindow = new ToDoListsWindow(process);
            newToDoListsWindow.Show();
            this.Close();
        }
    }
}
