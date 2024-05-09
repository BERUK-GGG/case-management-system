using Affärslager;
using PresentationLayer.MVVM.Commands;
using PresentationLayer.MVVM.Models;
using PresentationLayer.MVVM.Views;
using RB_Ärendesystem.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;
using System.Windows.Controls;

namespace PresentationLayer.MVVM.ViewModels
{
    public class UppdateraBokningModel: ObservableObject
    {
        TabellController _tabellController = new TabellController();
        UppdateraBokningController _uppdateraBokningController = new UppdateraBokningController();
        public UppdateraBokningModel()
        {
            Bokningar = _tabellController.BesökTabell();
            Mekaniker = _tabellController.MekanikerTabell();
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

        private string _kundNamn;
        public string KundNamn
        {
            get { return _kundNamn; }
            set { _kundNamn = value; OnPropertyChanged(); }
        }

        private string _syfte;
        public string Syfte
        {
            get { return _syfte; }
            set { _syfte = value; OnPropertyChanged(); }
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


        private ObservableCollection<Besök> _bokningar = null!;
        public ObservableCollection<Besök> Bokningar
        {
            get => _bokningar;
            set { _bokningar = value; OnPropertyChanged(); }
        }

        private Besök _selectedBesök;
        public Besök SelectedBesök
        {
            get { return _selectedBesök; }
            set
            {
                _selectedBesök = value;
                OnPropertyChanged();
                if (SelectedBesök != null)
                {
                    Syfte  = SelectedBesök.Syfte;
                    SelectedDate = SelectedBesök.DateAndTime;
                    KundNamn = SelectedBesök.Kund.ToString();
                    SelectedMekaniker = SelectedBesök.Mekaniker;
                    
                }
            }
        }
        private ICommand _refresh;
        public ICommand Refresh => _refresh ?? new RelayCommand(RefreshDataGrid);
        private void RefreshDataGrid()
        {
            Bokningar = _tabellController.BesökTabell();
        }
        private ICommand _backCommand;
        public ICommand BackCommand =>
            _backCommand ??= new RelayCommand(Back);

        private void Back()
        {
            // Implement navigation logic to go bac 
            StartSida startSida = new StartSida();
            startSida.Show();

            UppdateraBokning UpdateraPage = Application.Current.Windows.OfType<UppdateraBokning>().FirstOrDefault();
            UpdateraPage?.Close();

        }
        private ICommand _sparaCommand;
        public ICommand SparaCommand =>
            _sparaCommand ??= new RelayCommand(Spara);

        private void Spara()
        {
            // Implement save logic here
            if (SelectedBesök != null)
            {
                DateTime Date = SelectedDate;
                
                TimeOnly tid = TimeOnly.Parse(SelectedTime.ToString());

                DateTime selectedDateTime = Date.Date + tid.ToTimeSpan();

                SelectedBesök.Mekaniker = SelectedMekaniker;
                SelectedBesök.Syfte = Syfte;
                SelectedBesök.DateAndTime = selectedDateTime;


                _uppdateraBokningController.UppdateraBokning(SelectedBesök);
              
                RefreshDataGrid();

                //clear();

            }
        }

        private ICommand _raderaKundCommand;
        public ICommand RaderaKundCommand =>
            _raderaKundCommand ??= new RelayCommand(RaderaKund);

        private void RaderaKund()
        {
            // Implement logic to delete the customer
            bool success = false;
            if (SelectedBesök != null)
            {
                //if (!success)
                //{


                //    // Find all Besök entries with the same KundID as the one we tried to delete
                //    var relatedBesök = _tabellController.BesökTabell().Where(b => b.Kund.ID == SelectedKund.ID).ToList();

                //    foreach (var besök in relatedBesök)
                //    {
                //        var relatedJournal = _tabellController.JournalTabell().Where(j => j.Besök.ID == besök.ID).ToList();
                //        if (!relatedJournal.Any())
                //        {
                //            uppdateraBokningController.TaBortBokning(besök);
                //            RefreshDataGrid();
                //        }
                //        else
                //        {
                //            System.Windows.MessageBox.Show("This customer cannot be deleted due to associated with a booking that is already completed.", "Error");
                //            return; // Exit the method since deletion is not possible
                //        }
                //    }




                //    System.Windows.MessageBoxResult result = System.Windows.MessageBox.Show("Do you want to continue?", "Confirmation", System.Windows.MessageBoxButton.YesNo);

                //    if (result == System.Windows.MessageBoxResult.Yes)
                //    {
                //        // If user clicked Yes, delete the selected Kund
                //        UpdateraKundController.TaBortKund(SelectedKund);
                //        RefreshDataGrid();
                //        clear();
                //    }
                //}
            }
        }

        private ICommand _logoutCommand;
        public ICommand LogoutCommand =>
            _logoutCommand ??= new RelayCommand(Logout);

        private void Logout()
        {
            Login login = new Login();
            login.Show();

            UppdateraBokning UpdateraPage = Application.Current.Windows.OfType<UppdateraBokning>().FirstOrDefault();
            UpdateraPage?.Close();



        }
    }
}
