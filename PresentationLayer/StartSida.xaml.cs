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
        private Stack<UIElement> navigationStack = new Stack<UIElement>();
        public StartSida()
        {
            InitializeComponent();
        }

        private void CustomerButton_Click(object sender, RoutedEventArgs e)
        {

            ReservContentControl.Visibility = Visibility.Collapsed;
            KundContentControl.Visibility = Visibility.Visible;
            BokingContentControl.Visibility = Visibility.Collapsed;
            // Handle the click event for the "Kund" button
            // Add logic to navigate to the customer page or perform other actions
            navigationStack.Push(KundContentControl);
            BackButton.Visibility = Visibility.Visible;

        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (navigationStack.Count > 0)
            {
                UIElement previousContent = navigationStack.Pop();
                previousContent.Visibility = Visibility.Collapsed;
                if (navigationStack.Count == 0)
                {
                    BackButton.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void BookingButton_Click(object sender, RoutedEventArgs e)
        {
            ReservContentControl.Visibility = Visibility.Collapsed;
            BokingContentControl.Visibility = Visibility.Visible;
            KundContentControl.Visibility = Visibility.Collapsed;
            // Handle the click event for the "Bokning" button
            // Add logic to navigate to the booking page or perform other actions
            navigationStack.Push(BokingContentControl);
            BackButton.Visibility = Visibility.Visible;
        }

        private void PartsButton_Click(object sender, RoutedEventArgs e)


        {
            ReservContentControl.Visibility = Visibility.Visible;
            BokingContentControl.Visibility = Visibility.Collapsed;
            KundContentControl.Visibility = Visibility.Collapsed;
            // Handle the click event for the "Resevdelar" button
            // Add logic to navigate to the parts page or perform other actions
            navigationStack.Push(ReservContentControl);
            BackButton.Visibility = Visibility.Visible;
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Close();
            // Handle the click event for the "Log out" button
            // Add logic to log out the user and navigate back to the login page
        }
        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = SearchTextBox.Text;
            // Perform search operations based on the searchText
            // Update the UI accordingly
        }
    }
}
