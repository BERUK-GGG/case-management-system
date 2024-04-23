using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RB_Ärendesystem.Datalayer.Repositories.Base
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected DbContext Context { get;  }

        protected DbSet<TEntity> Table { get; }

        protected Repository(DbContext context)
        {
            Context = context;
            Table = context.Set<TEntity>();

        }
        // create

        public virtual TEntity Add(TEntity entity) { Table.Add(entity); return entity; }

        public virtual IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities) { Table.AddRange(entities); return entities; }

        //read
        public virtual TEntity Find(int id) => Table.Find(id);
        public virtual IEnumerable<TEntity> GetAll() => Table;

        //Update
        public virtual void Update(TEntity entity) => Table.Update(entity);
        public virtual TEntity Update(TEntity oldEntity, TEntity newEntity)
        {
            Context.Entry(oldEntity).CurrentValues.SetValues(newEntity);
            Table.Update(oldEntity);
            return oldEntity;
        }
        public virtual void UpdateRange(IEnumerable<TEntity> entities) => Table.UpdateRange(entities);


        //Delete
        public virtual void Delete(int id)
        {
            var entity = Table.Find(id);
            if (entity != null)
                Context.Entry(entity).State = EntityState.Deleted;
        }
        public virtual TEntity Delete(TEntity entity) { Table.Remove(entity); return entity; }
        public virtual void DeleteRange(IEnumerable<TEntity> entities) => Table.RemoveRange(entities);

    }
}
