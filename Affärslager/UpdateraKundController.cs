using RB_Ärendesystem.Datalayer;
using RB_Ärendesystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Affärslager
{
    public class UpdateraKundController
    {


        public void UpdateraKund(Kund selectedKund)
        {
            using (var uow = new UnitOfWork())
            {
                //context.Entry(selectedKund).State = EntityState.Modified;
                //context.SaveChanges();
                uow.Kunds.Update(selectedKund);
                uow.SaveChanges();
            }
        }
    }
}
