using Microsoft.Extensions.Logging;
using PortalFornecedor.Noventa.Application.Services.Interfaces;
using PortalFornecedor.Noventa.Application.Services.Wrappers;
using PortalFornecedor.Noventa.Data.Interfaces;
using PortalFornecedor.Noventa.Data.Repositories.Entities;
using PortalFornecedor.Noventa.Domain.Entities;
using PortalFornecedor.Noventa.Domain.Model;

namespace PortalFornecedor.Noventa.Application
{
    public class OutrasDespesasServices : IOutrasDespesasServices
    {
        private readonly ILogger<OutrasDespesasServices> _logger;
        private readonly IOutrasDespesasRepository _outrasDespesas;

        public OutrasDespesasServices(ILogger<OutrasDespesasServices> logger, IOutrasDespesasRepository outrasDespesas)
        {
            _logger = logger;
            _outrasDespesas = outrasDespesas;
        }

        public void ExcluirCotacaoOutrasDespesasAsync(int IdOutrasDespesas)
        {
            _logger.LogInformation("Iniciando o método   " +
          $"{nameof(ExcluirCotacaoOutrasDespesasAsync)}  " +
          "com os seguintes parâmetros: {IdOutrasDespesas}", IdOutrasDespesas);

            _outrasDespesas.RemoveAsync(IdOutrasDespesas);

            _logger.LogInformation("Finalizando o método   " +
               $"{nameof(ExcluirCotacaoOutrasDespesasAsync)}  " +
               "com os seguintes parâmetros: {IdOutrasDespesas}", IdOutrasDespesas);
        }

        public async Task<int> InserirIdOutrasDespesasAsync(Outras_Despesas outras_Despesas)
        {
            int id = 0;

            try
            {
                _logger.LogInformation("Iniciando o método   " +
                 $"{nameof(InserirIdOutrasDespesasAsync)}  " +
                 "com os seguintes parâmetros: {Outras_Despesas}",
                 outras_Despesas);


                await _outrasDespesas.AddAsync(outras_Despesas);

                id = outras_Despesas.Id;

                _logger.LogInformation("Finalizando o método   " +
                 $"{nameof(InserirIdOutrasDespesasAsync)}  " +
                 "com os seguintes parâmetros: {Outras_Despesas}",
                 outras_Despesas);

            }
            catch (Exception ex)
            {
                _logger.LogError("Erro na execução do método " +
                  $"{nameof(InserirIdOutrasDespesasAsync)}   " +
                  " Com o erro = " + ex.Message);

                throw new Exception("Erro para realizar o cadastro de cotação");
            }

            return id;
        }

        public async Task<int> ListarIdOutrasDespesasAsync(string IdCotacao, string NomeDespesa)
        {
            int id = 0;

            try
            {
                _logger.LogInformation("Iniciando o método   " +
                 $"{nameof(ListarIdOutrasDespesasAsync)}  " +
                 "com os seguintes parâmetros: {IdCotacao}, {NomeDespesa}",
                IdCotacao, NomeDespesa);

                var dadosDespesa = await _outrasDespesas.GetAsync(x => x.IdCotacao == IdCotacao && x.NomeDespesa == NomeDespesa);

                if (dadosDespesa.Any())
                {

                    id = dadosDespesa.FirstOrDefault().Id;
                }

                _logger.LogInformation("Finalizando o método   " +
                 $"{nameof(ListarIdOutrasDespesasAsync)}  " +
                 "com os seguintes parâmetros: {IdCotacao}, {NomeDespesa}",
                 IdCotacao, NomeDespesa);

            }
            catch (Exception ex)
            {
                _logger.LogError("Erro na execução do método " +
                  $"{nameof(ListarIdOutrasDespesasAsync)}   " +
                  " Com o erro = " + ex.Message);

                throw new Exception("Erro para realizar o cadastro de cotação");
            }

            return id;
        }

        public async Task<Response<OutrasDespesasResponse>> ListarIdOutrasDespesasAsync(string IdCotacao)
        {
            OutrasDespesasResponse outrasDespesasResponse = new OutrasDespesasResponse();

            try
            {
                _logger.LogInformation("Iniciando o método   " +
                 $"{nameof(ListarIdOutrasDespesasAsync)}  " +
                 "com os seguintes parâmetros: {IdCotacao}",
                 IdCotacao);

                var dadosDespesa = await _outrasDespesas.GetAsync(x => x.IdCotacao == IdCotacao);

                if (dadosDespesa.Any())
                {

                    outrasDespesasResponse.Executado = true;
                    outrasDespesasResponse.MensagemRetorno = "Lista de despesa consultada com sucesso !";
                    outrasDespesasResponse.OutrasDespesas = dadosDespesa.ToList();
                }

                _logger.LogInformation("Finalizando o método   " +
                 $"{nameof(ListarIdOutrasDespesasAsync)}  " +
                 "com os seguintes parâmetros: {IdCotacao}",
                 IdCotacao);

            }
            catch (Exception ex)
            {
                _logger.LogError("Erro na execução do método " +
                  $"{nameof(ListarIdOutrasDespesasAsync)}   " +
                  " Com o erro = " + ex.Message);

                throw new Exception("Erro para realizar o cadastro de cotação");
            }

            return new Response<OutrasDespesasResponse>(outrasDespesasResponse, $"Lista Despesas.");
        }

        public async Task<Response<OutrasDespesasResponse>> ListarOutrasDespesasAsync(int Id, string IdCotacao)
        {
            OutrasDespesasResponse outrasDespesasResponse = new OutrasDespesasResponse();

            try
            {
                _logger.LogInformation("Iniciando o método   " +
                 $"{nameof(ListarIdOutrasDespesasAsync)}  " +
                 "com os seguintes parâmetros: {IdCotacao}",
                 IdCotacao);

                var dadosDespesa = await _outrasDespesas.GetAsync(x => x.IdCotacao == IdCotacao && x.Id == Id);

                if (dadosDespesa.Any())
                {

                    outrasDespesasResponse.Executado = true;
                    outrasDespesasResponse.MensagemRetorno = "Lista de despesa consultada com sucesso !";
                    outrasDespesasResponse.OutrasDespesasDados = dadosDespesa.FirstOrDefault();
                }

                _logger.LogInformation("Finalizando o método   " +
                 $"{nameof(ListarIdOutrasDespesasAsync)}  " +
                 "com os seguintes parâmetros: {IdCotacao}",
                 IdCotacao);

            }
            catch (Exception ex)
            {
                _logger.LogError("Erro na execução do método " +
                  $"{nameof(ListarIdOutrasDespesasAsync)}   " +
                  " Com o erro = " + ex.Message);

                throw new Exception("Erro para realizar o cadastro de cotação");
            }

            return new Response<OutrasDespesasResponse>(outrasDespesasResponse, $"Lista Despesas.");
        }
    }
}
