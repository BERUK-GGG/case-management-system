using Entities;
using RB_Ärendesystem.Datalayer;
using RB_Ärendesystem.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Affärslager
{
    public class TabellController
    {
        public UnitOfWork UnitOfWork { get; set; }

        public TabellController()
        {
            UnitOfWork = new UnitOfWork();
        }

        //public IEnumerable<Journal> JournalTabell()
        //{
        //    using (var UoW = new UnitOfWork(new RB_context()))
        //    {

        //        return UoW.Journals.GetAll().ToList();
        //    }
        //}
        public ObservableCollection<Journal> JournalTabell()
        {
            using (var UoW = new UnitOfWork(new RB_context()))
            {
                // Load data from the database into local memory
                var journals = UoW.Journals.GetAll();

                // Return the data as an ObservableCollection
                return new ObservableCollection<Journal>(journals);
            }
        }




        public ObservableCollection<Besök> BesökTabell()
        {
            using (var UoW = new UnitOfWork(new RB_context()))
            {
                // Load data from the database into local memory
                var besöks = UoW.Besöks.GetAll();

                // Return the data as an ObservableCollection
                return new ObservableCollection<Besök>(besöks);
            }
        }
        public ObservableCollection<Kund> KundTabell()
        {
            using (var UoW = new UnitOfWork(new RB_context()))
            {
                // Load data from the database into local memory
                var kunds = UoW.Kunds.GetAll();

                // Return the data as an ObservableCollection
                return new ObservableCollection<Kund>(kunds);
            }
        }
        public ObservableCollection<Reservdel> ReservdellTabell()
        {
            using (var UoW = new UnitOfWork(new RB_context()))
            {
                // Load data from the database into local memory
                var Reservdels = UoW.Reservdels.GetAll();

                // Return the data as an ObservableCollection
                return new ObservableCollection<Reservdel>(Reservdels);
            }
        }

        public ObservableCollection<Mekaniker> MekanikerTabell()
        {
            using (var UoW = new UnitOfWork(new RB_context()))
            {
                // Load data from the database into local memory
                var mekanikers = UoW.Mekanikers.GetAll();

                // Return the data as an ObservableCollection
                return new ObservableCollection<Mekaniker>(mekanikers);
            }
        }

        
    }
}
