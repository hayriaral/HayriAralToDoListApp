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
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public Process process { get; set; }
        public LoginWindow()
        {
            InitializeComponent();
            process = new Process();
        }

        private void SignUpButton_Click( object sender, RoutedEventArgs e )
        {
            string message = null;
            message = process.SignUp( userNameTextBox.Text.ToString(), userPasswordPasswordBox.Password.ToString() );

            MessageBox.Show( message );

            userNameTextBox.Clear();
            userPasswordPasswordBox.Clear();
        }

        private void SignInButton_Click( object sender, RoutedEventArgs e )
        {
            string message = null;
            message = process.SignIn( userNameTextBox.Text.ToString(), userPasswordPasswordBox.Password.ToString() );

            if ( message != null )
            {
                MessageBox.Show( message );
                userNameTextBox.Clear();
                userPasswordPasswordBox.Clear();
            }
            else
            {
                ToDoListsWindow listWindow = new ToDoListsWindow(process);
                listWindow.Show();
                this.Close();
            }
        }
    }
}
