using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortalFornecedor.Noventa.Application.Services.Interfaces;


namespace PortalFornecedor.Noventa.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FreteController : ControllerBase
    {
        private readonly IfreteServices _ifreteServices;

        public FreteController(IfreteServices ifreteServices)
        {
            _ifreteServices = ifreteServices;
        }

        /// <summary>
        /// Listar o identificador dos frete da cotação
        /// </summary>
        /// <param name="IdCotacao">Identificador da cotação</param>
        [HttpGet]
        [Route("ListarFreteCotacao")]
        [AllowAnonymous]
        public async Task<IActionResult> ListarFreteCotacaoAsync(string IdCotacao)
        {
            try
            {
                HttpContext.Response.ContentType = "application/json";
         

                var response = await _ifreteServices.ListarIdFreteAsync(IdCotacao);

                if (response.Data.Executado)
                {
                    return Ok(response.Data.Frete);
                }
                else
                {
                    return BadRequest( new { msg = response.Data.MensagemRetorno });
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { msg = ex.Message });
            }
        }


    }
}
