using DataLayer;
using Microsoft.EntityFrameworkCore;
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
    /// Interaction logic for UppdateraKund.xaml
    /// </summary>
    public partial class UppdateraKund : Window
    {
        public UppdateraKund()
        {
             InitializeComponent();

            RB_context testDB = new RB_context();




            CustomerDataGrid.ItemsSource = testDB.kunder.ToList();

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

        private void CustomerDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Get the selected row
            if (CustomerDataGrid.SelectedItem != null)
            {
                // Cast the selected item to the type of your data object (assuming it's Kund)
                Kund selectedKund = (Kund)CustomerDataGrid.SelectedItem;

                // Populate TextBoxes with values from the selected row

                Namn.Text = selectedKund.Namn;
                PersonNr.Text = selectedKund.PersonNr.ToString();
                Address.Text = selectedKund.Address;
                Epost.Text = selectedKund.Epost;
                TeleNr.Text = selectedKund.TeleNr.ToString();
                // Populate other TextBoxes with relevant properties from the selected row
            }


        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Check if a row is selected in the DataGrid
            if (CustomerDataGrid.SelectedItem != null)
            {
                // Cast the selected item to the type of your data object (assuming it's Kund)
                Kund selectedKund = (Kund)CustomerDataGrid.SelectedItem;

                // Update the selectedKund object with the values from TextBoxes
                selectedKund.Namn = Namn.Text;
                selectedKund.PersonNr = int.Parse(PersonNr.Text);
                selectedKund.Address = Address.Text;

                selectedKund.Epost = Epost.Text;
                selectedKund.TeleNr = int.Parse(TeleNr.Text);
                // Update other properties as needed

                // Save changes to the database
                using (var context = new RB_context())
                {
                    context.Entry(selectedKund).State = EntityState.Modified;
                    context.SaveChanges();
                }

                // Refresh the DataGrid to reflect changes
                RefreshDataGrid();
            }
        }

        private void RefreshDataGrid()
        {
            // Re-bind the DataGrid to update its content
            using (var context = new RB_context())
            {
                CustomerDataGrid.ItemsSource = context.kunder.ToList();
            }
        }




    }
}
