using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<IReadOnlyCollection<Fact>> LoadByLearningSessionId(long learningSessionId)
        {
            return await Query()
                .Where(f => f.LearningSessionFacts.Select(fc => fc.LearningSessionId).Contains(learningSessionId))
                .ToListAsync();
        }

        protected override IQueryable<Fact> AppendIncludes(IQueryable<Fact> query)
        {
            return query.Include(f => f.LearningSessionFacts);
        }
    }
}