using PortalFornecedor.Noventa.Application.Services.Interfaces;
using PortalFornecedor.Noventa.Application.Services.Wrappers;
using PortalFornecedor.Noventa.Domain.Model;
using PortalFornecedor.Noventa.Data.Interfaces;
using Microsoft.Extensions.Logging;

namespace PortalFornecedor.Noventa.Application
{
    public class EstadoServices : IEstadoServices
    {
        private readonly IEstadoRepository _estadoRepository;
        private readonly ILogger<EstadoServices> _logger;


        public EstadoServices(IEstadoRepository estadoRepository, 
                              ILogger<EstadoServices> logger)
        {
            _estadoRepository = estadoRepository;
            _logger = logger;
        }

        public async Task<Response<EstadoResponse>> ListarEstadoAsync()
        {
            EstadoResponse estadoResponse = new EstadoResponse();

            
                _logger.LogInformation("Iniciando o método   " + 
                    $"{nameof(ListarEstadoAsync)}   ");
                   

                var estado = await _estadoRepository.GetAllAsync();
                estadoResponse.Estados = estado.ToList();
                estadoResponse.Executado = true;
                estadoResponse.MensagemRetorno = "Consulta efetuada com sucesso";

                _logger.LogInformation("Finalizando o método   " +
                    $"{nameof(ListarEstadoAsync)}   ");
            

            return new Response<EstadoResponse>(estadoResponse, $"Lista Estados.");
        }

       
    }
}