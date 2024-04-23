using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RB_Ärendesystem.Datalayer.Repositories.Base
{
    public interface IRepository<TEntity> where TEntity : class
    {
        //create

        TEntity Add(TEntity entity);
        IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities);

        //Read

        TEntity Find(int id);

        IEnumerable< TEntity> GetAll();

        //update

        void Update(TEntity entity);

        TEntity Update (TEntity oldEntity, TEntity newEntity);

        void UpdateRange (IEnumerable<TEntity> entities);

        //Delete 

        void Delete(int id);

        TEntity Delete(TEntity entity);

        void DeleteRange(IEnumerable<TEntity> entities);

    }
}
