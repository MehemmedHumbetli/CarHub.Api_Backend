using Repository.Repositories;

namespace Repository.Common;

public interface IUnitOfWork
{
    public ICarRepository CarRepository { get; }
    public IUserRepository UserRepository { get; }
    Task<int> SaveChangeAsync();   
}
