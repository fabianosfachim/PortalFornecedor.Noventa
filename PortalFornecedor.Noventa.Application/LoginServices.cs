using Microsoft.Extensions.Logging;
using PortalFornecedor.Noventa.Application.Services.Interfaces;
using PortalFornecedor.Noventa.Application.Services.Util;
using PortalFornecedor.Noventa.Application.Services.Wrappers;
using PortalFornecedor.Noventa.Data.Interfaces;
using PortalFornecedor.Noventa.Domain.Entities;
using PortalFornecedor.Noventa.Domain.Model;

namespace PortalFornecedor.Noventa.Application
{
    public class LoginServices : ILoginServices
    {
        private readonly ILoginRepository _loginRepository;
        private readonly ILogger<LoginServices> _logger;

        public LoginServices(ILoginRepository loginRepository,
                             ILogger<LoginServices> logger)
        {
            _loginRepository = loginRepository;
            _logger = logger;
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

        public async Task<Response<LoginResponse>> AtualizarCadastroLoginSistemaAsync(LoginRequest loginRequest)
        {
            LoginResponse loginResponse = new LoginResponse();

            var password = Utils.Criptografar(loginRequest.Password);

                _logger.LogInformation("Iniciando o método   " +
                  $"{nameof(AtualizarCadastroLoginSistemaAsync)}  " +
                "com os seguintes parâmetros: {loginRequest}", loginRequest);

                var dadosAcessoUsuario = await _loginRepository.GetByIdAsync(loginRequest.Id.Value);

                if (dadosAcessoUsuario != null)
                {
                 
                    dadosAcessoUsuario.Password = password;
                    dadosAcessoUsuario.DataAlteracaoCadastro = DateTime.Now;

                    await _loginRepository.UpdateAsync(dadosAcessoUsuario);

                    loginResponse.Executado = true;
                    loginResponse.MensagemRetorno = "Senha alterada com sucesso com sucesso no banco de dados";

                }
                else
                {
                    loginResponse.Executado = false;
                    loginResponse.MensagemRetorno = "Tente realizar o cadastro novamente";
                    return new Response<LoginResponse>(loginResponse, $"Atualização do Cadastro do Login Usuário.");
                }

                _logger.LogInformation("Finalizando o método   " +
                 $"{nameof(AtualizarCadastroLoginSistemaAsync)}  " +
                 "com os seguintes parâmetros: {loginRequest}", loginRequest);
            
           

            return new Response<LoginResponse>(loginResponse, $"Atualização do Cadastro do Login Usuário.");
        }

        public async Task<Response<LoginResponse>> DesativarCadastroLoginSistemaAsync(int idLogin)
        {
            LoginResponse loginResponse = new LoginResponse();

           
                _logger.LogInformation("Iniciando o método   " +
                 $"{nameof(DesativarCadastroLoginSistemaAsync)}  " +
               "com os seguintes parâmetros: {idLogin}", idLogin);

                var dadosAcessoUsuario = await _loginRepository.GetByIdAsync(idLogin);

                if (dadosAcessoUsuario != null)
                {

                    dadosAcessoUsuario.Ativo = false;
                    dadosAcessoUsuario.DataAlteracaoCadastro = DateTime.Now;

                    await _loginRepository.UpdateAsync(dadosAcessoUsuario);

                    loginResponse.Executado = true;
                    loginResponse.MensagemRetorno = "Cadastro de login desativado com sucesso";

                }
                else
                {
                    loginResponse.Executado = false;
                    loginResponse.MensagemRetorno = "Tente realizar a atualização do cadastro novamente";
                    return new Response<LoginResponse>(loginResponse, $"Atualização do Cadastro do Login Usuário.");
                }

                _logger.LogInformation("Finalizando o método   " +
                $"{nameof(DesativarCadastroLoginSistemaAsync)}  " +
                "com os seguintes parâmetros: {idLogin}", idLogin);



            return new Response<LoginResponse>(loginResponse, $"Atualização do Cadastro do Login Usuário.");
        }

        public async Task<Response<LoginResponse>> VerificarForcaSenhaAsync(LoginRequest loginRequest)
        {
            _logger.LogInformation("Iniciando o método   " +
                 $"{nameof(VerificarForcaSenhaAsync)}  " +
               "com os seguintes parâmetros: {loginRequest}", loginRequest);

            LoginResponse loginResponse = new LoginResponse();
            loginResponse.Executado = true;

            if (loginRequest.Password.Length < 6 || loginRequest.Password.Length > 12)
            {
                loginResponse.Executado = true;
                loginResponse.MensagemRetorno = "A password deve conter entre 6 e 12 caracteres.";
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

        public async Task<Response<LoginResponse>> RecuperarDadosAcessoAsync(string email)
        {
            LoginResponse loginResponse = new LoginResponse();

            
                _logger.LogInformation("Iniciando o método   " +
                 $"{nameof(RecuperarDadosAcessoAsync)}  " +
                 "com os seguintes parâmetros: {email}", email);

                var dadosAcesso = _loginRepository.Get(x => x.Email == email
                                                    && x.Ativo == true).FirstOrDefault();

                if (dadosAcesso != null)
                {
                    Login dadosAcessoUsuario = await BuscarDadosUsuario(dadosAcesso.Id);

                    loginResponse.Executado = true;
                    loginResponse.MensagemRetorno = Utils.Descriptografar(dadosAcesso.Password);
                }
                else
                {
                    loginResponse.Executado = false;
                    loginResponse.MensagemRetorno = "Dados incorretos, tente novamente!";
                }

                _logger.LogInformation("Finalizando o método   " +
                $"{nameof(RecuperarDadosAcessoAsync)}  " +
                "com os seguintes parâmetros: {email}", email);

            return new Response<LoginResponse>(loginResponse, $"Dados Acesso.");
        }

        public async Task<Response<Login>> ListarDadosLoginAsync(int IdFornecedor)
        {
            Login login = new Login();

            _logger.LogInformation("Iniciando o método   " +
             $"{nameof(ListarDadosLoginAsync)}  " +
             "com os seguintes parâmetros: {IdFornecedor}", IdFornecedor);

            var dadosAcesso = _loginRepository.Get(x => x.Id  == IdFornecedor).FirstOrDefault();

            login.Id = dadosAcesso.Id;
           // login.Cnpj = dadosAcesso.Cnpj;
            login.Nome = dadosAcesso.Nome;

            _logger.LogInformation("Finalizando o método   " +
             $"{nameof(ListarDadosLoginAsync)}  " +
             "com os seguintes parâmetros: {IdFornecedor}", IdFornecedor);

            return new Response<Login>(login, $"Dados Login Acesso.");
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
            login.Ativo = true;
            login.DataUltimaSessaoAtivaUsuario = DateTime.Now;

            return login;
        }

       
        #endregion

    }
}
