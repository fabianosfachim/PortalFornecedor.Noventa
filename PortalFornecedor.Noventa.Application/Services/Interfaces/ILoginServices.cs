using PortalFornecedor.Noventa.Application.Services.Wrappers;
using PortalFornecedor.Noventa.Domain.Entities;
using PortalFornecedor.Noventa.Domain.Model;


namespace PortalFornecedor.Noventa.Application.Services.Interfaces
{
    public interface ILoginServices
    {
        /// <summary>
        /// Realizar o Cadastro do login do Fornecedor no Portal
        /// </summary>
        /// <param name="loginRequest">Objeto para receber os dados necessários para cadastro do fornecedor</param>
        /// <returns>Retornar se o login do fornecedor foi cadastrado no portal</returns>
        Task<Response<LoginResponse>> CadastrarLoginSistemaAsync(LoginRequest loginRequest);

        /// <summary>
        /// Atualizar os dados do Cadastro do login do Fornecedor no Portal
        /// </summary>
        /// <param name="loginRequest">Objeto para receber os dados necessários para atualização do cadastro do fornecedor</param>
        /// <returns>Retornar se o login do fornecedor foi atualizado o cadastro no portal</returns>
        Task<Response<LoginResponse>> AtualizarCadastroLoginSistemaAsync(LoginRequest loginRequest);

        /// <summary>
        /// Verificar se os dados do fornecedor são válidos para fazer login na aplicação
        /// </summary>
        /// <param name="loginRequest"></param>
        /// <returns>Retorna se o fornecedor tem acesso ou não ao sistema</returns>
        Task<Response<LoginResponse>> LoginSistemaAsync(LoginRequest loginRequest);

        /// <summary>
        /// Verificar se a senha para acesso na aplicaçao tem os requisitos de negócio definidos no projeto
        /// </summary>
        /// <param name="loginRequest">Objeto para receber os dados necessários para verificação da senha do fornecedor</param>
        /// <returns>Retorna se a senha pode ser utilizada para cadastro ou não no portal</returns>
        Task<Response<LoginResponse>> VerificarForcaSenhaAsync(LoginRequest loginRequest);

        /// <summary>
        /// Desativar o cadastro de acesso de um fornecedor na aplicação
        /// </summary>
        /// <param name="idLogin">Identificador do Login do Fornecedor</param>
        /// <returns>Retornar a desativação do cadastro do fornecedor</returns>
        Task<Response<LoginResponse>> DesativarCadastroLoginSistemaAsync(int idLogin);


        /// <summary>
        /// Recuperar a senha de acesso do fornecedor ao portal caso o mesmo tenha esquecido
        /// </summary>
        /// <param name="email">Email do fornecedor para recuperação da senha</param>
        /// <returns>Retornar a senha de acesso do fornecedor no portal</returns>
        Task<Response<LoginResponse>> RecuperarDadosAcessoAsync(string email);

        /// <summary>
        /// Listar os dados do login do fornecedor pelo identificador do mesmo
        /// </summary>
        /// <param name="IdFornecedor">Identificador do fornecedor</param>
        /// <returns>Retornar os dados do login do fornecedor</returns>
        Task<Response<Login>> ListarDadosLoginAsync(int IdFornecedor);
    }
}
