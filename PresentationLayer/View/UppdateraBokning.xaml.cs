using Affärslager;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using RB_Ärendesystem.Datalayer;
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
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace PresentationLayer.View
{
    /// <summary>
    /// Interaction logic for UppdateraBokning.xaml
    /// </summary>
    public partial class UppdateraBokning : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        TabellController tabeller = new TabellController();
        UppdateraBokningController UppdateraBokningController = new UppdateraBokningController();
        public UppdateraBokning()
        {
            InitializeComponent();

            PopulateMechanicsComboBox();

            BesökDataGrid.ItemsSource = tabeller.BesökTabell();

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

        private void BesökDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Get the selected row
            if (BesökDataGrid.SelectedItem != null)
            {
                Besök selectedBokning = (Besök)BesökDataGrid.SelectedItem;

                // Populate TextBoxes with values from the selected row

                Namn.Text = selectedBokning.Kund.ToString();
                Mekaniker.Text = selectedBokning.Mekaniker.ToString();
                Syfte.Text = selectedBokning.Syfte;
                Datum.Text = selectedBokning.DateAndTime.ToString();

                // Populate other TextBoxes with relevant properties from the selected row
            }


        }

        private void PopulateMechanicsComboBox()
        {


            // Fetch the list of mechanics from the database
            var mechanics = tabeller.MekanikerTabell().ToList();

            // Bind the list to the ComboBox
            Mekaniker.ItemsSource = mechanics;

            // Set the display member path to a property of Mekaniker class that represents the name
            Mekaniker.DisplayMemberPath = "Namn"; // Replace "Name" with the actual property name in your Mekaniker class

        }

        private void Mekaniker_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Get the selected mechanic
            if (Mekaniker.SelectedItem != null)
            {
                // Cast the selected item to the type of your data object (assuming it's Mekaniker)
                Mekaniker selectedMekaniker = (Mekaniker)Mekaniker.SelectedItem;

                // Access the selected mechanic properties
                string selectedMechanicName = selectedMekaniker.Namn; // Replace "Name" with the actual property name in your Mekaniker class
                // Do whatever you need to do with the selected mechanic
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Check if a row is selected in the DataGrid
            if (BesökDataGrid.SelectedItem != null)
            {
                Besök selectedBokning = (Besök)BesökDataGrid.SelectedItem;
                DateTime Date = (DateTime)Datum.SelectedDate;
                string timeString = ((ComboBoxItem)Time.SelectedItem).Content.ToString();
                TimeOnly tid = TimeOnly.Parse(timeString.ToString());

                DateTime selectedDateTime = Date.Date + tid.ToTimeSpan();

                // Update the selectedBesök object with the values from TextBoxes

                Mekaniker selectedMekaniker = (Mekaniker)Mekaniker.SelectedItem;
                selectedBokning.Mekaniker = selectedMekaniker;
                selectedBokning.Syfte = Syfte.Text;
                selectedBokning.DateAndTime = selectedDateTime;




                // Update other properties as needed

                // Save changes to the database
                UppdateraBokningController.UppdateraBokning(selectedBokning);

                // Refresh the DataGrid to reflect changes
                RefreshDataGrid();
            }
        }

        private void RefreshDataGrid()
        {
            // Re-bind the DataGrid to update its content
            BesökDataGrid.ItemsSource = tabeller.BesökTabell().ToList();
        }

        private void RaderaBokning_Click(object sender, EventArgs e)
        {
            Besök selectedBesök = (Besök)BesökDataGrid.SelectedItem;

            System.Windows.MessageBoxResult result = System.Windows.MessageBox.Show("Do you want to continue?", "Confirmation", System.Windows.MessageBoxButton.YesNo);

            if (result == System.Windows.MessageBoxResult.Yes)
            {
                
                
                    // Find all Besök entries with the same KundID as the one we tried to delete
                    var relatedJournal = tabeller.JournalTabell().Where(b => b.Besök.ID == selectedBesök.ID).ToList();
                    if (!relatedJournal.Any())
                    {
                        UppdateraBokningController.TaBortBokning(selectedBesök);
                        RefreshDataGrid();
                    }
                    else
                    {
                        System.Windows.MessageBox.Show("This booking cannot be deleted due to associated records in other tables.", "Error");
                    }
                


            }


        }

        private void Name_TextChanged(object sender, TextChangedEventArgs e)
            {

            }

            private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
            {

            }

        }


    }

