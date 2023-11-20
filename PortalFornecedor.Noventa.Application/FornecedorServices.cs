using Microsoft.Extensions.Logging;
using PortalFornecedor.Noventa.Application.Services.Interfaces;
using PortalFornecedor.Noventa.Application.Services.Wrappers;
using PortalFornecedor.Noventa.Data.Interfaces;
using PortalFornecedor.Noventa.Domain.Model;

namespace PortalFornecedor.Noventa.Application
{
    public class FornecedorServices : IFornecedorServices
    {

        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly ILogger<FornecedorServices> _logger;

        public FornecedorServices(IFornecedorRepository fornecedorRepository,
                                  ILogger<FornecedorServices> logger)
        {
            _fornecedorRepository = fornecedorRepository;
            _logger = logger;
        }

        public async Task<Response<FornecedorResponse>> AdicionarFornecedorAsync(FornecedorRequest fornecedorRequest)
        {
            FornecedorResponse fornecedorResponse = new FornecedorResponse();

            
                _logger.LogInformation("Iniciando o método   " +
                 $"{nameof(AdicionarFornecedorAsync)}  " +
                "com os seguintes parâmetros: {fornecedorRequest}", fornecedorRequest);


                var verificaDadosFornecedor = await _fornecedorRepository.GetAsync(x => x.Id == fornecedorRequest.fornecedor.Id);

                if(!verificaDadosFornecedor.Any())
                {
                    fornecedorRequest.fornecedor.CNPJ = fornecedorRequest.fornecedor.CNPJ.Replace(".", "").Replace("-", "").Replace("/", "");
                    await _fornecedorRepository.AddAsync(fornecedorRequest.fornecedor);

                    fornecedorResponse.fornecedor = fornecedorRequest.fornecedor;
                    fornecedorResponse.Executado = true;
                    fornecedorResponse.MensagemRetorno = "Dados do Cadastro do Fornecedor gravados com sucesso";
                }
                else
                {
                    fornecedorResponse.Executado = false;
                    fornecedorResponse.MensagemRetorno = "Fornecedor já está cadastrado no sistema";
                }

                _logger.LogInformation("Finalizando o método   " +
                $"{nameof(AdicionarFornecedorAsync)}  " +
               "com os seguintes parâmetros: {fornecedorRequest}", fornecedorRequest);
            

            return new Response<FornecedorResponse>(fornecedorResponse, $"Cadastro Dados Fornecedor.");
        }

        public async Task<Response<FornecedorResponse>> AtualizarDadosFornecedorAsync(FornecedorRequest fornecedorRequest)
        {
            FornecedorResponse fornecedorResponse = new FornecedorResponse();

            
                _logger.LogInformation("Iniciando o método   " +
                 $"{nameof(AtualizarDadosFornecedorAsync)}  " +
                "com os seguintes parâmetros: {fornecedorRequest}", fornecedorRequest);
                
                fornecedorRequest.fornecedor.CNPJ = fornecedorRequest.fornecedor.CNPJ.Replace(".", "").Replace("-", "").Replace("/", "");

                await _fornecedorRepository.UpdateAsync(fornecedorRequest.fornecedor);

                _logger.LogInformation("Finalizando o método   " +
                 $"{nameof(AtualizarDadosFornecedorAsync)}  " +
                "com os seguintes parâmetros: {fornecedorRequest}", fornecedorRequest);

                fornecedorResponse.fornecedor = fornecedorRequest.fornecedor;
                fornecedorResponse.Executado = true;
                fornecedorResponse.MensagemRetorno = "Dados do Cadastro do Fornecedor alterado com sucesso";
            

            return new Response<FornecedorResponse>(fornecedorResponse, $"AtualizarDadosFornecedorAsync Dados Fornecedor.");
        }

        public async Task<Response<FornecedorResponse>> ExcluirDadosFornecedorAsync(int id)
        {
            FornecedorResponse fornecedorResponse = new FornecedorResponse();

            
                _logger.LogInformation("Iniciando o método   " +
                 $"{nameof(ExcluirDadosFornecedorAsync)}  " +
                "com os seguintes parâmetros: {id}", id);

                await _fornecedorRepository.RemoveAsync(id);

                _logger.LogInformation("Finalizando o método   " +
                 $"{nameof(ExcluirDadosFornecedorAsync)}  " +
                "com os seguintes parâmetros: {id}", id);

                fornecedorResponse.Executado = true;
                fornecedorResponse.MensagemRetorno = "Dados do Cadastro do Fornecedor excluído com sucesso";
            

            return new Response<FornecedorResponse>(fornecedorResponse, $"ExcluirDadosFornecedorAsync Dados Fornecedor.");
        }

        public async Task<Response<FornecedorResponse>> ListarDadosFornecedorAsync()
        {
            FornecedorResponse fornecedorResponse = new FornecedorResponse();

            
                var listaDadosFornecedor = await _fornecedorRepository.GetAllAsync();

                if (listaDadosFornecedor.Any())
                {
                    fornecedorResponse.listaFornecedor = listaDadosFornecedor.ToList();
                    fornecedorResponse.Executado = true;
                    fornecedorResponse.MensagemRetorno = "Consulta do dados de fornecedor efetuado com sucesso";
                }
                else
                {
                    fornecedorResponse.Executado = false;
                    fornecedorResponse.MensagemRetorno = "Não existem cadastrado no sistema";
                }
           

            return new Response<FornecedorResponse>(fornecedorResponse, $"ListarDadosFornecedorAsync Dados Fornecedor.");
        }

        public async Task<Response<FornecedorResponse>> ListarDadosFornecedorAsync(int id)
        {
            FornecedorResponse fornecedorResponse = new FornecedorResponse();

            
                _logger.LogInformation("Iniciando o método   " +
                  $"{nameof(ListarDadosFornecedorAsync)}  " +
                 "com os seguintes parâmetros: {id}", id);

                var dadosFornecedor = _fornecedorRepository.GetAsync(x => x.Id == id).Result.FirstOrDefault();

                if (dadosFornecedor != null)
                {
                    fornecedorResponse.fornecedor = dadosFornecedor;
                    fornecedorResponse.Executado = true;
                    fornecedorResponse.MensagemRetorno = "Consulta do dados de fornecedor efetuado com sucesso";
                }
                else
                {
                    fornecedorResponse.Executado = false;
                    fornecedorResponse.MensagemRetorno = "Não existem cadastrado no sistema";
                }

                _logger.LogInformation("Finalizando o método   " +
                  $"{nameof(ListarDadosFornecedorAsync)}  " +
                 "com os seguintes parâmetros: {id}", id);
            

            return new Response<FornecedorResponse>(fornecedorResponse, $"ListarDadosFornecedorAsync Dados Fornecedor.");
        }

        public async Task<Response<FornecedorResponse>> ListarDadosFornecedorAsync(string cnpj)
        {
            FornecedorResponse fornecedorResponse = new FornecedorResponse();
            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");

            
                _logger.LogInformation("Iniciando o método   " +
                $"{nameof(ListarDadosFornecedorAsync)}  " +
                "com os seguintes parâmetros: {cnpj}", cnpj);

                var dadosAcesso =  _fornecedorRepository.Get(x => x.CNPJ == cnpj).FirstOrDefault();

                if (dadosAcesso != null)
                {

                    fornecedorResponse.fornecedor = dadosAcesso;
                    fornecedorResponse.Executado = true;
                }
                else
                {
                    fornecedorResponse.Executado = false;
                    fornecedorResponse.MensagemRetorno = "Dados incorretos, tente novamente!";
                }

                _logger.LogInformation("Finalizando o método   " +
                 $"{nameof(ListarDadosFornecedorAsync)}  " +
                 "com os seguintes parâmetros: {cnpj}", cnpj);
            

            return new Response<FornecedorResponse>(fornecedorResponse, $"Dados Acesso.");
        }
    }
}
