using PresentationLayer.MVVM.Commands;
using PresentationLayer.MVVM.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using Affärslager;
using PresentationLayer.MVVM.Models;
using System.Windows;
using System.Windows.Controls;
using Castle.Core.Resource;
using System.Collections.ObjectModel;
using RB_Ärendesystem.Entities;
using Entities;
using PresentationLayer.MVVM.View;
using PresentationLayer.View;


namespace PresentationLayer.MVVM.ViewModels
{
    public class MainViewModel : ObservableObject
    {

        StartSida startSidaWindow = Application.Current.Windows.OfType<StartSida>().FirstOrDefault();

        TabellController tabeller = new TabellController();
        public MainViewModel() 
        {
            CustomerItems = tabeller.KundTabell();
            BookingItems = tabeller.BesökTabell();
            JournalItems = tabeller.JournalTabell();
            ReservdelItems = tabeller.ReservdellTabell();
        }

        private Visibility _isResultsVisible = Visibility.Collapsed;
        public Visibility IsResultsVisible
        {
            get { return _isResultsVisible; }
            set { _isResultsVisible = value; OnPropertyChanged(nameof(IsResultsVisible)); }
        }
        private string _searchText;
        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                OnPropertyChanged(nameof(SearchText));
                // Call the search command whenever the search text changes
                SearchCommandA.Execute(null);
            }
        }


        private ObservableCollection<string> _foundButtons;
        public ObservableCollection<string> FoundButtons
        {
            get { return _foundButtons; }
            set { _foundButtons = value; OnPropertyChanged(nameof(FoundButtons)); }
        }

        public ICommand SearchCommandA => new RelayCommand(Search);

        private void Search()
        {
            string searchText = SearchText.ToLower(); // Convert search text to lowercase for case-insensitive search
            if (startSidaWindow != null)
            {
                // Call a method in the view model to perform the search and update the UI
                List<Button> foundItems = FindButtons( startSidaWindow, searchText);

                // Update the collection bound to the ListBox
                FoundButtons = new ObservableCollection<string>();
                foreach (Button button in foundItems)
                {
                    FoundButtons.Add((string)button.Content);
                }

                // Update visibility based on search results
                if (string.IsNullOrEmpty(searchText) || FoundButtons.Count == 0)
                {
                    IsResultsVisible = Visibility.Collapsed;
                }
                else
                {
                    IsResultsVisible = Visibility.Visible;
                }
            }
        }

        public List<Button> FindButtons(DependencyObject container, string searchText)
        {
            List<Button> foundButtons = new List<Button>();

            // Iterate through all children of the container
            foreach (var child in LogicalTreeHelper.GetChildren(container).OfType<UIElement>())
            {
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
                if (child != null)
                {
                    foundButtons.AddRange(FindButtons(child, searchText));
                }
            }

            return foundButtons;
        }

        private string _selectedListBoxItem;
        public string SelectedListBoxItem
        {
            get { return _selectedListBoxItem; }
            set { _selectedListBoxItem = value; OnPropertyChanged(nameof(SelectedListBoxItem));
                ListBoxSelectionChangedCommand.Execute(null);
            }
        }

        public ICommand ListBoxSelectionChangedCommand => new RelayCommand(ExecuteListBoxSelectionChanged);

        private void ExecuteListBoxSelectionChanged()
        {
            if (SelectedListBoxItem != null)
            {
                string buttonName = SelectedListBoxItem;
                Button selectedButton = FindButtonByName(buttonName);

                //// Perform the same function as the selected button
                if (selectedButton != null)
                {
                    


                    selectedButton.Command.Execute(null);
                }
                else
                {
                    // Handle the case where no button is found with the given name
                }
            }
        }
        private Button FindButtonByName(string buttonName)
        {
            foreach (Button button in FindButtons(startSidaWindow, ""))
            {
                if (button.Content.ToString() == buttonName)
                {
                    return button;
                }
            }
            return null;
        }

        


        private ICommand exitCommand = null!;
        public ICommand ExitCommand =>
            exitCommand ??= exitCommand = new RelayCommand(Logout);



        private void Logout()
        {
            Login login = new Login();
            login.Show();
            // Get the reference to the StartSida window
            
            
            StartSida startSida = Application.Current.Windows.OfType<StartSida>().FirstOrDefault();

            // Close the StartSida window if it exists
            startSida?.Close();

            
            
        }

        // Define properties for DataGrids
        private DataGrid _journalDataGrid;
        private DataGrid _customerDataGrid;
        private DataGrid _bookingDataGrid;
        private DataGrid _reservDataGrid;

        // Define properties to bind to the DataGrids in your XAML
        public DataGrid JournalDataGrid
        {
            get { return _journalDataGrid; }
            set { _journalDataGrid = value; OnPropertyChanged(); }
        }

        public DataGrid customerDataGrid
        {
            get { return _customerDataGrid; }
            set { _customerDataGrid = value; OnPropertyChanged(); }
        }

        public DataGrid bookingDataGrid
        {
            get { return _bookingDataGrid; }
            set { _bookingDataGrid = value; OnPropertyChanged(); }
        }

        public DataGrid ReservDataGrid
        {
            get { return _reservDataGrid; }
            set { _reservDataGrid = value; OnPropertyChanged(); }
        }


        private ObservableCollection<Kund> _customerItems;
        public ObservableCollection<Kund> CustomerItems
        {
            get { return _customerItems; }
            set { _customerItems = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Besök> _bookingItems;
        public ObservableCollection<Besök> BookingItems
        {
            get { return _bookingItems; }
            set { _bookingItems = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Journal> _journalItems;
        public ObservableCollection<Journal> JournalItems
        {
            get { return _journalItems; }
            set { _journalItems = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Reservdel> _reservdelItems;
        public ObservableCollection<Reservdel> ReservdelItems
        {
            get { return _reservdelItems; }
            set { _reservdelItems = value; OnPropertyChanged(); }
        }


        private ICommand refreshCommand = null!;
        public ICommand RefreshCommand =>
            refreshCommand ??= refreshCommand = new RelayCommand(RefreshDataGrid);

        private void RefreshDataGrid()
        {
            // Re-bind the DataGrid to update its content
            
            CustomerItems = new ObservableCollection<Kund>(tabeller.KundTabell());
            JournalItems = new ObservableCollection<Journal>(tabeller.JournalTabell());
            BookingItems = new ObservableCollection<Besök>(tabeller.BesökTabell());
            ReservdelItems = new ObservableCollection<Reservdel>(tabeller.ReservdellTabell());

            JournalDataGrid.ItemsSource = tabeller.JournalTabell();
            customerDataGrid.ItemsSource = tabeller.KundTabell();
            bookingDataGrid.ItemsSource = tabeller.BesökTabell();
            ReservDataGrid.ItemsSource = tabeller.ReservdellTabell();




        }


        private Visibility _kundContentVisibility = Visibility.Collapsed;
        public Visibility KundContentVisibility
        {
            get { return _kundContentVisibility; }
            set { _kundContentVisibility = value; OnPropertyChanged(); }
        }

        private ICommand _customerCommand;
        public ICommand CustomerCommand
        {
            get
            {
                if (_customerCommand == null)
                {
                    _customerCommand = new RelayCommand(CustomerButton);
                }
                return _customerCommand;
            }
        }

        private Visibility _mainFrameVisibility = Visibility.Collapsed;
        public Visibility MainFrameVisibility
        {
            get { return _mainFrameVisibility; }
            set { _mainFrameVisibility = value; OnPropertyChanged(); }
        }



        private Visibility _bokningContentVisibility = Visibility.Collapsed;
        public Visibility BokningContentVisibility
        {
            get { return _bokningContentVisibility; }
            set { _bokningContentVisibility = value; OnPropertyChanged(); }
        }

        private Visibility _reservdelarContentVisibility = Visibility.Collapsed;
        public Visibility ReservdelarContentVisibility
        {
            get { return _reservdelarContentVisibility; }
            set { _reservdelarContentVisibility = value; OnPropertyChanged(); }
        }

        private Visibility _journalContentVisibility = Visibility.Collapsed;
        public Visibility JournalContentVisibility
        {
            get { return _journalContentVisibility; }
            set { _journalContentVisibility = value; OnPropertyChanged(); }
        }

        private ICommand _bokningCommand;
        public ICommand BokningCommand
        {
            get
            {
                if (_bokningCommand == null)
                {
                    _bokningCommand = new RelayCommand(BokningButton);
                }
                return _bokningCommand;
            }
        }

        private ICommand _reservdelarCommand;
        public ICommand ReservdelarCommand
        {
            get
            {
                if (_reservdelarCommand == null)
                {
                    _reservdelarCommand = new RelayCommand(ReservdelarButton);
                }
                return _reservdelarCommand;
            }
        }

        private ICommand _journalCommand;
        public ICommand JournalCommand
        {
            get
            {
                if (_journalCommand == null)
                {
                    _journalCommand = new RelayCommand(JournalButton);
                }
                return _journalCommand;
            }
        }

        private ContentControl _kundContentControl;
        public ContentControl KundContentControl
        {
            get { return _kundContentControl; }
            set { _kundContentControl = value; OnPropertyChanged(); }
        }

        private ContentControl _bokningContentControl;
        public ContentControl BokningContentControl
        {
            get { return _bokningContentControl; }
            set { _bokningContentControl = value; OnPropertyChanged(); }
        }

        private ContentControl _reservdelarContentControl;
        public ContentControl ReservdelarContentControl
        {
            get { return _reservdelarContentControl; }
            set { _reservdelarContentControl = value; OnPropertyChanged(); }
        }

        private ContentControl _journalContentControl;
        public ContentControl JournalContentControl
        {
            get { return _journalContentControl; }
            set { _journalContentControl = value; OnPropertyChanged(); }
        }


        private void CustomerButton()
        {
            navigationStack.Push(KundContentControl); // Push current content onto the stack
            KundContentVisibility = Visibility.Visible;
            JournalContentVisibility = Visibility.Collapsed;
            BokningContentVisibility = Visibility.Collapsed;
            ReservdelarContentVisibility = Visibility.Collapsed;
        }

        private void BokningButton()
        {
            navigationStack.Push(BokningContentControl); // Push current content onto the stack
            KundContentVisibility = Visibility.Collapsed;
            JournalContentVisibility = Visibility.Collapsed;
            BokningContentVisibility = Visibility.Visible;
            ReservdelarContentVisibility = Visibility.Collapsed;
        }

        private void ReservdelarButton()
        {
            navigationStack.Push(ReservdelarContentControl); // Push current content onto the stack
            KundContentVisibility = Visibility.Collapsed;
            JournalContentVisibility = Visibility.Collapsed;
            BokningContentVisibility = Visibility.Collapsed;
            ReservdelarContentVisibility = Visibility.Visible;
        }

        private void JournalButton()
        {
            navigationStack.Push(JournalContentControl); // Push current content onto the stack
            KundContentVisibility = Visibility.Collapsed;
            JournalContentVisibility = Visibility.Visible;
            BokningContentVisibility = Visibility.Collapsed;
            ReservdelarContentVisibility = Visibility.Collapsed;
        }


        private Stack<UIElement> navigationStack = new Stack<UIElement>();

        private Visibility _backButtonVisibility = Visibility.Visible;
        public Visibility BackButtonVisibility
        {
            get { return _backButtonVisibility; }
            set { _backButtonVisibility = value; OnPropertyChanged(); }
        }

        private ICommand _backCommand;
        public ICommand BackCommand
        {
            get
            {
                if (_backCommand == null)
                {
                    _backCommand = new RelayCommand(BackButton);
                }
                return _backCommand;
            }
        }

        //private void HideBackButtonIfNeeded()
        //{
        //    if (navigationStack.Count == 0)
        //    {
        //        BackButtonVisibility = Visibility.Collapsed;
        //    }
        //    else
        //    {
        //        BackButtonVisibility = Visibility.Visible;
        //    }
        //}

        private void BackButton()
        {
            // Hide all content controls
            KundContentVisibility = Visibility.Collapsed;
            BokningContentVisibility = Visibility.Collapsed;
            ReservdelarContentVisibility = Visibility.Collapsed;
            JournalContentVisibility = Visibility.Collapsed;

            // Show the main frame
            MainFrameVisibility = Visibility.Visible;
        }


        private ICommand _nyKundCommand;
        public ICommand NyKundCommand =>
            _nyKundCommand ??= new RelayCommand(NyKundButton);

        private void NyKundButton()
        {
            NyKund nyKund = new NyKund();
            nyKund.Show();
            // Get the reference to the StartSida window


            StartSida startSida = Application.Current.Windows.OfType<StartSida>().FirstOrDefault();

            // Close the StartSida window if it exists
            startSida?.Close();
        }


        private ICommand _beställReservdelCommand;
        public ICommand BeställReservdelCommand =>
            _beställReservdelCommand ??= new RelayCommand(BeställReservdelButton);

        private void BeställReservdelButton()
        {
            BeställReservdel beställReservdel = new BeställReservdel();
            beställReservdel.Show();
            // Get the reference to the StartSida window


            StartSida startSida = Application.Current.Windows.OfType<StartSida>().FirstOrDefault();

            // Close the StartSida window if it exists
            startSida?.Close();
        }

        private ICommand _uppdateraKundCommand;
        public ICommand UppdateraKundCommand =>
            _uppdateraKundCommand ??= new RelayCommand(UppdateraKundButton);

        private void UppdateraKundButton()
        {
            UppdateraKund uppdateraKund = new UppdateraKund();
            uppdateraKund.Show();
            // Get the reference to the StartSida window


            StartSida startSida = Application.Current.Windows.OfType<StartSida>().FirstOrDefault();

            // Close the StartSida window if it exists
            startSida?.Close();
        }

        private ICommand _uppdateraBokningCommand;
        public ICommand UppdateraBokningCommand =>
            _uppdateraBokningCommand ??= new RelayCommand(UppdateraBokningButton);

        private void UppdateraBokningButton()
        {
            UppdateraBokning uppdateraBokning = new UppdateraBokning();
            uppdateraBokning.Show();
            // Get the reference to the StartSida window


            StartSida startSida = Application.Current.Windows.OfType<StartSida>().FirstOrDefault();

            // Close the StartSida window if it exists
            startSida?.Close();
        }

        private ICommand _bokaTidCommand;
        public ICommand BokaTidCommand =>
            _bokaTidCommand ??= new RelayCommand(BokaTidButton);

        private void BokaTidButton()
        {
            BokaTid bokaTid = new BokaTid();
            bokaTid.Show();
            // Get the reference to the StartSida window


            StartSida startSida = Application.Current.Windows.OfType<StartSida>().FirstOrDefault();

            // Close the StartSida window if it exists
            startSida?.Close();
        }

        private ICommand _registreraJournalCommand;
        public ICommand RegistreraJournalCommand =>
            _registreraJournalCommand ??= new RelayCommand(RegistreraJournalButton);

        private void RegistreraJournalButton()
        {
            RegistreraJournal registreraJournal = new RegistreraJournal();
            registreraJournal.Show();
            // Get the reference to the StartSida window


            StartSida startSida = Application.Current.Windows.OfType<StartSida>().FirstOrDefault();

            // Close the StartSida window if it exists
            startSida?.Close();
        }


    }



}