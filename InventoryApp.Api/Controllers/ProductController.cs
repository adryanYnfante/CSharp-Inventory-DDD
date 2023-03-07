using InventoryApp.Api.Controllers.Base;
using InventoryApp.Domain.Entities;
using InventoryApp.Domain.UseCases.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InventoryApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : InventoryControllerBase
    {
        private readonly IProductUseCase _useCase;

        public ProductController(IProductUseCase useCase, ILogger<ProductController> logger)
            : base(logger)
        {
            _useCase = useCase;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            return await HandleRequestAsync(async () =>
            {
                return await _useCase.GetAll();
            });
        }

        [HttpGet("paging")]
        public async Task<IActionResult> GetAllProducts([FromQuery] ProductParameters parameters)
        {
            return await HandleRequestAsync(async () =>
            {
                return await _useCase.GetAllPaging(parameters);
            });
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> GetProductById([FromRoute] int productId)
        {
            return await HandleRequestAsync(async () =>
            {
                return await _useCase.GetById(productId);
            });
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] Product product)
        {
            return await HandleRequestCreateAsync(async () =>
            {
                return await _useCase.Add(product);
            });
        }

        [HttpPut("{productId}")]
        public async Task<IActionResult> UpdateProduct([FromRoute] int productId, [FromBody] Product product)
        {
            return await HandleRequestAsync(async () =>
            {
                product.Id = productId;
                return await _useCase.Update(product);
            });
        }

        [HttpPatch("disable/{productId}")]
        public async Task<IActionResult> DisableProductById([FromRoute] int productId)
        {
            return await HandleRequestAsync(async () =>
            {
                return await _useCase.SoftDelete(productId);
            });
        }
    }
}