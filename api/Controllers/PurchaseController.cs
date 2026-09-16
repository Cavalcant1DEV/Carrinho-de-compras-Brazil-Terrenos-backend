using api.DTOs.Common;
using api.DTOs.Error;
using api.DTOs.Purchase;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers

{
    [Route("api/purchase")]
    public class PurchaseController : ControllerBase
    {
        private readonly PurchaseService _ps;
        public PurchaseController(PurchaseService ps)
        {
            _ps = ps;
        }

        [HttpGet]
        [ProducesResponseType(typeof(PagedResponse<PurchaseResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PagedResponse<PurchaseResponse>>> GetAll(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 5,
            [FromQuery] DateTime? startDate = null,
            [FromQuery] DateTime? endDate = null
        )
        {
            if (page < 1)
                return BadRequest(
                    new ErrorResponse
                    {
                        message = "A página inicial deve ser maior que 0."
                    });

            if (pageSize < 1 || pageSize > 100)
                return BadRequest(
                    new ErrorResponse
                    {
                        message = "O tamanho de listagem deve estar entre 1 e 100."
                    });

            var response = await _ps.GetPagedAsync(page, pageSize, startDate, endDate);

            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(typeof(CreatePurchaseResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CreatePurchaseResponse>> Submit(
            [FromBody] CreatePurchaseRequest request
        )
        {
            try
            {
                var purchase = await _ps.CreatePurchase(request);
                return CreatedAtAction(
                    nameof(GetById),
                    new { id = purchase.Id },
                    purchase
                );
            }
            catch (ArgumentException ex)
            {
                return BadRequest(
                    new ErrorResponse
                    {
                        message = ex.Message
                    });
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(DetailedPurchaseResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<DetailedPurchaseResponse>> GetById(int id)
        {
            var purchase = await _ps.GetById(id);

            if (purchase == null)
                return NotFound(
                    new ErrorResponse
                    {
                        message = "Compra não encontrada."
                    });

            return Ok(purchase);
        }
    }
}