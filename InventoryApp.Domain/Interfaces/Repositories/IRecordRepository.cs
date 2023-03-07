namespace InventoryApp.Domain.Interfaces.Repositories
{
    public interface IRecordRepository<TEntity, TEntityId, TField>
        : IFind<TEntity,
        TEntityId>,
        IFindByField<TEntity, TEntityId, TField>
    { }
}