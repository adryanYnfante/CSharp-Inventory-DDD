using InventoryApp.Domain.Interfaces.Repositories.Base;
using InventoryApp.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace InventoryApp.Infrastructure.Repositories.Base
{
    public class ReadRepository<TEntity, TEntityId> : IReadRepository<TEntity, TEntityId>
        where TEntity : class
    {
        private readonly InventoryEfContext _efContext;

        public ReadRepository(InventoryEfContext efContext)
        {
            _efContext = efContext ?? throw new ArgumentNullException(nameof(efContext));
        }

        public async Task<List<TEntity>> GetAll()
        {
            return await _efContext.Set<TEntity>().ToListAsync();
        }

        public async Task<List<TEntity>> GetByCondition(Expression<Func<TEntity, bool>> condition)
        {
            return await _efContext.Set<TEntity>().Where(condition).ToListAsync();
        }

        public async Task<TEntity> GetById(TEntityId entityId)
        {
            return await _efContext.Set<TEntity>().FindAsync(entityId);
        }
    }
}