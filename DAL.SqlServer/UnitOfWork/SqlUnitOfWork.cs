using DAL.SqlServer.Context;
using DAL.SqlServer.Infracture;
using Repository.Common;
using Repository.Repositories;

namespace DAL.SqlServer.UnitOfWork;

public class SqlUnitOfWork(string connectionString, AppDbContext context) : IUnitOfWork
{
    private readonly string _connectionString = connectionString;
    private readonly AppDbContext _context = context;

    public SqlProductRepository _productRepository;


    public IProductRepository ProductRepository => _productRepository ?? new SqlProductRepository(_connectionString, _context);

    public async Task CompleteAsync()
    {
        await _context.SaveChangesAsync();
    }
    public async Task<int> SaveChangeAsync()
    {
        return await _context.SaveChangesAsync();
    }
}
