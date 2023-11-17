using PortalFornecedor.Noventa.Application.Services.Wrappers;
using PortalFornecedor.Noventa.Domain.Model;


namespace PortalFornecedor.Noventa.Application.Services.Interfaces
{
    public interface IEstadoServices
    {
        /// <summary>
        /// Listar os estados cadastrados no portal
        /// </summary>
        /// <returns>Listar os estados cadastrados no portal</returns>
        Task<Response<EstadoResponse>> ListarEstadoAsync();
    }
}
