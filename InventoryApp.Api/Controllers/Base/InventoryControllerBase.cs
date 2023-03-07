using InventoryApp.Domain.Entities;
using InventoryApp.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace InventoryApp.Api.Controllers.Base
{
    public class InventoryControllerBase : ControllerBase
    {
        private readonly ILogger<InventoryControllerBase> _logger;

        public InventoryControllerBase(ILogger<InventoryControllerBase> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> HandleRequestAsync<TResult>(Func<Task<TResult>> requestHandler)
        {
            try
            {
                TResult result = await requestHandler();

                if (result == null)
                {
                    return NotFound();
                }

                return Ok(result);
            }
            catch (BusinessException exception)
            {
                _logger.LogWarning("Invalid request: {exception}", exception.Message);
                return BadRequest(exception.Message);
            }
            catch (Exception exception)
            {
                _logger.LogError("Unhandled error: {exception}", exception);
                throw;
            }
        }

        public async Task<IActionResult> HandleRequestCreateAsync<TResult>(Func<Task<TResult>> requestHandler)
        {
            try
            {
                TResult result = await requestHandler();

                return Created(string.Empty, result);
            }
            catch (BusinessException exception)
            {
                _logger.LogWarning("Invalid creation on: {exception}", exception.Message);
                return BadRequest(exception.Message);
            }
            catch (Exception exception)
            {
                _logger.LogError("Unhandled error on : {exception}", exception);
                throw;
            }
        }
    }
}