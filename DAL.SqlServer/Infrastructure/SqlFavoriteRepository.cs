using DAL.SqlServer.Context;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories;

namespace DAL.SqlServer.Infrastructure;

public class SqlFavoriteRepository(AppDbContext context) : IFavoriteRepository
{
    private readonly AppDbContext _context = context;

    public async Task AddFavoriteCarAsync(int userId, int carId)
    {
        var favorite = await _context.UserFavorites
            .FirstOrDefaultAsync(uf => uf.UserId == userId && uf.CarId == carId);

        if (favorite == null)
        {
            favorite = new UserFavorite
            {
                UserId = userId,
                CarId = carId,
                IsFavorite = true
            };
            await _context.UserFavorites.AddAsync(favorite);
        }
        else
        {
            favorite.IsFavorite = true;
            _context.UserFavorites.Update(favorite);
        }

        await _context.SaveChangesAsync();
    }

    public async Task RemoveFavoriteCarAsync(int userId, int carId)
    {
        var favorite = await _context.UserFavorites
            .FirstOrDefaultAsync(uf => uf.UserId == userId && uf.CarId == carId);

        if (favorite != null)
        {
            favorite.IsFavorite = false;
            _context.UserFavorites.Update(favorite);
            await _context.SaveChangesAsync();
        }
    }
}
