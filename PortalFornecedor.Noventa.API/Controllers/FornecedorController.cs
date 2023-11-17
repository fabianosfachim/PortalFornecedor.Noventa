using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> AdicionarFornecedorAsync(FornecedorRequest fornecedorRequest)
        {

            var response = await _fornecedorServices.AdicionarFornecedorAsync(fornecedorRequest);

            HttpContext.Response.ContentType = "application/json";

            return Ok(response);
        }

        /// <summary>
        /// Realizar a atualização do cadastro dos dados do fornecedor no portal
        /// </summary>
        /// <param name="fornecedorRequest">Objeto para cadastro dos dados do fornecedor no portal</param>
        /// <returns>Retornar se os dados do fornecedor foram atualizados ou não no portal</returns>
        [HttpPost]
        [Route("AtualizarCadastroFornecedor")]
        [AllowAnonymous]
        public async Task<IActionResult> AtualizarDadosFornecedorAsync(FornecedorRequest fornecedorRequest)
        {

            var response = await _fornecedorServices.AtualizarDadosFornecedorAsync(fornecedorRequest);

            HttpContext.Response.ContentType = "application/json";

            return Ok(response);
        }

        /// <summary>
        /// Realizar a exclusão do cadastro dos dados do fornecedor no portal
        /// </summary>
        /// <param name="id">Id do cadastro do fornecedor</param>
        /// <returns>Retornar se os dados do fornecedor foram atualizados ou não no portal</returns>
        [HttpPost]
        [Route("ExclusaoCadastroFornecedor")]
        [AllowAnonymous]
        public async Task<IActionResult> ExcluirDadosFornecedorAsync(int id)
        {

            var response = await _fornecedorServices.ExcluirDadosFornecedorAsync(id);

            HttpContext.Response.ContentType = "application/json";

            return Ok(response);
        }


        /// <summary>
        /// Listar o cadastro de todos os fornecedores no portal
        /// </summary>
        /// <returns>Retornar os dados cadastrais dos fornecedores</returns>
        [HttpGet]
        [Route("ListarCadastroFornecedor")]
        [AllowAnonymous]
        public async Task<IActionResult> ListarDadosFornecedorAsync()
        {

            var response = await _fornecedorServices.ListarDadosFornecedorAsync();

            HttpContext.Response.ContentType = "application/json";

            return Ok(response);
        }


        /// <summary>
        /// Listar o cadastro de um fornecedor no portal pelo número de identificação do fornecedor
        /// </summary>
        /// <param name="id">Número de identificação do fornecedor</param>
        /// <returns></returns>
        [HttpGet]
        [Route("DadosCadastroFornecedor")]
        [AllowAnonymous]
        public async Task<IActionResult> ListarDadosFornecedor(int id)
        {

            var response = await _fornecedorServices.ListarDadosFornecedorAsync(id);

            HttpContext.Response.ContentType = "application/json";

            return Ok(response);
        }
    }
}
