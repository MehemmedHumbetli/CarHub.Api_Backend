using Application.CQRS.Cars.Handlers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarHub.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CarController(ISender sender) : Controller
{
    private readonly ISender _sender = sender;


    [HttpPost]
    public async Task<IActionResult> AddAsync([FromBody] Application.CQRS.Cars.Handlers.Add.AddCommand request)
    {
        return Ok(await _sender.Send(request));
    }

    [HttpPut("CarUpdate")]
    public async Task<IActionResult> CarUpdate([FromBody] Application.CQRS.Cars.Handlers.CarUpdate.UpdateCarCommand request)
    {
        return Ok(await _sender.Send(request));
    }

    [HttpDelete("Remove")]
    public async Task<IActionResult> Remove([FromQuery] int id)
    {
        var request = new Application.CQRS.Cars.Handlers.CarRemove.CarDeleteCommand { Id = id };
        return Ok(await _sender.Send(request));
    }

    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAllCars()
    {
        var result = await _sender.Send(new CarGetAll.GetAllCarsQuery());

        if (!result.IsSuccess)
        {
            return NotFound(result.Errors);
        }

        return Ok(result.Data);
    }
}
