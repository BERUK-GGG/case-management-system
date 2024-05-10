using Affärslager;
using PresentationLayer.MVVM.Commands;
using PresentationLayer.MVVM.Models;
using PresentationLayer.MVVM.Views;
using PresentationLayer.View;
using RB_Ärendesystem.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;

namespace PresentationLayer.MVVM.ViewModels
{
    public class BokaTidModel : ObservableObject
    {
        TabellController tabeller = new TabellController();
        NyBokningController _nyBokningController = new NyBokningController();
        public BokaTidModel()
        {
            CustomerItems = tabeller.KundTabell();
            Mekaniker = tabeller.MekanikerTabell();
            Bokningar = tabeller.BesökTabell();
        }

        private DataGrid _customerDataGrid;

        public DataGrid customerDataGrid
        {
            get { return _customerDataGrid; }
            set { _customerDataGrid = value; OnPropertyChanged(); }
        }

        private string _namn;
        public string Namn
        {
            get { return _namn; }
            set { _namn = value; OnPropertyChanged(); }
        }

        private DateTime _selectedDate;
        public DateTime SelectedDate
        {
            get { return _selectedDate; }
            set { _selectedDate = value; OnPropertyChanged(); }
        }

        private string _selectedTime;
        public string SelectedTime
        {
            get { return _selectedTime; }
            set { _selectedTime = value; OnPropertyChanged(); }
        }

        private string _syfte;
        public string Syfte
        {
            get { return _syfte; }
            set { _syfte = value; OnPropertyChanged(); }
        }


        private ObservableCollection<Kund> _customerItems;
        public ObservableCollection<Kund> CustomerItems
        {
            get { return _customerItems; }
            set { _customerItems = value; OnPropertyChanged(); }
        }

        private Kund _selectedKund;
        public Kund SelectedKund
        {
            get { return _selectedKund; }
            set
            {
                _selectedKund = value;
                OnPropertyChanged();


                // Only update the selected customer name if SelectedKund is not null
                if (SelectedKund != null)
                {
                    Namn = SelectedKund.Namn;
                }
            }
        }

        private ObservableCollection<Besök> _bokningar = null!;
        public ObservableCollection<Besök> Bokningar
        {
            get => _bokningar;
            set { _bokningar = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Mekaniker> _mekaniker = null!;
        public ObservableCollection<Mekaniker> Mekaniker
        {
            get => _mekaniker;
            set { _mekaniker = value; OnPropertyChanged(); }
        }

        private Mekaniker _selectedMekaniker;
        public Mekaniker SelectedMekaniker
        {
            get { return _selectedMekaniker; }
            set { _selectedMekaniker = value; OnPropertyChanged(); }
        }

        private ICommand _refresh;
        public ICommand Refresh => _refresh ?? new RelayCommand(RefreshDataGrid);
        private void RefreshDataGrid()
        {
            Bokningar = tabeller.BesökTabell();
        }


        private ICommand _sparaCommand;
        public ICommand SparaCommand =>
            _sparaCommand ??= new RelayCommand(Spara);

        private void Spara()
        {


            // Check if a customer is selected
            //if (SelectedKund != null)
            //{
            //    // Check if a mechanic is selected
            //    if (SelectedMekaniker != null)
            //    {
            //        DateTime Date = SelectedDate;
            //        TimeOnly tid = TimeOnly.Parse(SelectedTime.ToString());
            //        DateTime selectedDateTime = Date.Date + tid.ToTimeSpan();

            //        Namn = SelectedKund;
            //        Mekaniker = SelectedMekaniker;
            //        Syfte = Syfte;
            //        Datum = selectedDateTime;


                           
                            

            //            // Save the booking
            //            _nyBokningController.AddBooking(SelectedKund, SelectedMekaniker, Syfte, selectedDateTime);

            //            RefreshDataGrid();

            //            // Provide feedback to the user
            //            MessageBox.Show("New booking saved successfully.");

            //            // Close the window
            //            Application.Current.MainWindow.Close();
                    
            //    }
            //}
        }
                    
                
        



        private ICommand _backCommand;
        public ICommand BackCommand =>
            _backCommand ??= new RelayCommand(Back);

        private void Back()
        {
            StartSida startSida = new StartSida();
            startSida.Show();

            BokaTid bokaTidPage = Application.Current.Windows.OfType<BokaTid>().FirstOrDefault();
            bokaTidPage?.Close();


        }
        private ICommand _logoutCommand;
        public ICommand LogoutCommand =>
            _logoutCommand ??= new RelayCommand(Logout);

        private void Logout()
        {
            Login login = new Login();
            login.Show();

            BokaTid bokaTidPage = Application.Current.Windows.OfType<BokaTid>().FirstOrDefault();
            bokaTidPage?.Close();



        }

    }
    
    }

