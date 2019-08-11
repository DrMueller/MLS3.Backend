using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Mmu.Mls3.WebApi.Infrastructure.DataAccess.Entities.Base;
using Mmu.Mls3.WebApi.Infrastructure.DataAccess.Repositories.Servants;

namespace Mmu.Mls3.WebApi.Infrastructure.DataAccess.Repositories.Base
{
    public abstract class RepositoryBase<TEntity> : IRepository<TEntity>
           where TEntity : EntityBase
    {
        private readonly DbContext _dbContext;
        private readonly IEntityEntryStateHandler _entityEntryStateHandler;

        private DbSet<TEntity> DbSet => _dbContext.Set<TEntity>();

        public RepositoryBase(IDbContextFactory dbContextFactory, IEntityEntryStateHandler entityEntryStateHandler)
        {
            _dbContext = dbContextFactory.Create();
            _entityEntryStateHandler = entityEntryStateHandler;
        }

        public async Task DeleteAllAsync()
        {
            var allEntries = await LoadAllAsync();
            DbSet.RemoveRange(allEntries);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(long id)
        {
            var dataModel = await DbSet.SingleOrDefaultAsync(f => f.Id.Equals(id));
            if (dataModel == null)
            {
                return;
            }

            _dbContext.Remove(dataModel);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IReadOnlyCollection<TEntity>> LoadAllAsync()
        {
            return await LoadAsync(f => true);
        }

        public async Task<TEntity> LoadByIdAsync(long id)
        {
            return await LoadSingleAsync(f => f.Id == id);
        }

        public virtual async Task<TEntity> SaveAsync(TEntity entity)
        {
            EntityEntry<TEntity> entityEntry;

            if (!entity.Id.HasValue)
            {
                entityEntry = await DbSet.AddAsync(entity);
            }
            else
            {
                entityEntry = DbSet.Update(entity);
                var entryBeforeUpdate = await LoadSingleAsync(f => f.Id == entity.Id);
                var entityEntryBeforeUpdate = _dbContext.Entry(entryBeforeUpdate);
                _entityEntryStateHandler.AlignEntityEntryStatesRecursively(entityEntryBeforeUpdate, entityEntry, _dbContext);
            }

            await _dbContext.SaveChangesAsync();
            _entityEntryStateHandler.MarkAsDetachedRecursively(entityEntry, _dbContext);

            var newEntity = entityEntry.Entity;
            return newEntity;
        }

        protected virtual IQueryable<TEntity> AppendIncludes(IQueryable<TEntity> query)
        {
            return query;
        }

        protected async Task<IReadOnlyCollection<TEntity>> LoadAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var query = _dbContext.Set<TEntity>().Where(predicate);
            query = AppendIncludes(query);
            var result = await query.ToListAsync();
            return result;
        }

        protected async Task<TEntity> LoadSingleAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var query = _dbContext.Set<TEntity>().Where(predicate);
            query = AppendIncludes(query);
            var result = await query.SingleOrDefaultAsync();
            return result;
        }
    }
}