﻿using Microsoft.AspNetCore.Authorization;
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
        private readonly IConfiguration _configuration;

        public FornecedorController(IFornecedorServices fornecedorServices, 
                                    IConfiguration configuration)
        {
            _fornecedorServices = fornecedorServices;
            _configuration = configuration;
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
                
                var url = _configuration.GetValue<string>("URL");

                var response = await _fornecedorServices.AdicionarFornecedorAsync(fornecedorRequest, url);

                if (response.Data.Executado)
                {
                    return Ok();
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

        /// <summary>
        /// Listar o cadastro de um fornecedor no portal pelo número de identificação do fornecedor
        /// </summary>
        /// <param name="id">Número de identificação do fornecedor</param>
        /// <returns></returns>
        [HttpGet]
        [Route("ListarCadastroFornecedor")]
        [AllowAnonymous]
        public async Task<IActionResult> ListarCadastroFornecedor(int id)
        {
            try
            {
                HttpContext.Response.ContentType = "application/json";

                var response = await _fornecedorServices.ListarDadosFornecedorAsync(id);

                if (response.Data.Executado)
                {
                    return Ok(response.Data.fornecedor);
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
