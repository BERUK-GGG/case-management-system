using PresentationLayer.MVVM.Commands;
using PresentationLayer.MVVM.Models;
using RB_Ärendesystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Affärslager;
using System.Windows;
using PresentationLayer.MVVM.Views;
namespace PresentationLayer.MVVM.ViewModels
{
    public class BeställReservdelModel : ObservableObject
    {
        private string _reservdel;
        public string Reservdel
        {
            get { return _reservdel; }
            set { _reservdel = value; OnPropertyChanged(); }
        }

        private string _pris;
        public string Pris
        {
            get { return _pris; }
            set { _pris = value; OnPropertyChanged(); }
        }

        private ICommand _sparaCommand;
        public ICommand SparaCommand =>
            _sparaCommand ??= new RelayCommand(Spara);

        private void Spara()
        {
            List<string> errorMessages = new List<string>();
            try
            {
                // Check if Namn textbox contains only letters


                // Check if PersonNr textbox contains numeric value
                if (!double.TryParse(Pris, out _))
                {
                    // Collect error message for PersonNr
                    errorMessages.Add("Invalid input format for 'pris'. Please enter numeric value.");
                }

                // Check if TeleNr textbox contains numeric value
                

                if (errorMessages.Count > 0)
                {
                    // Display all collected error messages
                    MessageBox.Show(string.Join("\n", errorMessages), "Error");
                    return;
                }
                // Implement save logic here
                Reservdel newReserv = new Reservdel
                {
                    Namn = Reservdel,
                    Pris = decimal.Parse(Pris),



                };
                NyReservdelController NyReservdelController = new NyReservdelController();

                NyReservdelController.LäggTillReservdel(newReserv);

                // Show a message indicating successful saving
                MessageBox.Show("Ny reservdel registrerad!.");
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
            // Implement navigation logic to go back
            StartSida startSida = new StartSida();
            startSida.Show();

            BeställReservdel reservdelPage = Application.Current.Windows.OfType<BeställReservdel>().FirstOrDefault();
            reservdelPage?.Close();
        }

        private ICommand _logoutCommand;
        public ICommand LogoutCommand =>
            _logoutCommand ??= new RelayCommand(Logout);

        private void Logout()
        {
            Login login = new Login();
            login.Show();

            BeställReservdel reservdelPage = Application.Current.Windows.OfType<BeställReservdel>().FirstOrDefault();
            reservdelPage?.Close();



        }

    }
}
