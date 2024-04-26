using Entities;
using RB_Ärendesystem.Datalayer;
using RB_Ärendesystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Affärslager
{
    public class NyJournalController
    {
        public UnitOfWork UnitOfWork { get; set; }

        public NyJournalController()
        {
            UnitOfWork = new UnitOfWork();
        }
        public void AddJournal(string åtgärder, Besök besök, List<Reservdel> Reservdelar)
        {
            var u = new List<Reservdel>();
            foreach (Reservdel reservdel in Reservdelar)
            {
                u.Add(UnitOfWork.Reservdels.Find(reservdel.ID));

            }


            UnitOfWork.Journals.Add(new Journal()
            {
                Åtgärder = åtgärder,
                Besök = UnitOfWork.Besöks.Find(besök.ID),
                reservdelar = u


            });

            UnitOfWork.SaveChanges();
        }
    }
}

