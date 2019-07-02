using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Mmu.Mls3.WebApi.Areas.DataAccess.Repositories.Servants.Implementation
{
    public interface IEntityEntryStateHandler
    {
        public void AlignEntityEntryStatesRecursively(
            EntityEntry entryBeforeUpdate,
            EntityEntry entryToUpdate,
            DbContext dbContext);

        public void MarkAsDetachedRecursively(EntityEntry entityEntry, DbContext dbContext);
    }
}