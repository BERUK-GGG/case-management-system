using Affärslager;
using RB_Ärendesystem.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
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
    /// Interaction logic for BeställReservdel.xaml
    /// </summary>
    public partial class BeställReservdel : Window, INotifyPropertyChanged 
    {
        public event PropertyChangedEventHandler PropertyChanged;
        TabellController tabeller = new TabellController();
  

    
        public BeställReservdel()
        {
            InitializeComponent();

            //PopulateReservComboBox();
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

        private void SparaButton_Click(object sender, RoutedEventArgs e)
        {
            // Create a new Kund object with the information from the input fields
            Reservdel newReserv = new Reservdel
            {
                Namn = Reservdel.Text,
                Pris = int.Parse(Pris.Text),
    
 
      
            };

            NyReservdelController NyReservdelController = new NyReservdelController();

            NyReservdelController.LäggTillReservdel(newReserv);

            // Show a message indicating successful saving
            MessageBox.Show("Ny reservdel registrerad!.");

            // Navigate back to the previous window
            StartSida startSida = new StartSida();
            startSida.Show();
            this.Close();
        }
    }

    //private void PopulateReservComboBox()
    //{


    //    // Fetch the list of mechanics from the database
    //    var reserv = tabeller.ReservdellTabell().ToList();

    //    // Bind the list to the ComboBox
    //    Reservdel.ItemsSource = reserv;

    //    // Set the display member path to a property of Mekaniker class that represents the name
    //    Reservdel.DisplayMemberPath = "Namn"; // Replace "Name" with the actual property name in your Mekaniker class

    //}
}

