using Application.CQRS.Cars.Handlers;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Application.CQRS.Cars.Handlers.CarGetById;
using static Application.CQRS.Cars.Handlers.GetByBody;

namespace CarHub.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CarController(ISender sender) : Controller
{
    private readonly ISender _sender = sender;


    [HttpPost]
    public async Task<IActionResult> AddAsync([FromBody] Application.CQRS.Cars.Handlers.CarAdd.CarAddCommand request)
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

    [HttpGet("GetById")]
    public async Task<IActionResult> GetById([FromQuery] CarGetByIdCommand request)
    {
        return Ok(await _sender.Send(request));
    }

    [HttpGet("GetByBody")]
    public async Task<IActionResult> GetByBody([FromQuery] BodyTypes body)
    {
        var result = await _sender.Send(new GetByBodyQuery(body));

        if (!result.IsSuccess)
        {
            return NotFound(result.Errors);
        }

        return Ok(result.Data);
    }

    [HttpGet("GetByBrand")]
    public async Task<IActionResult> GetByBrand([FromQuery] string brand)
    {
        var result = await _sender.Send(new GetByBrand.GetByBrandQuery(brand));
        if (!result.IsSuccess)
        {
            return NotFound(result.Errors);
        }
        return Ok(result.Data);
    }
    [HttpGet("GetByFuel")]
    public async Task<IActionResult> GetByFuel([FromQuery] FuelTypes fuel)
    {
        var result = await _sender.Send(new GetByFuel.GetByFuelQuery(fuel));
        if (!result.IsSuccess)
        {
            return NotFound(result.Errors);
        }
        return Ok(result.Data);
    }
    [HttpGet("GetByColor")]
    public async Task<IActionResult> GetByColor([FromQuery] string color)
    {
        var result = await _sender.Send(new GetByColor.GetByColorQuery(color));
        if (!result.IsSuccess)
        {
            return NotFound(result.Errors);
        }
        return Ok(result.Data);
    }
    [HttpGet("GetByModel")]
    public async Task<IActionResult> GetByModel([FromQuery] string model)
    {
        var result = await _sender.Send(new GetByModel.GetByModelQuery(model));
        if (!result.IsSuccess)
        {
            return NotFound(result.Errors);
        }
        return Ok(result.Data);
    }
    [HttpGet("GetByTransmission")]
    public async Task<IActionResult> GetByTransmission([FromQuery] TransmissionTypes transmission)
    {
        var result = await _sender.Send(new GetByTransmission.GetByTransmissionQuery(transmission));
        if (!result.IsSuccess)
        {
            return NotFound(result.Errors);
        }
        return Ok(result.Data);
    }

    [HttpGet("GetByPrice")]
    public async Task<IActionResult> GetByPrice([FromQuery]  decimal maxPrice)
    {
        var query = new GetByPrice.GetByPriceQuery(maxPrice);
        var result = await _sender.Send(query);
        if (!result.IsSuccess)
        {
            return NotFound(result.Errors);
        }
        return Ok(result.Data);
    }
    [HttpGet("GetByMiles")]
    public async Task<IActionResult> GetByMiles([FromQuery] decimal minMiles, decimal maxMiles)
    {
        var query = new GetByMiles.GetByMilesQuery(minMiles, maxMiles);
        var result = await _sender.Send(query);
        if (!result.IsSuccess)
        {
            return NotFound(result.Errors);
        }
        return Ok(result.Data);
    }
    [HttpGet("GetByYear")]
    public async Task<IActionResult> GetByYear([FromQuery] int minYear, int maxYear)
    {
        var query = new GetByYear.GetByYearQuery(minYear, maxYear);
        var result = await _sender.Send(query);
        if (!result.IsSuccess)
        {
            return NotFound(result.Errors);
        }
        return Ok(result.Data);
    }

}
