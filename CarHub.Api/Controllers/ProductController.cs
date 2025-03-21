using Application.CRQS.Handlers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Application.CRQS.Handlers.AddProduct;

namespace CarHub.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(ISender sender) : ControllerBase
    {
        private readonly ISender _sender = sender;

        [HttpPost("AddProduct")]
        public async Task<IActionResult> AddProduct([FromBody] Application.CRQS.Handlers.AddProduct.AddProductCommand request)
        {
            var result = await _sender.Send(request);
            return Ok(result);
        }

        [HttpDelete("DeleteProduct")]
        public async Task<IActionResult> DeleteProduct([FromBody] Application.CRQS.Handlers.DeleteProduct.DeleteCommand request)
        {
            var result = await _sender.Send(request);
            return Ok(result);
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetByIdProduct([FromQuery] int id)
        {
            var request = new Application.CRQS.Handlers.GetByIdProduct.Query { Id = id };
            var result = await _sender.Send(request);
            if (!result.IsSuccess)
            {
                return NotFound(result.Errors);
            }
            return Ok(result.Data);
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllCategory()
        {
            var result = await _sender.Send(new GetAll.GetAllCategoryQuery());

            if (!result.IsSuccess)
            {
                return NotFound(result.Errors);
            }

            return Ok(result.Data);
        }
        [HttpPut("UpdateProduct")]
        public async Task<IActionResult> Update([FromBody] Application.CRQS.Handlers.UpdateProduct.Command request)
        {
            return Ok(await _sender.Send(request));
        }

        [HttpGet("GetProductsByCategoryId")]
        public async Task<ActionResult> GetProductsByCategoryId(int categoryId)
        { 
            var query = new ProductResponse.GetProductsByCategoryIdQuery(categoryId);
            var result = await _sender.Send(query);
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Errors);
        }

        [HttpGet("GetByNameProduct")]
        public async Task<IActionResult> GetByNameProduct([FromQuery] string name)
        {
            var request = new Application.CRQS.Handlers.GetByNameProduct.Query { Name = name };
            var result = await _sender.Send(request);
            if (!result.IsSuccess)
            {
                return NotFound(result.Errors);
            }
            return Ok(result.Data);
        }
    }
}


