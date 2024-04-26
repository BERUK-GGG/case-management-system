using RB_Ärendesystem.Datalayer;
using RB_Ärendesystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Affärslager
{
    public class NyReservdelController
    {
        public void LäggTillReservdel(Reservdel reservdel)
        {
            // Add the new Kund object to the database
            using (var UoW = new UnitOfWork())
            {

                UoW.Reservdels.Add(reservdel);
                UoW.SaveChanges();
            }
        }
    }
}
