using InventoryApp.Domain.Entities;

namespace InventoryApp.Domain.UseCases.Interfaces
{
    public interface IProductUseCase
    {
        Task<List<Product>> GetAll();

        Task<List<Product>> GetAllPaging(ProductParameters productParameters);

        Task<Product> GetById(int productId);

        Task<Product> Add(Product product);

        Task<Product> Update(Product product);

        Task<Product> SoftDelete(int productId);
    }
}