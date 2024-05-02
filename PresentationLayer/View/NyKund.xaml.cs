using Affärslager;
using RB_Ärendesystem.Datalayer;
using RB_Ärendesystem.Entities;
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
    /// Interaction logic for NyKund.xaml
    /// </summary>
    public partial class NyKund : Window
    {
        public NyKund()
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

        private void SparaButton_Click(object sender, RoutedEventArgs e)
        {


            List<string> errorMessages = new List<string>();

            try
            {
                // Check if Namn textbox contains only letters
                if (!IsNamnValid(Namn.Text))
                {
                    // Collect error message for Namn
                    errorMessages.Add("Invalid input format for 'Namn'. Please enter only letters.");
                }

                // Check if PersonNr textbox contains numeric value
                if (!int.TryParse(PersonNr.Text, out _))
                {
                    // Collect error message for PersonNr
                    errorMessages.Add("Invalid input format for 'PersonNr'. Please enter numeric value.");
                }

                // Check if TeleNr textbox contains numeric value
                if (!int.TryParse(TeleNr.Text, out _))
                {
                    // Collect error message for TeleNr
                    errorMessages.Add("Invalid input format for 'TeleNr'. Please enter numeric value.");
                }

                if (errorMessages.Count > 0)
                {
                    // Display all collected error messages
                    MessageBox.Show(string.Join("\n", errorMessages), "Error");
                    return;
                }

                // Create a new Kund object with the information from the input fields
                Kund newKund = new Kund
                {
                    Namn = Namn.Text,
                    PersonNr = int.Parse(PersonNr.Text),
                    Address = Address.Text,
                    TeleNr = int.Parse(TeleNr.Text),
                    Epost = Epost.Text
                };

                NyKundController nyKundController = new NyKundController();

                nyKundController.LäggTillNyKund(newKund);

                // Show a message indicating successful saving
                MessageBox.Show("New customer saved successfully.");

                // Navigate back to the previous window
                StartSida startSida = new StartSida();
                startSida.Show();
                this.Close();
            }
            catch (FormatException)
            {
                // Display an error message indicating invalid input format
                MessageBox.Show("Invalid input format. Please enter numeric values for 'PersonNr' and 'TeleNr'.", "Error");
            }
        }

        private bool IsNamnValid(string namn)
        {
            // Check if the string contains only letters
            return !string.IsNullOrWhiteSpace(namn) && namn.All(char.IsLetter);
        }





    }

}
