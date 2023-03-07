using InventoryApp.Domain.Entities;
using InventoryApp.Domain.Interfaces.Repositories;
using InventoryApp.Infrastructure.Contexts;
using InventoryApp.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace InventoryApp.Infrastructure.Repositories
{
    public class ProductRepository : CommandRepository<Product, int>, IProductRepository
    {
        private readonly InventoryEfContext _efContext;

        public ProductRepository(InventoryEfContext efContext) : base(efContext)
        {
            _efContext = efContext;
        }

        public async Task<List<Product>> GetAllProductsPaging(ProductParameters productParameters)
        {
            return await _efContext.Products
                        .Skip((productParameters.PageNumber - 1) * productParameters.PageSize)
                        .Take(productParameters.PageSize)
                        .ToListAsync();
        }
    }
}