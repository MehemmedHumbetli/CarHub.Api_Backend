using Application.CQRS.AuctionParticipants.Handlers;
using Application.CQRS.Auctions.Handler;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarHub.Api.Controllers;


[Route("api/[controller]")]
[ApiController]
public class AuctionParticipantController(ISender sender) : Controller
{
    private readonly ISender _sender = sender;

    [HttpPost]
    public async Task<IActionResult> JoinAuction([FromBody] JoinAuction.JoinAuctionCommand request)
    {
        return Ok(await _sender.Send(request));
    }
}
