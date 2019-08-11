using System.Threading.Tasks;
using Mmu.Mls3.WebApi.Infrastructure.DataAccess.Repositories.Base;
using Mmu.Mls3.WebApi.Infrastructure.DataAccess.Repositories.Servants;
using Mmu.Mls3.WebApi.Infrastructure.Security.DataAccess.Entities;

namespace Mmu.Mls3.WebApi.Infrastructure.Security.DataAccess.Repositories.Implementation
{
    public class AppUserRepository : RepositoryBase<AppUser>, IAppUserRepository
    {
        public AppUserRepository(IDbContextFactory dbContextFactory, IEntityEntryStateHandler entityEntryStateHandler)
            : base(dbContextFactory, entityEntryStateHandler)
        {
        }

        public async Task<AppUser> LoadByNormalizedUserNameAsync(string normalizedName)
        {
            return await LoadSingleAsync(f => f.NormalizedUserName == normalizedName);
        }
    }
}