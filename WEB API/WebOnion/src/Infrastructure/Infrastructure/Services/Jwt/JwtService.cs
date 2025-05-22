using Application.Abstractions.Services.Jwt;
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.Jwt
{
    public class JwtService : IJwtService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly TokenOptions _tokenOptions;
        public JwtService(IOptions<TokenOptions> tokenOptions, UserManager<AppUser> userManager)
        {
            _tokenOptions = tokenOptions.Value;
            _userManager = userManager;
        }

        public async Task<JwtSecurityToken> CreateAccessToken(AppUser user)
        {
            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email,user.Email),
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Name,user.UserName)
            };

            var roles = await _userManager.GetRolesAsync(user);
            foreach (var item in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, item));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenOptions.SecurityKey));
            var token = new JwtSecurityToken(

                issuer: _tokenOptions.Issuer,
                audience: _tokenOptions.Audience,
                expires: DateTime.Now.AddMinutes(_tokenOptions.TokenExpiration),
                notBefore:DateTime.Now,
                claims:claims,
                signingCredentials: new SigningCredentials(key,SecurityAlgorithms.HmacSha256)

            );

            return token;
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);

            return Convert.ToBase64String(randomNumber);
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token) //süresi dolmuş token kontrolü 
        {
            var tokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenOptions.SecurityKey)),
                ValidateLifetime = false,
                ValidIssuer = _tokenOptions.Issuer,
                ValidAudience = _tokenOptions.Audience,
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);

            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256))
            {
                throw new SecurityTokenException("Access Token Geçerli Değil");
            }

            return principal;
        }
    }
}
