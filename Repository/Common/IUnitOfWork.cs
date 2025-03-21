using Repository.Repositories;

namespace Repository.Common;

public interface IUnitOfWork
{
    IProductRepository ProductRepository { get; }

    Task CompleteAsync();

    Task<int> SaveChangeAsync();
}
