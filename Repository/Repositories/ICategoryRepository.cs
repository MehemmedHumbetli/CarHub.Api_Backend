using Domain.Entities;

namespace Repository.Repositories
{
    public interface ICategoryRepository
    {
        IQueryable<Category> GetAll();
        Task<Category> GetByIdAsync(int id);
        Task AddAsync(Category category);
        void Update(Category category);
        Task DeleteAsync(int id);

        Task<Category> GetByNameAsync(string name);
        Task<IEnumerable<Category>> GetCategoriesWithProductsAsync(); // category icinde ki produclarnan birlikde getirir
        Task<IEnumerable<Product>> GetProductsByPriceRangeAsync(decimal minPrice, decimal maxPrice); // filtreleme 
    }
}
