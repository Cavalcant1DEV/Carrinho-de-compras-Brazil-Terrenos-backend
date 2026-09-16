using api.DTOs.Cupom;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/cupom")]
    [ApiController]
    public class Cupom : ControllerBase
    {
        private readonly CupomService _cs;
        public Cupom(CupomService cs)
        {
            _cs = cs;
        }

        [HttpGet]

        public async Task<ActionResult<CupomResponse>> GetAll(
            [FromQuery] string code
        )
        {
            if (code.Length <= 0)
                return BadRequest("Informe um código de cupom válido.");

            var response = await _cs.GetCupomAsync(code);

            if (response == null)
                return NotFound();

            return Ok(response);
        }
    }
}