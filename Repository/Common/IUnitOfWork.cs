using Repository.Repositories;

namespace Repository.Common;

public interface IUnitOfWork
{
    public ICategoryRepository CategoryRepository { get; }
    public IProductRepository ProductRepository { get; }
    Task CompleteAsync();
    Task<int> SaveChangeAsync();
}
