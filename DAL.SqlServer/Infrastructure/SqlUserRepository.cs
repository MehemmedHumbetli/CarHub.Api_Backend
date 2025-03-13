using DAL.SqlServer.Context;
using Dapper;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories;

namespace DAL.SqlServer.Infrastructure;

public class SqlUserRepository(string connectionString, AppDbContext context) : BaseSqlRepository(connectionString), IUserRepository
{
    private readonly AppDbContext _context;

    public IQueryable<User> GetAll()
    {
        return _context.Users;
    }

    public async Task<User> GetByIdAsync(int id)
    {
        return (await _context.Users.FirstOrDefaultAsync(u => u.Id == id))!;
    }

    public async Task<User> GetUserByEmailAsync(string email)
    {
        return (await _context.Users.FirstOrDefaultAsync(u => u.Email == email))!;
    }
    public void Update(User user)
    {
        user.UpdatedDate = DateTime.Now;
        _context.Update(user);
        _context.SaveChanges();
    }
    public async Task RegisterAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public async Task Remove(int id)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        user.IsDeleted = true;
        user.DeletedDate = DateTime.Now;
        user.DeletedBy = 0;
    }


    //Dapper Operations
    public async Task<IEnumerable<Car>> GetUserFavoritesAsync(int userId)
    {
        using var connection = OpenConnection();
        string sql = @"SELECT c.Id, c.Brand, c.Model, c.Year 
                       FROM FavoriteCars fc
                       JOIN Cars c ON fc.CarId = c.Id
                       WHERE fc.UserId = @UserId";
        return await connection.QueryAsync<Car>(sql, new { UserId = userId });
    }

    public async Task AddFavoriteCarAsync(int userId, int carId)
    {
        using var connection = OpenConnection();
        string sql = "INSERT INTO FavoriteCars (UserId, CarId) VALUES (@UserId, @CarId)";
        await connection.ExecuteAsync(sql, new { UserId = userId, CarId = carId });
    }

    public async Task RemoveFavoriteCarAsync(int userId, int carId)
    {
        using var connection = OpenConnection();
        string sql = "DELETE FROM FavoriteCars WHERE UserId = @UserId AND CarId = @CarId";
        await connection.ExecuteAsync(sql, new { UserId = userId, CarId = carId });
    }

    public async Task<IEnumerable<Car>> GetUserCarsAsync(int userId)
    {
        using var connection = OpenConnection();
        string sql = @"SELECT c.Id, c.Brand, c.Model, c.Year 
                       FROM UserCars uc
                       JOIN Cars c ON uc.CarId = c.Id
                       WHERE uc.UserId = @UserId";
        return await connection.QueryAsync<Car>(sql, new { UserId = userId });
    }

    public async Task AddUserCarAsync(int userId, Car car)
    {
        using var connection = OpenConnection();
        string sql = @"INSERT INTO Cars (Brand, Model, Year) 
                       VALUES (@Brand, @Model, @Year);
                       DECLARE @CarId INT = SCOPE_IDENTITY();
                       INSERT INTO UserCars (UserId, CarId) VALUES (@UserId, @CarId)";
        await connection.ExecuteAsync(sql, new { car.Brand, car.Model, car.Year, UserId = userId });
    }

    public async Task RemoveUserCarAsync(int userId, int carId)
    {
        using var connection = OpenConnection();
        string sql = "DELETE FROM UserCars WHERE UserId = @UserId AND CarId = @CarId";
        await connection.ExecuteAsync(sql, new { UserId = userId, CarId = carId });
    }

    public async Task<IEnumerable<string>> GetUserImagePathsAsync(int userId)
    {
        using var connection = OpenConnection();
        string sql = "SELECT ImagePath FROM UserImages WHERE UserId = @UserId";
        return await connection.QueryAsync<string>(sql, new { UserId = userId });
    }

    public async Task AddUserImagePathAsync(int userId, string imagePath)
    {
        using var connection = OpenConnection();
        string sql = "INSERT INTO UserImages (UserId, ImagePath) VALUES (@UserId, @ImagePath)";
        await connection.ExecuteAsync(sql, new { UserId = userId, ImagePath = imagePath });
    }

    public async Task RemoveUserImagePathAsync(int userId, string imagePath)
    {
        using var connection = OpenConnection();
        string sql = "DELETE FROM UserImages WHERE UserId = @UserId AND ImagePath = @ImagePath";
        await connection.ExecuteAsync(sql, new { UserId = userId, ImagePath = imagePath });
    }
}