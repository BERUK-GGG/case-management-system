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

        private void CustomerButton()
        {
            KundContentVisibility = Visibility.Visible;
            JournalContentVisibility = Visibility.Collapsed;
            BokningContentVisibility = Visibility.Collapsed;
            ReservdelarContentVisibility = Visibility.Collapsed;


            // Hide other content controls if needed
        }

        private void BokningButton()
        {
            KundContentVisibility = Visibility.Collapsed;
            JournalContentVisibility = Visibility.Collapsed;
            BokningContentVisibility = Visibility.Visible;
            ReservdelarContentVisibility = Visibility.Collapsed;
            // Hide other content controls if needed
        }

        private void ReservdelarButton()
        {
            KundContentVisibility = Visibility.Collapsed;
            JournalContentVisibility = Visibility.Collapsed;
            BokningContentVisibility = Visibility.Collapsed;
            ReservdelarContentVisibility = Visibility.Visible;
            // Hide other content controls if needed
        }

        private void JournalButton()
        {
            KundContentVisibility = Visibility.Collapsed;
            JournalContentVisibility = Visibility.Visible;
            BokningContentVisibility = Visibility.Collapsed;
            ReservdelarContentVisibility = Visibility.Collapsed;
            // Hide other content controls if needed
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
            if (navigationStack.Count > 0)
            {
                UIElement previousContent = navigationStack.Pop();
                previousContent.Visibility = Visibility.Collapsed;
                if (navigationStack.Count == 0)
                {
                    BackButtonVisibility = Visibility.Collapsed;
                }
            }
        }
    }



}