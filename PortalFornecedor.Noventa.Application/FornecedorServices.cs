using Microsoft.Extensions.Logging;
using PortalFornecedor.Noventa.Application.Services.Interfaces;
using PortalFornecedor.Noventa.Application.Services.Util;
using PortalFornecedor.Noventa.Application.Services.Wrappers;
using PortalFornecedor.Noventa.Data.Interfaces;
using PortalFornecedor.Noventa.Domain.Entities;
using PortalFornecedor.Noventa.Domain.Model;

namespace PortalFornecedor.Noventa.Application
{
    public class FornecedorServices : IFornecedorServices
    {

        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly ILogger<FornecedorServices> _logger;
        private readonly ILoginRepository _loginRepository;

        public FornecedorServices(IFornecedorRepository fornecedorRepository,
                                  ILogger<FornecedorServices> logger,
                                  ILoginRepository loginRepository)
        {
            _fornecedorRepository = fornecedorRepository;
            _logger = logger;
            _loginRepository = loginRepository;
        }

        public async Task<Response<FornecedorResponse>> AdicionarFornecedorAsync(FornecedorRequest fornecedorRequest)
        {
            FornecedorResponse fornecedorResponse = new FornecedorResponse();

            var dadosAcessoUsuario = await _loginRepository.GetByIdAsync(fornecedorRequest.fornecedor.Id);

            var htmlmessage = WriteMessageAtivacao();
            htmlmessage = htmlmessage.Replace("@nome", fornecedorRequest.fornecedor.RazaoSocial).Replace("@link", "#linkdaativacao");

            Utils.EnviarEmail(dadosAcessoUsuario.Email, "Ativacao de Senha", htmlmessage, true, null, null);

            _logger.LogInformation("Iniciando o método   " +
                 $"{nameof(AdicionarFornecedorAsync)}  " +
                "com os seguintes parâmetros: {fornecedorRequest}", fornecedorRequest);


                var verificaDadosFornecedor = await _fornecedorRepository.GetAsync(x => x.Id == fornecedorRequest.fornecedor.Id);

                if(!verificaDadosFornecedor.Any())
                {
                    fornecedorRequest.fornecedor.CnpjCpf = fornecedorRequest.fornecedor.CnpjCpf.Replace(".", "").Replace("-", "").Replace("/", "").Replace("-","");
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

        public async Task<Response<FornecedorResponse>> ListarDadosFornecedorAsync(string CnpjCpf)
        {
            FornecedorResponse fornecedorResponse = new FornecedorResponse();
            CnpjCpf = CnpjCpf.Replace(".", "").Replace("-", "").Replace("/", "").Replace("-","");

            
                _logger.LogInformation("Iniciando o método   " +
                $"{nameof(ListarDadosFornecedorAsync)}  " +
                "com os seguintes parâmetros: {CnpjCpf}", CnpjCpf);

                var dadosAcesso =  _fornecedorRepository.Get(x => x.CnpjCpf == CnpjCpf).FirstOrDefault();

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
                 "com os seguintes parâmetros: {CnpjCpf}", CnpjCpf);
            

            return new Response<FornecedorResponse>(fornecedorResponse, $"Dados Acesso.");
        }

        private string WriteMessageAtivacao()
        {
            string html = File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "emails\\emails\\ativacao.html"));
            return html;
        }
    }
}
