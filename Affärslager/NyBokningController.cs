using RB_Ärendesystem.Datalayer;
using RB_Ärendesystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Affärslager
{
    public class NyBokningController
    {
        public UnitOfWork UnitOfWork { get; set; }

        public NyBokningController()
        {
            UnitOfWork = new UnitOfWork();
        }
        public void AddBooking(Kund selectedKund, Mekaniker selectedMekaniker, string syfte, DateTime selectedDate)
        {
            UnitOfWork.Besöks.Add(new Besök()
            {
                Kund = UnitOfWork.Kunds.Find(selectedKund.ID),
                Mekaniker = UnitOfWork.Mekanikers.Find(selectedMekaniker.Id),
                DateAndTime = selectedDate,
                Syfte = syfte
            });

            UnitOfWork.SaveChanges();
        }


    }
}
