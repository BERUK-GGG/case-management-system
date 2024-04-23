using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RB_Ärendesystem.Datalayer.Repositories.interfaces;

namespace RB_Ärendesystem.Datalayer
{
    public interface IUnitofWork : IDisposable
    {
        IBesökRepository Besöks {  get; }
        IJournalRepository Journals { get; }    
        IKundRepository Kunds { get; }
        IMekanikerRepository Mekanikers { get; }
        IReservdelRepository Reservdels { get; }

        int SaveChanges();
    }
}
