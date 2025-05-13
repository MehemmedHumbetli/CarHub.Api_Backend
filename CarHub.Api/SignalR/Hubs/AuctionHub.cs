using Domain.Entities;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Repository.Common;
using Repository.Repositories;
using System.Text.RegularExpressions;


public class AuctionHub : Hub
{
    private readonly IUnitOfWork _unitOfWork;
    public static Dictionary<int, RunningAuction> ActiveAuctions = new();


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

        await Clients.OthersInGroup(groupName).SendAsync("ParticipantJoined", message);
        await Clients.Caller.SendAsync("ParticipantJoined", message);

        if (ActiveAuctions.TryGetValue(auctionId, out var runningAuction))
        {
            await Clients.Caller.SendAsync("InitialAuctionState", new
            {
                topBidder = runningAuction.LastBidderUserName,
                price = runningAuction.CurrentPrice
            });
        }
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

    public async Task PlaceBid(int auctionId, int userId, decimal bidIncrement)
    {
        var auction = await _unitOfWork.AuctionRepository.GetByIdAsync(auctionId);
        var user = await _unitOfWork.UserRepository.GetByIdAsync(userId);

        if (auction == null || user == null)
            throw new Exception("Auction or User not found.");

        if (!ActiveAuctions.ContainsKey(auctionId))
        {
            ActiveAuctions[auctionId] = new RunningAuction
            {
                AuctionId = auctionId,
                CurrentPrice = auction.StartingPrice,
                LastBidderUserName = "",
                ExpireAt = DateTime.UtcNow.AddSeconds(15)
            };
        }

        var running = ActiveAuctions[auctionId];

        decimal newBid = running.CurrentPrice + bidIncrement;
        running.CurrentPrice = newBid;
        running.LastBidderUserName = $"{user.Name} {user.Surname}";
        running.ResetTimer();

        await Clients.Group($"auction-{auctionId}").SendAsync("BidPlaced", new
        {
            auctionId,
            newPrice = newBid,
            bidder = running.LastBidderUserName,
            remainingSeconds = running.RemainingSeconds
        });
    }

}
