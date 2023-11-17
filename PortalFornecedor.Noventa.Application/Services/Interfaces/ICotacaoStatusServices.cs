using PortalFornecedor.Noventa.Application.Services.Wrappers;
using PortalFornecedor.Noventa.Domain.Entities;
using PortalFornecedor.Noventa.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalFornecedor.Noventa.Application.Services.Interfaces
{
    public interface ICotacaoStatusServices
    {
        /// <summary>
        ///  Listar o identificador do status da cotação
        /// </summary>
        /// <param name="StatusCotacao">Descrição do status da cotação</param>
        /// <returns>Retornar o identificador do status da cotação</returns>
        Task<int> ListarCotacaoStatusIdAsync(string StatusCotacao);

        /// <summary>
        ///  Listar o status da cotação
        /// </summary>
        /// <param name="cotacaoId">Identificador da cotação</param>
        /// <param name="StatusCotacao">Descrição do status da cotação</param>
        /// <returns>Retornar a lista de cotação</returns>
        Task<int> ListarCotacaoStatusIdAsync(string IdCotacao, int idStatus);

        /// <summary>
        ///  Listar o status da cotação
        /// </summary>
        /// <param name="cotacaoId">Identificador da cotação</param>
        /// <returns>Retornar a lista de cotação</returns>
        Task<int> ListarStatusIdAsync(string IdCotacao);

        /// <summary>
        /// Excluir o status da cotação
        /// </summary>
        /// <param name="IdStatusCotacao">Identificador do status da cotação</param>
        void ExcluirCotacaoStatusAsync(int IdStatusCotacao);

        /// <summary>
        /// Atualizar o status da cotação
        /// </summary>
        /// <param name="IdStatusCotacao">Identificador do status da cotação</param>
        void AtualizarCotacaoStatusAsync(Cotacao_Status cotacao_Status);

        /// <summary>
        ///  Listar o status da cotação
        /// </summary>
        /// <param name="id">Identificador do status da cotacao</param>
        /// <returns>Retornar a lista de cotação</returns>
        Task<Response<StatusResponse>> ListarCotacaoAsync(int id);
    }
}
