using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortalFornecedor.Noventa.Application.Services.Interfaces;
using PortalFornecedor.Noventa.Domain.Model;

namespace PortalFornecedor.Noventa.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CotacaoController : ControllerBase
    {
        private readonly ICotacaoServices _cotacaoServices;
        public CotacaoController(ICotacaoServices cotacaoServices)
        {
            _cotacaoServices = cotacaoServices;
        }

        /// <summary>
        /// Adicionar uma nova cotação para um fornecedor
        /// </summary>
        /// <param name="cotacaoRequest">Objeto contendo os dados para cadastro de uma nova cotação</param>
        /// <returns>Retornar se a cotaçao foi cadastrada no banco de dados</returns>
        [HttpPost]
        [Route("AdicionarCotacao")]
        [AllowAnonymous]
        public async Task<IActionResult> AdicionarCotacaoAsync(CotacaoRequest cotacaoRequest)
        {

            var response = await _cotacaoServices.AdicionarCotacaoAsync(cotacaoRequest);

            HttpContext.Response.ContentType = "application/json";

            return Ok(response);
        }

        /// <summary>
        /// Atualizar uma cotação para um fornecedor
        /// </summary>
        /// <param name="cotacaoRequest">Objeto contendo os dados para atualização do cadastro de uma cotação</param>
        /// <returns>Retornar se os dados da cotação foram atualizados</returns>
        [HttpPost]
        [Route("AtualizarCotacao")]
        [AllowAnonymous]
        public async Task<IActionResult> AtualizarCotacaoAsync(AtualizarCotacaoRequest cotacaoRequest)
        {

            var response = await _cotacaoServices.AtualizarCotacaoAsync(cotacaoRequest);

            HttpContext.Response.ContentType = "application/json";

            return Ok(response);
        }

        /// <summary>
        /// Atualizar o status cotação para um fornecedor
        /// </summary>
        /// <param name="cotacaoRequest">Objeto contendo os dados para atualização do cadastro de uma cotação</param>
        /// <returns>Retornar se os dados da cotação foram atualizados</returns>
        [HttpPost]
        [Route("AtualizarStatusCotacao")]
        [AllowAnonymous]
        public async Task<IActionResult> AtualizarStatusCotacaoAsync(int idCotacao, int idStatus)
        {

            var response = await _cotacaoServices.AtualizarStatusCotacaoAsync(idCotacao, idStatus);

            HttpContext.Response.ContentType = "application/json";

            return Ok(response);
        }

        /// <summary>
        /// Retornar os dados de uma cotação
        /// </summary>
        /// <param name="idCotacao">Identificador da cotação</param>
        /// <param name="cnpj">CNPJ Fornecedor</param>
        /// <returns>Retornaros dados de uma cotação</returns>
        [HttpPost]
        [Route("ListarCotacao")]
        [AllowAnonymous]
        public async Task<IActionResult> ListarCotacaoAsync(string idCotacao, string cnpj)
        {

            var response = await _cotacaoServices.ListarCotacaoAsync(idCotacao, cnpj);

            HttpContext.Response.ContentType = "application/json";

            return Ok(response);
        }

        /// <summary>
        /// Retornar os dados de uma cotação
        /// </summary>
        /// <param name="idCotacao">Identificador da cotação</param>
        /// <param name="cnpj">CNPJ Fornecedor</param>
        /// <returns>Retornaros dados de uma cotação</returns>
        [HttpPost]
        [Route("ListarCotacaoId")]
        [AllowAnonymous]
        public async Task<IActionResult> ListarCotacaoIdAsync(int Id)
        {

            var response = await _cotacaoServices.ListarCotacaoAsync(Id);

            HttpContext.Response.ContentType = "application/json";

            return Ok(response);
        }

        /// <summary>
        /// Retornar os dados de uma cotação
        /// </summary>
        /// <param name="cotacaoDetalheFiltroRequest">Filtro cotação fornecedor</param>
        /// <returns>Retornaros dados de uma cotação</returns>
        [HttpPost]
        [Route("ListarCotacaoFornecedor")]
        [AllowAnonymous]
        public async Task<IActionResult> ListarCotacaoAsync(CotacaoDetalheFiltroRequest cotacaoDetalheFiltroRequest)
        {

            var response = await _cotacaoServices.ListarCotacaoAsync(cotacaoDetalheFiltroRequest);

            HttpContext.Response.ContentType = "application/json";

            return Ok(response);
        }
    }
}
