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
            throw new Exception("User not found.");

        string groupName = $"auction-{auctionId}";

        await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

        var message = $"{userInfo.Name} {userInfo.Surname} joined the auction.";

        // ✔️ Bu mesajı yalnız digər user-lərə göndər (özünə yox)
        await Clients.OthersInGroup(groupName).SendAsync("ParticipantJoined", message);

        // ✔️ Caller-a da ayrıca göndər
        await Clients.Caller.SendAsync("ParticipantJoined", message);
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
