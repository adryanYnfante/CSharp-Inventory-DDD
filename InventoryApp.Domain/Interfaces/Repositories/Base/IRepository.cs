namespace InventoryApp.Domain.Interfaces.Repositories.Base
{
    public interface IRepository
    {
        IUnitOfWork UnitOfWork { get; }
    }
}