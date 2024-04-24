using Entities;
using RB_Ärendesystem.Datalayer;
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
    
    public void BesökTabell()
        {
            using (var UoW = new UnitOfWork(new RB_context()))
            {

                UoW.Besöks.GetAll();
            }
        }
        public void kundTabell()
        {
            using (var UoW = new UnitOfWork(new RB_context()))
            {

                UoW.Kunds.GetAll();
            }
        }
        public void ReservdelTabell()
        {
            using (var UoW = new UnitOfWork(new RB_context()))
            {

                UoW.Reservdels.GetAll();
            }
        }
    }
}
