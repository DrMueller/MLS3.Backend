using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Mmu.Mls3.WebApi.Infrastructure.Security.DataAccess.Entities;
using Mmu.Mls3.WebApi.Infrastructure.Security.Services;
using Mmu.Mls3.WebApi.Infrastructure.Security.Web.Dtos;

namespace Mmu.Mls3.WebApi.Infrastructure.Security.Controllers
{
    ////public class Tra
    ////{
    ////    public string Hello { get; set; }
    ////}

    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IJwtTokenFactory _jwtTokenFactory;
        private readonly UserManager<AppUser> _userManager;

        public UsersController(
            UserManager<AppUser> userManager,
            IJwtTokenFactory jwtTokenFactory)
        {
            _userManager = userManager;
            _jwtTokenFactory = jwtTokenFactory;
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<ActionResult<LoginResultDto>> LoginAsync([FromBody] LoginRequestDto requestDto)
        {
            var user = await _userManager.FindByNameAsync(requestDto.UserName);
            LoginResultDto result;

            if (user != null && await _userManager.CheckPasswordAsync(user, requestDto.Password))
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName)
                };

                var token = _jwtTokenFactory.CreateToken(claims);
                result = new LoginResultDto
                {
                    Claims = claims,
                    LoginSuccess = true,
                    Token = token,
                };
            }
            else
            {
                result = new LoginResultDto
                {
                    LoginSuccess = false
                };
            }

            return Ok(result);
        }

        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<ActionResult> RegisterAsync([FromBody] RegisterDto dto)
        {
            var user = new AppUser
            {
                UserName = dto.UserName
            };

            var result = await _userManager.CreateAsync(user, dto.Password);
            return Ok();
        }
    }
}