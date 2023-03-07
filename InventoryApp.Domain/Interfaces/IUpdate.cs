namespace InventoryApp.Domain.Interfaces
{
    public interface IUpdate<TEntity>
    {
        TEntity Update(TEntity entity);
    }
}