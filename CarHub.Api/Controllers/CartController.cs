using Application.CQRS.Cart.Handlers;
using Application.CQRS.Carts.Handlers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarHub.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController(ISender sender) : ControllerBase
    {
        private readonly ISender _sender = sender;

        [HttpPost]
        public async Task<IActionResult> AddCart([FromBody] Application.CQRS.Cart.Handlers.AddCart.AddCartCommand request)
        {
            var result = await _sender.Send(request);
            return Ok(result);
        }
        [HttpPost("add-product")]
        public async Task<IActionResult> AddProductToCart([FromBody] AddProductToCart.AddProductToCartCommand request)
        {
            var result = await _sender.Send(request);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpDelete("ClearCartLines")]
        public async Task<IActionResult> ClearCartLines([FromBody] ClearCartLineCommand request)
        {
            var result = await _sender.Send(request);

            if (result.IsSuccess)
            {
                return Ok(new { message = "Cart lines cleared successfully" });
            }

            return BadRequest(new { errors = result.Errors });
        }

        [HttpGet("GetCartWithLinesByUserId")]
        public async Task<IActionResult> GetCartWithLinesByUserId([FromQuery] int userId)
        {
            var request = new GetCartWithLinesByUserId.GetCartWithLinesByUserIdCommand { UserId = userId };
            var result = await _sender.Send(request);
            if (!result.IsSuccess)
            {
                return NotFound(result.Errors);
            }
            return Ok(result.Data);
        }

    }
}
