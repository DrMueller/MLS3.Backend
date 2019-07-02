using Mmu.Mls3.WebApi.Areas.DataAccess.Entities;
using Mmu.Mls3.WebApi.Areas.DataAccess.Repositories.Base;
using Mmu.Mls3.WebApi.Areas.DataAccess.Repositories.Servants;
using Mmu.Mls3.WebApi.Areas.DataAccess.Repositories.Servants.Implementation;

namespace Mmu.Mls3.WebApi.Areas.DataAccess.Repositories.Implementation
{
    public class FactRepository : RepositoryBase<Fact>, IFactRepository
    {
        public FactRepository(IDbContextFactory dbContextFactory, IEntityEntryStateHandler entityEntryStateHandler)
            : base(dbContextFactory, entityEntryStateHandler)
        {
        }
    }
}