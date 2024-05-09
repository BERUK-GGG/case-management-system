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


namespace PresentationLayer.MVVM.ViewModels
{
    public class MainViewModel : ObservableObject
    {
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
            TabellController tabeller = new TabellController();
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






    }



}