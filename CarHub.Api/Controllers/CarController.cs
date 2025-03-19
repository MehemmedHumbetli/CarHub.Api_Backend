using Application.CQRS.Users.Handlers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarHub.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CarController(ISender sender) : Controller
{
    private readonly ISender _sender = sender;


    [HttpPost]
    public async Task<IActionResult> AddAsync([FromBody] Application.CQRS.Users.Handlers.Add.AddCommand request)
    {
        return Ok(await _sender.Send(request));
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
}
