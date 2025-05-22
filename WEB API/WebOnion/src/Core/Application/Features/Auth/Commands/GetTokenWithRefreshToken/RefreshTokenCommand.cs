using Application.Abstractions.Services.Jwt;
using Application.Exceptions;
using Application.Models.Dtos.Auth;
using Application.Models.ResponseWrappers;
using Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Commands.GetTokenWithRefreshToken
{
    public class RefreshTokenCommand:IRequest<Response<GetAccessTokenDto>>
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }


        public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, Response<GetAccessTokenDto>>
        {
            private readonly IJwtService _jwtService;
            private readonly UserManager<AppUser> _userManager;
            private readonly IConfiguration _configuration;

            public RefreshTokenCommandHandler(IJwtService jwtService, UserManager<AppUser> userManager, IConfiguration configuration)
            {
                _jwtService = jwtService;
                _userManager = userManager;
                _configuration = configuration;
            }
            public async Task<Response<GetAccessTokenDto>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
            {
                var principal = _jwtService.GetPrincipalFromExpiredToken(request.AccessToken);

                if (principal == null)
                {
                    throw new BadRequestException("Geçersiz Access Token");
                }
                
                var claimsIdentity = (ClaimsIdentity)principal.Identity;
                var email = claimsIdentity.Claims.SingleOrDefault(x => x.Type.Contains("email")).Value;

               var user = await _userManager.FindByEmailAsync(email);

                var storedRefreshToken = user.RefreshToken;

                if (request.RefreshToken != storedRefreshToken)
                {
                    throw new BadRequestException("Refresh Token Geçersizdir");
                }
                var storedRefreshTokenEndDate = user.RefreshTokenEndDate;
                if (storedRefreshTokenEndDate < DateTime.Now)
                    throw new BadRequestException("Refresh Token Süresi Dolmuştur");

                
                int refreshTokenExpirationInDays = Convert.ToInt32(_configuration["TokenOptions:RefreshTokenExpirationInDays"]);

                JwtSecurityToken jwtSecuritytoken = await _jwtService.CreateAccessToken(user);

                string _token = new JwtSecurityTokenHandler().WriteToken(jwtSecuritytoken);

                string refreshToken = _jwtService.GenerateRefreshToken();

                user.RefreshToken = refreshToken;
                user.RefreshTokenEndDate = DateTime.Now.AddDays(refreshTokenExpirationInDays);

                await _userManager.UpdateAsync(user);

                return new Response<GetAccessTokenDto>(new GetAccessTokenDto()

                {
                    AccessToken = _token,
                    RefreshToken = refreshToken,
                    Expiration = jwtSecuritytoken.ValidTo

                });
               
            }
        }
    }
}
