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

namespace PresentationLayer.View
{
    /// <summary>
    /// Interaction logic for UppdateraKund.xaml
    /// </summary>
    public partial class UppdateraKund : Window
    {
        public UppdateraKund()
        {
             InitializeComponent();
           
        }
        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Close();
            // Handle the click event for the "Log out" button
            // Add logic to log out the user and navigate back to the login page
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {


            StartSida startSida = new StartSida();
            startSida.Show();
            this.Close();
            // Handle the click event for the "Back" button
            // Add logic to navigate back to the previous page
  
        }

        // a method for the search button that will search for the customerin the database and display the information in the textboxes 
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            // Handle the click event for the "Search" button
            // Add logic to search for the customer in the database and display the information in the textboxes
        }


    }
}
