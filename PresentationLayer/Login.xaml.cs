using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PresentationLayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;

            // Perform authentication logic here
            if (username == "admin" && password == "password")
            {
                // Successful login, navigate to the main window or perform other actions
                
                StartSida startSida = new StartSida();
                startSida.Show();
                this.Close();
            }
            else
            {
                // Display error message
                ErrorMessage.Text = "Invalid username or password.";
            }
        }
    }
}