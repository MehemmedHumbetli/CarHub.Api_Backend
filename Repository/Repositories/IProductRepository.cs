using Domain.Entities;

namespace Repository.Repositories;

public interface IProductRepository
{
    #region
    Task AddAsync(Product product); 
    void Update(Product product);
    Task DeleteAsync(int productId);
    Task<Product> GetByIdAsync(int id);
    IQueryable<Product> GetAll();

    #endregion


    Task<IEnumerable<Product>> GetByNameAsync(string productName);

}
