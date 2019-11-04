using System.Linq;
using Microsoft.EntityFrameworkCore;
using Mmu.Mls3.WebApi.Areas.DataAccess.Entities;
using Mmu.Mls3.WebApi.Infrastructure.DataAccess.Repositories.Base;
using Mmu.Mls3.WebApi.Infrastructure.DataAccess.Repositories.Servants;

namespace Mmu.Mls3.WebApi.Areas.DataAccess.Repositories.Implementation
{
    public class FactRepository : RepositoryBase<Fact>, IFactRepository
    {
        public FactRepository(IDbContextFactory dbContextFactory, IEntityEntryStateHandler entityEntryStateHandler)
            : base(dbContextFactory, entityEntryStateHandler)
        {
        }

        protected override IQueryable<Fact> AppendIncludes(IQueryable<Fact> query)
        {
            return query.Include(f => f.LearningSessionFacts);
        }
    }
}