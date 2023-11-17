using Microsoft.Extensions.Logging;
using PortalFornecedor.Noventa.Application.Services.Interfaces;
using PortalFornecedor.Noventa.Application.Services.Wrappers;
using PortalFornecedor.Noventa.Data.Interfaces;
using PortalFornecedor.Noventa.Data.Repositories.Entities;
using PortalFornecedor.Noventa.Domain.Entities;
using PortalFornecedor.Noventa.Domain.Model;

namespace PortalFornecedor.Noventa.Application
{
    public class FreteServices : IfreteServices
    {
        private readonly ILogger<FreteServices> _logger;
        private readonly IFreteRepository _freteRepository;

        public FreteServices(ILogger<FreteServices> logger, IFreteRepository freteRepository)
        {
            _logger = logger;
            _freteRepository = freteRepository;
        }

        public void ExcluirCotacaoFreteAsync(int IdFrete)
        {
            _logger.LogInformation("Iniciando o método   " +
            $"{nameof(ExcluirCotacaoFreteAsync)}  " +
            "com os seguintes parâmetros: {IdFrete}", IdFrete);

             _freteRepository.RemoveAsync(IdFrete);

            _logger.LogInformation("Finalizando o método   " +
               $"{nameof(ExcluirCotacaoFreteAsync)}  " +
               "com os seguintes parâmetros: {IdFrete}", IdFrete);
        }

        public async Task<int> InserirIdFreteAsync(Frete frete)
        {
            int idFrete = 0;

            try
            {
                _logger.LogInformation("Iniciando o método   " +
                 $"{nameof(InserirIdFreteAsync)}  " +
                 "com os seguintes parâmetros: {frete}",
                 frete);


                await _freteRepository.AddAsync(frete);

                idFrete = frete.Id;

                _logger.LogInformation("Finalizando o método   " +
                 $"{nameof(InserirIdFreteAsync)}  " +
                 "com os seguintes parâmetros: {frete}",
                 frete);

            }
            catch (Exception ex)
            {
                _logger.LogError("Erro na execução do método " +
                  $"{nameof(InserirIdFreteAsync)}   " +
                  " Com o erro = " + ex.Message);

                throw new Exception("Erro para realizar o cadastro de cotação");
            }

            return idFrete;
        }

        public async Task<Response<FreteResponse>> ListarFreteAsync(int id, string IdCotacao)
        {
            FreteResponse freteResponse = new FreteResponse();

            try
            {
                _logger.LogInformation("Iniciando o método   " +
                 $"{nameof(ListarFreteAsync)}  " +
                 "com os seguintes parâmetros: {id}, {IdCotacao}",
                 id, IdCotacao);

                var dadosFrete = await _freteRepository.GetAsync(x => x.IdCotacao == IdCotacao && x.Id == id);

                if (dadosFrete.Any())
                {

                    freteResponse.Executado = true;
                    freteResponse.MensagemRetorno = "Lista de frete consultada com sucesso !";
                    freteResponse.FreteDados = dadosFrete.FirstOrDefault();
                }

                _logger.LogInformation("Finalizando o método   " +
                $"{nameof(ListarFreteAsync)}  " +
                "com os seguintes parâmetros: {id}, {IdCotacao}",
                id, IdCotacao);

            }
            catch (Exception ex)
            {
                _logger.LogError("Erro na execução do método " +
                  $"{nameof(ListarFreteAsync)}   " +
                  " Com o erro = " + ex.Message);

                throw new Exception("Erro para realizar o cadastro de cotação");
            }

            return new Response<FreteResponse>(freteResponse, $"Lista Frete.");
        }

        public async Task<int> ListarIdCotacaoFreteAsync(string IdCotacao, string TipoFrete)
        {
            int idFrete = 0;

            try
            {
                _logger.LogInformation("Iniciando o método   " +
                 $"{nameof(ListarIdCotacaoFreteAsync)}  " +
                 "com os seguintes parâmetros: {IdCotacao}, {TipoFrete}",
                IdCotacao, TipoFrete);

                var dadosFrete = await _freteRepository.GetAsync(x => x.IdCotacao == IdCotacao && x.TipoFrete == TipoFrete);

                if (dadosFrete.Any())
                {

                    idFrete = dadosFrete.FirstOrDefault().Id;
                }

                _logger.LogInformation("Finalizando o método   " +
                 $"{nameof(ListarIdCotacaoFreteAsync)}  " +
                 "com os seguintes parâmetros: {IdCotacao}, {TipoFrete}",
                 IdCotacao, TipoFrete);

            }
            catch (Exception ex)
            {
                _logger.LogError("Erro na execução do método " +
                  $"{nameof(ListarIdCotacaoFreteAsync)}   " +
                  " Com o erro = " + ex.Message);

                throw new Exception("Erro para realizar o cadastro de cotação");
            }

            return idFrete;
        }

        public async Task<Response<FreteResponse>> ListarIdFreteAsync(string IdCotacao)
        {
            FreteResponse freteResponse = new FreteResponse();

            try
            {
                _logger.LogInformation("Iniciando o método   " +
                 $"{nameof(ListarIdFreteAsync)}  " +
                 "com os seguintes parâmetros: {IdCotacao}",
                 IdCotacao);

                var dadosFrete = await _freteRepository.GetAsync(x => x.IdCotacao == IdCotacao);

                if (dadosFrete.Any())
                {

                    freteResponse.Executado = true;
                    freteResponse.MensagemRetorno = "Lista de frete consultada com sucesso !";
                    freteResponse.Frete = dadosFrete.ToList();
                }

                _logger.LogInformation("Finalizando o método   " +
                 $"{nameof(ListarIdFreteAsync)}  " +
                 "com os seguintes parâmetros: {IdCotacao}",
                 IdCotacao);

            }
            catch (Exception ex)
            {
                _logger.LogError("Erro na execução do método " +
                  $"{nameof(ListarIdFreteAsync)}   " +
                  " Com o erro = " + ex.Message);

                throw new Exception("Erro para realizar o cadastro de cotação");
            }

            return new Response<FreteResponse>(freteResponse, $"Lista Frete.");
        }
    }
}
