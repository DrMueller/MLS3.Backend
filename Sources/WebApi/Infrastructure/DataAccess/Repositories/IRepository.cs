using System.Collections.Generic;
using System.Threading.Tasks;
using Mmu.Mls3.WebApi.Infrastructure.DataAccess.Entities.Base;

namespace Mmu.Mls3.WebApi.Infrastructure.DataAccess.Repositories
{
    public interface IRepository<TEntity>
        where TEntity : EntityBase
    {
        Task DeleteAllAsync();

        Task DeleteAsync(long id);

        Task<IReadOnlyCollection<TEntity>> LoadAllAsync();

        Task<TEntity> LoadByIdAsync(long id);

        Task<TEntity> SaveAsync(TEntity entity);
    }
}