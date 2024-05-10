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
        public BokaTidModel()
        {
            CustomerItems = tabeller.KundTabell();
        }

        private DataGrid _customerDataGrid;

        public DataGrid customerDataGrid
        {
            get { return _customerDataGrid; }
            set { _customerDataGrid = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Kund> _customerItems;
        public ObservableCollection<Kund> CustomerItems
        {
            get { return _customerItems; }
            set { _customerItems = value; OnPropertyChanged(); }
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

