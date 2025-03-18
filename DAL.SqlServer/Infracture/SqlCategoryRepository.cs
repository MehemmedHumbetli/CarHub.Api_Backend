using DAL.SqlServer.Context;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories;

namespace DAL.SqlServer.Infracture;

public class SqlCategoryRepository(string connectionString, AppDbContext context) : BaseSqlRepository(connectionString), ICategoryRepository
{
    private readonly AppDbContext _context = context;

    public async Task AddAsync(Category category)
    {
        await _context.Categories.AddAsync(category);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
        category.IsDeleted = true;
        category.DeletedDate = DateTime.Now;
        category.DeletedBy = 0;
    }

    public IQueryable<Category> GetAll()
    {
        return _context.Categories;
    }

    public async Task<Category> GetByIdAsync(int id)
    {
        var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
        return category;

    }

    public async Task<Category> GetByNameAsync(string name)
    {
        var category = await _context.Categories.FirstOrDefaultAsync(c => c.Name == name);
        return category;
    }

    public void Update(Category category)
    {
        category.UpdatedDate = DateTime.Now;
        _context.Update(category);
        _context.SaveChanges();
    }

    public Task<IEnumerable<Category>> GetCategoriesWithProductsAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Product>> GetProductsByPriceRangeAsync(decimal minPrice, decimal maxPrice)
    {
        //var sql = @"
        //    SELECT * FROM Products 
        //    WHERE UnitPrice BETWEEN @MinPrice AND @MaxPrice";

        //using (var connection = OpenConnection())
        //{
        //    return await connection.QueryAsync<Product>(sql, new { MinPrice = minPrice, MaxPrice = maxPrice });
        //}

        throw new NotImplementedException();
    }
}



