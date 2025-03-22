using Domain.Entities;

namespace Repository.Repositories;

public interface IUserRepository
{
    Task RegisterAsync(User user);
    void Update(User user);
    Task Remove(int id);
    IQueryable<User> GetAll();
    Task<User> GetByIdAsync(int id);
    Task<User> GetUserByEmailAsync(string email);
    //Task AddUserCarAsync(int userId, Car car);
    //Task RemoveUserCarAsync(int userId, int carId);

    // Dapper Operations
    Task<IEnumerable<Car>> GetUserFavoritesAsync(int userId);  
    Task AddFavoriteCarAsync(int userId, int carId);           
    Task RemoveFavoriteCarAsync(int userId, int carId);        

    Task<IEnumerable<Car>> GetUserCarsAsync(int userId);                   
  
}
