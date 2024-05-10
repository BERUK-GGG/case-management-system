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
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.Windows.Controls;

namespace PresentationLayer.MVVM.ViewModels
{
    public class RegistreraJournalModel : ObservableObject
    {
        TabellController _tabellController = new TabellController();
        NyJournalController _nyJournalController = new NyJournalController();
        public RegistreraJournalModel()
        {
            // Initialize properties or fetch data here if needed

            Bokningar = _tabellController.BesökTabell();
            var journalItems = _tabellController.JournalTabell();

            // Fetch the list of mechanics from the database
            var reservdel = _tabellController.ReservdellTabell();

            var reservdelIdsInJournal = journalItems.Select(j => j.ID);

            // Filter reservdel based on ReservdelIds not present in the journalItems
            var reservdelFiltered = reservdel.Where(r => !reservdelIdsInJournal.Contains(r.ID)).ToList();
            Reservdelar = new List<Reservdel>(reservdelFiltered);
        }




        private ObservableCollection<Besök> _bokningar;
        public ObservableCollection<Besök> Bokningar
        {
            get { return _bokningar; }
            set { _bokningar = value; OnPropertyChanged(); }
        }

        private List<Reservdel> _reservdelar;
        public List<Reservdel> Reservdelar
        {
            get { return _reservdelar; }
            set { _reservdelar = value; OnPropertyChanged(); }
        }

        private string _besökID;
        public string BesökID
        {
            get { return _besökID; }
            set { _besökID = value; OnPropertyChanged(); }
        }

        private string _åtgärder;
        public string Åtgärder
        {
            get { return _åtgärder; }
            set { _åtgärder = value; OnPropertyChanged(); }
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
                    BesökID = SelectedBesök.ID.ToString();

                }
            }
        }

        private List<Reservdel> _selectedReservdelar;
        public List<Reservdel>  SelectedReservdelar
        {
            get { return _selectedReservdelar; }
            set { _selectedReservdelar = value ; OnPropertyChanged(); } 
        }

        private ICommand _sparaCommand;
        public ICommand SparaCommand =>
            _sparaCommand ??= new RelayCommand(Spara);

        private void Spara()
        {

            if (SelectedBesök != null)
            {
                if (SelectedReservdelar != null && SelectedReservdelar.Any())
                {
                    List<Reservdel> selectedReserv = new List<Reservdel>();
                    foreach (var selectedItem in SelectedReservdelar)
                    {
                        selectedReserv.Add((Reservdel)selectedItem);
                    }
                    _nyJournalController.AddJournal(åtgärder: Åtgärder, besök: SelectedBesök, Reservdelar: selectedReserv);
                    MessageBox.Show("Data saved successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            else
            {
                MessageBox.Show("Please select at least one Reservdel.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
            else
            {
                MessageBox.Show("Please select a Besök.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

}
        private ICommand _backCommand;
        public ICommand BackCommand =>
            _backCommand ??= new RelayCommand(Back);

        private void Back()
        {
            StartSida startSida = new StartSida();
            startSida.Show();

            RegistreraJournal RegJournalPage = Application.Current.Windows.OfType<RegistreraJournal>().FirstOrDefault();
            RegJournalPage?.Close();


        }
        private ICommand _logoutCommand;
        public ICommand LogoutCommand =>
            _logoutCommand ??= new RelayCommand(Logout);

        private void Logout()
        {
            Login login = new Login();
            login.Show();

            RegistreraJournal RegJournalPage = Application.Current.Windows.OfType<RegistreraJournal>().FirstOrDefault();
            RegJournalPage?.Close();



        }
    }
   
}
