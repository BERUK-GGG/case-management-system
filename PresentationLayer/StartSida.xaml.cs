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

using RB_Ärendesystem.Entities;
using RB_Ärendesystem;
using DataLayer;
using Microsoft.EntityFrameworkCore;




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

            //LoadBookings();

            //RB_context testDB = new RB_context();
            ////TestData.SeedData();


            

            //customerDataGrid.ItemsSource = testDB.kunder.ToList();
            //bookingDataGrid.ItemsSource = testDB.besök.ToList();

            RefreshDataGrid();

        }

        private void LoadBookings()
        {
            using (var context = new RB_context())
            {
                bookingDataGrid.ItemsSource = context.besök.ToList();
            }
        }
        private void RefreshDataGrid()
        {
            // Re-bind the DataGrid to update its content
            using (var context = new RB_context())
            {
                customerDataGrid.ItemsSource = context.kunder.ToList();
                bookingDataGrid.ItemsSource = context.besök.ToList();
            }
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

        private void SearchTextBoxStartSida_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = SearchTextBox.Text.ToLower(); // Assuming SearchTextBox is the name of your search TextBox
            using (var context = new RB_context())
            {
                var searchResult = context.kunder.Where(k => k.Namn.ToLower().Contains(searchText)
                                                          || k.PersonNr.ToString().Contains(searchText)
                                                          || k.Address.ToLower().Contains(searchText)
                                                          || k.Epost.ToLower().Contains(searchText)
                                                          || k.TeleNr.ToString().Contains(searchText)).ToList();
                customerDataGrid.ItemsSource = searchResult;
            }
        }

        private void SearchTextBoxBokaTid_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = SökTidBox.Text.ToLower();
            using (var context = new RB_context())
            {
                if (string.IsNullOrWhiteSpace(searchText))
                {
                    bookingDataGrid.ItemsSource = context.besök.ToList();

                }
                else
                {
                

                        int searchInt;
                        bool isInt = int.TryParse(searchText, out searchInt);

                        var searchResult = context.besök
                            .Where(b => b.KundID == searchInt)

                            .ToList();

                        bookingDataGrid.ItemsSource = searchResult;
                    

                }
            }
        }




        private void BokaTidButton_Click(object sender, RoutedEventArgs e)
        {
            BokaTid bokaTidWindow = new BokaTid();
            bokaTidWindow.Show();
            this.Close();
        }








        // a method for the search button that will search for the customer


    }
}
