

using PortalFornecedor.Noventa.Application.Services.Wrappers;
using PortalFornecedor.Noventa.Domain.Entities;
using PortalFornecedor.Noventa.Domain.Model;

namespace PortalFornecedor.Noventa.Application.Services.Interfaces
{
    public interface IfreteServices
    {
        /// <summary>
        /// Inserir os fretes da cotação
        /// </summary>
        /// <param name="frete">Dados do frete da cotação</param>
        Task<int> InserirIdFreteAsync(Frete frete);

        /// <summary>
        /// Listar o identificador do frete de pagamento da cotação
        /// </summary>
        /// <param name="IdCotacao">Identificador da cotação</param>
        Task<int> ListarIdCotacaoFreteAsync(string IdCotacao, string TipoFrete);

        /// <summary>
        /// Excluir o cadastro da Cotação dos dados de solicitante
        /// </summary>
        /// <param name="IdFrete">Identificador do frete de pagamento</param>
        void ExcluirCotacaoFreteAsync(int IdFrete);

        /// <summary>
        /// Listar o identificador dos frete da cotação
        /// </summary>
        /// <param name="IdCotacao">Identificador da cotação</param>
        Task<Response<FreteResponse>> ListarIdFreteAsync(string IdCotacao);

        /// <summary>
        /// Listar o identificador dos frete da cotação
        /// </summary>
        /// <param name="Id">Identificador do Frete</param>
        /// <param name="IdCotacao">Identificador da cotação</param>
        Task<Response<FreteResponse>> ListarFreteAsync(int id , string IdCotacao);
    }
}
