using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ES.InventoryRegister.Entities;

namespace ES.InventoryRegister.Data.Repositories
{
    public interface IRepository<TEntity> where TEntity : class, IEntity
    {
        IQueryable<TEntity> All();

        TEntity Get(int id);

        TEntity New();

        void Add(TEntity entity);

        void Add(IEnumerable<TEntity> entities);

        void Delete(int id);

        void Update(TEntity entity);
    }
}
