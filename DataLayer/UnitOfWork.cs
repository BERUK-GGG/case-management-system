using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using RB_Ärendesystem.Datalayer.Repositories.interfaces;
using RB_Ärendesystem.Datalayer.Repositories;

namespace RB_Ärendesystem.Datalayer
{
    public class UnitOfWork : IUnitofWork
    {
        private bool isDisposed = false;
        private readonly bool disposeContext = false;
        protected RB_context Context {  get;}

        public UnitOfWork(RB_context context)
        {
            Context = context;
            besöks = new BesökRepository(Context);
            journals = new JournalRepository(Context);
            kunds = new KundRepository(Context);
            mekaniker = new MekanikerRepository(Context);
            reservdels = new ReservdelRepository(Context);
        }


        public IBesökRepository besöks { get; }
        public IJournalRepository journals { get; }
        public IKundRepository kunds { get; }
        public IMekanikerRepository mekaniker { get; }
        public IReservdelRepository reservdels { get; }

        public IBesökRepository Besöks => throw new NotImplementedException();

        public IJournalRepository Journals => throw new NotImplementedException();

        public IKundRepository Kunds => throw new NotImplementedException();

        public IMekanikerRepository Mekanikers => throw new NotImplementedException();

        public IReservdelRepository Reservdels => throw new NotImplementedException();

        public int SaveChanges()
        {
            try
            {
                return Context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                Debug.WriteLine(ex.Message);
                throw;
            }
            catch (RetryLimitExceededException ex)
            {
                Debug.WriteLine(ex.Message);
                throw;
            }
            catch (DbUpdateException ex)
            {
                Debug.WriteLine(ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw;
            }
        }
        public void Dispose()
        {
            Context.Dispose();
        }
    }

}
