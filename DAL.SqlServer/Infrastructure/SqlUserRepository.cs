using DAL.SqlServer.Context;
using Dapper;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories;

namespace DAL.SqlServer.Infrastructure;

public class SqlUserRepository(string connectionString, AppDbContext context) : BaseSqlRepository(connectionString), IUserRepository
{
    private readonly AppDbContext _context = context;

    public IQueryable<User> GetAll()
    {
        return _context.Users;
    }

    public async Task<User> GetByIdAsync(int id)
    {
        return (await _context.Users.Include(c => c.Favorites).FirstOrDefaultAsync(u => u.Id == id))!;
    }

    public async Task<User> GetUserByEmailAsync(string email)
    {
        return (await _context.Users.FirstOrDefaultAsync(u => u.Email == email))!;
    }
    public void Update(User user)
    {
        var users = _context.Users.ToList();
        Console.WriteLine(users);
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
        string sql = @"SELECT c.Id, c.Brand, c.BrandImagePath, c.Model, c.Year, c.Price, 
                          c.Fuel, c.Transmission, c.Miles, c.Body, c.BodyTypeImage, 
                          c.Color, c.VIN, c.Text 
                   FROM UserFavorite fc
                   JOIN Cars c ON fc.CarId = c.Id
                   WHERE fc.UserId = @UserId";
        return await connection.QueryAsync<Car>(sql, new { UserId = userId });
    }

    public async Task AddFavoriteCarAsync(int userId, int carId)
    {
        using var connection = OpenConnection();
        string sql = "INSERT INTO UserFavorite (UserId, CarId, IsFavorite) VALUES (@UserId, @CarId, @IsFavorite)";
        await connection.ExecuteAsync(sql, new { UserId = userId, CarId = carId, IsFavorite = true });
    }


    public async Task RemoveFavoriteCarAsync(int userId, int carId)
    {
        using var connection = OpenConnection();
        string sql = "UPDATE UserFavorite SET IsFavorite = 0 WHERE UserId = @UserId AND CarId = @CarId";
        await connection.ExecuteAsync(sql, new { UserId = userId, CarId = carId });
    }


    public async Task<IEnumerable<Car>> GetUserCarsAsync(int userId)
    {
        using var connection = OpenConnection();
        string sql = @"SELECT c.Id, c.Brand, c.Model, c.Year, c.Price, 
                          c.Fuel, c.Transmission, c.Miles, 
                          c.Color, c.VIN, c.Text 
                   FROM UserCars uc
                   JOIN Cars c ON uc.CarId = c.Id
                   WHERE uc.UserId = @UserId";
        return await connection.QueryAsync<Car>(sql, new { UserId = userId });
    }

    //public async Task RemoveUserCarAsync(int userId, int carId)
    //{
    //    using var connection = OpenConnection();
    //    string sql = "DELETE FROM UserCars WHERE UserId = @UserId AND CarId = @CarId";
    //    await connection.ExecuteAsync(sql, new { UserId = userId, CarId = carId });
    //}


    //public async Task AddUserCarAsync(int userId, Car car)
    //{
    //    car.UserId = userId;

    //    await _context.Cars.AddAsync(car);
    //}
}