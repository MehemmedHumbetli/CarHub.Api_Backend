using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories;

public interface ICarRepository
{
    Task AddAsync(Car car);
    void Update(Car car);
    Task RemoveAsync(int id);
    IQueryable<Car> GetAll();
    Task<Car> GetByIdAsync(int id);

    //Dapper Operations
    Task<IEnumerable<Car>> GetByBrandAsync(string brand);
    Task<IEnumerable<Car>> GetByModelAsync(string model);
    Task<IEnumerable<Car>> GetByYearAsync(int year);
    Task<IEnumerable<Car>> GetByPriceAsync(int price);
    Task<IEnumerable<Car>> GetByFuelAsync(FuelTypes fuel);
    Task<IEnumerable<Car>> GetByTransmissionAsync(TransmissionTypes transmission);
    Task<IEnumerable<Car>> GetByMilesAsync(double miles);
    Task<IEnumerable<Car>> GetByBodyAsync(BodyTypes body);
    Task<IEnumerable<Car>> GetByColorAsync(string color);
    Task<IEnumerable<string>> GetImagePathsAsync(int carId);
    Task AddImagePathAsync(int carId, string imagePath);
    Task RemoveImagePathAsync(int carId, string imagePath);
}
