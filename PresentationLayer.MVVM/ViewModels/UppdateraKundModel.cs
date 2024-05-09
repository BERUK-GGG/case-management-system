using Affärslager;
using PresentationLayer.MVVM.Commands;
using PresentationLayer.MVVM.Models;
using PresentationLayer.MVVM.Views;
using RB_Ärendesystem.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace PresentationLayer.MVVM.ViewModels
{
    public class UppdateraKundModel: ObservableObject
    {
        TabellController _tabellController = new TabellController();
        UpdateraKundController UpdateraKundController = new UpdateraKundController();
        UppdateraBokningController uppdateraBokningController = new UppdateraBokningController();

        public UppdateraKundModel() 
        {
            // Initialize the Kunder property with data from the TabellController
            Kunder = _tabellController.KundTabell();

            
        }

        private string _kundId;
        public string KundId
        {
            get { return _kundId; }
            set { _kundId = value; OnPropertyChanged(); }
        }

        private string _namn;
        public string Namn
        {
            get { return _namn; }
            set { _namn = value; OnPropertyChanged(); }
        }

        private string _personNr;
        public string PersonNr
        {
            get { return _personNr; }
            set { _personNr = value; OnPropertyChanged(); }
        }

        private string _address;
        public string Address
        {
            get { return _address; }
            set { _address = value; OnPropertyChanged(); }
        }

        private string _teleNr;
        public string TeleNr
        {
            get { return _teleNr; }
            set { _teleNr = value; OnPropertyChanged(); }
        }

        private string _epost;
        public string Epost
        {
            get { return _epost; }
            set { _epost = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Kund> _kunder = null!;
        public ObservableCollection<Kund> Kunder
        {
            get => _kunder;
            set { _kunder = value; OnPropertyChanged(); }
        }
        private Kund _selectedKund;
        public Kund SelectedKund
        {
            get { return _selectedKund; }
            set 
            { 
                _selectedKund = value; 
                OnPropertyChanged();
                if (SelectedKund != null)
                {
                    Namn = SelectedKund.Namn;
                    PersonNr = SelectedKund.PersonNr.ToString();
                    Address = SelectedKund.Address;
                    Epost = SelectedKund.Epost;
                    TeleNr = SelectedKund.TeleNr.ToString();
                }
            }
        }
      

        private ICommand _refresh;
        public ICommand Refresh => _refresh ?? new RelayCommand(RefreshDataGrid);
        private void RefreshDataGrid()
        {
            Kunder = _tabellController.KundTabell();
        }


        private ICommand _sparaCommand;
        public ICommand SparaCommand =>
            _sparaCommand ??= new RelayCommand(Spara);

        private void Spara()
        {
            // Implement save logic here
            if (SelectedKund != null)
            {
                SelectedKund.Namn = Namn;
                SelectedKund.PersonNr = int.Parse(PersonNr);
                SelectedKund.Address = Address;
                SelectedKund.Epost = Epost;
                SelectedKund.TeleNr = int.Parse(TeleNr);

                UpdateraKundController.UpdateraKund(SelectedKund);
                RefreshDataGrid();

                clear();

            }
        }

        private void clear()
        {
            Namn = string.Empty;
            PersonNr = string.Empty;
            Epost = string.Empty;
            TeleNr = string.Empty;
            Address = string.Empty;
        }

        private ICommand _backCommand;
        public ICommand BackCommand =>
            _backCommand ??= new RelayCommand(Back);

        private void Back()
        {
            // Implement navigation logic to go back
            StartSida startSida = new StartSida();
            startSida.Show();

            UppdateraKund UpdateraPage = Application.Current.Windows.OfType<UppdateraKund>().FirstOrDefault();
            UpdateraPage?.Close();

        }

        private ICommand _raderaKundCommand;
        public ICommand RaderaKundCommand =>
            _raderaKundCommand ??= new RelayCommand(RaderaKund);

        private void RaderaKund()
        {
            // Implement logic to delete the customer
            bool success = false;
            if (SelectedKund != null)
            {
                if (!success)
                {


                    // Find all Besök entries with the same KundID as the one we tried to delete
                    var relatedBesök = _tabellController.BesökTabell().Where(b => b.Kund.ID == SelectedKund.ID).ToList();

                    foreach (var besök in relatedBesök)
                    {
                        var relatedJournal = _tabellController.JournalTabell().Where(j => j.Besök.ID == besök.ID).ToList();
                        if (!relatedJournal.Any())
                        {
                            uppdateraBokningController.TaBortBokning(besök);
                            RefreshDataGrid();
                        }
                        else
                        {
                            System.Windows.MessageBox.Show("This customer cannot be deleted due to associated with a booking that is already completed.", "Error");
                            return; // Exit the method since deletion is not possible
                        }
                    }




                    System.Windows.MessageBoxResult result = System.Windows.MessageBox.Show("Do you want to continue?", "Confirmation", System.Windows.MessageBoxButton.YesNo);

                    if (result == System.Windows.MessageBoxResult.Yes)
                    {
                        // If user clicked Yes, delete the selected Kund
                        UpdateraKundController.TaBortKund(SelectedKund);
                        RefreshDataGrid();
                        clear();
                    }
                }
            }
        }
        private ICommand _logoutCommand;
        public ICommand LogoutCommand =>
            _logoutCommand ??= new RelayCommand(Logout);

        private void Logout()
        {
            Login login = new Login();
            login.Show();

            UppdateraKund UpdateraPage = Application.Current.Windows.OfType<UppdateraKund>().FirstOrDefault();
            UpdateraPage?.Close();



        }

        private string _searchText;
        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                OnPropertyChanged(nameof(SearchText));
                Search(_searchText);
            }
        }
        private void Search(string searchText)
        {
            // Convert the search text to lowercase
            searchText = searchText.ToLower();

            // Perform the search based on the search text
            var searchResult = _tabellController.KundTabell().Where(k =>
                k.Namn.ToLower().Contains(searchText) 
            ).ToList();

            // Update the Kunder collection with the search result
            Kunder = new ObservableCollection<Kund>(searchResult);
        }

    }
}
