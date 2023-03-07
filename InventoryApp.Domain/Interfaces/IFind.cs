namespace InventoryApp.Domain.Interfaces
{
    public interface IFind<TEntity, TEntityId>
    {
        Task<List<TEntity>> GetAll();

        Task<TEntity> GetById(TEntityId entityId);
    }
}