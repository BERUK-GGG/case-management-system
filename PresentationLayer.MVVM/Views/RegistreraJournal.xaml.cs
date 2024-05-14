
using Affärslager;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using PresentationLayer.MVVM.ViewModels;
using RB_Ärendesystem.Datalayer;
using RB_Ärendesystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

namespace PresentationLayer.MVVM.Views
{
    /// <summary>
    /// Interaction logic for RegistreraJournal.xaml
    /// </summary>
    public partial class RegistreraJournal : Window
    {

        RegistreraJournalModel DC;
        public RegistreraJournal()
        {
            InitializeComponent();

            DC = new RegistreraJournalModel();

            
            base.DataContext = DC;

        }


        private void UpdateSelectedItem(object sender, SelectionChangedEventArgs e)
        {
            var items = (ListBox)sender;

            foreach( Reservdel item in Reservdel.SelectedItems)
            {
                if (!DC.SelectedReservdelar.Contains(item))
                {
                    DC.SelectedReservdelar.Add(item);
                }
            }
        }
    }




}
