
using Affärslager;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using PresentationLayer.MVVM.ViewModels;
using RB_Ärendesystem.Datalayer;
using RB_Ärendesystem.Entities;
using System;
using System.Collections.Generic;
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

namespace PresentationLayer.MVVM.Views
{
    /// <summary>
    /// Interaction logic for RegistreraJournal.xaml
    /// </summary>
    public partial class RegistreraJournal : Window
    {
       
        public RegistreraJournal()
        {
            InitializeComponent();

           

            

            
        }

        //private void LogoutButton_Click(object sender, RoutedEventArgs e)
        //{
        //    Login login = new Login();
        //    login.Show();
        //    this.Close();
        //    // Handle the click event for the "Log out" button
        //    // Add logic to log out the user and navigate back to the login page
        //}

        //private void BackButton_Click(object sender, RoutedEventArgs e)
        //{
        //    StartSida startSida = new StartSida();
        //    startSida.Show();
        //    this.Close();
        //    // Handle the click event for the "Back" button
        //    // Add logic to navigate back to the previous page
        //}

        ////private void BesökDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        ////{
        ////    // Get the selected row
        ////    if (BesökDataGrid.SelectedItem != null)
        ////    {

        ////        Besök selectedBesök = (Besök)BesökDataGrid.SelectedItem;

        ////        // Populate TextBoxes with values from the selected row

        ////        BesökID.Text = selectedBesök.ID.ToString();

                

        ////    }


        ////}

        ////private void PopulateReservPartListBox()
        ////{

        ////    var journalItems = tabeller.JournalTabell().ToList();

        ////        // Fetch the list of mechanics from the database
        ////   var reservdel = tabeller.ReservdellTabell().ToList();

        ////    var reservdelIdsInJournal = journalItems.Select(j => j.ID).ToList();

        ////    // Filter reservdel based on ReservdelIds not present in the journalItems
        ////    var reservdelFiltered = reservdel.Where(r => !reservdelIdsInJournal.Contains(r.ID)).ToList();



        ////    // Bind the list to the ComboBox
        ////    Reservdel.ItemsSource = reservdelFiltered;

            

        ////    // Set the display member path to a property of Mekaniker class that represents the name
        ////    Reservdel.DisplayMemberPath = "Namn"; // Replace "Name" with the actual property name in your Mekaniker class
            
        ////}

        //private void SaveButton_Click(object sender, RoutedEventArgs e)
        //{
        //    //if (BesökDataGrid.SelectedItem != null )
        //    //{

        //    //    List<Reservdel> selectedReserv = new List<Reservdel>();
        //    //    foreach (var selectedItem in Reservdel.SelectedItems)
        //    //    {
        //    //        selectedReserv.Add((Reservdel)selectedItem);
        //    //    }
        //    //    Besök selectedBesök = (Besök)BesökDataGrid.SelectedItem;
           
        //    //    TabellController controller = new TabellController();
        //    //    nyJournalController.AddJournal(åtgärder: åtgärder.Text, besök: selectedBesök, Reservdelar: selectedReserv);

        //    //    MessageBox.Show("Data saved successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        //    //}
        //}

        //private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        //{
        //    TextBox textBox = sender as TextBox;
        //    if (textBox != null)
        //    {
        //        if (textBox.Text == textBox.Name)
        //        {
        //            textBox.Text = "";
        //        }
        //    }
        //}

        //private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        //{
        //    TextBox textBox = sender as TextBox;
        //    if (textBox != null)
        //    {
        //        if (string.IsNullOrWhiteSpace(textBox.Text))
        //        {
        //            textBox.Text = textBox.Name;
        //        }
        //    }
        //}

        private void UpdateSelectedItem(object sender, SelectionChangedEventArgs e)
        {
            var listSelectedItems = ((ListBox)sender).SelectedItems;
            

            RegistreraJournalModel reg = new RegistreraJournalModel();

            reg.SelectedReservdelar = listSelectedItems.Cast<Reservdel>().ToList();
        }
    }




}
