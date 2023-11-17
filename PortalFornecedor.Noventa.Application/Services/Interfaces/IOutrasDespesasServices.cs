using PortalFornecedor.Noventa.Application.Services.Wrappers;
using PortalFornecedor.Noventa.Domain.Entities;
using PortalFornecedor.Noventa.Domain.Model;

namespace PortalFornecedor.Noventa.Application.Services.Interfaces
{
    public interface IOutrasDespesasServices
    {
        /// <summary>
        /// Inserir as despesas da cotação
        /// </summary>
        /// <param name="outras_Despesas">Dados da despesa da cotação</param>
        Task<int> InserirIdOutrasDespesasAsync(Outras_Despesas outras_Despesas);

        /// <summary>
        /// Listar o identificador da despesa da cotação
        /// </summary>
        /// <param name="IdCotacao">Identificador da cotação</param>
        /// <param name="NomeDespesa">Nome da Despesa</param>
        Task<int> ListarIdOutrasDespesasAsync(string IdCotacao, string NomeDespesa);

        /// <summary>
        /// Excluir o cadastro da  despesa da Cotação 
        /// </summary>
        /// <param name="IdOutrasDespesas">Identificador da despesa da cotacao</param>
        void ExcluirCotacaoOutrasDespesasAsync(int IdOutrasDespesas);

        /// <summary>
        /// Listar o identificador das despesas da cotação
        /// </summary>
        /// <param name="IdCotacao">Identificador da cotação</param>
        Task<Response<OutrasDespesasResponse>> ListarIdOutrasDespesasAsync(string IdCotacao);

        /// <summary>
        /// Listar o identificador das despesas da cotação
        /// </summary>
        /// <param name="Id">Identificador de Outras Despesas</param>
        /// <param name="IdCotacao">Identificador da cotação</param>
        Task<Response<OutrasDespesasResponse>> ListarOutrasDespesasAsync(int Id, string IdCotacao);
    }
}
