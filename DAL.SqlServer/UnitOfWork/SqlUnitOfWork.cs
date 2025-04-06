using DAL.SqlServer.Context;

using DAL.SqlServer.Infrastructure;

using Repository.Common;
using Repository.Repositories;

namespace DAL.SqlServer.UnitOfWork;

public class SqlUnitOfWork(string connectionString, AppDbContext context) : IUnitOfWork
{
    private readonly string _connectionString = connectionString;
    private readonly AppDbContext _context = context;

    public SqlCategoryRepository _categoryRepository;
    public SqlProductRepository _productRepository;
    public SqlCarRepository _carRepository;
    public SqlUserRepository _userRepository;
    public SqlRefreshTokenRepository _refreshTokenRepository;
    public SqlCartRepository _cartRepository;

    public ICategoryRepository CategoryRepository => _categoryRepository ?? new SqlCategoryRepository(_connectionString, _context);
    public IProductRepository ProductRepository => _productRepository ?? new SqlProductRepository(_connectionString, _context);
    public ICarRepository CarRepository => _carRepository ?? new SqlCarRepository(_connectionString, context);
    public IUserRepository UserRepository => _userRepository ?? new SqlUserRepository(_connectionString,context);
    public IRefreshTokenRepository RefreshTokenRepository => _refreshTokenRepository ?? new SqlRefreshTokenRepository(_context);
    public ICartRepository CartRepository => _cartRepository ?? new SqlCartRepository(_connectionString, _context);

    public async Task CompleteAsync()
    {
        await _context.SaveChangesAsync();
    }
    public async Task<int> SaveChangeAsync()
    {
        return await _context.SaveChangesAsync();
    }
}
