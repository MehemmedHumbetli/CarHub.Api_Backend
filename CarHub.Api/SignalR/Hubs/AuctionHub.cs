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
        Console.WriteLine("Join Auction is working!");
        // İstifadəçi məlumatlarını əldə et
        var userInfo = await _unitOfWork.UserRepository.GetByIdAsync(userId);

        if (userInfo == null)
        {
            throw new Exception("User not found.");
        }

        // İstifadəçini qrupda qoşuruq
        await Groups.AddToGroupAsync(Context.ConnectionId, $"auction-{auctionId}");

        // İştirakçını yaradın və məlumatı göndərin
        var participantMessage = $"{userInfo.Name} {userInfo.Surname} joined the auction.";

        await Clients.Group($"auction-{auctionId}")
        .SendAsync("ParticipantJoined", participantMessage);

        // əlavə olaraq özünə də göndər:
        await Clients.Caller.SendAsync("ParticipantJoined", participantMessage);
    }
    public override async Task OnConnectedAsync()
    {
        await Clients.Caller.SendAsync("ParticipantJoined", "Test mesaj: bağlantı uğurla alındı!");
        await base.OnConnectedAsync();
    }

}
