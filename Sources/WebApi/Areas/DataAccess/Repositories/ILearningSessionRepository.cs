using System.Threading.Tasks;
using Mmu.Mls3.WebApi.Areas.DataAccess.Entities;
using Mmu.Mls3.WebApi.Infrastructure.DataAccess.Repositories;

namespace Mmu.Mls3.WebApi.Areas.DataAccess.Repositories
{
    public interface ILearningSessionRepository : IRepository<LearningSession>
    {
        Task<long> LoadNextIdAsync(long currentId);
    }
}