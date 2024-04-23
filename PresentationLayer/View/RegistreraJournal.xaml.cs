
using Microsoft.EntityFrameworkCore;
using RB_Ärendesystem.Datalayer;
using RB_Ärendesystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PresentationLayer.View
{
    /// <summary>
    /// Interaction logic for RegistreraJournal.xaml
    /// </summary>
    public partial class RegistreraJournal : Window
    {
        public RegistreraJournal()
        {
            InitializeComponent();

            RB_context testDB = new RB_context();
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Close();
            // Handle the click event for the "Log out" button
            // Add logic to log out the user and navigate back to the login page
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            StartSida startSida = new StartSida();
            startSida.Show();
            this.Close();
            // Handle the click event for the "Back" button
            // Add logic to navigate back to the previous page
        }
    }




}
