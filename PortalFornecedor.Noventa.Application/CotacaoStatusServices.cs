using Microsoft.Extensions.Logging;
using PortalFornecedor.Noventa.Application.Services.Interfaces;
using PortalFornecedor.Noventa.Application.Services.Wrappers;
using PortalFornecedor.Noventa.Data.Interfaces;
using PortalFornecedor.Noventa.Data.Repositories.Entities;
using PortalFornecedor.Noventa.Domain.Entities;
using PortalFornecedor.Noventa.Domain.Model;
using System.Net.NetworkInformation;

namespace PortalFornecedor.Noventa.Application
{
    public class CotacaoStatusServices : ICotacaoStatusServices
    {
        private readonly ILogger<CotacaoStatusServices> _logger;
        private readonly IStatusRepository _statusRepository;
        private readonly IStatusCotacaoRepository _statusCotacaoRepository;
        public CotacaoStatusServices(ILogger<CotacaoStatusServices> logger,
                                    IStatusRepository statusRepository,
                                    IStatusCotacaoRepository statusCotacaoRepository)
        {
            _logger = logger;
            _statusRepository = statusRepository;
            _statusCotacaoRepository = statusCotacaoRepository;
        }

        public async Task<int> ListarCotacaoStatusIdAsync(string StatusCotacao)
        {
            int idStatus = 0;

            try
            {
                _logger.LogInformation("Iniciando o método   " +
                 $"{nameof(ListarCotacaoStatusIdAsync)}  " +
                        "com os seguintes parâmetros:  {StatusCotacao} ",
                         StatusCotacao);

                var status = await _statusRepository.GetAsync(x => x.NomeStatus.Trim().ToUpper() == StatusCotacao.Trim().ToUpper());

                if (status != null && status.Any())
                {
                    idStatus = status.FirstOrDefault().Id;
                }

                _logger.LogInformation("Finalizando o método   " +
                $"{nameof(ListarCotacaoStatusIdAsync)}  " +
                      "com os seguintes parâmetros:  {StatusCotacao} ",
                       StatusCotacao);

            }
            catch (Exception ex)
            {
                _logger.LogError("Erro na execução do método " +
                  $"{nameof(ListarCotacaoStatusIdAsync)}   " +
                  " Com o erro = " + ex.Message);

                throw new Exception("Erro para realizar o cadastro de cotação");
            }

            return idStatus;
        }

        public async Task<int> ListarCotacaoStatusIdAsync(string IdCotacao, int idStatus)
        {
            int idCotacaoStatus = 0;

            try
            {
                _logger.LogInformation("Iniciando o método   " +
                 $"{nameof(ListarCotacaoStatusIdAsync)}  " +
                        "com os seguintes parâmetros:  {IdCotacao} ",
                         IdCotacao);

                var cotacaoStatus = await _statusCotacaoRepository.GetAsync(x => x.IdCotacao == IdCotacao);

                if (!cotacaoStatus.Any())
                {
                    Cotacao_Status statusCotacao = new Cotacao_Status();
                    statusCotacao.IdCotacao = IdCotacao;
                    statusCotacao.IdStatus = idStatus;
                    statusCotacao.DataStatus = DateTime.Now;
                    await _statusCotacaoRepository.AddAsync(statusCotacao);

                    idCotacaoStatus = statusCotacao.Id;
                }
                else
                {
                    idCotacaoStatus = cotacaoStatus.FirstOrDefault().Id;
                }

                _logger.LogInformation("Finalizando o método   " +
                   $"{nameof(ListarCotacaoStatusIdAsync)}  " +
                          "com os seguintes parâmetros:  {IdCotacao} ",
                           IdCotacao);

            }
            catch (Exception ex)
            {
                _logger.LogError("Erro na execução do método " +
                  $"{nameof(ListarCotacaoStatusIdAsync)}   " +
                  " Com o erro = " + ex.Message);

                throw new Exception("Erro para realizar o cadastro de cotação");
            }

            return idCotacaoStatus;
        }

        public async Task<int> ListarStatusIdAsync(string IdCotacao)
        {
            int idCotacaoStatus = 0;

            try
            {
                _logger.LogInformation("Iniciando o método   " +
                 $"{nameof(ListarStatusIdAsync)}  " +
                        "com os seguintes parâmetros:  {IdCotacao} ",
                         IdCotacao);

                var cotacaoStatus = await _statusCotacaoRepository.GetAsync(x => x.IdCotacao == IdCotacao);

                if(cotacaoStatus.Any()) 
                {
                    idCotacaoStatus = cotacaoStatus.FirstOrDefault().Id;
                }
                
                _logger.LogInformation("Finalizando o método   " +
                   $"{nameof(ListarStatusIdAsync)}  " +
                          "com os seguintes parâmetros:  {IdCotacao} ",
                           IdCotacao);

            }
            catch (Exception ex)
            {
                _logger.LogError("Erro na execução do método " +
                  $"{nameof(ListarStatusIdAsync)}   " +
                  " Com o erro = " + ex.Message);

                throw new Exception("Erro para realizar o cadastro de cotação");
            }

            return idCotacaoStatus;
        }

        public async void ExcluirCotacaoStatusAsync(int IdStatusCotacao)
        {
            _logger.LogInformation("Iniciando o método   " +
            $"{nameof(ExcluirCotacaoStatusAsync)}  " +
                   "com os seguintes parâmetros: {IdStatusCotacao}", IdStatusCotacao);

             _statusCotacaoRepository.RemoveAsync(IdStatusCotacao);

            _logger.LogInformation("Finalizando o método   " +
           $"{nameof(ExcluirCotacaoStatusAsync)}  " +
                  "com os seguintes parâmetros: {IdStatusCotacao}", IdStatusCotacao);
        }

        public void AtualizarCotacaoStatusAsync(Cotacao_Status cotacao_Status)
        {
            _logger.LogInformation("Iniciando o método   " +
           $"{nameof(AtualizarCotacaoStatusAsync)}  " +
                  "com os seguintes parâmetros: {cotacao_Status}", cotacao_Status);

            _statusCotacaoRepository.UpdateAsync(cotacao_Status);

            _logger.LogInformation("Finalizando o método   " +
           $"{nameof(AtualizarCotacaoStatusAsync)}  " +
                  "com os seguintes parâmetros: {cotacao_Status}", cotacao_Status);
        }

        public async Task<Response<StatusResponse>> ListarCotacaoAsync(int id)
        {
            StatusResponse statusResponse = new StatusResponse();
            StatusDashBoard statusDashBoard = new StatusDashBoard();

            try
            {
                _logger.LogInformation("Iniciando o método   " +
                    $"{nameof(ListarCotacaoAsync)}   ");


                var statusCotacao = await _statusCotacaoRepository.GetByIdAsync(id);

                var status = await _statusRepository.GetByIdAsync(statusCotacao.IdStatus);

                statusDashBoard.Id = status.Id;
                statusDashBoard.NomeStatus = status.NomeStatus;
                statusDashBoard.DataStatus = statusCotacao.DataStatus;


                statusResponse.statusDashBoard = statusDashBoard;
                statusResponse.Executado = true;
                statusResponse.MensagemRetorno = "Consulta efetuada com sucesso";

                _logger.LogInformation("Finalizando o método   " +
                    $"{nameof(ListarCotacaoAsync)}   ");
            }
            catch (Exception ex)
            {
                _logger.LogError("Erro na execução do método " +
                    $"{nameof(ListarCotacaoAsync)}   " +
                    " Com o erro = " + ex.Message);

                statusResponse.Executado = false;
                statusResponse.MensagemRetorno = "Erro na consulta de lista de status";
            }

            return new Response<StatusResponse>(statusResponse, $"Lista Status.");
        }
    }
}
