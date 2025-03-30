
using Domain.Entities;
using Domain.Enums;

namespace Repository.Repositories;

public interface ICarRepository
{
    Task AddAsync(Car car);
    void Update(Car car);
    Task Remove(int id);
    IQueryable<Car> GetAll();
    Task<Car> GetByIdAsync(int id);

    //Dapper Operations
    Task<IEnumerable<Car>> GetByBrandAsync(string brand); //
    Task<IEnumerable<Car>> GetByModelAsync(string model); //
    Task<IEnumerable<Car>> GetByYearAsync(int minYear, int maxYear); 
    Task<IEnumerable<Car>> GetByPriceAsync(decimal minPrice, decimal maxPrice);
    Task<IEnumerable<Car>> GetByFuelAsync(FuelTypes fuel); // 
    Task<IEnumerable<Car>> GetByTransmissionAsync(TransmissionTypes transmission); //
    Task<IEnumerable<Car>> GetByMilesAsync(decimal minMiles, decimal maxMiles); 
    Task<IEnumerable<Car>> GetByBodyAsync(BodyTypes body); // 
    Task<IEnumerable<Car>> GetByColorAsync(string color); //
  
}
