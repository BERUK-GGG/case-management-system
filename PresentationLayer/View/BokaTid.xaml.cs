
using Affärslager;
using Microsoft.EntityFrameworkCore;
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




namespace PresentationLayer.View
{
    /// <summary>
    /// Interaction logic for BokaTid.xaml
    /// </summary>
    public partial class BokaTid : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        TabellController tabeller = new TabellController();
        NyBokningController BokningsController = new NyBokningController();
        public BokaTid()
        {
            InitializeComponent();

            

            PopulateMechanicsComboBox();

            InitializeDatePicker();

            CustomerDataGrid.ItemsSource = tabeller.KundTabell();
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

        private void customerDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Get the selected row
            if (CustomerDataGrid.SelectedItem != null)
            {
                // Cast the selected item to the type of your data object (assuming it's Kund)
                Kund selectedKund = (Kund)CustomerDataGrid.SelectedItem;

                Namn.Text = selectedKund.Namn;               

            }
        }

        private DateTime _selectedDate;

        public DateTime SelectedDate
        {
            get { return _selectedDate; }
            set
            {
                _selectedDate = value;
                OnPropertyChanged(nameof(SelectedDate));
            }
        }

        private void InitializeDatePicker()
        {
            // Set the DatePicker's SelectedDate property to today's date
            SelectedDate = DateTime.Today;
        }

        // Implement INotifyPropertyChanged interface
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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
            if (CustomerDataGrid.SelectedItem != null)
            {
                // Cast the selected item to the type of your data object (assuming it's Kund)
                Kund selectedKund = (Kund)CustomerDataGrid.SelectedItem;

                Mekaniker selectedMekaniker = (Mekaniker)Mekaniker.SelectedItem;

                // Update the selectedKund object with the values from TextBoxes
                selectedKund.Namn = Namn.Text;

                TabellController controller = new TabellController();
                BokningsController.AddBooking(selectedKund, selectedMekaniker, Syfte.Text, SelectedDate);

                // Save changes to the database
                

                //boka.LäggTillBesök(ny_besök);

                MessageBox.Show("New booking saved successfully.");

                // Navigate back to the previous window
                StartSida startSida = new StartSida();
                startSida.Show();
                this.Close();



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