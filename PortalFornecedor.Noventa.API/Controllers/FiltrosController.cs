using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortalFornecedor.Noventa.Application;
using PortalFornecedor.Noventa.Application.Services.Interfaces;
using PortalFornecedor.Noventa.Domain.Model;

namespace PortalFornecedor.Noventa.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FiltrosController : ControllerBase
    {
        private readonly IFiltrosServices _filtrosServices;
        public FiltrosController(IFiltrosServices filtrosServices)
        {
            _filtrosServices = filtrosServices;
        }

        /// <summary>
        /// Listar o status das cotações
        /// </summary>
        /// <returns>Retornar o status da cotaçao</returns>
        [HttpGet]
        [Route("ListarStatus")]
        [AllowAnonymous]
        public async Task<IActionResult> ListarStatusAsync()
        {

            var response = await _filtrosServices.ListarStatusAsync();

            HttpContext.Response.ContentType = "application/json";

            return Ok(response);
        }

        /// <summary>
        /// Listar o motivo das cotações
        /// </summary>
        /// <returns>Retornar o motivo da cotaçao</returns>
        [HttpGet]
        [Route("ListarMotivo")]
        [AllowAnonymous]
        public async Task<IActionResult> ListarMotivoAsync()
        {

            var response = await _filtrosServices.ListarMotivoAsync();

            HttpContext.Response.ContentType = "application/json";

            return Ok(response);
        }

        /// <summary>
        /// Listar os dados do DashBoard
        /// </summary>
        /// <returns>Retornar o motivo da cotaçao</returns>
        [HttpGet]
        [Route("ListarDadosDashBoard")]
        [AllowAnonymous]
        public async Task<IActionResult> ListarDadosDashBoardAsync(int idFornecedor, int idData, int peddingPage, int currentPage)
        {

            var response = await _filtrosServices.ListarDadosDashBoardAsync(idFornecedor, idData, peddingPage, currentPage);

            HttpContext.Response.ContentType = "application/json";

            return Ok(response);
        }


    }
}
