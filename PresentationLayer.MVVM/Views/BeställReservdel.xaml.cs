using Affärslager;
using RB_Ärendesystem.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for BeställReservdel.xaml
    /// </summary>
    public partial class BeställReservdel : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        TabellController tabeller = new TabellController();



        public BeställReservdel()
        {
            InitializeComponent();

            //PopulateReservComboBox();
        }

    }


}

