namespace InventoryApp.Domain.Interfaces.Repositories.Base
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}