using PortalFornecedor.Noventa.Application.Services.Wrappers;
using PortalFornecedor.Noventa.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalFornecedor.Noventa.Application.Services.Interfaces
{
    public interface IMotivoServices
    {
        /// <summary>
        ///  Listar o identificador do motivo da cotação
        /// </summary>
        /// <param name="motivo">Descrição do motivo da cotação</param>
        /// <returns>Retornar o identificador do motivo da cotação</returns>
        Task<int> ListarCotacaoMotivoIdAsync(string motivo);

        /// <summary>
        ///  Listar o motivo da cotação
        /// </summary>
        /// <param name="cotacaoId">Identificador da cotação</param>
        /// <param name="motivo">Descrição do motivo da cotação</param>
        /// <returns>Retornar a lista de cotação</returns>
        Task<int> ListarCotacaoMotivoIdAsync(string IdCotacao, int Idmotivo);

        /// <summary>
        ///  Listar o motivo da cotação
        /// </summary>
        /// <param name="cotacaoId">Identificador da cotação</param>
        /// <returns>Retornar a lista de cotação</returns>
        Task<int> ListarIdCotacaoMotivoAsync(string IdCotacao);

        /// <summary>
        /// Excluir o motivo da cotação
        /// </summary>
        /// <param name="IdMotivoCotacao">Identificador do motivo da cotação</param>
        void ExcluirCotacaoMotivoAsync(int IdMotivoCotacao);

        /// <summary>
        /// Listar o motivo das cotações
        /// </summary>
        /// <param name="idMotivo">Identificador do motivo da cotacao</param>
        /// <returns>Retornar o motivo da cotaçao</returns>
        Task<Response<MotivoResponse>> ListarMotivoAsync(int id);
    }
}
