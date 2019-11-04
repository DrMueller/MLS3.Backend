using Mmu.Mls3.WebApi.Infrastructure.DataAccess.Entities.Base;

namespace Mmu.Mls3.WebApi.Infrastructure.Security.DataAccess.Entities
{
    public class AppUser : EntityBase
    {
        public string HashedPassword { get; set; }
        public string NormalizedUserName { get; set; }
        public string UserName { get; set; }
    }
}