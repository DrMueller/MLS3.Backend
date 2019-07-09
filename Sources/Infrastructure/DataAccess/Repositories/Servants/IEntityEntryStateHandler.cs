using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Mmu.Mls3.WebApi.Infrastructure.DataAccess.Repositories.Servants
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