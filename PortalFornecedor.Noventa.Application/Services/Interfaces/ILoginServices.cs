using PortalFornecedor.Noventa.Application.Services.Wrappers;
using PortalFornecedor.Noventa.Domain.Entities;
using PortalFornecedor.Noventa.Domain.Model;


namespace PortalFornecedor.Noventa.Application.Services.Interfaces
{
    public interface ILoginServices
    {
        /// <summary>
        /// Verificar se a senha para acesso na aplicaçao tem os requisitos de negócio definidos no projeto
        /// </summary>
        /// <param name="loginRequest">Objeto para receber os dados necessários para verificação da senha do fornecedor</param>
        /// <returns>Retorna se a senha pode ser utilizada para cadastro ou não no portal</returns>
        Task<Response<LoginResponse>> VerificarForcaSenhaAsync(LoginRequest loginRequest);

        /// <summary>
        /// Realizar o Cadastro do login do Fornecedor no Portal
        /// </summary>
        /// <param name="loginRequest">Objeto para receber os dados necessários para cadastro do fornecedor</param>
        /// <returns>Retornar se o login do fornecedor foi cadastrado no portal</returns>
        Task<Response<LoginResponse>> CadastrarLoginSistemaAsync(LoginRequest loginRequest);

        /// <summary>
        /// Desativar o cadastro de acesso de um fornecedor na aplicação
        /// </summary>
        /// <param name="idUsuario">Identificador do Usuario</param>
        /// <returns>Retornar a desativação do cadastro do fornecedor</returns>
        Task<Response<LoginResponse>> AtivarCadastroLoginSistemaAsync(string idUsuario);

        /// <summary>
        /// Verificar se os dados do fornecedor são válidos para fazer login na aplicação
        /// </summary>
        /// <param name="loginRequest"></param>
        /// <returns>Retorna se o fornecedor tem acesso ou não ao sistema</returns>
        Task<Response<LoginResponse>> LoginSistemaAsync(LoginRequest loginRequest);

        /// <summary>
        /// Recuperar a senha de acesso do fornecedor ao portal caso o mesmo tenha esquecido
        /// </summary>
        /// <param name="CnpjCpf">Documento do fornecedor para recuperação da senha</param>
        /// <returns>Retornar a senha de acesso do fornecedor no portal</returns>
        Task<Response<LoginResponse>> RecuperarDadosAcessoAsync(string CnpjCpf);

        /// <summary>
        /// Atualizar o cadastro da senha do usuário
        /// </summary>
        /// <param name="loginRequest">Objeto de login</param>
        /// <returns>Atualizar o cadastro da senha do fornecedor no portal</returns>
        Task<Response<LoginResponse>> AtualizarCadastroLoginSistemaAsync(string email, string password);

        /// <summary>
        /// Confirmar a recuperação de senha de acesso do fornecedor ao portal caso o mesmo tenha esquecido
        /// </summary>
        /// <param name="Email">Email do fornecedor para recuperação da senha</param>
        /// <returns>Retornar a senha de acesso do fornecedor no portal</returns>
        Task<Response<LoginResponse>> ConfirmarRecuperacaoDadosAcessoAsync(string Email, string url);

    }
}
