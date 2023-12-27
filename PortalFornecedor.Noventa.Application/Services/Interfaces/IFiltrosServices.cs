using PortalFornecedor.Noventa.Application.Services.Wrappers;
using PortalFornecedor.Noventa.Domain.Model;

namespace PortalFornecedor.Noventa.Application.Services.Interfaces
{
    public interface IFiltrosServices
    {
        /// <summary>
        /// Listar o status das cotações
        /// </summary>
        /// <returns>Retornar o status da cotaçao</returns>
        Task<Response<StatusResponse>> ListarStatusAsync();

        /// <summary>
        /// Listar o motivo das cotações
        /// </summary>
        /// <returns>Retornar o motivo da cotaçao</returns>
        Task<Response<MotivoResponse>> ListarMotivoAsync();

        /// <summary>
        /// Listar o motivo das cotações
        /// </summary>
        /// <returns>Retornar o motivo da cotaçao</returns>
        Task<Response<DashBoardResponse>> ListarDadosDashBoardAsync(int idFornecedor, int idData, int peddingPage, int currentPage);


    }
}
