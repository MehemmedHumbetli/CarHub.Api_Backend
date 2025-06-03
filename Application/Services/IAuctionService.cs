namespace Application.Services;

public interface IAuctionService
{
    Task<decimal> IncreaseAuctionPriceAsync(int auctionId, int amount);
}
