using InventoryApp.Domain.Entities;
using InventoryApp.Domain.Interfaces.Repositories.Base;

namespace InventoryApp.Domain.Interfaces.Repositories
{
    public interface IProductRepository : ICommandRepository<Product, int>
    {
        Task<List<Product>> GetAllProductsPaging(ProductParameters productParameters);
    }
}