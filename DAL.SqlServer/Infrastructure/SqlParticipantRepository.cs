using DAL.SqlServer.Context;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories;

namespace DAL.SqlServer.Infrastructure;

public class SqlParticipantRepository(AppDbContext context) : IParticipantRepository
{
    private readonly AppDbContext _context = context;

    public async Task JoinAuction(AuctionParticipant participant)
    {
        _context.AuctionParticipants.Add(participant);
        await _context.SaveChangesAsync();
    }

    public async Task LeaveAuction(int auctionId, int userId)
    {
        var participant = await _context.AuctionParticipants
            .FirstOrDefaultAsync(ap => ap.AuctionId == auctionId && ap.UserId == userId);

        if (participant != null)
        {
            _context.AuctionParticipants.Remove(participant);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<List<User>> GetParticipants(int auctionId)
    {
        return await _context.AuctionParticipants
            .Where(ap => ap.AuctionId == auctionId)
            .Select(ap => ap.User) 
            .ToListAsync();
    }


    public async Task<bool> IsParticipant(int auctionId, int userId)
    {
        return await _context.AuctionParticipants
            .AnyAsync(ap => ap.AuctionId == auctionId && ap.UserId == userId);
    }
}
