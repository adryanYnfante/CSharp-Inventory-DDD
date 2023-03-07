using InventoryApp.Api.Controllers.Base;
using InventoryApp.Domain.Entities;
using InventoryApp.Domain.UseCases.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InventoryApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : InventoryControllerBase
    {
        private readonly ICreateInvoiceUseCase _useCase;

        public InvoiceController(ICreateInvoiceUseCase useCase, ILogger<InvoiceController> logger)
            : base(logger)
        {
            _useCase = useCase;
        }

        [HttpPost]
        public async Task<IActionResult> CreateInvoice([FromBody] Invoice invoice)
        {
            return await HandleRequestCreateAsync(async () =>
            {
                return await _useCase.Add(invoice);
            });
        }

        [HttpPatch("cancel/{invoiceId}")]
        public async Task<IActionResult> CancelInvoice([FromRoute] int invoiceId)
        {
            return await HandleRequestAsync(async () =>
            {
                return await _useCase.Cancel(invoiceId);
            });
        }
    }
}