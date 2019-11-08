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
    /// Interaction logic for AddToDoItemWindow.xaml
    /// </summary>
    public partial class AddToDoItemWindow : Window
    {
        public Process process { get; set; }
        public AddToDoItemWindow( Process _process )
        {
            this.process = _process;
            InitializeComponent();
        }

        private void SubmitButton_Click( object sender, RoutedEventArgs e )
        {
            if ( itemNameTextBox.Text != null && itemNameTextBox.Text != "" && itemDescriptionTextBox.Text != null && itemDescriptionTextBox.Text != "" && itemDeadlineDatePicker.SelectedDate != null )
            {
                process.CreateToDoItem( itemNameTextBox.Text.ToString(), itemDescriptionTextBox.Text.ToString(), itemDeadlineDatePicker.SelectedDate.Value );
                MessageBox.Show( "The item added successfully!" );

                ToDoItemsWindow newToDoItemsWindow = new ToDoItemsWindow(process);
                newToDoItemsWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show( "Please fill all fields." );
            }
        }

        private void BackButton_Click( object sender, RoutedEventArgs e )
        {
            ToDoItemsWindow newToDoItemsWindow = new ToDoItemsWindow(process);
            newToDoItemsWindow.Show();
            this.Close();
        }
    }
}
