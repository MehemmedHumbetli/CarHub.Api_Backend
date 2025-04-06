using Repository.Repositories;

namespace Repository.Common;

public interface IUnitOfWork
{
    public IFavoriteRepository FavoriteRepository {  get; } 
    public ICategoryRepository CategoryRepository { get; }
    public IProductRepository ProductRepository { get; }
    public ICarRepository CarRepository { get; }
    public IUserRepository UserRepository { get; }
    public IRefreshTokenRepository RefreshTokenRepository { get; }
    Task CompleteAsync();
    Task<int> SaveChangeAsync();
  
}
