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

    public async Task<Car> GetByIdAsync(int id)
    {
        return (await _context.Cars.FirstOrDefaultAsync(u => u.Id == id))!;
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
}
