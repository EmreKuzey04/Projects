using Application.Features.Shippers.Queries.GetAllShippers;
using Application.Features.Shippers.Queries.GetShippersByDeliveryTime;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Roles.Admin)]
    public class ShippersController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ShippersController(IMediator mediator)
        {
            _mediator=mediator;
        }

        [HttpGet("getbydeliverytime")]
        public async Task<IActionResult> GetAll([FromQuery] GetShippersByDeliveryTimeQuery query)
        {
            return Ok(await _mediator.Send(query));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetAllShippersQuery query)
        {
            return Ok(await _mediator.Send(query));
        }
    }
}
