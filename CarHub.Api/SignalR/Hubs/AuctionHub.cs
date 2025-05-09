using Application.Services;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;

namespace CarHub.Api.SignalR.Hubs
{
    public class AuctionHub : Hub
    {
        private readonly IAuctionService _auctionService;
        private static readonly ConcurrentDictionary<int, ConcurrentDictionary<string, string>> AuctionUsers = new();


        public AuctionHub(IAuctionService auctionService)
        {
            _auctionService = auctionService;
        }

        public async Task JoinAuction(int auctionId, int userId, string fullName)
        {
            var usersDict = AuctionUsers.GetOrAdd(auctionId, _ => new ConcurrentDictionary<string, string>());
            usersDict[Context.ConnectionId] = fullName;

            await Groups.AddToGroupAsync(Context.ConnectionId, $"auction-{auctionId}");

            var users = usersDict.Values.ToList();
            await Clients.Group($"auction-{auctionId}").SendAsync("UserJoinedAuction", new { userId, fullName, users });

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
            foreach (var auction in AuctionUsers)
            {
                var auctionId = auction.Key;
                var usersDict = auction.Value;

                if (usersDict.TryRemove(Context.ConnectionId, out var removedUser))
                {
                    var users = usersDict.Values.ToList();

                    await Clients.Group($"auction-{auctionId}")
                        .SendAsync("UserLeftAuction", new { fullName = removedUser, users });
                }
            }

            await base.OnDisconnectedAsync(exception);
        }


    }
}
