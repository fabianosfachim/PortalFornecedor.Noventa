using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PortalFornecedor.Noventa.Application.Services.Interfaces;
using PortalFornecedor.Noventa.Application.Services.Util;
using PortalFornecedor.Noventa.Application.Services.Wrappers;
using PortalFornecedor.Noventa.Data.Interfaces;
using PortalFornecedor.Noventa.Data.Repositories.Entities;
using PortalFornecedor.Noventa.Domain.Entities;
using PortalFornecedor.Noventa.Domain.Model;

namespace PortalFornecedor.Noventa.Application
{
    public class LoginServices : ILoginServices
    {
        private readonly ILoginRepository _loginRepository;
        private readonly ILogger<LoginServices> _logger;
        private readonly IFornecedorServices _fornecedorServices;

        public LoginServices(ILoginRepository loginRepository,
                             ILogger<LoginServices> logger,
                             IFornecedorServices fornecedorServices)
        {
            _loginRepository = loginRepository;
            _logger = logger;
            _fornecedorServices = fornecedorServices;
        }

        public async Task<Response<LoginResponse>> VerificarForcaSenhaAsync(LoginRequest loginRequest)
        {
            _logger.LogInformation("Iniciando o método   " +
                 $"{nameof(VerificarForcaSenhaAsync)}  " +
               "com os seguintes parâmetros: {loginRequest}", loginRequest);

            LoginResponse loginResponse = new LoginResponse();
            loginResponse.Executado = true;

            if (loginRequest.Password.Length < 6 || loginRequest.Password.Length > 20)
            {
                loginResponse.Executado = true;
                loginResponse.MensagemRetorno = "A password deve conter entre 6 e 20 caracteres.";
            }

            if (!loginRequest.Password.Any(c => char.IsDigit(c)))
            {
                loginResponse.Executado = false;
                loginResponse.MensagemRetorno = "A password deve pelo menos um número.";
            }

            if (!loginRequest.Password.Any(c => char.IsUpper(c)))
            {
                loginResponse.Executado = false;
                loginResponse.MensagemRetorno = "A password deve pelo menos um letra maíuscula.";
            }

            if (!loginRequest.Password.Any(c => char.IsLower(c)))
            {
                loginResponse.Executado = false;
                loginResponse.MensagemRetorno = "A password deve pelo menos um letra minúscula.";
            }

            if (!loginRequest.Password.Any(c => char.IsLetter(c) || char.IsDigit(c)))
            {
                loginResponse.Executado = false;
                loginResponse.MensagemRetorno = "A password deve pelo menos um caractere especial.";
            }

            if (loginResponse.Executado == true)
            {
                loginResponse.MensagemRetorno = "A password foi validada com sucesso.";
                loginResponse.Executado = true;
            }

            _logger.LogInformation("Finalizando o método   " +
                 $"{nameof(VerificarForcaSenhaAsync)}  " +
               "com os seguintes parâmetros: {loginRequest}", loginRequest);

            return new Response<LoginResponse>(loginResponse, $"Verificar Força Senha.");
        }

        public async Task<Response<LoginResponse>> CadastrarLoginSistemaAsync(LoginRequest loginRequest)
        {
            LoginResponse loginResponse = new LoginResponse();

                _logger.LogInformation("Iniciando o método   " +
                $"{nameof(CadastrarLoginSistemaAsync)}  " +
                "com os seguintes parâmetros: {loginRequest}", loginRequest);

                var dadosAcesso = _loginRepository.Get(x => x.Email == loginRequest.Email).FirstOrDefault();

                if (dadosAcesso == null)
                {
                    var login = DadosLogin(loginRequest);

                    await _loginRepository.AddAsync(login);

                    var idLogin = login.Id;

                    if (idLogin > 0)
                    {
                        login.Id = idLogin;
                        loginResponse.login = login;
                        loginResponse.Executado = true;
                        loginResponse.MensagemRetorno = "Login cadastrado com sucesso no banco de dados";
                    }
                }
                else
                {
                    loginResponse.Executado = false;
                    loginResponse.MensagemRetorno = "Já existe um login com este email cadastrado no banco de dados";
                }

                _logger.LogInformation("Finalizando o método   " +
               $"{nameof(CadastrarLoginSistemaAsync)}  " +
               "com os seguintes parâmetros: {loginRequest}", loginRequest);
            

            return new Response<LoginResponse>(loginResponse, $"Cadastro Login Usuário.");
        }

        public async Task<Response<LoginResponse>> AtivarCadastroLoginSistemaAsync(int idUsuario)
        {
            LoginResponse loginResponse = new LoginResponse();


            _logger.LogInformation("Iniciando o método   " +
             $"{nameof(AtivarCadastroLoginSistemaAsync)}  " +
           "com os seguintes parâmetros: {idUsuario}", idUsuario);

            var dadosAcessoUsuario = await _loginRepository.GetByIdAsync(idUsuario);

            if (dadosAcessoUsuario != null)
            {

                dadosAcessoUsuario.Ativo = true;
                dadosAcessoUsuario.DataAlteracaoCadastro = DateTime.Now;

                await _loginRepository.UpdateAsync(dadosAcessoUsuario);

                loginResponse.Executado = true;
                loginResponse.MensagemRetorno = "Login Ativado com Sucesso";

            }
            else
            {
                loginResponse.Executado = false;
                loginResponse.MensagemRetorno = "Tente realizar a atualização do cadastro novamente";
                return new Response<LoginResponse>(loginResponse, $"Atualização do Cadastro do Login Usuário.");
            }

            _logger.LogInformation("Finalizando o método   " +
            $"{nameof(AtivarCadastroLoginSistemaAsync)}  " +
            "com os seguintes parâmetros: {idUsuario}", idUsuario);



            return new Response<LoginResponse>(loginResponse, $"Atualização do Cadastro do Login Usuário.");
        }

        public async Task<Response<LoginResponse>> LoginSistemaAsync(LoginRequest loginRequest)
        {
            LoginResponse loginResponse = new LoginResponse();


            _logger.LogInformation("Iniciando o método   " +
                $"{nameof(LoginSistemaAsync)}  " +
            "com os seguintes parâmetros: {loginRequest}", loginRequest);

            loginRequest.Email = loginRequest.Email;
            loginRequest.Password = Utils.Criptografar(loginRequest.Password);

            var dadosAcesso = _loginRepository.Get(x => x.Email == loginRequest.Email
                                            && x.Password == loginRequest.Password
                                            && x.Ativo == true).FirstOrDefault();

            if (dadosAcesso != null)
            {
                Login dadosAcessoUsuario = await BuscarDadosUsuario(dadosAcesso.Id);

                dadosAcessoUsuario.DataUltimaSessaoAtivaUsuario = DateTime.Now;
                await _loginRepository.UpdateAsync(dadosAcessoUsuario);

                loginResponse.login = dadosAcessoUsuario;
                loginResponse.Executado = true;
                loginResponse.MensagemRetorno = "Login efetuado com sucesso";
            }
            else
            {
                loginResponse.Executado = false;
                loginResponse.MensagemRetorno = "Dados incorretos, tente novamente!";
            }


            _logger.LogInformation("Finalizando o método   " +
                $"{nameof(LoginSistemaAsync)}  " +
              "com os seguintes parâmetros: {loginRequest}", loginRequest);

            return new Response<LoginResponse>(loginResponse, $"Dados Acesso.");
        }


        public async Task<Response<LoginResponse>> RecuperarDadosAcessoAsync(string CnpjCpf)
        {
            LoginResponse loginResponse = new LoginResponse();


            _logger.LogInformation("Iniciando o método   " +
             $"{nameof(RecuperarDadosAcessoAsync)}  " +
             "com os seguintes parâmetros: {CnpjCpf}", CnpjCpf);

            CnpjCpf = CnpjCpf.Replace(".", "").Replace("-", "").Replace("/", "").Replace("-", ""); 

            var dadosAcesso = await _fornecedorServices.ListarDadosFornecedorAsync(CnpjCpf);

            if (dadosAcesso.Data != null && dadosAcesso.Data.fornecedor != null)
            {
                loginResponse.fornecedor = dadosAcesso.Data.fornecedor;
                loginResponse.Executado = true;
                loginResponse.MensagemRetorno = "Dados do Fornecedor Recuperadados com sucesso";
            }
            else
            {
                loginResponse.Executado = false;
                loginResponse.MensagemRetorno = "Não existem dados cadastrados no banco de dados!";
            }

            _logger.LogInformation("Finalizando o método   " +
            $"{nameof(RecuperarDadosAcessoAsync)}  " +
            "com os seguintes parâmetros: {CnpjCpf}", CnpjCpf);

            return new Response<LoginResponse>(loginResponse, $"Dados Acesso.");
        }

        public async Task<Response<LoginResponse>> AtualizarCadastroLoginSistemaAsync(LoginRequest loginRequest)
        {
            LoginResponse loginResponse = new LoginResponse();
      

            _logger.LogInformation("Iniciando o método   " +
              $"{nameof(AtualizarCadastroLoginSistemaAsync)}  " +
            "com os seguintes parâmetros: {loginRequest}", loginRequest);

            var senha = await VerificarForcaSenhaAsync(loginRequest);

            if(senha.Data != null & senha.Data?.Executado == true)
            {
                loginRequest.CnpjCpf = loginRequest.CnpjCpf.Replace(".", "").Replace("-", "").Replace("/", "").Replace("-", "");

                var dadosAcesso = await _fornecedorServices.ListarDadosFornecedorAsync(loginRequest.CnpjCpf);

                if (dadosAcesso.Data != null && dadosAcesso.Data.fornecedor != null)
                {
                    var login = await BuscarDadosUsuario(dadosAcesso.Data.fornecedor.Id);

                    login.Password = Utils.Criptografar(loginRequest.Password);
                    login.DataAlteracaoCadastro = DateTime.Now;
                    login.NomeUsuarioAlteracao = login.NomeUsuarioCadastro;

                    _loginRepository.Update(login);

                    loginResponse.Executado = true;
                    loginResponse.MensagemRetorno = "Alteração de password com sucesso";
                }
                else
                {
                    loginResponse.Executado = false;
                    loginResponse.MensagemRetorno = "Não foi possível alterar a password no banco de dados!";
                }
            }
            else
            {
                loginResponse.Executado = false;
                loginResponse.MensagemRetorno = "A password informada não atende os requisitos necessários!";
            }

            _logger.LogInformation("Finalizando o método   " +
           $"{nameof(AtualizarCadastroLoginSistemaAsync)}  " +
         "com os seguintes parâmetros: {loginRequest}", loginRequest);

            return new Response<LoginResponse>(loginResponse, $"Atualização do Cadastro do Login Usuário.");
        }

        #region RotinasAuxiliaresApi

        private async Task<Login> BuscarDadosUsuario(int id)
        {
            Login login = new Login();

            var dadosAcessoUsuario = await _loginRepository.GetByIdAsync(id);

            return dadosAcessoUsuario;
        }

        private Login DadosLogin(LoginRequest loginRequest)
        {
            Login login = new Login();

            var password = Utils.Criptografar(loginRequest.Password);


            login.Email = loginRequest.Email;
            login.Password = password;
            login.Nome = loginRequest.Nome;
            login.NomeUsuarioCadastro = loginRequest.Nome;
            login.DataCadastro = DateTime.Now;
            login.NomeUsuarioAlteracao = loginRequest.Nome;
            login.DataAlteracaoCadastro = DateTime.Now;
            login.Ativo = false;
            login.DataUltimaSessaoAtivaUsuario = DateTime.Now;

            return login;
        }

       

        #endregion

    }
}
