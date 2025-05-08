using Application.Services;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;

namespace CarHub.Api.SignalR.Hubs
{
    public class AuctionHub : Hub
    {
        private readonly IAuctionService _auctionService;
        private static readonly ConcurrentDictionary<string, string> ConnectedUsers = new();

        public AuctionHub(IAuctionService auctionService)
        {
            _auctionService = auctionService;
        }

        public async Task JoinAuction(string userId, string fullName)
        {
            ConnectedUsers[Context.ConnectionId] = fullName;

            var users = ConnectedUsers.Values.ToList();
            await Clients.All.SendAsync("UserJoinedAuction", new { userId, fullName, users });
        }


        public async Task IncreasePrice(int auctionId, int amount)
        {
            var newPrice = await _auctionService.IncreaseAuctionPriceAsync(auctionId, amount);
            await Clients.All.SendAsync("PriceUpdated", new { auctionId, newPrice });
        }

        public override async Task OnConnectedAsync()
        {
            var userId = Context.UserIdentifier;
            Console.WriteLine($"User connected: {userId}");
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            ConnectedUsers.TryRemove(Context.ConnectionId, out var removedUser);
            var users = ConnectedUsers.Values.ToList();
            await Clients.All.SendAsync("UserLeftAuction", new { fullName = removedUser, users });
            await base.OnDisconnectedAsync(exception);
        }

    }
}
