using Application.Abstractions.Services.Jwt;
using Application.Features.Auth.Exceptions;
using Application.Models.Dtos.Auth;
using Application.Models.ResponseWrappers;
using Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;

namespace Application.Features.Auth.Commands.GetAccessToken
{
    public class GetAccessTokenCommand:IRequest<Response<GetAccessTokenDto>>
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public class GetAccessTokenCommandHandler : IRequestHandler<GetAccessTokenCommand, Response<GetAccessTokenDto>>
        {
            private readonly UserManager<AppUser> _userManager;
            private readonly IJwtService _jwtService;
            private readonly IConfiguration _configuration;
            public GetAccessTokenCommandHandler(UserManager<AppUser> userManager, IJwtService jwtService, IConfiguration configuration)
            {
                _userManager = userManager;
                _jwtService = jwtService;
                _configuration = configuration;
            }

            public async Task<Response<GetAccessTokenDto>> Handle(GetAccessTokenCommand request, CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByEmailAsync(request.Email);
                if (user is null)
                    throw new UserNotFoundException();

              var checkpassword = await _userManager.CheckPasswordAsync(user,request.Password);

                if (!checkpassword)
                    throw new WrongPasswordException();

                JwtSecurityToken jwtSecuritytoken = await _jwtService.CreateAccessToken(user);
                
                string token = new JwtSecurityTokenHandler().WriteToken(jwtSecuritytoken);

                int refreshTokenExpirationInDays = Convert.ToInt32(_configuration["TokenOptions:RefreshTokenExpirationInDays"]);

                string refreshToken = _jwtService.GenerateRefreshToken();

                user.RefreshToken = refreshToken;
                user.RefreshTokenEndDate = DateTime.Now.AddDays(refreshTokenExpirationInDays);

                await _userManager.UpdateAsync(user);

                return new Response<GetAccessTokenDto>(new GetAccessTokenDto()

                {
                    AccessToken = token,
                    RefreshToken = refreshToken,
                    Expiration = jwtSecuritytoken.ValidTo

                });
                    
                    

            }
        }
    }
}
