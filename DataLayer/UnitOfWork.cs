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
            Besöks = new BesökRepository(Context);
            Journals = new JournalRepository(Context);
            Kunds = new KundRepository(Context);
            Mekanikers = new MekanikerRepository(Context);
            Reservdels = new ReservdelRepository(Context);
        }


        public IBesökRepository Besöks { get; }
        public IJournalRepository Journals { get; }
        public IKundRepository Kunds { get; }
        public IMekanikerRepository Mekanikers { get; }
        public IReservdelRepository Reservdels { get; }

        //public IBesökRepository Besöks => throw new NotImplementedException();

        //public IJournalRepository Journals => throw new NotImplementedException();

        //public IKundRepository Kunds => throw new NotImplementedException();

        //public IMekanikerRepository Mekanikers => throw new NotImplementedException();

        //public IReservdelRepository Reservdels => throw new NotImplementedException();

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

        public UnitOfWork(): this (new RB_context())
        {
            disposeContext = true;

        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (isDisposed)
            {
                return;
            }
            if (disposing)
            {
                if (disposeContext)
                {
                    Context.Dispose();
                }
            }
            isDisposed = true;
        }
    }

}
