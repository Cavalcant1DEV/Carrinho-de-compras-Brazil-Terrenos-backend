using api.DTOs.Common;
using api.DTOs.Error;
using api.DTOs.Product;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _ps;
        public ProductController(ProductService ps)
        {
            _ps = ps;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PagedResponse<ProductResponse>>> GetAll(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 6,
            [FromQuery] string? name = null
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

            var response = await _ps.GetPagedAsync(page, pageSize, name);

            return Ok(response);
        }
    }
}