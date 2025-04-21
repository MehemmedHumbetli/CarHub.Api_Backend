using DAL.SqlServer.Context;
using Repository.Repositories;

namespace DAL.SqlServer.Infrastructure;

public class SqlFileUploadRepository(AppDbContext context) : IFileUploadRepository
{
    private readonly AppDbContext _context = context;

    public Task AddAsync(Domain.Entities.File file)
    {
        throw new NotImplementedException();
    }

    public Task<Domain.Entities.File> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Domain.Entities.File> GetByNameAsync(string name)
    {
        throw new NotImplementedException();
    }

    public Task Remove(int id)
    {
        throw new NotImplementedException();
    }
}
