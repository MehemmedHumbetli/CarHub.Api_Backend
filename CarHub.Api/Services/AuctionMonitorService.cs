using Microsoft.AspNetCore.SignalR;
using Repository.Common;

namespace CarHub.Api.Services;

public class AuctionMonitorService : BackgroundService
{
    private readonly IHubContext<AuctionHub> _hubContext;
    private readonly IServiceScopeFactory _scopeFactory;

    public AuctionMonitorService(IHubContext<AuctionHub> hubContext, IServiceScopeFactory scopeFactory)
    {
        _hubContext = hubContext;
        _scopeFactory = scopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

                foreach (var entry in AuctionHub.ActiveAuctions.ToList())
                {
                    var running = entry.Value;

                    if (running.IsExpired)
                    {
                        var dbAuction = await unitOfWork.AuctionRepository.GetByIdAsync(running.AuctionId);
                        dbAuction.IsActive = false;
                        dbAuction.EndTime = DateTime.UtcNow;

                        await unitOfWork.CompleteAsync();

                        await _hubContext.Clients.Group($"auction-{running.AuctionId}")
                            .SendAsync("AuctionEnded", new
                            {
                                auctionId = running.AuctionId,
                                winner = running.LastBidderUserName,
                                finalPrice = running.CurrentPrice
                            });

                        AuctionHub.ActiveAuctions.Remove(running.AuctionId);
                    }
                }
            }

            await Task.Delay(1000, stoppingToken);
        }
    }
}
