using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortalFornecedor.Noventa.Application;
using PortalFornecedor.Noventa.Application.Services.Interfaces;
using PortalFornecedor.Noventa.Domain.Model;

namespace PortalFornecedor.Noventa.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AcessoController : ControllerBase
    {
        private readonly ILoginServices _loginServices;
        public AcessoController(ILoginServices loginServices)
        {
            _loginServices = loginServices;
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

            LoginRequest loginRequest = new LoginRequest();
            loginRequest.Password = parametro;

            var response = await _loginServices.VerificarForcaSenhaAsync(loginRequest);

            HttpContext.Response.ContentType = "application/json";

            return Ok(response);
        }

        /// <summary>
        /// Realizar o Cadastro do login do Fornecedor no Portal
        /// </summary>
        /// <param name="loginRequest">Objeto para receber os dados necessários para cadastro do fornecedor</param>
        /// <returns>Retornar se o login do fornecedor foi cadastrado no portal</returns>
        [HttpPost]
        [Route("CadastroAcesso")]
        [AllowAnonymous]
        public async Task<IActionResult> CadastrarLoginSistemaAsync(LoginRequest loginRequest)
        {

            var response = await _loginServices.CadastrarLoginSistemaAsync(loginRequest);

            HttpContext.Response.ContentType = "application/json";

            return Ok(response);
        }

        /// <summary>
        /// Atualizar os dados do Cadastro do login do Fornecedor no Portal
        /// </summary>
        /// <param name="loginRequest">Objeto para receber os dados necessários para atualização do cadastro do fornecedor</param>
        /// <returns>Retornar se o login do fornecedor foi atualizado o cadastro no portal</returns>
        [HttpPost]
        [Route("AlteracaoSenha")]
        [AllowAnonymous]
        public async Task<IActionResult> AtualizarCadastroLoginSistemaAsync(LoginRequest loginRequest)
        {

            var response = await _loginServices.AtualizarCadastroLoginSistemaAsync(loginRequest);

            HttpContext.Response.ContentType = "application/json";

            return Ok(response);
        }

        /// <summary>
        /// Desativar o cadastro de acesso de um fornecedor na aplicação
        /// </summary>
        /// <param name="idLogin">Identificador do login do cadastro do fornecedor</param>
        /// <returns>Retornar a desativação do cadastro do fornecedor</returns>
        [HttpPost]
        [Route("DesativarAcesso")]
        [AllowAnonymous]
        public async Task<IActionResult> DesativarCadastroLoginSistemaAsync(int idLogin)
        {

            var response = await _loginServices.DesativarCadastroLoginSistemaAsync(idLogin);

            HttpContext.Response.ContentType = "application/json";

            return Ok(response);
        }


        /// <summary>
        /// Verificar se os dados do fornecedor são válidos para fazer login na aplicação
        /// </summary>
        /// <param name="loginRequest"></param>
        /// <returns>Retorna se o fornecedor tem acesso ou não ao sistema</returns>
        [HttpGet]
        [Route("AutenticarAcesso")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginSistemaAsync(string email, string password)
        {
            LoginRequest loginRequest = new LoginRequest();
            loginRequest.Email = email;
            loginRequest.Password = password;

            var response = await _loginServices.LoginSistemaAsync(loginRequest);

            HttpContext.Response.ContentType = "application/json";

            return Ok(response);
        }


        /// <summary>
        /// Recuperar a senha de acesso do fornecedor ao portal caso o mesmo tenha esquecido
        /// </summary>
        /// <param name="email">Email do fornecedor para recuperação da senha</param>
        /// <returns>Retornar a senha de acesso do fornecedor no portal</returns>
        [HttpGet]
        [Route("RecuperarAcesso")]
        [AllowAnonymous]
        public async Task<IActionResult> RecuperarDadosAcessoAsync(string email)
        {

            var response = await _loginServices.RecuperarDadosAcessoAsync(email);

            HttpContext.Response.ContentType = "application/json";

            return Ok(response);
        }

    }
}
