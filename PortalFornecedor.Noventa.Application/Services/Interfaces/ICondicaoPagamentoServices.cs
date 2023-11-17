using PortalFornecedor.Noventa.Application.Services.Wrappers;
using PortalFornecedor.Noventa.Domain.Entities;
using PortalFornecedor.Noventa.Domain.Model;

namespace PortalFornecedor.Noventa.Application.Services.Interfaces
{
    public interface ICondicaoPagamentoServices
    {

        /// <summary>
        /// Inserir as condições de pagamento da cotação
        /// </summary>
        /// <param name="condicao_Pagamento">Dados do pagamento da cotação</param>
        Task<int> InserirIdCondicaoPagamentoAsync(Condicao_Pagamento condicao_Pagamento);

        /// <summary>
        /// Listar o identificador das condições de pagamento da cotação
        /// </summary>
        /// <param name="IdCotacao">Identificador da cotação</param>
        Task<int> ListarIdCotacaoCondicaoPagamentoAsync(string IdCotacao, string StatusCondicoesPagamento);

        /// <summary>
        /// Excluir o cadastro da Cotação dos dados de solicitante
        /// </summary>
        /// <param name="IdCotacaoCondicaoPagamento">Identificador da cotação de pagamento</param>
        void ExcluirCotacaoCondicaoPagamentoAsync(int IdCotacaoCondicaoPagamento);

        /// <summary>
        /// Listar o identificador das condições de pagamento da cotação
        /// </summary>
        /// <param name="IdCotacao">Identificador da cotação</param>
        Task<Response<CondicaoPagamentoResponse>> ListarIdCotacaoCondicaoPagamentoAsync(string IdCotacao);

        /// <summary>
        /// Listar os dados da condição de pagamento
        /// </summary>
        /// <param name="id">Identificador da condição de pagamento</param>
        /// <param name="IdCotacao">Identificador da cotação</param>
        Task<Response<CondicaoPagamentoResponse>> ListarCondicaoPagamentoAsync(int id, string IdCotacao);

    }
}
