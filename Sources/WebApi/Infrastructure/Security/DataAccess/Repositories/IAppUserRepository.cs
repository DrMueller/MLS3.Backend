using System.Threading.Tasks;
using Mmu.Mls3.WebApi.Infrastructure.DataAccess.Repositories;
using Mmu.Mls3.WebApi.Infrastructure.Security.DataAccess.Entities;

namespace Mmu.Mls3.WebApi.Infrastructure.Security.DataAccess.Repositories
{
    public interface IAppUserRepository : IRepository<AppUser>
    {
        Task<AppUser> LoadByNormalizedUserNameAsync(string normalizedName);
    }
}