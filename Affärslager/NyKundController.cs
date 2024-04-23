using RB_Ärendesystem.Datalayer;
using RB_Ärendesystem.Entities;

namespace Affärslager
{
    public class NyKundController
    {
        public NyKundController()
        {
                
        }

        public void LäggTillNyKund(Kund NyKund)
        {
            // Add the new Kund object to the database
            using (var UoW = new UnitOfWork(new RB_context()))
            {

                UoW.kunds.Add(NyKund);
                
            }
        }
    }
}
