using DAL.SqlServer.Context;
using Dapper;
using Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories;

namespace DAL.SqlServer.Infracture;

public class SqlProductRepository(string connectionString, AppDbContext context) : BaseSqlRepository(connectionString), IProductRepository
{
    private readonly AppDbContext _context = context;

    public async Task AddAsync(Product product)
    {
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();
    }

    public Task DeleteAsync(int productId)
    {
        var product = _context.Products.FirstOrDefaultAsync(p => p.Id == productId);
        return product;



    }

    public IQueryable<Product> GetAll()
    {
        return _context.Products;
    }

    public IEnumerable<Product> GetByCategoryId(int categoryId)
    {
        var sql = @" SELECT p.* FROM Products p
        WHERE p.CategoryId = @CategoryId";
        using var connection = OpenConnection();
        return connection.Query<Product>(sql, new { CategoryId = categoryId });
    }

    public Task<Product> GetByIdAsync(int id)
    {
        var product = _context.Products.FirstOrDefaultAsync(p => p.Id == id);
        return product;

    }

    public Task <Product> GetByNameAsync(string productName)
    {
        var product = _context.Products.FirstOrDefaultAsync(p => p.Name == productName);
        return product;
    }

    public async Task<List<Product>> GetProductsByPriceRange(decimal minPrice, decimal maxPrice)
    {
        return await _context.Products.Where(p => p.UnitPrice >= minPrice && p.UnitPrice <= maxPrice).ToListAsync();
    }

    public void Update(Product product)
    {
        product.UpdatedDate = DateTime.Now;
        _context.Products.Update(product);
        _context.SaveChanges();
    }

   
}

