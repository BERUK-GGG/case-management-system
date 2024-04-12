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

namespace PresentationLayer
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class StartSida : Window
    {
        public StartSida()
        {
            InitializeComponent();
        }

        private void CustomerButton_Click(object sender, RoutedEventArgs e)
        {

            KundContentControl.Visibility = Visibility.Visible;

            // Handle the click event for the "Kund" button
            // Add logic to navigate to the customer page or perform other actions
        }

        private void BookingButton_Click(object sender, RoutedEventArgs e)
        {
            // Handle the click event for the "Bokning" button
            // Add logic to navigate to the booking page or perform other actions
        }

        private void PartsButton_Click(object sender, RoutedEventArgs e)
        {
            // Handle the click event for the "Resevdelar" button
            // Add logic to navigate to the parts page or perform other actions
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            // Handle the click event for the "Log out" button
            // Add logic to log out the user and navigate back to the login page
        }
    }
}
