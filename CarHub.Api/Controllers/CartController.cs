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
    }
}
