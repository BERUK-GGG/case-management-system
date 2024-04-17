using PresentationLayer.View;
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
using RB_Ärendesystem.Datalayer;
using Entities;
using RB_Ärendesystem.Entities;


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

            RB_context testDB = new RB_context();
            testDB.Database.EnsureDeleted();
            testDB.Database.EnsureCreated();

            Kund kund1 = new Kund { Namn = "me", PersonNr = 123456, Address = "Gatan 1", TeleNr = 0701234567, Epost = "me@gmail.com" };
            Mekaniker mekaniker1 = new Mekaniker { Namn = "you", Roll = "Mekaniker", specialisering = "Motor", AnvändarNamn = "you", lösenord = "password" };
            Reservdel reservdel1 = new Reservdel { Namn = "Motor", Pris = 1000, };
            Reservdel reservdel2 = new Reservdel { Namn = "Däck", Pris = 500, };
            Besök besök1 = new Besök { KundID = kund1, DateAndTime = new System.DateTime(2021, 12, 24, 12, 00, 00), syfte = "Service", MekanikerID = mekaniker1, };
            Receptionist receptionist = new Receptionist { lösenord = "password", AnvändarNamn = "Receptionist", Namn = "Hanna" };
            // Console.WriteLine(kund1);

            testDB.kunder.Add(kund1);
            testDB.mekaniker.Add(mekaniker1);
            testDB.receptionister.Add(receptionist);

            testDB.reservdelar.Add(reservdel1);
            testDB.reservdelar.Add(reservdel2);
            testDB.besök.Add(besök1);

            testDB.SaveChanges();
            customerDataGrid.ItemsSource = testDB.kunder.ToList();

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

        private void customerDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void UppdateraKundButton_Click(object sender, RoutedEventArgs e)
        {
            UppdateraKund uppdateraKundWindow = new UppdateraKund();
            uppdateraKundWindow.Show();
            this.Close();
        }

        private void NyKundButton_Click(object sender, RoutedEventArgs e)
        {
            NyKund nyKundWindow = new NyKund();
            nyKundWindow.Show();
            this.Close();
        }






        // a method for the search button that will search for the customer


    }
}
