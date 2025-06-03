using DAL.SqlServer.Context;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories;

namespace DAL.SqlServer.Infrastructure;

public class SqlNotificationRepository(AppDbContext context) : INotificationRepository
{
    private readonly AppDbContext _context = context;

    public async Task<List<Notification>> GetAllNotification(int userId)
    {
        return await _context.Notifications
                                .Where(a => a.UserId == userId)
                                .ToListAsync();
    }

    public async Task Remove(int id)
    {
        var notification = await _context.Notifications.FindAsync(id);
        if (notification != null)
        {
            _context.Notifications.Remove(notification);
            await _context.SaveChangesAsync();
        }
    }
}
