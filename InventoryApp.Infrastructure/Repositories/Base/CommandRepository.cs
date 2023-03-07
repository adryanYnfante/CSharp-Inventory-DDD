using InventoryApp.Domain.Interfaces.Repositories.Base;
using InventoryApp.Infrastructure.Contexts;

namespace InventoryApp.Infrastructure.Repositories.Base
{
    public class CommandRepository<TEntity, TEntityId> :
        ReadRepository<TEntity, TEntityId>, ICommandRepository<TEntity, TEntityId>
        where TEntity : class
    {
        private readonly InventoryEfContext _efContext;

        public IUnitOfWork UnitOfWork => _efContext;

        public CommandRepository(InventoryEfContext efContext) : base(efContext)
        {
            _efContext = efContext ?? throw new ArgumentNullException(nameof(efContext));
        }

        public async Task<TEntity> Add(TEntity entity)
        {
            var added = await _efContext.AddAsync(entity);

            return added.Entity;
        }

        public TEntity Update(TEntity entity) => _efContext.Update(entity).Entity;
    }
}