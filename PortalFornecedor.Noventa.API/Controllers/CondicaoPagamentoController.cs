using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortalFornecedor.Noventa.Application;
using PortalFornecedor.Noventa.Application.Services.Interfaces;


namespace PortalFornecedor.Noventa.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CondicaoPagamentoController : ControllerBase
    {
        private readonly ICondicaoPagamentoServices _condicaoPagamentoServices;

        public CondicaoPagamentoController(ICondicaoPagamentoServices condicaoPagamentoServices)
        {
            _condicaoPagamentoServices = condicaoPagamentoServices;
        }

        /// <summary>
        /// Listar o identificador das condições de pagamento da cotação
        /// </summary>
        /// <param name="IdCotacao">Identificador da cotação</param>
        [HttpGet]
        [Route("ListarIdCotacaoCondicaoPagamentoCotacao")]
        [AllowAnonymous]
        public async Task<IActionResult> ListarIdCotacaoCondicaoPagamentoAsync(string IdCotacao)
        {
            try
            {
                HttpContext.Response.ContentType = "application/json";


                var response = await _condicaoPagamentoServices.ListarIdCotacaoCondicaoPagamentoAsync(IdCotacao);

                if (response.Data.Executado)
                {
                    return Ok(response.Data.Pagamentos);
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
