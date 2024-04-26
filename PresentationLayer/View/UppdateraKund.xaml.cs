using Affärslager;
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
        TabellController tabeller = new TabellController();

        public UppdateraKund()
        {
             InitializeComponent();

           




            CustomerDataGrid.ItemsSource = tabeller.KundTabell().ToList();

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
                using (var uow = new UnitOfWork())
                {
                    //context.Entry(selectedKund).State = EntityState.Modified;
                    //context.SaveChanges();
                    uow.Kunds.Update(selectedKund);
                    uow.SaveChanges();
                }

                // Refresh the DataGrid to reflect changes
                RefreshDataGrid();
            }
        }

        private void RefreshDataGrid()
        {
            // Re-bind the DataGrid to update its content
            CustomerDataGrid.ItemsSource = tabeller.KundTabell().ToList();
        }

        private void SökTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = SökTextBox.Text.ToLower(); // Assuming SökTextBox is the name of your search TextBox
            using (var Uow = new UnitOfWork())
            {
                var searchResult = Uow.Kunds.GetAll().Where(k => k.Namn.ToLower().Contains(searchText)
                                                          || k.PersonNr.ToString().Contains(searchText)
                                                          || k.Address.ToLower().Contains(searchText)
                                                          || k.Epost.ToLower().Contains(searchText)
                                                          || k.TeleNr.ToString().Contains(searchText)).ToList();
                
                CustomerDataGrid.ItemsSource = searchResult;
            }
        }






    }
}
