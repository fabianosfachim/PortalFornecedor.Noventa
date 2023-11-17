using Microsoft.Extensions.Logging;
using PortalFornecedor.Noventa.Application.Services.Interfaces;
using PortalFornecedor.Noventa.Application.Services.Wrappers;
using PortalFornecedor.Noventa.Data.Interfaces;
using PortalFornecedor.Noventa.Data.Repositories.Entities;
using PortalFornecedor.Noventa.Domain.Entities;
using PortalFornecedor.Noventa.Domain.Model;

namespace PortalFornecedor.Noventa.Application
{
    public class CondicaoPagamentoServices : ICondicaoPagamentoServices
    {
        private readonly ILogger<CotacaoDadosSolicitanteServices> _logger;
        private readonly ICondicaoPagamentoRepository _condicaoPagamentoRepository;

        public CondicaoPagamentoServices(ILogger<CotacaoDadosSolicitanteServices> logger, ICondicaoPagamentoRepository condicaoPagamentoRepository)
        {
            _logger = logger;
            _condicaoPagamentoRepository = condicaoPagamentoRepository;
        }

        public  void ExcluirCotacaoCondicaoPagamentoAsync(int IdCotacaoCondicaoPagamento)
        {
            _logger.LogInformation("Iniciando o método   " +
              $"{nameof(ExcluirCotacaoCondicaoPagamentoAsync)}  " +
              "com os seguintes parâmetros: {IdCotacaoCondicaoPagamento}", IdCotacaoCondicaoPagamento);

             _condicaoPagamentoRepository.RemoveAsync(IdCotacaoCondicaoPagamento);

            _logger.LogInformation("Finalizando o método   " +
               $"{nameof(ExcluirCotacaoCondicaoPagamentoAsync)}  " +
               "com os seguintes parâmetros: {IdCotacaoCondicaoPagamento}", IdCotacaoCondicaoPagamento);
        }

        public async Task<int> InserirIdCondicaoPagamentoAsync(Condicao_Pagamento condicao_Pagamento)
        {
            int idCotacaoCondicaoPagamento = 0;

            try
            {
                _logger.LogInformation("Iniciando o método   " +
                 $"{nameof(InserirIdCondicaoPagamentoAsync)}  " +
                 "com os seguintes parâmetros: {condicao_Pagamento}",
                 condicao_Pagamento);


                await _condicaoPagamentoRepository.AddAsync(condicao_Pagamento);

                idCotacaoCondicaoPagamento = condicao_Pagamento.Id;

                _logger.LogInformation("Finalizando o método   " +
                 $"{nameof(InserirIdCondicaoPagamentoAsync)}  " +
                 "com os seguintes parâmetros: {condicao_Pagamento}",
                 condicao_Pagamento);

            }
            catch (Exception ex)
            {
                _logger.LogError("Erro na execução do método " +
                  $"{nameof(InserirIdCondicaoPagamentoAsync)}   " +
                  " Com o erro = " + ex.Message);

                throw new Exception("Erro para realizar o cadastro de cotação");
            }

            return idCotacaoCondicaoPagamento;
        }

        public async Task<Response<CondicaoPagamentoResponse>> ListarCondicaoPagamentoAsync(int id, string IdCotacao)
        {
            CondicaoPagamentoResponse condicaoPagamentoResponse = new CondicaoPagamentoResponse();


            try
            {
                _logger.LogInformation("Iniciando o método   " +
                 $"{nameof(ListarIdCotacaoCondicaoPagamentoAsync)}  " +
                 "com os seguintes parâmetros: {IdCotacao}",
                 IdCotacao);

                var dadosCondicaoPagamento = await _condicaoPagamentoRepository.GetAsync(x => x.IdCotacao == IdCotacao && x.Id == id);

                if (dadosCondicaoPagamento.Any())
                {
                    condicaoPagamentoResponse.Executado = true;
                    condicaoPagamentoResponse.MensagemRetorno = "Lista de condição de pagamento consultada com sucesso !";
                    condicaoPagamentoResponse.PagamentosDados = dadosCondicaoPagamento.FirstOrDefault();
                }

                _logger.LogInformation("Finalizando o método   " +
                 $"{nameof(ListarIdCotacaoCondicaoPagamentoAsync)}  " +
                 "com os seguintes parâmetros: {IdCotacao}",
                 IdCotacao);

            }
            catch (Exception ex)
            {
                _logger.LogError("Erro na execução do método " +
                  $"{nameof(ListarIdCotacaoCondicaoPagamentoAsync)}   " +
                  " Com o erro = " + ex.Message);

                condicaoPagamentoResponse.Executado = false;
                condicaoPagamentoResponse.MensagemRetorno = "Erro na consulta de condicao pagamento";
            }

            return new Response<CondicaoPagamentoResponse>(condicaoPagamentoResponse, $"Lista Condição Pagamento.");
        }

        public async Task<int> ListarIdCotacaoCondicaoPagamentoAsync(string IdCotacao, string StatusCondicoesPagamento)
        {
            int idCotacaoCondicaoPagamento = 0;

            try
            {
                _logger.LogInformation("Iniciando o método   " +
                 $"{nameof(ListarIdCotacaoCondicaoPagamentoAsync)}  " +
                 "com os seguintes parâmetros: {IdCotacao}, {StatusCondicoesPagamento}",
                 IdCotacao, StatusCondicoesPagamento);

                var dadosCondicaoPagamento = await _condicaoPagamentoRepository.GetAsync(x => x.IdCotacao == IdCotacao && x.StatusCondicoesPagamento == StatusCondicoesPagamento);

                if (dadosCondicaoPagamento.Any())
                {

                    idCotacaoCondicaoPagamento = dadosCondicaoPagamento.FirstOrDefault().Id;
                }

                _logger.LogInformation("Finalizando o método   " +
                 $"{nameof(ListarIdCotacaoCondicaoPagamentoAsync)}  " +
                 "com os seguintes parâmetros: {IdCotacao}, {StatusCondicoesPagamento}",
                 IdCotacao, StatusCondicoesPagamento);

            }
            catch (Exception ex)
            {
                _logger.LogError("Erro na execução do método " +
                  $"{nameof(ListarIdCotacaoCondicaoPagamentoAsync)}   " +
                  " Com o erro = " + ex.Message);

                throw new Exception("Erro para realizar o cadastro de cotação");
            }

            return idCotacaoCondicaoPagamento;
        }

        public async Task<Response<CondicaoPagamentoResponse>> ListarIdCotacaoCondicaoPagamentoAsync(string IdCotacao)
        {

            CondicaoPagamentoResponse condicaoPagamentoResponse = new CondicaoPagamentoResponse();


            try
            {
                _logger.LogInformation("Iniciando o método   " +
                 $"{nameof(ListarIdCotacaoCondicaoPagamentoAsync)}  " +
                 "com os seguintes parâmetros: {IdCotacao}",
                 IdCotacao);

                var dadosCondicaoPagamento = await _condicaoPagamentoRepository.GetAsync(x => x.IdCotacao == IdCotacao);

                if (dadosCondicaoPagamento.Any())
                {
                    condicaoPagamentoResponse.Executado = true;
                    condicaoPagamentoResponse.MensagemRetorno = "Lista de condição de pagamento consultada com sucesso !";
                    condicaoPagamentoResponse.Pagamentos = dadosCondicaoPagamento.ToList();
                }

                _logger.LogInformation("Finalizando o método   " +
                 $"{nameof(ListarIdCotacaoCondicaoPagamentoAsync)}  " +
                 "com os seguintes parâmetros: {IdCotacao}",
                 IdCotacao);

            }
            catch (Exception ex)
            {
                _logger.LogError("Erro na execução do método " +
                  $"{nameof(ListarIdCotacaoCondicaoPagamentoAsync)}   " +
                  " Com o erro = " + ex.Message);

                condicaoPagamentoResponse.Executado = false;
                condicaoPagamentoResponse.MensagemRetorno = "Erro na consulta de condicao pagamento";
            }

            return new Response<CondicaoPagamentoResponse>(condicaoPagamentoResponse, $"Lista Condição Pagamento.");
        }
    }
}
