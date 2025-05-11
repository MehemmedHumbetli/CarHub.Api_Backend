using Domain.Entities;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Repository.Common;
using Repository.Repositories;
using System.Text.RegularExpressions;


public class AuctionHub : Hub
{
    private readonly IUnitOfWork _unitOfWork;

    public AuctionHub(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task JoinAuction(int auctionId, int userId)
    {
        var userInfo = await _unitOfWork.UserRepository.GetByIdAsync(userId);

        if (userInfo == null)
        {
            throw new Exception("User not found.");
        }

        await Groups.AddToGroupAsync(Context.ConnectionId, $"auction-{auctionId}");

        var participantMessage = $"{userInfo.Name} {userInfo.Surname} joined the auction back.";

        await Clients.Group($"auction-{auctionId}")
        .SendAsync("ParticipantJoined", participantMessage);

        await Clients.Caller.SendAsync("ParticipantJoined", participantMessage);
    }

    public async Task LeaveAuction(int auctionId, int userId)
    {
        var userInfo = await _unitOfWork.UserRepository.GetByIdAsync(userId);

        if (userInfo == null)
        {
            throw new Exception("User not found.");
        }

        await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"auction-{auctionId}");

        var leaveMessage = $"{userInfo.Name} {userInfo.Surname} left the auction.";

        await Clients.Group($"auction-{auctionId}")
            .SendAsync("ParticipantLeft", leaveMessage);

        await Clients.Caller.SendAsync("ParticipantLeft", leaveMessage);
    }


}
