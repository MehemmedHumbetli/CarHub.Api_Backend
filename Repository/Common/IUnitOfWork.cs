using Repository.Repositories;

namespace Repository.Common;

public interface IUnitOfWork
{
    public IUserRepository UserRepository { get; }
    public ICarRepository CarRepository { get; }

    Task<int> SaveChangesAsync();   
}
