namespace InventoryApp.Domain.Interfaces.Repositories.Base
{
    public interface ICommandRepository<TEntity, TEntityId>
        : IReadRepository<TEntity, TEntityId>,
        IAdd<TEntity>,
        IUpdate<TEntity>,
        IRepository
    { }
}