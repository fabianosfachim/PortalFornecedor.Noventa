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
        public async Task<IActionResult> AdicionarFornecedorAsync(FornecedorRequest fornecedorRequest)
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

        /// <summary>
        /// Realizar a atualização do cadastro dos dados do fornecedor no portal
        /// </summary>
        /// <param name="fornecedorRequest">Objeto para cadastro dos dados do fornecedor no portal</param>
        /// <returns>Retornar se os dados do fornecedor foram atualizados ou não no portal</returns>
        [HttpPut]
        [Route("AtualizarCadastroFornecedor")]
        [AllowAnonymous]
        public async Task<IActionResult> AtualizarDadosFornecedorAsync(FornecedorRequest fornecedorRequest)
        {
            HttpContext.Response.ContentType = "application/json";

            try
            {
                var response = await _fornecedorServices.AtualizarDadosFornecedorAsync(fornecedorRequest);

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

        /// <summary>
        /// Realizar a exclusão do cadastro dos dados do fornecedor no portal
        /// </summary>
        /// <param name="id">Id do cadastro do fornecedor</param>
        /// <returns>Retornar se os dados do fornecedor foram atualizados ou não no portal</returns>
        [HttpDelete]
        [Route("ExclusaoCadastroFornecedor")]
        [AllowAnonymous]
        public async Task<IActionResult> ExcluirDadosFornecedorAsync(int id)
        {
            HttpContext.Response.ContentType = "application/json";

            try
            {
                var response = await _fornecedorServices.ExcluirDadosFornecedorAsync(id);

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


        /// <summary>
        /// Listar o cadastro de todos os fornecedores no portal
        /// </summary>
        /// <returns>Retornar os dados cadastrais dos fornecedores</returns>
        [HttpGet]
        [Route("ListarCadastroFornecedor")]
        [AllowAnonymous]
        public async Task<IActionResult> ListarDadosFornecedorAsync()
        {

            HttpContext.Response.ContentType = "application/json";

            try
            {
                var response = await _fornecedorServices.ListarDadosFornecedorAsync();

                if (response.Data.Executado)
                {
                    return Ok(response.Data.fornecedor);
                }
                else
                {
                    return BadRequest(response.Data.MensagemRetorno);
                }
                
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            
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
            HttpContext.Response.ContentType = "application/json";

            try
            {
                var response = await _fornecedorServices.ListarDadosFornecedorAsync(id);

                if (response.Data.Executado)
                {
                    return Ok(response.Data.fornecedor);
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
