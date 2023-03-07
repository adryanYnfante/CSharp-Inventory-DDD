using System.Linq.Expressions;

namespace InventoryApp.Domain.Interfaces
{
    public interface IFindByCondition<TEntity>
    {
        Task<List<TEntity>> GetByCondition(Expression<Func<TEntity, bool>> condition);
    }
}