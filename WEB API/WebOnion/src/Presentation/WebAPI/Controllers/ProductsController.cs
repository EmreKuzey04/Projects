using Application.Features.Products.Commands.CreateProduct;
using Application.Features.Products.Queries.GetAllActiveProducts;
using Application.Features.Products.Queries.GetProductsByCategories;
using Application.Features.Products.Queries.GetProductsByPrice;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Roles.Admin)]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("getbycategory")]

        public async Task<IActionResult> GetByCategory([FromQuery]GetProductsByCategoryQuery query)
        {
               return Ok(await _mediator.Send(query));
        }

        [HttpGet("getbyprice")]
        public async Task<IActionResult> GetByPrice([FromQuery] GetProductsByPriceQuery query)
        {
            return Ok(await _mediator.Send(query));
        }


        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetAllProductsQuery query)
        {
            return Ok(await _mediator.Send(query));
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] CreateProductCommand command)
        {
            var response = await _mediator.Send(command);
            var addedProduct = response.Data;

            return Created($"api/products/getById/{addedProduct.ProductID}", addedProduct);
        }
    }
}
