using Application.Services;
using Microsoft.AspNetCore.SignalR;

namespace CarHub.Api.SignalR.Hubs
{
    public class AuctionHub : Hub
    {
        private readonly IAuctionService _auctionService;

        public AuctionHub(IAuctionService auctionService)
        {
            _auctionService = auctionService;
        }

        public async Task JoinAuction(string userId, string fullName)
        {
            await Clients.All.SendAsync("UserJoinedAuction", new { userId, fullName });
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
            var userId = Context.UserIdentifier;
            Console.WriteLine($"User disconnected: {userId}");
            await base.OnDisconnectedAsync(exception);
        }
    }
}
