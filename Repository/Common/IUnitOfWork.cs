using Repository.Repositories;

namespace Repository.Common;

public interface IUnitOfWork
{
    public ICarRepository CarRepository { get; }

    Task<int> SaveChangeAsync();   
}
