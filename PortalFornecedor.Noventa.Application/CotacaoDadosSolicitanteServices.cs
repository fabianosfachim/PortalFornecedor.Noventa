using Microsoft.Extensions.Logging;
using PortalFornecedor.Noventa.Application.Services.Interfaces;
using PortalFornecedor.Noventa.Application.Services.Wrappers;
using PortalFornecedor.Noventa.Data.Interfaces;
using PortalFornecedor.Noventa.Data.Repositories.Entities;
using PortalFornecedor.Noventa.Domain.Entities;
using PortalFornecedor.Noventa.Domain.Model;

namespace PortalFornecedor.Noventa.Application
{
    public class CotacaoDadosSolicitanteServices : ICotacaoDadosSolicitanteServices
    {
        private readonly ILogger<CotacaoDadosSolicitanteServices> _logger;
        private readonly ICotacaoDadosSolicitanteRepository _cotacaoDadosSolicitanteRepository;

        public CotacaoDadosSolicitanteServices(ILogger<CotacaoDadosSolicitanteServices> logger, ICotacaoDadosSolicitanteRepository cotacaoDadosSolicitanteRepository)
        {
            _logger = logger;
            _cotacaoDadosSolicitanteRepository = cotacaoDadosSolicitanteRepository;
        }

        public async void ExcluirCotacaoDadosSolicitanteAsync(int IdCotacaoDadosSolicitante)
        {
            _logger.LogInformation("Iniciando o método   " +
                $"{nameof(ExcluirCotacaoDadosSolicitanteAsync)}  " +
                "com os seguintes parâmetros: {IdCotacaoDadosSolicitante}", IdCotacaoDadosSolicitante);

            await _cotacaoDadosSolicitanteRepository.RemoveAsync(IdCotacaoDadosSolicitante);

            _logger.LogInformation("Finalizando o método   " +
               $"{nameof(ExcluirCotacaoDadosSolicitanteAsync)}  " +
               "com os seguintes parâmetros: {IdCotacaoDadosSolicitante}", IdCotacaoDadosSolicitante);
        }

        public async Task<int> InserirIdCotacaoDadosSolicitanteAsync(CotacaoDadosSolicitanteRequest cotacaoDadosSolicitanteRequest)
        {
            int idCotacaoDadosSolicitante = 0;

            try
            {
                _logger.LogInformation("Iniciando o método   " +
                 $"{nameof(InserirIdCotacaoDadosSolicitanteAsync)}  " +
                 "com os seguintes parâmetros: {cotacaoDadosSolicitanteRequest}",
                 cotacaoDadosSolicitanteRequest);

                Cotacao_Dados_Solicitante _Solicitante = new Cotacao_Dados_Solicitante();

                _Solicitante.IdCotacao = cotacaoDadosSolicitanteRequest.IdCotacao;
                _Solicitante.DataSolicitacao = DateTime.Parse(cotacaoDadosSolicitanteRequest.DataSolicitacao);
                _Solicitante.Nome = cotacaoDadosSolicitanteRequest.Nome;
                _Solicitante.CNPJ = cotacaoDadosSolicitanteRequest.CNPJ.Replace(".", "").Replace("-", "").Replace("/", "");
                _Solicitante.DataEntrega = DateTime.Parse(cotacaoDadosSolicitanteRequest.DataEntrega);
                _Solicitante.Endereco = cotacaoDadosSolicitanteRequest.Endereco;
                _Solicitante.CEP = cotacaoDadosSolicitanteRequest.CEP;
                _Solicitante.Cidade = cotacaoDadosSolicitanteRequest.Cidade;
                _Solicitante.Estado = cotacaoDadosSolicitanteRequest.Estado;
                _Solicitante.Contato = cotacaoDadosSolicitanteRequest.Contato;
                _Solicitante.Email = cotacaoDadosSolicitanteRequest.Email;

                await _cotacaoDadosSolicitanteRepository.AddAsync(_Solicitante);

                idCotacaoDadosSolicitante = _Solicitante.Id;

                _logger.LogInformation("Finalizando o método   " +
                 $"{nameof(InserirIdCotacaoDadosSolicitanteAsync)}  " +
                 "com os seguintes parâmetros: {cotacaoDadosSolicitanteRequest}",
                 cotacaoDadosSolicitanteRequest);

            }
            catch (Exception ex)
            {
                _logger.LogError("Erro na execução do método " +
                  $"{nameof(InserirIdCotacaoDadosSolicitanteAsync)}   " +
                  " Com o erro = " + ex.Message);

                throw new Exception("Erro para realizar o cadastro de cotação");
            }

            return idCotacaoDadosSolicitante;
        }

        public async Task<Response<CotacaoDadosSolicitanteResponse>> ListarDadosSolicitanteAsync(string IdCotacao)
        {
            CotacaoDadosSolicitanteResponse cotacaoDadosSolicitanteResponse = new CotacaoDadosSolicitanteResponse();

            try
            {
                _logger.LogInformation("Iniciando o método   " +
               $"{nameof(ListarDadosSolicitanteAsync)}  " +
               "com os seguintes parâmetros: {IdCotacao}",
               IdCotacao);

                var dados = await _cotacaoDadosSolicitanteRepository.GetAsync(x => x.IdCotacao == IdCotacao);

                cotacaoDadosSolicitanteResponse.solicitante = dados.FirstOrDefault();
                cotacaoDadosSolicitanteResponse.Executado = true;
                cotacaoDadosSolicitanteResponse.MensagemRetorno = "Consulta efetuada com sucesso";

                _logger.LogInformation("Finalizando o método   " +
                  $"{nameof(ListarDadosSolicitanteAsync)}  " +
                  "com os seguintes parâmetros: {IdCotacao}",
                  IdCotacao);
            }
            catch (Exception ex)
            {
                _logger.LogError("Erro na execução do método " +
                    $"{nameof(ListarDadosSolicitanteAsync)}   " +
                    " Com o erro = " + ex.Message);
            }

            return new Response<CotacaoDadosSolicitanteResponse>(cotacaoDadosSolicitanteResponse, $"Lista Dados Solicitante.");
        }

        public async Task<int> ListarIdCotacaoDadosSolicitanteAsync(string IdCotacao)
        {
            int idCotacaoDadosSolicitante = 0;

            try
            {
                _logger.LogInformation("Iniciando o método   " +
                 $"{nameof(ListarIdCotacaoDadosSolicitanteAsync)}  " +
                 "com os seguintes parâmetros: {IdCotacao}",
                 IdCotacao);

                var dadosSolicitante = await _cotacaoDadosSolicitanteRepository.GetAsync(x => x.IdCotacao == IdCotacao);

                if (dadosSolicitante.Any())
                {
                  
                    idCotacaoDadosSolicitante = dadosSolicitante.FirstOrDefault().Id;
                }
              
                _logger.LogInformation("Finalizando o método   " +
                 $"{nameof(ListarIdCotacaoDadosSolicitanteAsync)}  " +
                 "com os seguintes parâmetros: {IdCotacao}",
                 IdCotacao);

            }
            catch (Exception ex)
            {
                _logger.LogError("Erro na execução do método " +
                  $"{nameof(ListarIdCotacaoDadosSolicitanteAsync)}   " +
                  " Com o erro = " + ex.Message);

                throw new Exception("Erro para realizar o cadastro de cotação");
            }

            return idCotacaoDadosSolicitante;
        }

    }
}
