using Microsoft.EntityFrameworkCore;

namespace Mmu.Mls3.WebApi.Areas.DataAccess.Repositories.Servants
{
    public interface IDbContextFactory
    {
        DbContext Create();
    }
}