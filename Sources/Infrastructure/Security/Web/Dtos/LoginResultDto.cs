using System.Collections.Generic;
using System.Security.Claims;

namespace Mmu.Mls3.WebApi.Infrastructure.Security.Web.Dtos
{
    public class LoginResultDto
    {
        public List<Claim> Claims { get; set; }
        public bool LoginSuccess { get; set; }
        public string Token { get; set; }
    }
}