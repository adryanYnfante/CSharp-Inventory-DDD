using InventoryApp.Domain.Entities;
using InventoryApp.Domain.Interfaces.Repositories;
using InventoryApp.Domain.UseCases.Interfaces;

namespace InventoryApp.Domain.UseCases
{
    public class ProductUseCase : IProductUseCase
    {
        private readonly IProductRepository _repository;

        public ProductUseCase(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Product>> GetAll()
        {
            return await _repository.GetByCondition(product => product.Enabled && product.InInventory > 0);
        }

        public async Task<List<Product>> GetAllPaging(ProductParameters productParameters)
        {
            return await _repository.GetAllProductsPaging(productParameters);
        }

        public async Task<Product> GetById(int productId)
        {
            return await _repository.GetById(productId);
        }

        public async Task<Product> Add(Product product)
        {
            var productAdded = await _repository.Add(product);
            await _repository.UnitOfWork.SaveChangesAsync();
            return productAdded;
        }

        public async Task<Product> Update(Product product)
        {
            var productUpdated = _repository.Update(product);
            await _repository.UnitOfWork.SaveChangesAsync();
            return productUpdated;
        }

        public async Task<Product> SoftDelete(int productId)
        {
            var productSelected = await _repository.GetById(productId);
            productSelected.Enabled = false;
            _repository.Update(productSelected);
            await _repository.UnitOfWork.SaveChangesAsync();
            return productSelected;
        }
    }
}