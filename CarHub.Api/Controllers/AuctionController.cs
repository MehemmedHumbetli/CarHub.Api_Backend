using Application.CQRS.Auctions.Handler;
using Azure.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static Application.CQRS.Cars.Handlers.CarGetById;

namespace CarHub.Api.Controllers;


[Route("api/[controller]")]
[ApiController]
public class AuctionController(ISender sender) : Controller
{
    private readonly ISender _sender = sender;

    [HttpPost]
    public async Task<IActionResult> CreateAuction([FromBody] CreateAuction.CreateAuctionCommand request)
    {
        return Ok(await _sender.Send(request));
    }

    [HttpGet("AuctionGetById")]
    public async Task<IActionResult> AuctionGetById([FromQuery] GetByIdAsync.GetByIdAuctionCommand request)
    {
        return Ok(await _sender.Send(request));
    }
}
