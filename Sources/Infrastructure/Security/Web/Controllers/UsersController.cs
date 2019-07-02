using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mmu.Mls3.WebApi.Infrastructure.Security.Services;
using Mmu.Mls3.WebApi.Infrastructure.Security.Web.Dtos;

namespace Mmu.Mls3.WebApi.Infrastructure.Security.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class UsersController
    {
        private readonly IJwtTokenFactory _jwtTokenFactory;

        public UsersController(IJwtTokenFactory jwtTokenFactory)
        {
            _jwtTokenFactory = jwtTokenFactory;
        }

        [HttpPost("Login")]
        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Controllers can't use static methods")]
        public ActionResult<LoginResultDto> Login([FromBody] LoginRequestDto requestDto)
        {
            // TODO: Outsource username and password
            if (requestDto.UserName != "Matthias" || requestDto.Password != "test")
            {
                return new LoginResultDto
                {
                    LoginSuccess = false,
                };
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, "mlm"),
                new Claim(ClaimTypes.Name, "Matthias Müller"),
            };

            var token = _jwtTokenFactory.CreateToken(claims);

            return new LoginResultDto
            {
                Claims = claims,
                LoginSuccess = true,
                Token = token,
            };
        }
    }
}