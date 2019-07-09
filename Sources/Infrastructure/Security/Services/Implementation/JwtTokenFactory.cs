using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Mmu.Mls3.WebApi.Infrastructure.Settings.Models;

namespace Mmu.Mls3.WebApi.Infrastructure.Security.Services.Implementation
{
    public class JwtTokenFactory : IJwtTokenFactory
    {
        private readonly IOptions<AppSettings> _appSettings;

        public JwtTokenFactory(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings;
        }

        public string CreateToken(IReadOnlyCollection<Claim> claims)
        {
            var signingKey = Encoding.ASCII.GetBytes(_appSettings.Value.SecretKey);
            var credentials = new SigningCredentials(new SymmetricSecurityKey(signingKey), SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = null,
                Audience = null,
                IssuedAt = DateTime.UtcNow,
                NotBefore = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddMinutes(15),
                Subject = new ClaimsIdentity(claims),
                SigningCredentials = credentials,
            };

            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = jwtTokenHandler.CreateJwtSecurityToken(tokenDescriptor);
            var token = jwtTokenHandler.WriteToken(jwtToken);

            return token;
        }
    }
}