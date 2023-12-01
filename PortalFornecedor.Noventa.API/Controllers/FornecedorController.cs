using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PortalFornecedor.Noventa.Application;
using PortalFornecedor.Noventa.Application.Services.Interfaces;
using PortalFornecedor.Noventa.Domain.Model;

namespace PortalFornecedor.Noventa.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FornecedorController : ControllerBase
    {
        private readonly IFornecedorServices _fornecedorServices;

        public FornecedorController(IFornecedorServices fornecedorServices)
        {
            _fornecedorServices = fornecedorServices;
        }

        /// <summary>
        /// Realizar o cadastro dos dados do fornecedor no portal
        /// </summary>
        /// <param name="fornecedorRequest">Objeto para cadastro dos dados do fornecedor no portal</param>
        /// <returns>Retornar se os dados do fornecedor foram cadastrados ou não no portal</returns>
        [HttpPost]
        [Route("AdicionarCadastroFornecedor")]
        [AllowAnonymous]
        public async Task<IActionResult> AdicionarFornecedorAsync([FromBody] FornecedorRequest fornecedorRequest)
        {
            try
            {
                HttpContext.Response.ContentType = "application/json";

                var response = await _fornecedorServices.AdicionarFornecedorAsync(fornecedorRequest);

                if (response.Data.Executado)
                {
                    return Ok(response.Data.MensagemRetorno);
                }
                else
                {
                    return BadRequest(response.Data.MensagemRetorno);
                }
                
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
