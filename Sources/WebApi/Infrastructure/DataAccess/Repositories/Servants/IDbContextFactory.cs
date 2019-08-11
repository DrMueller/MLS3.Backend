using Microsoft.EntityFrameworkCore;

namespace Mmu.Mls3.WebApi.Infrastructure.DataAccess.Repositories.Servants
{
    public interface IDbContextFactory
    {
        DbContext Create();
    }
}