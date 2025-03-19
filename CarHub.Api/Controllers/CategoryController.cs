using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application.CQRS.Handlers;

namespace CarHub.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController(ISender sender) : ControllerBase
    {

        private readonly ISender _sender = sender;

        [HttpPost("Add")]
        public async Task<IActionResult> AddCategory(Application.CQRS.Handlers.Add.AddCommand request)
        {
            var result = await _sender.Send(request);
            return Ok(result);
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllCars()
        {
            var result = await _sender.Send(new GetAll.GetAllCarsQuery());

            if (!result.IsSuccess)
            {
                return NotFound(result.Errors);
            }

            return Ok(result.Data);
        }

        [HttpDelete("Remove")]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            var request = new Application.CQRS.Handlers.Remove.DeleteCommand { Id = id };
            return Ok(await _sender.Send(request));
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById([FromQuery] int id)
        {
            var request = new Application.CQRS.Handlers.GetById.Query { Id = id };
            var result = await _sender.Send(request);
            if (!result.IsSuccess)
            {
                return NotFound(result.Errors);
            }
            return Ok(result.Data);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] Application.CQRS.Handlers.Update.Command request)
        {
            return Ok(await _sender.Send(request));
        }

    }
}
