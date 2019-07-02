﻿using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Mmu.Mls3.WebApi.Areas.DataAccess.Repositories.Servants.Implementation
{
    public class EntityEntryStateHandler : IEntityEntryStateHandler
    {
        public void AlignEntityEntryStatesRecursively(
           EntityEntry entryBeforeUpdate,
           EntityEntry entryToUpdate,
           DbContext dbContext)
        {
            MarkDeletedItemsRecursively(entryBeforeUpdate, entryToUpdate, dbContext);
            MarkAddedItemsRecursively(entryBeforeUpdate, entryToUpdate, dbContext);
        }

        public void MarkAsDetachedRecursively(EntityEntry entityEntry, DbContext dbContext)
        {
            entityEntry.State = EntityState.Detached;

            foreach (var col in entityEntry.Collections)
            {
                foreach (var entity in col?.CurrentValue ?? Enumerable.Empty<object>())
                {
                    var currentEntityEntry = dbContext.Entry(entity);
                    currentEntityEntry.State = EntityState.Detached;
                    MarkAsDetachedRecursively(currentEntityEntry, dbContext);
                }
            }
        }

        private static void MarkAddedItemsRecursively(
            EntityEntry entryBeforeUpdate,
            EntityEntry entryToUpdate,
            DbContext dbContext)
        {
            foreach (var col in entryToUpdate.Collections)
            {
                var collectionBeforeUpdate = entryBeforeUpdate.Collection(col.Metadata.Name).CurrentValue.Cast<object>().ToList();
                var collectionToUpdate = col.CurrentValue.Cast<object>().ToList();

                foreach (var collectionEntityToUpdate in collectionToUpdate)
                {
                    var collectionEntityBeforeUpdate = collectionBeforeUpdate.FirstOrDefault(f => f.Equals(collectionEntityToUpdate));
                    var collectionEntryToUpdate = dbContext.Entry(collectionEntityToUpdate);

                    if (collectionEntityBeforeUpdate == null)
                    {
                        collectionEntryToUpdate.State = EntityState.Added;
                    }
                    else
                    {
                        var collectionEntryBeforeUpdate = dbContext.Entry(collectionEntityBeforeUpdate);
                        MarkAddedItemsRecursively(collectionEntryBeforeUpdate, collectionEntryToUpdate, dbContext);
                    }
                }
            }
        }

        private static void MarkDeletedItemsRecursively(
            EntityEntry entryBeforeUpdate,
            EntityEntry entryToUpdate,
            DbContext dbContext)
        {
            foreach (var collectionsForEntryToUpdate in entryToUpdate.Collections)
            {
                var collectionBeforeUpdate = entryBeforeUpdate.Collection(collectionsForEntryToUpdate.Metadata.Name).CurrentValue.Cast<object>().ToList();
                var collectionToUpdate = collectionsForEntryToUpdate.CurrentValue.Cast<object>().ToList();

                foreach (var collectionEntityBeforeUpdate in collectionBeforeUpdate)
                {
                    var collectionEntryBeforeUpdate = dbContext.Entry(collectionEntityBeforeUpdate);
                    var currentEntityToUpdate = collectionToUpdate.FirstOrDefault(f => f.Equals(collectionEntityBeforeUpdate));

                    if (currentEntityToUpdate == null)
                    {
                        collectionEntryBeforeUpdate.State = EntityState.Deleted;
                    }
                    else
                    {
                        var currentEntityEntryAfterUpdate = dbContext.Entry(currentEntityToUpdate);
                        MarkDeletedItemsRecursively(collectionEntryBeforeUpdate, currentEntityEntryAfterUpdate, dbContext);
                    }
                }
            }
        }
    }
}