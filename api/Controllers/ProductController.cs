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

        public async Task<IActionResult> GetAll(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 5,
            [FromQuery] string? name = null
        )
        {
            if (page < 1)
                return BadRequest("A página inicial deve ser maior que 0.");

            if (pageSize < 1 || pageSize > 100)
                return BadRequest("O tamanho de listagem deve estar entre 1 e 100.");

            var response = await _ps.GetPagedAsync(page, pageSize, name);

            return Ok(response);
        }
    }
}