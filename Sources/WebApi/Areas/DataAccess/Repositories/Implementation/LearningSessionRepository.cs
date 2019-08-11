using System.Linq;
using Microsoft.EntityFrameworkCore;
using Mmu.Mls3.WebApi.Areas.DataAccess.Entities;
using Mmu.Mls3.WebApi.Infrastructure.DataAccess.Repositories.Base;
using Mmu.Mls3.WebApi.Infrastructure.DataAccess.Repositories.Servants;

namespace Mmu.Mls3.WebApi.Areas.DataAccess.Repositories.Implementation
{
    public class LearningSessionRepository : RepositoryBase<LearningSession>, ILearningSessionRepository
    {
        public LearningSessionRepository(IDbContextFactory dbContextFactory, IEntityEntryStateHandler entityEntryStateHandler)
            : base(dbContextFactory, entityEntryStateHandler)
        {
        }

        protected override IQueryable<LearningSession> AppendIncludes(IQueryable<LearningSession> query)
        {
            return query.Include(f => f.LearningSessionFacts);
        }
    }
}