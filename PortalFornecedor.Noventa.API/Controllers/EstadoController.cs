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

            var response = await _estadoServices.ListarEstadoAsync();

            HttpContext.Response.ContentType = "application/json";

            return Ok(response);
        }

   
    }
}
