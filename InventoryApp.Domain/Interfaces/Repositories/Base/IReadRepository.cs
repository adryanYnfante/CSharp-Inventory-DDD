using InventoryApp.Domain.Entities;

namespace InventoryApp.Domain.Interfaces.Repositories.Base
{
    public interface IReadRepository<TEntity, TEntityId>
        : IFind<TEntity,
        TEntityId>,
        IFindByCondition<TEntity>
    { }
}