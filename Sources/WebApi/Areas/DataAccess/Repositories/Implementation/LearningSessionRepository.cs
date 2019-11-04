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
            var largerId = await Query()
                .Where(f => f.Id.HasValue && f.Id > currentId)
                .OrderBy(f => f.Id)
                .FirstOrDefaultAsync();

            if (largerId?.Id != null)
            {
                return largerId.Id.Value;
            }

            // If there is no larger id, we start with the smallest one
            var firstId = Query().Select(f => f.Id).OrderBy(id => id).FirstOrDefault();
            return firstId ?? 0;
        }

        protected override IQueryable<LearningSession> AppendIncludes(IQueryable<LearningSession> query)
        {
            return query.Include(f => f.LearningSessionFacts);
        }
    }
}