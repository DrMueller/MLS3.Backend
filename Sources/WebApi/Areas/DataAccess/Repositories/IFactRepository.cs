using System.Collections.Generic;
using System.Threading.Tasks;
using Mmu.Mls3.WebApi.Areas.DataAccess.Entities;
using Mmu.Mls3.WebApi.Infrastructure.DataAccess.Repositories;

namespace Mmu.Mls3.WebApi.Areas.DataAccess.Repositories.Implementation
{
    public interface IFactRepository : IRepository<Fact>
    {
        Task<IReadOnlyCollection<Fact>> LoadByIdsAsync(IReadOnlyCollection<long> factIds);
    }
}