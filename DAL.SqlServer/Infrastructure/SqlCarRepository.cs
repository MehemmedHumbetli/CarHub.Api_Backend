using DAL.SqlServer.Context;
using Dapper;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories;

namespace DAL.SqlServer.Infrastructure;

public class SqlCarRepository(string connectionString, AppDbContext context) : BaseSqlRepository(connectionString), ICarRepository
{
    private readonly AppDbContext _context = context;
    public async Task AddAsync(Car car)
    {
        await _context.Cars.AddAsync(car);
        await _context.SaveChangesAsync();
    }

    public async Task Remove(int id)
    {
        var car = await _context.Cars.FirstOrDefaultAsync(u => u.Id == id);
        car.IsDeleted = true;
        car.DeletedDate = DateTime.Now;
        car.DeletedBy = 0;
    }

    public void Update(Car car)
    {
        var cars = _context.Cars.ToList();
        car.UpdatedDate = DateTime.Now;
        _context.Update(car);
        _context.SaveChanges();
    }

    public IQueryable<Car> GetAll()
    {
        return _context.Cars
            .Where(c => !c.IsDeleted)
            .Include(c => c.CarImagePaths);
    }

    public async Task<IEnumerable<Car>> GetByBodyAsync(BodyTypes body)
    {
        await using var connection = OpenConnection();
        string query = "SELECT * FROM Cars WHERE Body = @Body";
        return await connection.QueryAsync<Car>(query, new { Body = (int)body });
    }

    public async Task<IEnumerable<Car>> GetByBrandAsync(string brand)
    {
        await using var connection = OpenConnection();
        string query = "SELECT * FROM Cars WHERE Brand = @Brand";
        return await connection.QueryAsync<Car>(query, new { Brand = brand });
    }

    public async Task<IEnumerable<Car>> GetByColorAsync(string color)
    {
        await using var connection = OpenConnection();
        string query = "SELECT * FROM Cars WHERE Color = @Color";
        return await connection.QueryAsync<Car>(query, new { Color = color });
    }

    public async Task<IEnumerable<Car>> GetByFuelAsync(FuelTypes fuel)
    {
        await using var connection = OpenConnection();
        string query = "SELECT * FROM Cars WHERE Fuel = @Fuel";
        return await connection.QueryAsync<Car>(query, new { Fuel = (int)fuel });
    }

    public async Task<Car> GetByIdAsync(int id)
    {
        return (await _context.Cars.FirstOrDefaultAsync(u => u.Id == id))!;
    }

    public async Task<IEnumerable<Car>> GetByMilesAsync(decimal minMiles, decimal maxMiles)
    {
        await using var connection = OpenConnection();
        string query = "SELECT * FROM Cars WHERE Miles BETWEEN @MinMiles AND @MaxMiles";
        return await connection.QueryAsync<Car>(query, new { MinMiles = minMiles, MaxMiles = maxMiles });
    }

    public async  Task<IEnumerable<Car>> GetByModelAsync(string model)
    {
        await using var connection = OpenConnection();
        string query = "SELECT * FROM Cars WHERE LOWER(Model) LIKE LOWER(@Model)";
        return await connection.QueryAsync<Car>(query, new { Model = model.ToLower() + "%" });
    }

    public async Task<IEnumerable<Car>> GetByPriceAsync(decimal minPrice, decimal maxPrice)
    {
        await using var connection = OpenConnection();
        string query = "SELECT * FROM Cars WHERE Price BETWEEN @MinPrice AND @MaxPrice /*ORDER BY  Price DESC*/";
        return await connection.QueryAsync<Car>(query, new { MinPrice = minPrice, MaxPrice = maxPrice });
    }

    public async Task<IEnumerable<Car>> GetByTransmissionAsync(TransmissionTypes transmission)
    {
        await using var connection = OpenConnection();
        string query = "SELECT * FROM Cars WHERE Transmission = @Transmission";
        return await connection.QueryAsync<Car>(query, new { Transmission = (int)transmission });
    }

    public async Task<IEnumerable<Car>> GetByYearAsync(int minYear, int maxYear)
    {
        await using var connection = OpenConnection();  
        string query = "SELECT * FROM Cars WHERE Year BETWEEN @MInYear AND @MaxYear";
        return await connection.QueryAsync<Car>(query, new { MinYear = minYear, MaxYear = maxYear });
    }

    
}
