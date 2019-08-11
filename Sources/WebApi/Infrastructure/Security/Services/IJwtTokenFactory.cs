using System.Collections.Generic;
using System.Security.Claims;

namespace Mmu.Mls3.WebApi.Infrastructure.Security.Services
{
    public interface IJwtTokenFactory
    {
        string CreateToken(IReadOnlyCollection<Claim> claims);
    }
}