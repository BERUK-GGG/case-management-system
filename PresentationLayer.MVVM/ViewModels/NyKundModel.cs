using Affärslager;
using PresentationLayer.MVVM.Commands;
using PresentationLayer.MVVM.Models;
using RB_Ärendesystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.Windows.Controls;
using PresentationLayer.MVVM.Views;
using PresentationLayer.MVVM.View;

namespace PresentationLayer.MVVM.ViewModels
{
    public class NyKundModel : ObservableObject
    {
        private string _namn  ;
        public string Namn
        {
            get { return _namn; }
            set { _namn = value; OnPropertyChanged(); }
        }

        private string _personNr  ;
        public string PersonNr
        {
            get { return _personNr; }
            set { _personNr = value; OnPropertyChanged(); }
        }

        private string _address  ;
        public string Address
        {
            get { return _address; }
            set { _address = value; OnPropertyChanged(); }
        }

        private string _teleNr ;
        public string TeleNr
        {
            get { return _teleNr; }
            set { _teleNr = value; OnPropertyChanged(); }
        }

        private string _epost ;
        public string Epost
        {
            get { return _epost; }
            set { _epost = value; OnPropertyChanged(); }
        }

        private ICommand _sparaCommand;
        public ICommand SparaCommand =>
            _sparaCommand ??= new RelayCommand(Spara);

        private void Spara()
        {
            List<string> errorMessages = new List<string>();
            // Handle save logic here
            try
            {
                // Check if Namn textbox contains only letters
                

                // Check if PersonNr textbox contains numeric value
                if (!int.TryParse(PersonNr, out _))
                {
                    // Collect error message for PersonNr
                    errorMessages.Add("Invalid input format for 'PersonNr'. Please enter numeric value.");
                }

                // Check if TeleNr textbox contains numeric value
                if (!int.TryParse(TeleNr, out _))
                {
                    // Collect error message for TeleNr
                    errorMessages.Add("Invalid input format for 'TeleNr'. Please enter numeric value.");
                }

                if (errorMessages.Count > 0)
                {
                    // Display all collected error messages
                    MessageBox.Show(string.Join("\n", errorMessages), "Error");
                    return;
                }


                Kund newKund = new Kund
                {
                    Namn = Namn,
                    PersonNr = int.Parse(PersonNr),
                    Address = Address,
                    TeleNr = int.Parse(TeleNr),
                    Epost = Epost
                };
                NyKundController nyKundController = new NyKundController();

                nyKundController.LäggTillNyKund(newKund);
                MessageBox.Show("New customer saved successfully.");
            }
            catch (FormatException)
            {
                // Display an error message indicating invalid input format
                MessageBox.Show("Invalid input format. Please enter numeric values for 'PersonNr' and 'TeleNr'.", "Error");
            }
        }
        private ICommand _backCommand;
        public ICommand BackCommand =>
            _backCommand ??= new RelayCommand(Back);

        private void Back()
        {
            StartSida startSida = new StartSida();
            startSida.Show();

            NyKund nykundPage = Application.Current.Windows.OfType<NyKund>().FirstOrDefault();
            nykundPage?.Close();

            
        }
        private ICommand _logoutCommand;
        public ICommand LogoutCommand =>
            _logoutCommand ??= new RelayCommand(Logout);

        private void Logout()
        {
            Login login = new Login();
            login.Show();

            NyKund nykundPage = Application.Current.Windows.OfType<NyKund>().FirstOrDefault();
            nykundPage?.Close();

            

        }
    }
}

