using api.DTOs.Cupom;
using api.DTOs.Error;
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
        [ProducesResponseType(typeof(CupomResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CupomResponse>> GetAll(
            [FromQuery] string code
        )
        {
            if (code.Length <= 0)
                return BadRequest(new ErrorResponse { message = "Informe um código de cupom válido." });
            try
            {
                var response = await _cs.GetCupomAsync(code);

                if (response == null)
                    return NotFound(new ErrorResponse
                    {
                        message = "Cupom não encontrado."
                    });

                return Ok(response);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new ErrorResponse
                {
                    message = ex.Message
                });
            }
        }
    }
}