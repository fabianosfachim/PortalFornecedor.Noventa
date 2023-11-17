using Microsoft.Extensions.Logging;
using PortalFornecedor.Noventa.Application.Services.Interfaces;
using PortalFornecedor.Noventa.Application.Services.Wrappers;
using PortalFornecedor.Noventa.Data.Interfaces;
using PortalFornecedor.Noventa.Data.Repositories.Entities;
using PortalFornecedor.Noventa.Domain.Entities;
using PortalFornecedor.Noventa.Domain.Model;

namespace PortalFornecedor.Noventa.Application
{
    public class MotivoServices : IMotivoServices
    {
        private readonly ILogger<FiltrosServices> _logger;
        private readonly IMotivoRepository _motivoRepository;
        private readonly IMotivoCotacaoRepository _motivoCotacaoRepository;
        public MotivoServices(ILogger<FiltrosServices> logger,
                              IMotivoRepository motivoRepository,
                              IMotivoCotacaoRepository motivoCotacaoRepository)
        {
            _logger = logger;
            _motivoRepository = motivoRepository;
            _motivoCotacaoRepository = motivoCotacaoRepository;
        }

        public async void ExcluirCotacaoMotivoAsync(int IdMotivoCotacao)
        {
            _logger.LogInformation("Iniciando o método   " +
            $"{nameof(ExcluirCotacaoMotivoAsync)}  " +
                   "com os seguintes parâmetros: {IdMotivoCotacao}", IdMotivoCotacao);

            var cotacaoMotivo = await _motivoCotacaoRepository.RemoveAsync(IdMotivoCotacao);

          
            _logger.LogInformation("Finalizando o método   " +
           $"{nameof(ExcluirCotacaoMotivoAsync)}  " +
                  "com os seguintes parâmetros: {IdMotivoCotacao}", IdMotivoCotacao);
        }

        public async Task<int> ListarCotacaoMotivoIdAsync(string motivo)
        {
            int idMotivo = 0;

            try
            {
                _logger.LogInformation("Iniciando o método   " +
                 $"{nameof(ListarCotacaoMotivoIdAsync)}  " +
                  "com os seguintes parâmetros:  {motivo} ", motivo);

                var motivoStatus = await _motivoRepository.GetAsync(x => x.NomeMotivo == motivo);

                if (motivoStatus != null && motivoStatus.Any())
                {
                    idMotivo = motivoStatus.FirstOrDefault().Id;
                }

                _logger.LogInformation("Finalizando o método   " +
                 $"{nameof(ListarCotacaoMotivoIdAsync)}  " +
                  "com os seguintes parâmetros:  {motivo} ", motivo);


            }
            catch (Exception ex)
            {
                _logger.LogError("Erro na execução do método " +
                  $"{nameof(ListarCotacaoMotivoIdAsync)}   " +
                  " Com o erro = " + ex.Message);

                throw new Exception("Erro para realizar o cadastro de cotação");
            }

            return idMotivo;
        }

        public async Task<int> ListarCotacaoMotivoIdAsync(string IdCotacao, int Idmotivo)
        {
            int idCotacaoMotivo = 0;

            try
            {
                _logger.LogInformation("Finalizando o método   " +
                  $"{nameof(ListarCotacaoMotivoIdAsync)}  " +
                 "com os seguintes parâmetros: {IdCotacao}, {Idmotivo} ",
                          IdCotacao, Idmotivo);

                var cotacaoMotivo = await _motivoCotacaoRepository.GetAsync(x => x.IdCotacao == IdCotacao);

                if (!cotacaoMotivo.Any())
                {
                    Cotacao_Motivo cotacao_Motivo = new Cotacao_Motivo();
                    cotacao_Motivo.IdCotacao = IdCotacao;
                    cotacao_Motivo.IdMotivo = Idmotivo;
                    await _motivoCotacaoRepository.AddAsync(cotacao_Motivo);

                    idCotacaoMotivo = cotacao_Motivo.Id;
                }
                else
                {
                    idCotacaoMotivo = cotacaoMotivo.FirstOrDefault().Id;
                }

                _logger.LogInformation("Finalizando o método   " +
                     $"{nameof(ListarCotacaoMotivoIdAsync)}  " +
                    "com os seguintes parâmetros: {IdCotacao}, {Idmotivo} ",
                             IdCotacao, Idmotivo);
            }
            catch (Exception ex)
            {
                   _logger.LogError("Erro na execução do método " +
                  $"{nameof(ListarCotacaoMotivoIdAsync)}   " +
                  " Com o erro = " + ex.Message);

                throw new Exception("Erro para realizar o cadastro de cotação");
            }

            return idCotacaoMotivo;
        }

        public async Task<int> ListarIdCotacaoMotivoAsync(string IdCotacao)
        {
            int idCotacaoMotivo = 0;

            try
            {
                _logger.LogInformation("Finalizando o método   " +
                  $"{nameof(ListarCotacaoMotivoIdAsync)}  " +
                 "com os seguintes parâmetros: {IdCotacao} ",
                          IdCotacao);

                var cotacaoMotivo = await _motivoCotacaoRepository.GetAsync(x => x.IdCotacao == IdCotacao);

                if(cotacaoMotivo.Any())
                {
                    idCotacaoMotivo = cotacaoMotivo.FirstOrDefault().Id;
                }
                
                _logger.LogInformation("Finalizando o método   " +
                     $"{nameof(ListarCotacaoMotivoIdAsync)}  " +
                    "com os seguintes parâmetros: {IdCotacao} ",
                             IdCotacao);
            }
            catch (Exception ex)
            {
                _logger.LogError("Erro na execução do método " +
               $"{nameof(ListarCotacaoMotivoIdAsync)}   " +
               " Com o erro = " + ex.Message);

                throw new Exception("Erro para realizar o cadastro de cotação");
            }

            return idCotacaoMotivo;
        }

        public async Task<Response<MotivoResponse>> ListarMotivoAsync(int id)
        {
            MotivoResponse motivoResponse = new MotivoResponse();

            try
            {
                _logger.LogInformation("Iniciando o método   " +
                    $"{nameof(ListarMotivoAsync)}   ");


                var cotacaoMotivo = await _motivoCotacaoRepository.GetByIdAsync(id);

                int idMotivo = cotacaoMotivo.IdMotivo;

                var motivo = await _motivoRepository.GetByIdAsync(idMotivo);
               
                motivoResponse.MotivoDados = motivo;
                motivoResponse.Executado = true;
                motivoResponse.MensagemRetorno = "Consulta efetuada com sucesso";

                _logger.LogInformation("Finalizando o método   " +
                    $"{nameof(ListarMotivoAsync)}   ");
            }
            catch (Exception ex)
            {
                _logger.LogError("Erro na execução do método " +
                    $"{nameof(ListarMotivoAsync)}   " +
                    " Com o erro = " + ex.Message);

                motivoResponse.Executado = false;
                motivoResponse.MensagemRetorno = "Erro na consulta de lista de motivos";
            }

            return new Response<MotivoResponse>(motivoResponse, $"Lista Motivo.");
        }
    }
}
