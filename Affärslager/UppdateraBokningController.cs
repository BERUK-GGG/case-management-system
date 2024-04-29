using RB_Ärendesystem.Datalayer;
using RB_Ärendesystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Affärslager
{
    public class UppdateraBokningController
    {
        public void UppdateraBokning(Besök selectedBokning)
        {
            using (var uow = new UnitOfWork())
            {
                //context.Entry(selectedKund).State = EntityState.Modified;
                //context.SaveChanges();
                uow.Besöks.Update(selectedBokning);
                uow.SaveChanges();
            }
        }

        public void TaBortBokning(Besök selectedBokning)
        {
            using (var uow = new UnitOfWork())
            {
                uow.Besöks.Delete(selectedBokning);
                uow.SaveChanges();
            }

        }
    }


}
