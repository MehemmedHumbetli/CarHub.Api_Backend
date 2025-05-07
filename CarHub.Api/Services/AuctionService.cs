using Application.Services;
using DAL.SqlServer.Context;
using Microsoft.EntityFrameworkCore;

namespace CarHub.Api.Services;

public class AuctionService : IAuctionService
{
    private readonly AppDbContext _context;

    public AuctionService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<decimal> IncreaseAuctionPriceAsync(int auctionId, int amount)
    {
        var auction = await _context.Auctions.FindAsync(auctionId);

        if (auction == null)
            throw new Exception("Auction not found");

        auction.StartingPrice += amount;
        await _context.SaveChangesAsync();

        return auction.StartingPrice;
    }

}
