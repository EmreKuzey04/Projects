using Application.Features.Auth.Commands.GetAccessToken;
using Application.Features.Auth.Commands.GetTokenWithRefreshToken;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("getaccesstoken")]
        public async Task<IActionResult> GetAccessToken([FromBody] GetAccessTokenCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPost("getrefreshtoken")]
        public async Task<IActionResult> GetTokenWithRefreshToken([FromBody] RefreshTokenCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
    }
}
