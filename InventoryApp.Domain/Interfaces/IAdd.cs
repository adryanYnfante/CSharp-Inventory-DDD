namespace InventoryApp.Domain.Interfaces
{
    public interface IAdd<TEntity>
    {
        Task<TEntity> Add(TEntity entity);
    }
}