using InventoryApp.Api.Controllers.Base;
using InventoryApp.Domain.Entities;
using InventoryApp.Domain.UseCases.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InventoryApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecordController : InventoryControllerBase
    {
        private readonly IInvoiceRecordUseCase _useCase;

        public RecordController(IInvoiceRecordUseCase useCase, ILogger<RecordController> logger)
            : base(logger)
        {
            _useCase = useCase;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllInvoices()
        {
            return await HandleRequestAsync(async () =>
            {
                return await _useCase.GetAll();
            });
        }

        [HttpGet("{invoiceId}")]
        public async Task<IActionResult> GetInvoiceById([FromRoute] int invoiceId)
        {
            return await HandleRequestAsync(async () =>
            {
                return await _useCase.GetById(invoiceId);
            });
        }

        [HttpGet("client/{clientId}")]
        public async Task<IActionResult> GetInvoicesByClienteId([FromRoute] string clientId)
        {
            return await HandleRequestAsync(async () =>
            {
                return await _useCase.FindByClientId(clientId);
            });
        }
    }
}