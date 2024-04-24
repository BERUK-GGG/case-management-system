using RB_Ärendesystem.Datalayer;
using RB_Ärendesystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Affärslager
{
    public class BokaTidController
    {
        public void LäggTillBesök(Besök NyTid)
        {
            using (var UoW = new UnitOfWork(new RB_context()))
            {

                UoW.Besöks.Add(NyTid);

                UoW.SaveChanges();
            }
        }
        
    }
}
