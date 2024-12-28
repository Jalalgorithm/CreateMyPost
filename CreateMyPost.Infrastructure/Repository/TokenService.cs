using CreateMyPost.Domain.Entities;
using CreateMyPost.Domain.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CreateMyPost.Infrastructure.Repository
{
    public class TokenService : ITokenService
    {
        private readonly SymmetricSecurityKey _secretKey;
        private readonly string? _validIssuer;
        private readonly string? _validAudience;
        private readonly double _expires;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<TokenService> _logger;

        public TokenService(IConfiguration configuration, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            var jwtSettings = configuration.GetSection("JwtSettings").Get<JwtSettings>();
            if (jwtSettings is null ||string.IsNullOrEmpty(jwtSettings.Key))
            {
                throw new InvalidOperationException("Jwt Secret key is not configured");

            }

            _secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key));
            _validIssuer = jwtSettings.Issuer;
            _validAudience = jwtSettings.Audience;
            _expires = jwtSettings.Expires;
        }


        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var range = RandomNumberGenerator.Create();
            range.GetBytes(randomNumber);

            var refreshToken = Convert.ToBase64String(randomNumber);

            return refreshToken;
        }

        private async Task<List<Claim>> GetClaimsAsync(ApplicationUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name , user.FirstName ?? string.Empty),
                new Claim(ClaimTypes.NameIdentifier , user.Id),
                new Claim(ClaimTypes.Email , user.Email),
                new Claim("LastName" , user.LastName),
                new Claim("PhoneNumber" , user.PhoneNumber?? string.Empty)
            };

            var roles = await _userManager.GetRolesAsync(user);
            claims.AddRange(roles.Select(role=> new Claim(ClaimTypes.Role, role))); 

            return claims;
        }

        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials , List<Claim> claims)
        {
            return new JwtSecurityToken(
                issuer: _validIssuer,
                audience: _validAudience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_expires),
                signingCredentials: signingCredentials
                );
        }

        public async Task<string> GenerateToken(ApplicationUser user)
        {
            var signingCredentials =  new SigningCredentials(_secretKey , SecurityAlgorithms.HmacSha256);
            var claims = await GetClaimsAsync(user);
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);

            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }
    }
}
