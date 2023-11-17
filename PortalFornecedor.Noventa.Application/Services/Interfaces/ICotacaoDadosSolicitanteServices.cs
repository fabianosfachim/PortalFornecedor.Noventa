using PortalFornecedor.Noventa.Application.Services.Wrappers;
using PortalFornecedor.Noventa.Domain.Entities;
using PortalFornecedor.Noventa.Domain.Model;

namespace PortalFornecedor.Noventa.Application.Services.Interfaces
{
    public interface ICotacaoDadosSolicitanteServices
    {
        /// <summary>
        /// Inserir o identificador dos dados do solictante da cotação
        /// </summary>
        /// <param name="cotacaoDadosSolicitanteRequest">Dados do solicitante da cotação</param>
        Task<int> InserirIdCotacaoDadosSolicitanteAsync(CotacaoDadosSolicitanteRequest cotacaoDadosSolicitanteRequest);

        /// <summary>
        /// Inserir o identificador dos dados do solictante da cotação
        /// </summary>
        /// <param name="cotacaoDadosSolicitanteRequest">Dados do solicitante da cotação</param>
        Task<int> ListarIdCotacaoDadosSolicitanteAsync(string IdCotacao);

        /// <summary>
        /// Excluir o cadastro da Cotação dos dados de solicitante
        /// </summary>
        /// <param name="IdCotacaoDadosSolicitante">Identificador da Cotação dos dados de solicitante</param>
        void ExcluirCotacaoDadosSolicitanteAsync(int IdCotacaoDadosSolicitante);

        /// <summary>
        /// Listar os dados do solicitante da cotação
        /// </summary>
        /// <returns>Retornar os dados do solicitante da cotação</returns>
        Task<Response<CotacaoDadosSolicitanteResponse>> ListarDadosSolicitanteAsync(string IdCotacao);

    }
}
