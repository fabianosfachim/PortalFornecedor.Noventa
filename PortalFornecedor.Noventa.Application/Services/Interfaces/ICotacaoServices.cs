using PortalFornecedor.Noventa.Application.Services.Wrappers;
using PortalFornecedor.Noventa.Domain.Entities;
using PortalFornecedor.Noventa.Domain.Model;


namespace PortalFornecedor.Noventa.Application.Services.Interfaces
{
    public interface ICotacaoServices
    {
        /// <summary>
        /// Adicionar uma nova cotação para um fornecedor
        /// </summary>
        /// <param name="cotacaoRequest">Objeto contendo os dados para cadastro de uma nova cotação</param>
        /// <returns>Retornar se a cotaçao foi cadastrada no banco de dados</returns>
        Task<Response<CotacaoResponse>> AdicionarCotacaoAsync(CotacaoRequest cotacaoRequest, string url);

        /// <summary>
        /// Atualizar uma cotação para um fornecedor e mudar o status para enviada
        /// </summary>
        /// <param name="cotacaoRequest">Objeto contendo os dados para atualização do cadastro de uma cotação</param>
        /// <returns>Retornar se os dados da cotação foram atualizados</returns>
        Task<Response<CotacaoResponse>> AtualizarCotacaoAsync(AtualizarCotacaoRequest cotacaoRequest);

        /// <summary>
        /// Atualizar uma cotação para um fornecedor durante o preenchimento
        /// </summary>
        /// <param name="cotacaoRequest">Objeto contendo os dados para atualização do cadastro de uma cotação</param>
        /// <returns>Retornar se os dados da cotação foram atualizados</returns>
        Task<Response<CotacaoResponse>> SalvarPreenchimentoCotacaoAsync(AtualizarCotacaoRequest cotacaoRequest);

        /// <summary>
        /// Atualizar uma o status da cotação 
        /// </summary>
        /// <param name="idCotacao">Identificador da cotação</param>
        /// <param name="idStatus">Identificador do status da cotação</param>
        /// <param name="cnpj">CNPJ Fornecedor</param>
        /// <returns>Retornar se os dados da cotação foram atualizados</returns>
        Task<Response<CotacaoResponse>> AtualizarStatusCotacaoAsync(int idCotacao, int idStatus);


        /// <summary>
        /// Retornar os dados de uma cotação
        /// </summary>
        /// <param name="idCotacao">Identificador da cotação</param>
        /// <param name="cnpj">CNPJ Fornecedor</param>
        /// <returns> Retornar os dados de uma cotação</returns>
        Task<Response<CotacaoResponse>> ListarCotacaoAsync(string idCotacao, string cnpj);

        /// <summary>
        /// Retornar os dados de uma cotação
        /// </summary>
        /// <param name="Id">Identificador da cotação</param>
        /// <returns> Retornar os dados de uma cotação</returns>
        Task<Response<CotacaoResponse>> ListarCotacaoAsync(int Id);

        /// <summary>
        /// Retornar os dados de uma cotação
        /// </summary>
        /// <param name="guid">Identificador da cotação</param>
        /// <returns> Retornar os dados de uma cotação</returns>
        Task<Response<CotacaoResponse>> ListarCotacaoAsync(Guid guid);

        /// <summary>
        /// Retornar os dados de uma cotação
        /// </summary>
        /// <param name="cotacaoDetalheFiltroRequest">Filtro de Cotação</param>
        /// <returns> Retornar os dados de uma cotação</returns>
        Task<Response<CotacaoDetalheFiltroResponse>> ListarCotacaoAsync(CotacaoDetalheFiltroRequest cotacaoDetalheFiltroRequest);

    }
}
