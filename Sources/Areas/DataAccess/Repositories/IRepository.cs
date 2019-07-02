using System.Collections.Generic;
using System.Threading.Tasks;
using Mmu.Mls3.WebApi.Areas.DataAccess.Entities.Base;

namespace Mmu.Mls3.WebApi.Areas.DataAccess.Repositories
{
    public interface IRepository<TEntity>
        where TEntity : EntityBase
    {
        Task DeleteAsync(long id);

        Task<IReadOnlyCollection<TEntity>> LoadAllAsync();

        Task<TEntity> LoadByIdAsync(long id);

        Task<TEntity> SaveAsync(TEntity entity);
    }
}