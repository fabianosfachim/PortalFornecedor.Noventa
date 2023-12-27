using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortalFornecedor.Noventa.Application.Services.Interfaces;

namespace PortalFornecedor.Noventa.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadoController : ControllerBase
    {
        private readonly IEstadoServices _estadoServices;
        public EstadoController(IEstadoServices estadoServices)
        {
            _estadoServices = estadoServices;
        }

        /// <summary>
        /// Listar os estados cadastrados no portal
        /// </summary>
        /// <returns>Listar os estados cadastrados no portal</returns>
        [HttpGet]
        [Route("ListarEstado")]
        [AllowAnonymous]
        public async Task<IActionResult> ListarEstadoAsync()
        {
            HttpContext.Response.ContentType = "application/json";

            try
            {
                var response = await _estadoServices.ListarEstadoAsync();

                if (response.Data.Executado)
                {
                    return Ok(response.Data.Estados);
                } else
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
