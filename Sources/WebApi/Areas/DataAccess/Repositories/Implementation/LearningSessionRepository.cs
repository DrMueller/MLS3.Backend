using System.Linq;
using System.Threading.Tasks;
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

        public async Task<long> LoadNextIdAsync(long currentId)
        {
            var largerIds = await Query()
                .Where(f => f.Id > currentId)
                .OrderBy(f => f.Id)
                .ToListAsync();

            if (largerIds.Any())
            {
                return largerIds.First().Id.Value;
            }

            return Query().OrderBy(f => f.Id).First().Id.Value;
        }

        protected override IQueryable<LearningSession> AppendIncludes(IQueryable<LearningSession> query)
        {
            return query.Include(f => f.LearningSessionFacts);
        }
    }
}