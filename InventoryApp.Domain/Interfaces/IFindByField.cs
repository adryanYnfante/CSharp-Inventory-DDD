namespace InventoryApp.Domain.Interfaces
{
    public interface IFindByField<TEntity, TEntityId, TField>
    {
        Task<List<TEntity>> FindByField(TField field);
    }
}