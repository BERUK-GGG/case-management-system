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
using Affärslager;





namespace PresentationLayer
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    
    public partial class StartSida : Window
    {
        TabellController tabell = new TabellController();
        private Stack<UIElement> navigationStack = new Stack<UIElement>();
        public StartSida()
        {
            InitializeComponent();

            RefreshDataGrid();

        }


        private void RefreshDataGrid()
        {
            // Re-bind the DataGrid to update its content
            TabellController tabeller = new TabellController();

            JournalDataGrid.ItemsSource = tabeller.JournalTabell();
            customerDataGrid.ItemsSource = tabeller.KundTabell();
            bookingDataGrid.ItemsSource = tabeller.BesökTabell();
            ReservDataGrid.ItemsSource = tabeller.ReservdellTabell();

            


        }

        private void CustomerButton_Click(object sender, RoutedEventArgs e)
        {

            ReservContentControl.Visibility = Visibility.Collapsed;
            KundContentControl.Visibility = Visibility.Visible;
            BokingContentControl.Visibility = Visibility.Collapsed;
            JournalContentControl.Visibility = Visibility.Collapsed;
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
            JournalContentControl.Visibility = Visibility.Collapsed;
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
            JournalContentControl.Visibility = Visibility.Collapsed;
            // Handle the click event for the "Resevdelar" button
            // Add logic to navigate to the parts page or perform other actions
            navigationStack.Push(ReservContentControl);
            BackButton.Visibility = Visibility.Visible;
        }

        private void Journal_Click(object sender, RoutedEventArgs e)
        {
            ReservContentControl.Visibility = Visibility.Collapsed;
            BokingContentControl.Visibility = Visibility.Collapsed;
            KundContentControl.Visibility = Visibility.Collapsed;
            JournalContentControl.Visibility = Visibility.Visible;
            // Handle the click event for the "Journal" button
            // Add logic to navigate to the booking page or perform other actions
            navigationStack.Push(JournalContentControl);
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

            //TabellController tabell = new TabellController();
            //string searchText = SearchTextBox.Text.ToLower(); // Assuming SearchTextBox is the name of your search TextBox


            //SearchAndUpdateGrid(tabell.KundTabell(), k => k.Namn.ToLower().Contains(searchText)
            //                                   || k.PersonNr.ToString().Contains(searchText)
            //                                   || k.Address.ToLower().Contains(searchText)
            //                                   || k.Epost.ToLower().Contains(searchText)
            //                                   || k.TeleNr.ToString().Contains(searchText), customerDataGrid);


            //SearchAndUpdateGrid(tabell.JournalTabell(), k => k.Besök.ToString().Contains(searchText), JournalDataGrid);

            //// Search in bookingDataGrid
            //SearchAndUpdateGrid(tabell.BesökTabell(), item => item.Kund.ToString().Contains(searchText), bookingDataGrid);

            //// Search in ReservDataGrid
            //SearchAndUpdateGrid(tabell.ReservdellTabell(), item => item.Namn.ToString().Contains(searchText), ReservDataGrid);

            string searchText = SearchTextBox.Text.ToLower(); // Convert search text to lowercase for case-insensitive search
            List<Button> foundButtons = FindButtons(this, searchText);
           
            
            lstResults.Items.Clear();

            if (string.IsNullOrEmpty(searchText))
            {
                lstResults.Visibility = Visibility.Collapsed;
            }
            else if (foundButtons.Count > 0)
            {
                lstResults.Visibility = Visibility.Visible;

                // Display found buttons in the ListBox
                foreach (Button button in foundButtons)
                {
                    lstResults.Items.Add(button.Content);
                }
            }


        }
        private void lstResults_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstResults.SelectedIndex != -1)
            {
                string buttonName = lstResults.SelectedItem.ToString();
                Button selectedButton = FindButtonByName(buttonName);

                //// Perform the same function as the selected button
                if (selectedButton != null)
                {
                    selectedButton.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                }

            }
        }

        private Button FindButtonByName(string buttonName)
        {
            foreach (Button button in FindButtons(this, ""))
            {
                if (button.Content == buttonName)
                {
                    return button;
                }
            }
            return null;
        }
        private void SearchAndUpdateGrid<T>(IEnumerable<T> source, Func<T, bool> searchCriteria, DataGrid grid)
        {
            var searchResult = source.Where(searchCriteria).ToList();
            grid.ItemsSource = searchResult;
        }


        private void SearchTextBoxBokaTid_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = SökTidBox.Text.ToLower();
            
            
                if (string.IsNullOrWhiteSpace(searchText))
                {
                    bookingDataGrid.ItemsSource = tabell.BesökTabell();

                }
                else
                {
                

                        int searchInt;
                        bool isInt = int.TryParse(searchText, out searchInt);

                        var searchResult = tabell.BesökTabell()
                            .Where(b => b.Kund.ID == searchInt)

                            .ToList();

                        bookingDataGrid.ItemsSource = searchResult;
                    

                }
            
        }

        private void SearchTextBoxReserv_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = SökReservBox.Text.ToLower();
           
            
                if (string.IsNullOrWhiteSpace(searchText))
                {
                    ReservDataGrid.ItemsSource = tabell.ReservdellTabell();
                }
                else
                {
                    int searchInt;
                    bool isInt = int.TryParse(searchText, out searchInt);

                    var searchResult = tabell.ReservdellTabell()
                        .Where(r => r.ID == searchInt) // Search by ID
                        .ToList();

                    ReservDataGrid.ItemsSource = searchResult;
                }
           
        }




        private void BokaTidButton_Click(object sender, RoutedEventArgs e)
        {
            BokaTid bokaTidWindow = new BokaTid();
            bokaTidWindow.Show();
            this.Close();
        }

        private void UppdateraBokningButton_Click(object sender, RoutedEventArgs e)
        {
            UppdateraBokning uppdaterabokningWindow = new UppdateraBokning();
            uppdaterabokningWindow.Show();
            this.Close();
        }

        private void RegistreraJournal_Click(object sender, RoutedEventArgs e)
        {
            RegistreraJournal RegistreraJournalWindow = new RegistreraJournal();
            RegistreraJournalWindow.Show();
            this.Close();
        }

        private void BeställReservdel_Click(object sender, RoutedEventArgs e)
        {
            BeställReservdel BeställReservdelWindow = new BeställReservdel();
            BeställReservdelWindow.Show();
            this.Close();
        }
        private List<Button> FindButtons(DependencyObject container, string searchText)
        {
            List<Button> foundButtons = new List<Button>();

            // Iterate through all children of the container
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(container); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(container, i);

                // Check if the child is a Button
                if (child is Button button)
                {
                    // Check if the button's content (text) contains the search text
                    if (button.Content != null && button.Content.ToString().ToLower().Contains(searchText))
                    {
                        foundButtons.Add(button);
                    }
                }

                // If the child has children, recursively search its children
                if (child != null && VisualTreeHelper.GetChildrenCount(child) > 0)
                {
                    foundButtons.AddRange(FindButtons(child, searchText));
                }
            }

            return foundButtons;
        }






        // a method for the search button that will search for the customer


    }
}
