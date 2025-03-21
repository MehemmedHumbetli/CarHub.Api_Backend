using Repository.Repositories;

namespace Repository.Common;

public interface IUnitOfWork
{
    public ICarRepository CarRepository { get; }
    public IUserRepository UserRepository { get; }
    public IRefreshTokenRepository RefreshTokenRepository { get; }
    Task<int> SaveChangeAsync();   
}
