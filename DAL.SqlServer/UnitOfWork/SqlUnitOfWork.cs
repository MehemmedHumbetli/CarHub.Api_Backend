using DAL.SqlServer.Context;
using DAL.SqlServer.Infrastructure;
using Repository.Common;
using Repository.Repositories;

namespace DAL.SqlServer.UnitOfWork;

public class SqlUnitOfWork(string connectionString, AppDbContext context) : IUnitOfWork
{
    private readonly string _connectionString = connectionString;
    private readonly AppDbContext _context = context;

    public SqlCarRepository _carRepository;
    public SqlUserRepository _userRepository;
    public SqlRefreshTokenRepository _refreshTokenRepository;

    public ICarRepository CarRepository => _carRepository ?? new SqlCarRepository(_connectionString, context);
    public IUserRepository UserRepository => _userRepository ?? new SqlUserRepository(_connectionString,context);
    public IRefreshTokenRepository RefreshTokenRepository => _refreshTokenRepository ?? new SqlRefreshTokenRepository(_context);

    public async Task<int> SaveChangeAsync()
    {
        return await _context.SaveChangesAsync();
    }

}
