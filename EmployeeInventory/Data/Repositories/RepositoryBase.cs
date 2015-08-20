using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ES.InventoryRegister.Entities;
using ES.InventoryRegister.Data.Infrastructure;

namespace ES.InventoryRegister.Data.Repositories
{
    public class RepositoryBase<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {
        private InventoryDbContext _context;

        public RepositoryBase(InventoryDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets all entities of a specified type from the database
        /// </summary>
        /// <returns>Entities</returns>
        public IQueryable<TEntity> All()
        {
            return _context.Set<TEntity>();
        }

        /// <summary>
        /// Gets an entity with a specific ID
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>Entity</returns>
        public TEntity Get(int id)
        {
            return _context.Set<TEntity>().FirstOrDefault(x => x.Id == id);
        }

        /// <summary>
        /// Creates a new entity with a specified type and adds it
        /// to the context
        /// </summary>
        /// <returns>Entity</returns>
        public TEntity New()
        {
            // Create a new instance of the entity
            TEntity entity = _context.Set<TEntity>().Create();

            // Give it a creation date
            entity.CreationDate = DateTime.Now;

            // Add it to the context
            _context.Set<TEntity>().Add(entity);

            return entity;
        }

        /// <summary>
        /// Add a single entity instance to the context
        /// </summary>
        /// <param name="entity">Entity</param>
        public void Add(TEntity entity)
        {
            // Give the entity a creation date
            entity.CreationDate = DateTime.Now;

            // Add it to the context
            _context.Set<TEntity>().Add(entity);
        }

        /// <summary>
        /// Adds multiple entitiy instances to the context
        /// </summary>
        /// <param name="entities">Entity group</param>
        public void Add(IEnumerable<TEntity> entities)
        {
            // Loop through given entities
            foreach (TEntity entity in entities)
            {
                // Give each entity a creation date
                entity.CreationDate = DateTime.Now;
            }

            // Add the entities to the context
            _context.Set<TEntity>().AddRange(entities);
        }

        /// <summary>
        /// Deletes an entity from the context with a given ID
        /// </summary>
        /// <param name="id">Entity ID</param>
        public void Delete(int id)
        {
            // Finds the entity to delete
            TEntity toDelete = _context.Set<TEntity>().FirstOrDefault(p => p.Id == id);

            // Check if the entity has been successfully found
            if (toDelete != null)
            {
                // Remove it from the context
                _context.Set<TEntity>().Remove(toDelete);
            }
        }

        /// <summary>
        /// Updates the UpdateDate property for an entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public void Update(TEntity entity)
        {
            entity.UpdateDate = DateTime.Now;
        }
    }
}
