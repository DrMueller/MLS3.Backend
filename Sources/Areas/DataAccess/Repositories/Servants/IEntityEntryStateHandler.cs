using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Mmu.Mls3.WebApi.Areas.DataAccess.Repositories.Servants.Implementation
{
    public interface IEntityEntryStateHandler
    {
        void AlignEntityEntryStatesRecursively(
            EntityEntry entryBeforeUpdate,
            EntityEntry entryToUpdate,
            DbContext dbContext);

        void MarkAsDetachedRecursively(EntityEntry entityEntry, DbContext dbContext);
    }
}