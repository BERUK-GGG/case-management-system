using PresentationLayer.MVVM.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using Affärslager;
using PresentationLayer.MVVM.Models;
using PresentationLayer.MVVM.Views;
using System.Windows;

namespace PresentationLayer.MVVM.ViewModels
{
    public class LoginModel: ObservableObject
    {

        TabellController tabell = new TabellController();

        private string _username;
        public string Username
        {
            get { return _username; }
            set { _username = value; OnPropertyChanged(); }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set { _password = value; OnPropertyChanged(); }
        }

        private string _error;
        public string Error
        {
            get { return _error; }
            set { _error = value; OnPropertyChanged(); }
        }

        private ICommand _loginCommand;
        public ICommand LoginCommand =>
            _loginCommand ??= new RelayCommand(Login);

        private void Login()
        {
            if (Authenticate(Username, Password))
            {
                // Login successful
                // You can perform any action here, such as navigating to another page
                StartSida startSida = new StartSida();
                startSida.Show();

                Login login = Application.Current.Windows.OfType<Login>().FirstOrDefault();
                login?.Close();

                
                Username = string.Empty;
                Password = string.Empty;


            }
            else
            {
                // Login failed
                // You can display an error message here
                Error = "Invalid username or password.";
            }
        }

        public bool Authenticate(string Username, string Password)
        {
            var employee = tabell.MekanikerTabell().FirstOrDefault(a => a.AnvändarNamn == Username && a.lösenord == Password);
            return employee != null;
        }
    }
}
