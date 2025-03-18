using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarHub.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController(ISender sender) : ControllerBase
    {

        private readonly ISender _sender = sender;

        [HttpPost]
        public async Task<IActionResult> AddCategory(Application.CQRS.Handlers.Add.AddCommand request)
        {
            var result = await _sender.Send(request);
            return Ok(result);
        }
    }
}
