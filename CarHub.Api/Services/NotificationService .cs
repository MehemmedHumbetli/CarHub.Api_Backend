using Application.CQRS.Auctions.ResponseDtos;
using Application.Services;
using CarHub.Api.SignalR.Hubs;
using DAL.SqlServer.Context;
using Domain.Entities;
using Microsoft.AspNetCore.SignalR;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CarHub.Api.Services;

public class NotificationService : INotificationService
{
    private readonly IHubContext<NotificationHub> _hubContext;
    private readonly AppDbContext _context;

    public NotificationService(IHubContext<NotificationHub> hubContext, AppDbContext context)
    {
        _hubContext = hubContext;
        _context = context;
    }

    public async Task SendAuctionActivatedNotificationAsync(AuctionActivatedNotificationDto data)
    {
        var users = _context.Users.ToList();

        foreach (var user in users)
        {
            var notification = new Notification
            {
                UserId = user.Id,
                Title = "Auction Notification",
                Message = $"{data.Car.Brand} {data.Car.Model} have been auction",
            };

            _context.Notifications.Add(notification);
        }

        await _hubContext.Clients.All.SendAsync("ReceiveNotification", new
        {
            id = Guid.NewGuid().ToString(),
            message = $"{data.Car.Brand} {data.Car.Model} have been auction"
        });

        await _context.SaveChangesAsync();
    }

    public async Task SendAuctionStoppedNotificationAsync(int auctionId, string msgReason, User winner)
    {
        if (msgReason == "win")
        {
            var notification = new Notification
            {
                UserId = winner.Id,
                Title = "Auction Notification",
                Message = $"{winner.Name} {winner.Surname} won Auction!"
            };

            _context.Notifications.Add(notification);

            await _hubContext.Clients.User(winner.Id.ToString()).SendAsync("ReceiveNotification", new
            {
                id = auctionId,
                message = $"You won the auction!"
            });

            Console.WriteLine($"Notification sent to user {winner.Id}");
        }
        else if (msgReason == "time")
        {
            var joinedUserIds = _context.AuctionParticipants
                .Where(ap => ap.AuctionId == auctionId)
                .Select(ap => ap.UserId)
                .ToList();

            foreach (var userId in joinedUserIds)
            {
                var notification = new Notification
                {
                    UserId = userId,
                    Title = "Auction Notification",
                    Message = "Auction time out"
                };

                _context.Notifications.Add(notification);
            }

            var stringUserIds = joinedUserIds.Select(id => id.ToString()).ToList();

            await _hubContext.Clients.Users(stringUserIds).SendAsync("ReceiveNotification", new
            {
                id = auctionId,
                message = "Auction time out"
            });

            Console.WriteLine("Notification sent to auction participants due to time out.");
        }

        await _context.SaveChangesAsync();
    }



}

