using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortalFornecedor.Noventa.Application.Services.Interfaces;
using PortalFornecedor.Noventa.Domain.DTO;
using PortalFornecedor.Noventa.Domain.Model;

namespace PortalFornecedor.Noventa.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AcessoController : ControllerBase
    {
        private readonly ILoginServices _loginServices;
        private readonly IConfiguration _configuration;

        public AcessoController(ILoginServices loginServices, IConfiguration configuration)
        {
            _loginServices = loginServices;
            _configuration = configuration;
        }


        /// <summary>
        /// Verificar se a senha para acesso na aplicaçao tem os requisitos de negócio definidos no projeto
        /// </summary>
        /// <param name="loginRequest">Objeto para receber os dados necessários para verificação da senha do fornecedor</param>
        /// <returns>Retorna se a senha pode ser utilizada para cadastro ou não no portal</returns>
        [HttpGet]
        [Route("VerificarSenha")]
        [AllowAnonymous]
        public async Task<IActionResult> VerificarForcaSenhaAsync(string parametro)
        {
            try
            {
                LoginRequest loginRequest = new LoginRequest();
                loginRequest.Password = parametro;

                var response = await _loginServices.VerificarForcaSenhaAsync(loginRequest);
                
                if (response.Data.Executado)
                {
                    return Ok();
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
        /// Realizar o Cadastro do login do Fornecedor no Portal
        /// </summary>
        /// <param name="loginRequest">Objeto para receber os dados necessários para cadastro do fornecedor</param>
        /// <returns>Retornar se o login do fornecedor foi cadastrado no portal</returns>
        [HttpPost]
        [Route("CadastroAcesso")]
        [AllowAnonymous]
        public async Task<IActionResult> CadastrarLoginSistemaAsync([FromBody] LoginRequest loginRequest)
        {
            HttpContext.Response.ContentType = "application/json";

            try
            {
                var response = await _loginServices.CadastrarLoginSistemaAsync(loginRequest);

                if (response.Data.Executado)
                {
                    return Ok(response.Data.login);
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
        /// Ativar o cadastro de acesso de um fornecedor na aplicação
        /// </summary>
        /// <param name="idLogin">Identificador do login do cadastro do fornecedor</param>
        /// <returns>Retornar a desativação do cadastro do fornecedor</returns>
        [HttpPost]
        [Route("AtivarAcesso")]
        [AllowAnonymous]
        public async Task<IActionResult> AtivarCadastroLoginSistemaAsync(Guid idUsuario)
        {
            try
            {
                var response = await _loginServices.AtivarCadastroLoginSistemaAsync(idUsuario);

                if (response.Data.Executado)
                {
                    return Ok();
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
        /// Verificar se os dados do fornecedor são válidos para fazer login na aplicação
        /// </summary>
        /// <param name="loginRequest"></param>
        /// <returns>Retorna se o fornecedor tem acesso ou não ao sistema</returns>
        [HttpPost]
        [Route("AutenticarAcesso")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginSistemaAsync([FromBody] LoginDto login)
        {
            HttpContext.Response.ContentType = "application/json";

            try
            {
                LoginRequest loginRequest = new LoginRequest();
                loginRequest.Email = login.Email;
                loginRequest.Password = login.Password;

                var response = await _loginServices.LoginSistemaAsync(loginRequest);

                if (response.Data.Executado)
                {
                    return Ok(response.Data.login);
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
        /// Recuperar a senha de acesso do fornecedor ao portal caso o mesmo tenha esquecido
        /// </summary>
        /// <param name="CnpjCpf">Documento do fornecedor para recuperação da senha</param>
        /// <returns>Retornar a senha de acesso do fornecedor no portal</returns>
        [HttpPost]
        [Route("RecuperarAcesso")]
        [AllowAnonymous]
        public async Task<IActionResult> RecuperarDadosAcessoAsync(string CnpjCpf)
        {
            try
            {
                var response = await _loginServices.RecuperarDadosAcessoAsync(CnpjCpf);

                if (response.Data.Executado)
                {
                    return Ok(new
                    {
                        login = response.Data.login,
                        fornecedor = response.Data.fornecedor
                    });
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
        /// Atualizar o cadastro da senha do usuário
        /// </summary>
        /// <param name="email">Email</param>
        /// <param name="password">password</param>
        /// <returns>Atualizar o cadastro da senha do fornecedor no portal</returns>
        [HttpPost]
        [Route("AtualizarPassword")]
        [AllowAnonymous]
        public async Task<IActionResult> AtualizarCadastroLoginSistemaAsync(string email, string password)
        {
            try
            {
                var response = await _loginServices.AtualizarCadastroLoginSistemaAsync(Guid.Parse(email), password);

                if (response.Data.Executado)
                {
                    return Ok();
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
        /// Confirmar a recuperação de senha de acesso do fornecedor ao portal caso o mesmo tenha esquecido
        /// </summary>
        /// <param name="Email">Email do fornecedor para recuperação da senha</param>
        /// <returns>Retornar a senha de acesso do fornecedor no portal</returns>
        [HttpPost]
        [Route("ConfirmarRecuperacaoDadosAcesso")]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmarRecuperacaoDadosAcessoAsync(string Email)
        {
            try
            {
                var url = _configuration.GetValue<string>("URLSENHA");

                var response = await _loginServices.ConfirmarRecuperacaoDadosAcessoAsync(Email, url);

                if (response.Data.Executado)
                {
                    return Ok();
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
