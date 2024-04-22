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
            using (var context = new RB_Ärendesystem.Datalayer.RB_context())
            {
                context.kunder.Add(NyKund);
                context.SaveChanges();
            }
        }
    }
}
