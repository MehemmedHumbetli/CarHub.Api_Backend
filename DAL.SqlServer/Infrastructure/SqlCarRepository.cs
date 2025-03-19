using DAL.SqlServer.Context;
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

    public Task AddImagePathAsync(int carId, string imagePath)
    {
        throw new NotImplementedException();
    }

    public IQueryable<Car> GetAll()
    {
        return _context.Cars.Include(c => c.CarImagePaths);
    }

    public Task<IEnumerable<Car>> GetByBodyAsync(BodyTypes body)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Car>> GetByBrandAsync(string brand)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Car>> GetByColorAsync(string color)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Car>> GetByFuelAsync(FuelTypes fuel)
    {
        throw new NotImplementedException();
    }

    public Task<Car> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Car>> GetByMilesAsync(double miles)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Car>> GetByModelAsync(string model)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Car>> GetByPriceAsync(int price)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Car>> GetByTransmissionAsync(TransmissionTypes transmission)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Car>> GetByYearAsync(int year)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<string>> GetImagePathsAsync(int carId)
    {
        throw new NotImplementedException();
    }

    public Task RemoveAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task RemoveImagePathAsync(int carId, string imagePath)
    {
        throw new NotImplementedException();
    }

    public void Update(Car car)
    {
        throw new NotImplementedException();
    }
}
