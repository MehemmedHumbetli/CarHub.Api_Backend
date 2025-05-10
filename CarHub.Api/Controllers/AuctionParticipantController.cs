using Application.CQRS.AuctionParticipants.Handlers;
using Application.CQRS.Auctions.Handler;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace CarHub.Api.Controllers;


[Route("api/[controller]")]
[ApiController]
public class AuctionParticipantController(ISender sender, IHubContext<AuctionHub> auctionHub) : Controller
{
    private readonly ISender _sender = sender;
    private readonly IHubContext<AuctionHub> _auctionHub = auctionHub;


    [HttpPost("JoinAuction")]
    public async Task<IActionResult> JoinAuction([FromBody] JoinAuction.JoinAuctionCommand request)
    {
        var result = await _sender.Send(request);

        if (result.IsSuccess)
        {
            // SignalR mesaj göndər
            await _auctionHub.Clients
                .Group($"auction-{result.Data.AuctionId}")
                .SendAsync("ReceiveJoinMessage", result.Data);
        }

        return Ok(result);
    }


    [HttpDelete("LeaveAuction")]
    public async Task<IActionResult> LeaveAuction([FromBody] LeaveAuction.LeaveAuctionCommand request)
    {
        return Ok(await _sender.Send(request));
    }

    [HttpGet("GetParticipants")]
    public async Task<IActionResult> GetParticipants([FromQuery] GetParticipants.GetParticipantsCommand request)
    {
        return Ok(await _sender.Send(request));
    }
}
