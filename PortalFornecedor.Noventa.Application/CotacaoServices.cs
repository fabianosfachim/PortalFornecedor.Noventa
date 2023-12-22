using Microsoft.Extensions.Logging;
using PortalFornecedor.Noventa.Application.Services.Interfaces;
using PortalFornecedor.Noventa.Application.Services.Util;
using PortalFornecedor.Noventa.Application.Services.Wrappers;
using PortalFornecedor.Noventa.Data.Interfaces;
using PortalFornecedor.Noventa.Data.Repositories.Entities;
using PortalFornecedor.Noventa.Domain.Entities;
using PortalFornecedor.Noventa.Domain.Model;
using System;

namespace PortalFornecedor.Noventa.Application
{
    public class CotacaoServices : ICotacaoServices
    {
        private readonly ILogger<CotacaoServices> _logger;
        private readonly IFornecedorServices _fornecedorServices;
        private readonly ICotacaoStatusServices _cotacaoStatusServices;
        private readonly IMotivoServices _motivoServices;
        private readonly ICotacaoDadosSolicitanteServices _cotacaoDadosSolicitanteServices;
        private readonly ICondicaoPagamentoServices _condicaoPagamentoServices;
        private readonly IfreteServices _ifreteServices;
        private readonly ICotacaoRepository _cotacaoRepository;
        private readonly IMaterialCotacaoRepository _materialCotacaoRepository;
        private readonly ILoginRepository _loginRepository;

        public CotacaoServices(ILogger<CotacaoServices> logger,
                               IFornecedorServices fornecedorServices,
                               ICotacaoStatusServices cotacaoStatusServices,
                               IMotivoServices motivoServices,
                               ICotacaoDadosSolicitanteServices cotacaoDadosSolicitanteServices,
                               ICondicaoPagamentoServices condicaoPagamentoServices,
                               IfreteServices ifreteServices,
                               ICotacaoRepository cotacaoRepository,
                               IMaterialCotacaoRepository materialCotacaoRepository,
                               ILoginRepository loginRepository)
        {
            _logger = logger;
            _fornecedorServices = fornecedorServices;
            _cotacaoStatusServices = cotacaoStatusServices;
            _motivoServices = motivoServices;
            _cotacaoDadosSolicitanteServices = cotacaoDadosSolicitanteServices;
            _condicaoPagamentoServices = condicaoPagamentoServices;
            _ifreteServices = ifreteServices;
            _cotacaoRepository = cotacaoRepository;
            _materialCotacaoRepository = materialCotacaoRepository;
            _loginRepository = loginRepository;
        }

        public async Task<Response<CotacaoResponse>> AdicionarCotacaoAsync(CotacaoRequest cotacaoRequest, string url)
        {
            CotacaoResponse cotacaoResponse = new CotacaoResponse();
            int idFornecedor = 0;
            int idStatus = 0;
            int idStatusCotacao = 0;
            int idMotivo = 0;
            int idMotivoCotacao = 0;
            int idCotacaoDadosSolicitante = 0;
            int i = 0;
            List<Material_Cotacao> materialCotacaoList = new List<Material_Cotacao>();
            Cotacao dadosCotacao =  new Cotacao();

            try
            {
                _logger.LogInformation("Iniciando o método   " +
                    $"{nameof(AdicionarCotacaoAsync)}  " +
                    "com os seguintes parâmetros: {cotacaoRequest}", cotacaoRequest);


                idFornecedor = await ListarIdFornecedorAsync(cotacaoRequest.CNPJ);

                if (idFornecedor == 0)
                {
                    cotacaoResponse.Executado = false;
                    cotacaoResponse.MensagemRetorno = "Não foi possível realizar o cadastro de cotação pois não existe CNPJ cadastrado no banco de dados!";

                    _logger.LogError("Erro na execução do método " +
                    $"{nameof(AdicionarCotacaoAsync)}   " +
                    " Com o erro = " + cotacaoResponse.MensagemRetorno);

                    return new Response<CotacaoResponse>(cotacaoResponse, $"Adicionar Cotacao.");
                }

                if (!string.IsNullOrEmpty(cotacaoRequest.CotacaoStatusDescricao))
                {
                    idStatus = await ListarIdStatusCotacaoAsync(cotacaoRequest.CotacaoStatusDescricao);

                    if (idStatus == 0)
                    {
                        cotacaoResponse.Executado = false;
                        cotacaoResponse.MensagemRetorno = "Não foi possível realizar o cadastro de cotação pois não existe o status da cotação cadastrado no banco de dados!";

                        _logger.LogError("Erro na execução do método " +
                         $"{nameof(AdicionarCotacaoAsync)}   " +
                         " Com o erro = " + cotacaoResponse.MensagemRetorno);

                        return new Response<CotacaoResponse>(cotacaoResponse, $"Adicionar Cotacao.");
                    }

                    idStatusCotacao = await ListarIdStatusCotacaoAsync(cotacaoRequest.ERPCotacao_Id, idStatus);


                    if (idStatusCotacao == 0)
                    {
                        cotacaoResponse.Executado = false;
                        cotacaoResponse.MensagemRetorno = "Não foi possível realizar o cadastro de cotação pois não existe o status da cotação cadastrado no banco de dados!";

                        RemoverDadosCotacao(cotacaoRequest.ERPCotacao_Id);

                        _logger.LogError("Erro na execução do método " +
                         $"{nameof(AdicionarCotacaoAsync)}   " +
                         " Com o erro = " + cotacaoResponse.MensagemRetorno);

                        return new Response<CotacaoResponse>(cotacaoResponse, $"Adicionar Cotacao.");
                    }
                }
                else
                {
                    cotacaoResponse.Executado = false;
                    cotacaoResponse.MensagemRetorno = "Não foi possível realizar o cadastro de cotação pois não existe o status da cotação cadastrado no banco de dados!";
                    return new Response<CotacaoResponse>(cotacaoResponse, $"Adicionar Cotacao.");
                }

                if (!string.IsNullOrEmpty(cotacaoRequest.motivo))
                {

                    idMotivo = await ListarIdMotivoCotacaoAsync(cotacaoRequest.motivo);

                    if (idMotivo == 0)
                    {
                        cotacaoResponse.Executado = false;
                        cotacaoResponse.MensagemRetorno = "Não foi possível realizar o cadastro de cotação pois não existe o motivo da cotação cadastrado no banco de dados!";

                        _logger.LogError("Erro na execução do método " +
                         $"{nameof(AdicionarCotacaoAsync)}   " +
                         " Com o erro = " + cotacaoResponse.MensagemRetorno);

                        return new Response<CotacaoResponse>(cotacaoResponse, $"Adicionar Cotacao.");
                    }

                    idMotivoCotacao = await ListarIdMotivoCotacaoAsync(cotacaoRequest.ERPCotacao_Id, idMotivo);

                    if (idMotivoCotacao == 0)
                    {
                        cotacaoResponse.Executado = false;
                        cotacaoResponse.MensagemRetorno = "Não foi possível realizar o cadastro de cotação pois não existe o motivo da cotação cadastrado no banco de dados!";

                        RemoverMotivoCotacao(cotacaoRequest.ERPCotacao_Id);

                        _logger.LogError("Erro na execução do método " +
                         $"{nameof(AdicionarCotacaoAsync)}   " +
                         " Com o erro = " + cotacaoResponse.MensagemRetorno);

                        return new Response<CotacaoResponse>(cotacaoResponse, $"Adicionar Cotacao.");
                    }
                }
                else
                {
                    RemoverDadosCotacao(cotacaoRequest.ERPCotacao_Id);
                    cotacaoResponse.Executado = false;
                    cotacaoResponse.MensagemRetorno = "Não foi possível realizar o cadastro de cotação pois não existe o motivo da cotação cadastrado no banco de dados!";
                    return new Response<CotacaoResponse>(cotacaoResponse, $"Adicionar Cotacao.");
                }

                idCotacaoDadosSolicitante = await ListarIdCotacaoDadosSolicitanteAsync(cotacaoRequest.cotacaoDadosSolicitante, cotacaoRequest.ERPCotacao_Id);

                if (idCotacaoDadosSolicitante == 0)
                {
                    cotacaoResponse.Executado = false;
                    cotacaoResponse.MensagemRetorno = "Não foi possível realizar o cadastro de cotação pois os dados do solicitante não foram cadastrado no banco de dados!";
                    RemoverDadosCotacao(cotacaoRequest.ERPCotacao_Id);

                    _logger.LogError("Erro na execução do método " +
                     $"{nameof(AdicionarCotacaoAsync)}   " +
                     " Com o erro = " + cotacaoResponse.MensagemRetorno);

                    return new Response<CotacaoResponse>(cotacaoResponse, $"Adicionar Cotacao.");
                }

                var retornoCondicaoPagamento = await ListarIdCotacaoCondicaoPagamentoAsync(cotacaoRequest.CondicoesPagamento);

                if (retornoCondicaoPagamento == false)
                {
                    cotacaoResponse.Executado = false;
                    cotacaoResponse.MensagemRetorno = "Não foi possível realizar o cadastro de cotação pois as condições de pagamento não foram cadastrado no banco de dados!";
                    RemoverDadosCotacao(cotacaoRequest.ERPCotacao_Id);

                    _logger.LogError("Erro na execução do método " +
                     $"{nameof(AdicionarCotacaoAsync)}   " +
                     " Com o erro = " + cotacaoResponse.MensagemRetorno);

                    return new Response<CotacaoResponse>(cotacaoResponse, $"Adicionar Cotacao.");
                }

                var frete = await ListarIdFreteAsync(cotacaoRequest.TipoFrete);

                if (frete == false)
                {
                    cotacaoResponse.Executado = false;
                    cotacaoResponse.MensagemRetorno = "Não foi possível realizar o cadastro de cotação pois o frete não foi cadastrado no banco de dados!";
                    RemoverDadosCotacao(cotacaoRequest.ERPCotacao_Id);

                    _logger.LogError("Erro na execução do método " +
                     $"{nameof(AdicionarCotacaoAsync)}   " +
                     " Com o erro = " + cotacaoResponse.MensagemRetorno);

                    return new Response<CotacaoResponse>(cotacaoResponse, $"Adicionar Cotacao.");
                }

                dadosCotacao = PreencherDadosCotacao(idFornecedor,
                                                     idMotivoCotacao,
                                                     idStatusCotacao,
                                                     0,
                                                     0,
                                                     0,
                                                     cotacaoRequest);

                var idCotacao = await _cotacaoRepository.GetAsync(x => x.IdCotacao == cotacaoRequest.ERPCotacao_Id && x.Fornecedor_Id == idFornecedor);

                if (!idCotacao.Any())
                {
                    var cotacao = await _cotacaoRepository.AddAsync(dadosCotacao);
                    var id = dadosCotacao.Id;

                    _logger.LogInformation("Acionando a cotação no banco de dados   " +
                    $"{nameof(AdicionarCotacaoAsync)}   " +
                    "Com o parâmetro {dadosCotacao}", dadosCotacao);

                    if (cotacaoRequest.MaterialCotacao != null && cotacaoRequest.MaterialCotacao.Any())
                    {
                        foreach (var item in cotacaoRequest.MaterialCotacao)
                        {
                            var dadosmaterialCotacao = PreencherDadosMaterialCotacao(cotacaoRequest, i, id);
                            materialCotacaoList.Add(dadosmaterialCotacao);
                            i = i + 1;
                        }

                        _logger.LogInformation("Acionando os dados de material de cotação no banco de dados   " +
                             $"{nameof(AdicionarCotacaoAsync)}   " +
                             "Com o parâmetro {materialCotacaoList}", materialCotacaoList);

                        foreach (var item in materialCotacaoList)
                        {
                            _materialCotacaoRepository.Add(item);
                        }
                    }
                }

                var dadosAcesso = await _fornecedorServices.ListarDadosFornecedorAsync(cotacaoRequest.CNPJ);
                var dadosLogin = _loginRepository.GetById(dadosAcesso.Data.fornecedor.Id);

                var htmlmessage = WriteMessageNovaCotacao();
                var link = url + dadosCotacao.Guid;

                htmlmessage = htmlmessage.Replace("@nome", dadosAcesso.Data.fornecedor.RazaoSocial).Replace("@link", link);

                Utils.EnviarEmail(dadosLogin.Email, "Nova Cotacao", htmlmessage, true, null, null);

            }
            catch (Exception ex)
            {
                _logger.LogError("Erro na execução do método " +
                  $"{nameof(AdicionarCotacaoAsync)}   " +
                  " Com o erro = " + ex.Message);

                RemoverDadosCotacao(cotacaoRequest.ERPCotacao_Id);
                cotacaoResponse.Executado = false;
                cotacaoResponse.MensagemRetorno = "Não foi possível realizar o cadastro de cotação!";
            }

            cotacaoResponse.Executado = true;
            cotacaoResponse.MensagemRetorno = "Cotação Cadastrada com Sucesso!";

            _logger.LogInformation("Finalizando o método   " +
                    $"{nameof(AdicionarCotacaoAsync)}  " +
                    "com os seguintes parâmetros: {cotacaoRequest}", cotacaoRequest);

            return new Response<CotacaoResponse>(cotacaoResponse, $"Adicionar Cotacao.");
        }

        public async Task<Response<CotacaoResponse>> AtualizarCotacaoAsync(AtualizarCotacaoRequest cotacaoRequest)
        {
            CotacaoResponse cotacaoResponse = new CotacaoResponse();
            List<Material_Cotacao> materialCotacaoList = new List<Material_Cotacao>();
            int i = 0;

            try
            {
                _logger.LogInformation("Iniciando o método   " +
                  $"{nameof(AtualizarCotacaoAsync)}  " +
                  "com os seguintes parâmetros: {cotacaoRequest}", cotacaoRequest);

                var dadosCotacao = AtualizarDadosCotacao(cotacaoRequest.Fornecedor_Id,
                                                         cotacaoRequest.Motivo_Id,
                                                         cotacaoRequest.CotacaoStatus_Id,
                                                         cotacaoRequest.CondicoesPagamento_Id,
                                                         cotacaoRequest.Frete_Id,
                                                         cotacaoRequest);


                await _cotacaoRepository.UpdateAsync(dadosCotacao);

                AtualizarStatusCotacao(cotacaoRequest.CotacaoStatus_Id, cotacaoRequest.ERPCotacao_Id, 2);

                if (cotacaoRequest.MaterialCotacao != null && cotacaoRequest.MaterialCotacao.Any())
                {
                    foreach (var item in cotacaoRequest.MaterialCotacao)
                    {
                        var dadosmaterialCotacao = AtualizarDadosMaterialCotacao(cotacaoRequest, i);
                        materialCotacaoList.Add(dadosmaterialCotacao);
                        i = i + 1;
                    }

                    _logger.LogInformation("Atulaizando os dados de material de cotação no banco de dados   " +
                         $"{nameof(AdicionarCotacaoAsync)}   " +
                         "Com o parâmetro {materialCotacaoList}", materialCotacaoList);

                    foreach (var item in materialCotacaoList)
                    {
                        await _materialCotacaoRepository.UpdateAsync(item);
                    }
                }

                _logger.LogInformation("Finalizar o método   " +
                $"{nameof(AtualizarCotacaoAsync)}  " +
                "com os seguintes parâmetros: {cotacaoRequest}", cotacaoRequest);

                cotacaoResponse.Executado = true;
                cotacaoResponse.MensagemRetorno = "Cotação Alterada com Sucesso!";
            }
            catch (Exception ex)
            {
                _logger.LogError("Erro na execução do método " +
                  $"{nameof(AtualizarCotacaoAsync)}   " +
                  " Com o erro = " + ex.Message);

                cotacaoResponse.Executado = false;
                cotacaoResponse.MensagemRetorno = "Não foi possível realizar a alteração do cadastro de cotação!";
            }


            return new Response<CotacaoResponse>(cotacaoResponse, $"Atualizar Cotacao.");
        }

        public async Task<Response<CotacaoResponse>> AtualizarStatusCotacaoAsync(int idCotacao, int idStatus)
        {
            CotacaoResponse cotacaoResponse = new CotacaoResponse();
            List<Material_Cotacao> materialCotacaoList = new List<Material_Cotacao>();
            int i = 0;

            try
            {
                _logger.LogInformation("Iniciando o método   " +
                  $"{nameof(AtualizarStatusCotacao)}  " +
                  "com os seguintes parâmetros: {idCotacao}, {idStatus}", idCotacao, idStatus);


                var cotacao = _cotacaoRepository.GetById(idCotacao);

                AtualizarStatusCotacao(cotacao.CotacaoStatus_Id, cotacao.IdCotacao, idStatus);

                _logger.LogInformation("Finalizando o método   " +
                  $"{nameof(AtualizarStatusCotacao)}  " +
                  "com os seguintes parâmetros: {idCotacao}, {idStatus}", idCotacao, idStatus);


                cotacaoResponse.Executado = true;
                cotacaoResponse.MensagemRetorno = "Status da Cotação Alterada com Sucesso!";
            }
            catch (Exception ex)
            {
                _logger.LogError("Erro na execução do método " +
                  $"{nameof(AtualizarCotacaoAsync)}   " +
                  " Com o erro = " + ex.Message);

                cotacaoResponse.Executado = false;
                cotacaoResponse.MensagemRetorno = "Não foi possível realizar a alteração do status de cotação!";
            }


            return new Response<CotacaoResponse>(cotacaoResponse, $"Atualizar Cotacao.");
        }

        public async Task<Response<CotacaoResponse>> ListarCotacaoAsync(string idCotacao, string cnpj)
        {
            int idFornecedor = 0;
            decimal subTotalItens = 0;
            CotacaoResponse cotacaoResponse = new CotacaoResponse();
            List<Material_Cotacao> materialCotacaoList = new List<Material_Cotacao>();

            try
            {
                _logger.LogInformation("Iniciando o método   " +
                    $"{nameof(ListarCotacaoAsync)}  " +
                    "com os seguintes parâmetros: {idCotacao}, {cnpj}", idCotacao, cnpj);

                idFornecedor = await ListarIdFornecedorAsync(cnpj);

                if (idFornecedor == 0)
                {
                    cotacaoResponse.Executado = false;
                    cotacaoResponse.MensagemRetorno = "Não existe fornecedor para o cnpj informado";
                    return new Response<CotacaoResponse>(cotacaoResponse, $"ListarCotacao.");
                }

                var cotacao = await _cotacaoRepository.GetAsync(x => x.IdCotacao == idCotacao && x.Fornecedor_Id == idFornecedor);

                if (!cotacao.Any())
                {
                    cotacaoResponse.Executado = false;
                    cotacaoResponse.MensagemRetorno = "Não existe cotação cadastrado no banco de dados";
                    return new Response<CotacaoResponse>(cotacaoResponse, $"ListarCotacao.");
                }

                var dadosSolicitante = await _cotacaoDadosSolicitanteServices.ListarDadosSolicitanteAsync(idCotacao);

                var materialCotacao = await _materialCotacaoRepository.GetAsync(x => x.Cotacao_Id == cotacao.FirstOrDefault().Id);

                if (materialCotacao.Any())
                {
                    foreach (var item in materialCotacao)
                    {
                        if (item.SubTotal != null && item.SubTotal > 0)
                        {
                            subTotalItens = subTotalItens + item.SubTotal.Value;
                        }
                        materialCotacaoList.Add(item);
                    }
                }

                cotacaoResponse.listarDadosCotacao = new ListarDadosCotacao();
                cotacaoResponse.listarDadosCotacao.dadosSolicitante = dadosSolicitante.Data.solicitante;
                cotacaoResponse.listarDadosCotacao.cotacao = PreencherDadosCotacao(cotacao.FirstOrDefault());
                cotacaoResponse.listarDadosCotacao.material = materialCotacaoList;

                cotacaoResponse.listarDadosCotacao.resumoCotacao = new ResumoCotacao();

                cotacaoResponse.listarDadosCotacao.resumoCotacao.subTotalItens = subTotalItens;

                if (cotacao.FirstOrDefault().OutrasDespesas != null)
                {
                    cotacaoResponse.listarDadosCotacao.resumoCotacao.OutrasDespesas = cotacao.FirstOrDefault().OutrasDespesas.Value;
                }
                else
                {
                    cotacaoResponse.listarDadosCotacao.resumoCotacao.OutrasDespesas = 0;
                }

                if (cotacao.FirstOrDefault().ValorFrete != null)
                {
                    cotacaoResponse.listarDadosCotacao.resumoCotacao.valorFrete = cotacao.FirstOrDefault().ValorFrete.Value;
                }
                else
                {
                    cotacaoResponse.listarDadosCotacao.resumoCotacao.valorFrete = 0;
                }

                if (cotacao.FirstOrDefault().ValorSeguro != null)
                {
                    cotacaoResponse.listarDadosCotacao.resumoCotacao.valorSeguro = cotacao.FirstOrDefault().ValorSeguro.Value;
                }
                else
                {
                    cotacaoResponse.listarDadosCotacao.resumoCotacao.valorSeguro = 0;
                }

                if (cotacao.FirstOrDefault().ValorDesconto != null)
                {
                    cotacaoResponse.listarDadosCotacao.resumoCotacao.ValorDesconto = cotacao.FirstOrDefault().ValorDesconto.Value;
                }
                else
                {
                    cotacaoResponse.listarDadosCotacao.resumoCotacao.ValorDesconto = 0;
                }


                if (!string.IsNullOrEmpty(cotacaoResponse.listarDadosCotacao.cotacao.NomeCondicaoPagamento))
                {
                    cotacaoResponse.listarDadosCotacao.resumoCotacao.formaPagamento = cotacaoResponse.listarDadosCotacao.cotacao.NomeCondicaoPagamento;
                }
                else
                {
                    cotacaoResponse.listarDadosCotacao.resumoCotacao.formaPagamento = string.Empty;
                }

                decimal valorFinalCotacao = ((subTotalItens + cotacao.FirstOrDefault().ValorFrete.Value +
                                             cotacaoResponse.listarDadosCotacao.resumoCotacao.valorSeguro +
                                             cotacaoResponse.listarDadosCotacao.resumoCotacao.OutrasDespesas) - (cotacaoResponse.listarDadosCotacao.resumoCotacao.ValorDesconto));

                cotacaoResponse.listarDadosCotacao.resumoCotacao.valorFinalCotacao = valorFinalCotacao;

                cotacaoResponse.Executado = true;
                cotacaoResponse.MensagemRetorno = "Cotação consultada com sucesso";

                _logger.LogInformation("Finalizando o método   " +
                   $"{nameof(ListarCotacaoAsync)}  " +
                   "com os seguintes parâmetros: {idCotacao}, {cnpj}", idCotacao, cnpj);

            }
            catch (Exception ex)
            {
                _logger.LogError("Erro na execução do método " +
                $"{nameof(ListarCotacaoAsync)}   " +
                " Com o erro = " + ex.Message);

                cotacaoResponse.Executado = false;
                cotacaoResponse.MensagemRetorno = "Não foi possível consultar esta cotação!";
            }

            return new Response<CotacaoResponse>(cotacaoResponse, $"ListarCotacao.");
        }

        public async Task<Response<CotacaoResponse>> ListarCotacaoAsync(int Id)
        {
            int idFornecedor = 0;
            decimal subTotalItens = 0;
            CotacaoResponse cotacaoResponse = new CotacaoResponse();
            List<Material_Cotacao> materialCotacaoList = new List<Material_Cotacao>();

            try
            {
                _logger.LogInformation("Iniciando o método   " +
                    $"{nameof(ListarCotacaoAsync)}  " +
                    "com os seguintes parâmetros: {Id}", Id);

                var cotacao = await _cotacaoRepository.GetAsync(x => x.Id == Id);

                if (cotacao == null && !cotacao.Any())
                {
                    cotacaoResponse.Executado = false;
                    cotacaoResponse.MensagemRetorno = "Não existe cotação cadastrado no banco de dados";
                    return new Response<CotacaoResponse>(cotacaoResponse, $"ListarCotacao.");
                }

                var dadosSolicitante = await _cotacaoDadosSolicitanteServices.ListarDadosSolicitanteAsync(cotacao.FirstOrDefault().IdCotacao);

                var materialCotacao = await _materialCotacaoRepository.GetAsync(x => x.Cotacao_Id == cotacao.FirstOrDefault().Id);

                if (materialCotacao.Any())
                {
                    foreach (var item in materialCotacao)
                    {
                        if (item.SubTotal != null && item.SubTotal > 0)
                        {
                            subTotalItens = subTotalItens + item.SubTotal.Value;
                        }
                        materialCotacaoList.Add(item);
                    }
                }

                cotacaoResponse.listarDadosCotacao = new ListarDadosCotacao();
                cotacaoResponse.listarDadosCotacao.dadosSolicitante = dadosSolicitante.Data.solicitante;

                cotacaoResponse.listarDadosCotacao.cotacao = PreencherDados(cotacao.FirstOrDefault()); 
                cotacaoResponse.listarDadosCotacao.material = materialCotacaoList;

                cotacaoResponse.listarDadosCotacao.resumoCotacao = new ResumoCotacao();

                cotacaoResponse.listarDadosCotacao.resumoCotacao.subTotalItens = subTotalItens;

                if (cotacao.FirstOrDefault().OutrasDespesas != null)
                {
                    cotacaoResponse.listarDadosCotacao.resumoCotacao.OutrasDespesas = cotacao.FirstOrDefault().OutrasDespesas.Value;
                }
                else
                {
                    cotacaoResponse.listarDadosCotacao.resumoCotacao.OutrasDespesas = 0;
                }

                if (cotacao.FirstOrDefault().ValorFrete != null)
                {
                    cotacaoResponse.listarDadosCotacao.resumoCotacao.valorFrete = cotacao.FirstOrDefault().ValorFrete.Value;
                }
                else
                {
                    cotacaoResponse.listarDadosCotacao.resumoCotacao.valorFrete = 0;
                }

                if (cotacao.FirstOrDefault().ValorSeguro != null)
                {
                    cotacaoResponse.listarDadosCotacao.resumoCotacao.valorSeguro = cotacao.FirstOrDefault().ValorSeguro.Value;
                }
                else
                {
                    cotacaoResponse.listarDadosCotacao.resumoCotacao.valorSeguro = 0;
                }

                if (cotacao.FirstOrDefault().ValorDesconto != null)
                {
                    cotacaoResponse.listarDadosCotacao.resumoCotacao.ValorDesconto = cotacao.FirstOrDefault().ValorDesconto.Value;
                }
                else
                {
                    cotacaoResponse.listarDadosCotacao.resumoCotacao.ValorDesconto = 0;
                }


                if (!string.IsNullOrEmpty(cotacaoResponse.listarDadosCotacao.cotacao.NomeCondicaoPagamento))
                {
                    cotacaoResponse.listarDadosCotacao.resumoCotacao.formaPagamento = cotacaoResponse.listarDadosCotacao.cotacao.NomeCondicaoPagamento;
                }
                else
                {
                    cotacaoResponse.listarDadosCotacao.resumoCotacao.formaPagamento = string.Empty;
                }

                decimal valorFinalCotacao = ((subTotalItens + cotacao.FirstOrDefault().ValorFrete.Value +
                                             cotacaoResponse.listarDadosCotacao.resumoCotacao.valorSeguro +
                                             cotacaoResponse.listarDadosCotacao.resumoCotacao.OutrasDespesas) - (cotacaoResponse.listarDadosCotacao.resumoCotacao.ValorDesconto));

                cotacaoResponse.listarDadosCotacao.resumoCotacao.valorFinalCotacao = valorFinalCotacao;

                cotacaoResponse.Executado = true;
                cotacaoResponse.MensagemRetorno = "Cotação consultada com sucesso";

                _logger.LogInformation("Finalizando o método   " +
                    $"{nameof(ListarCotacaoAsync)}  " +
                    "com os seguintes parâmetros: {Id}", Id);


            }
            catch (Exception ex)
            {
                _logger.LogError("Erro na execução do método " +
                $"{nameof(ListarCotacaoAsync)}   " +
                " Com o erro = " + ex.Message);

                cotacaoResponse.Executado = false;
                cotacaoResponse.MensagemRetorno = "Não foi possível consultar esta cotação!";
            }

            return new Response<CotacaoResponse>(cotacaoResponse, $"ListarCotacao.");
        }


        public async Task<Response<CotacaoResponse>> ListarCotacaoAsync(Guid guid)
        {
            int idFornecedor = 0;
            decimal subTotalItens = 0;
            CotacaoResponse cotacaoResponse = new CotacaoResponse();
            List<Material_Cotacao> materialCotacaoList = new List<Material_Cotacao>();

            try
            {
                _logger.LogInformation("Iniciando o método   " +
                    $"{nameof(ListarCotacaoAsync)}  " +
                    "com os seguintes parâmetros: {guid}", guid);

                var cotacao = await _cotacaoRepository.GetAsync(x => x.Guid == guid);

                if (cotacao == null && !cotacao.Any())
                {
                    cotacaoResponse.Executado = false;
                    cotacaoResponse.MensagemRetorno = "Não existe cotação cadastrado no banco de dados";
                    return new Response<CotacaoResponse>(cotacaoResponse, $"ListarCotacao.");
                }

                var dadosSolicitante = await _cotacaoDadosSolicitanteServices.ListarDadosSolicitanteAsync(cotacao.FirstOrDefault().IdCotacao);

                var materialCotacao = await _materialCotacaoRepository.GetAsync(x => x.Cotacao_Id == cotacao.FirstOrDefault().Id);

                if (materialCotacao.Any())
                {
                    foreach (var item in materialCotacao)
                    {
                        if (item.SubTotal != null && item.SubTotal > 0)
                        {
                            subTotalItens = subTotalItens + item.SubTotal.Value;
                        }
                        materialCotacaoList.Add(item);
                    }
                }

                cotacaoResponse.listarDadosCotacao = new ListarDadosCotacao();
                cotacaoResponse.listarDadosCotacao.dadosSolicitante = dadosSolicitante.Data.solicitante;

                cotacaoResponse.listarDadosCotacao.cotacao = PreencherDados(cotacao.FirstOrDefault());
                cotacaoResponse.listarDadosCotacao.material = materialCotacaoList;

                cotacaoResponse.listarDadosCotacao.resumoCotacao = new ResumoCotacao();

                cotacaoResponse.listarDadosCotacao.resumoCotacao.subTotalItens = subTotalItens;

                if (cotacao.FirstOrDefault().OutrasDespesas != null)
                {
                    cotacaoResponse.listarDadosCotacao.resumoCotacao.OutrasDespesas = cotacao.FirstOrDefault().OutrasDespesas.Value;
                }
                else
                {
                    cotacaoResponse.listarDadosCotacao.resumoCotacao.OutrasDespesas = 0;
                }

                if (cotacao.FirstOrDefault().ValorFrete != null)
                {
                    cotacaoResponse.listarDadosCotacao.resumoCotacao.valorFrete = cotacao.FirstOrDefault().ValorFrete.Value;
                }
                else
                {
                    cotacaoResponse.listarDadosCotacao.resumoCotacao.valorFrete = 0;
                }

                if (cotacao.FirstOrDefault().ValorSeguro != null)
                {
                    cotacaoResponse.listarDadosCotacao.resumoCotacao.valorSeguro = cotacao.FirstOrDefault().ValorSeguro.Value;
                }
                else
                {
                    cotacaoResponse.listarDadosCotacao.resumoCotacao.valorSeguro = 0;
                }

                if (cotacao.FirstOrDefault().ValorDesconto != null)
                {
                    cotacaoResponse.listarDadosCotacao.resumoCotacao.ValorDesconto = cotacao.FirstOrDefault().ValorDesconto.Value;
                }
                else
                {
                    cotacaoResponse.listarDadosCotacao.resumoCotacao.ValorDesconto = 0;
                }


                if (!string.IsNullOrEmpty(cotacaoResponse.listarDadosCotacao.cotacao.NomeCondicaoPagamento))
                {
                    cotacaoResponse.listarDadosCotacao.resumoCotacao.formaPagamento = cotacaoResponse.listarDadosCotacao.cotacao.NomeCondicaoPagamento;
                }
                else
                {
                    cotacaoResponse.listarDadosCotacao.resumoCotacao.formaPagamento = string.Empty;
                }

                decimal valorFinalCotacao = ((subTotalItens + cotacao.FirstOrDefault().ValorFrete.Value +
                                             cotacaoResponse.listarDadosCotacao.resumoCotacao.valorSeguro +
                                             cotacaoResponse.listarDadosCotacao.resumoCotacao.OutrasDespesas) - (cotacaoResponse.listarDadosCotacao.resumoCotacao.ValorDesconto));

                cotacaoResponse.listarDadosCotacao.resumoCotacao.valorFinalCotacao = valorFinalCotacao;

                cotacaoResponse.Executado = true;
                cotacaoResponse.MensagemRetorno = "Cotação consultada com sucesso";

                _logger.LogInformation("Finalizando o método   " +
                    $"{nameof(ListarCotacaoAsync)}  " +
                    "com os seguintes parâmetros: {guid}", guid);


            }
            catch (Exception ex)
            {
                _logger.LogError("Erro na execução do método " +
                $"{nameof(ListarCotacaoAsync)}   " +
                " Com o erro = " + ex.Message);

                cotacaoResponse.Executado = false;
                cotacaoResponse.MensagemRetorno = "Não foi possível consultar esta cotação!";
            }

            return new Response<CotacaoResponse>(cotacaoResponse, $"ListarCotacao.");
        }

        public async Task<Response<CotacaoDetalheFiltroResponse>> ListarCotacaoAsync(CotacaoDetalheFiltroRequest cotacaoDetalheFiltroRequest)
        {

            CotacaoDetalheFiltroResponse cotacaoResponse = new CotacaoDetalheFiltroResponse();
            List<ListaFiltroCotacao> listaFiltroCotacao = new List<ListaFiltroCotacao>();

            bool retornoSolicitante = true;
            bool retornoStatus = true;
            bool retornoMotivo = true;
            bool retornoPeriodo = true;
            bool controleStatus = false;
            bool controleMotivo = false;

            try
            {
                _logger.LogInformation("Iniciando o método   " +
                    $"{nameof(ListarCotacaoAsync)}  " +
                    "com os seguintes parâmetros: {cotacaoDetalheFiltroRequest}", cotacaoDetalheFiltroRequest);

                var cotacao = await _cotacaoRepository.GetAsync(x => x.Fornecedor_Id == cotacaoDetalheFiltroRequest.IdFornecedor);

                if (cotacao == null && !cotacao.Any())
                {
                    cotacaoResponse.Executado = false;
                    cotacaoResponse.MensagemRetorno = "Não existe cotações cadastrados no banco de dados";
                    return new Response<CotacaoDetalheFiltroResponse>(cotacaoResponse, $"ListarCotacao.");
                }

                foreach (var item in cotacao)
                {

                    var dadosSolicitante = await _cotacaoDadosSolicitanteServices.ListarDadosSolicitanteAsync(item.IdCotacao);

                    string solicitante = dadosSolicitante.Data.solicitante.Nome;
                    string localDestino = dadosSolicitante.Data.solicitante.Cidade + " (" + dadosSolicitante.Data.solicitante.Estado + ")";
                    DateTime dataSolicitacao = dadosSolicitante.Data.solicitante.DataSolicitacao.Value;
                    DateTime dataEntrega = dadosSolicitante.Data.solicitante.DataEntrega.Value;

                    var DadosMotivo = _motivoServices.ListarMotivoAsync(item.Motivo_Id).Result;
                    string motivo = DadosMotivo.Data.MotivoDados.NomeMotivo;
                    string contato = dadosSolicitante.Data.solicitante.Contato;

                    var DadosStatus = _cotacaoStatusServices.ListarCotacaoAsync(item.CotacaoStatus_Id).Result;
                    string status = DadosStatus.Data.StatusDados.NomeStatus;


                    if (!string.IsNullOrEmpty(cotacaoDetalheFiltroRequest.solicitacao))
                    {
                        if (solicitante.Contains(cotacaoDetalheFiltroRequest.solicitacao) || localDestino.Contains(cotacaoDetalheFiltroRequest.solicitacao))
                        {
                            retornoSolicitante = true;
                        }
                        else
                        {
                            retornoSolicitante = false;
                        }
                    }

                    if (cotacaoDetalheFiltroRequest.statusId != null)
                    {
                        foreach(var itemStatus in cotacaoDetalheFiltroRequest.statusId)
                        {
                            if (itemStatus == DadosStatus.Data.StatusDados.Id)
                            {
                                controleStatus = true;
                                break;
                            }
                        }
                        
                        if (controleStatus == false)
                        {
                            retornoStatus = false;
                        }
                    }

                    if (cotacaoDetalheFiltroRequest.motivoId != null)
                    {
                        foreach(var itemMotivo in cotacaoDetalheFiltroRequest.motivoId)
                        {
                            if (itemMotivo == DadosMotivo.Data.MotivoDados.Id)
                            {
                                controleMotivo = true;
                                break;
                            }
                        }

                        if (controleMotivo == false)
                        {
                            retornoMotivo = false;
                        }
                    }

                    if (cotacaoDetalheFiltroRequest.dataInicio != null && cotacaoDetalheFiltroRequest.dataTermino != null)
                    {
                        bool retornoData = Utils.Between(DateTime.Parse(dataSolicitacao.ToString("yyyy-MM-dd")), 
                                                  DateTime.Parse(cotacaoDetalheFiltroRequest.dataInicio.Value.ToString("yyyy-MM-dd")), 
                                                  DateTime.Parse(cotacaoDetalheFiltroRequest.dataTermino.Value.ToString("yyyy-MM-dd")));

                        if (retornoData == true)
                        {
                            retornoPeriodo = true;
                        }
                        else
                        {
                            retornoPeriodo = false;
                        }
                    }

                    if (retornoSolicitante == true && retornoStatus == true && retornoMotivo == true && retornoPeriodo == true)
                    {
                        ListaFiltroCotacao objFiltroCotacao = new ListaFiltroCotacao();

                        objFiltroCotacao.id = item.Id;
                        objFiltroCotacao.solicitante = solicitante;
                        objFiltroCotacao.localDestino = localDestino;
                        objFiltroCotacao.dataSolicitacao = dataSolicitacao;
                        objFiltroCotacao.dataEntrega = dataEntrega;
                        objFiltroCotacao.motivo = motivo;
                        objFiltroCotacao.contato = contato;
                        objFiltroCotacao.status = status;

                        listaFiltroCotacao.Add(objFiltroCotacao);
                    }
                }

                cotacaoResponse.listaFiltroCotacaos = listaFiltroCotacao;
                cotacaoResponse.Executado = true;
                cotacaoResponse.MensagemRetorno = "Cotação consultada com sucesso";

                _logger.LogInformation("Finalizando o método   " +
                       $"{nameof(ListarCotacaoAsync)}  " +
                       "com os seguintes parâmetros: {cotacaoDetalheFiltroRequest}", cotacaoDetalheFiltroRequest);


            }
            catch (Exception ex)
            {
                _logger.LogError("Erro na execução do método " +
                $"{nameof(ListarCotacaoAsync)}   " +
                " Com o erro = " + ex.Message);

                cotacaoResponse.Executado = false;
                cotacaoResponse.MensagemRetorno = "Não foi possível consultar esta cotação!";
            }

            return new Response<CotacaoDetalheFiltroResponse>(cotacaoResponse, $"ListarCotacao.");
        }

        #region TabelasAuxiliaresCotacao


        private CotacaoDetalhe PreencherDados(Cotacao cotacao)
        {
            CotacaoDetalhe cotacaoDetalhe = new CotacaoDetalhe();

            cotacaoDetalhe.Id = cotacao.Id;
            cotacaoDetalhe.Fornecedor_Id = cotacao.Fornecedor_Id;

            var fornecedor = _fornecedorServices.ListarDadosFornecedorAsync(cotacao.Fornecedor_Id).Result;
            cotacaoDetalhe.NomeFornecedor = fornecedor.Data.fornecedor.RazaoSocial;
            cotacaoDetalhe.IdCotacao = cotacao.IdCotacao;
            cotacaoDetalhe.Motivo_Id = cotacao.Motivo_Id;

            if (cotacao.Motivo_Id != null)
            {
                var motivo = _motivoServices.ListarMotivoAsync(cotacao.Motivo_Id).Result;
                cotacaoDetalhe.NomeMotivo = motivo.Data.MotivoDados.NomeMotivo;
            }

            cotacaoDetalhe.CotacaoStatus_Id = cotacao.CotacaoStatus_Id;

            if (cotacaoDetalhe.CotacaoStatus_Id != null)
            {
                var status = _cotacaoStatusServices.ListarCotacaoAsync(cotacaoDetalhe.CotacaoStatus_Id).Result;
                cotacaoDetalhe.NomeStatus = status.Data.StatusDados.NomeStatus;
            }

            cotacaoDetalhe.Vendedor = cotacao.Vendedor;
            cotacaoDetalhe.DataPostagem = cotacao.DataPostagem;
            cotacaoDetalhe.CondicoesPagamento_Id = cotacao.CondicoesPagamento_Id;

            if (cotacao.CondicoesPagamento_Id != null)
            {
                var condicaoPagamento = _condicaoPagamentoServices.ListarCondicaoPagamentoAsync(cotacao.CondicoesPagamento_Id.Value, cotacao.IdCotacao).Result;
                cotacaoDetalhe.NomeCondicaoPagamento = condicaoPagamento.Data.PagamentosDados.StatusCondicoesPagamento;
            }

            cotacaoDetalhe.Frete_Id = cotacao.Frete_Id;

            if (cotacaoDetalhe.Frete_Id != null)
            {
                var frete = _ifreteServices.ListarFreteAsync(cotacao.Frete_Id.Value, cotacao.IdCotacao).Result;
                cotacaoDetalhe.NomeFrete = frete.Data.FreteDados.TipoFrete;
            }

            cotacaoDetalhe.ValorFrete = cotacao.ValorFrete;
            cotacaoDetalhe.ValorFreteForaNota = cotacao.ValorFreteForaNota;
            cotacaoDetalhe.ValorSeguro = cotacao.ValorSeguro;
            cotacaoDetalhe.ValorDesconto = cotacao.ValorDesconto;
            cotacaoDetalhe.DataEntregaDesejavel = cotacao.DataEntregaDesejavel;
            cotacaoDetalhe.PrazoMaximoCotacao = cotacao.PrazoMaximoCotacao;
            cotacaoDetalhe.Observacao = cotacao.Observacao;
            cotacaoDetalhe.NomeUsuarioCadastro = cotacao.NomeUsuarioCadastro;
            cotacaoDetalhe.DataCadastro = cotacao.DataCadastro;
            cotacaoDetalhe.NomeUsuarioAlteracao = cotacao.NomeUsuarioAlteracao;
            cotacaoDetalhe.DataAlteracao = cotacao.DataAlteracao;

            return cotacaoDetalhe;
        }

        private CotacaoDetalhe PreencherDadosCotacao(Cotacao cotacao)
        {
            CotacaoDetalhe cotacaoDetalhe = new CotacaoDetalhe();

            cotacaoDetalhe.Id = cotacao.Id;
            cotacaoDetalhe.Fornecedor_Id = cotacao.Fornecedor_Id;

            var fornecedor = _fornecedorServices.ListarDadosFornecedorAsync(cotacao.Fornecedor_Id).Result;
            cotacaoDetalhe.NomeFornecedor = fornecedor.Data.fornecedor.RazaoSocial;
            cotacaoDetalhe.IdCotacao = cotacao.IdCotacao;
            cotacaoDetalhe.Motivo_Id = cotacao.Motivo_Id;

            if (cotacao.Motivo_Id != null)
            {
                var motivo = _motivoServices.ListarMotivoAsync(cotacao.Motivo_Id).Result;
                cotacaoDetalhe.NomeMotivo = motivo.Data.MotivoDados.NomeMotivo;
            }

            cotacaoDetalhe.CotacaoStatus_Id = cotacao.CotacaoStatus_Id;

            if (cotacaoDetalhe.CotacaoStatus_Id != null)
            {
                var status = _cotacaoStatusServices.ListarCotacaoAsync(cotacaoDetalhe.CotacaoStatus_Id).Result;
                cotacaoDetalhe.NomeStatus = status.Data.StatusDados.NomeStatus;
            }

            cotacaoDetalhe.Vendedor = cotacao.Vendedor;
            cotacaoDetalhe.DataPostagem = cotacao.DataPostagem;
            cotacaoDetalhe.CondicoesPagamento_Id = cotacao.CondicoesPagamento_Id;

            if (cotacao.CondicoesPagamento_Id != null)
            {
                var condicaoPagamento = _condicaoPagamentoServices.ListarCondicaoPagamentoAsync(cotacao.CondicoesPagamento_Id.Value, cotacao.IdCotacao).Result;
                cotacaoDetalhe.NomeCondicaoPagamento = condicaoPagamento.Data.PagamentosDados.StatusCondicoesPagamento;
            }

            cotacaoDetalhe.Frete_Id = cotacao.Frete_Id;

            if (cotacaoDetalhe.Frete_Id != null)
            {
                var frete = _ifreteServices.ListarFreteAsync(cotacao.Frete_Id.Value, cotacao.IdCotacao).Result;
                cotacaoDetalhe.NomeFrete = frete.Data.FreteDados.TipoFrete;
            }

            cotacaoDetalhe.ValorFrete = cotacao.ValorFrete;
            cotacaoDetalhe.ValorFreteForaNota = cotacao.ValorFreteForaNota;
            cotacaoDetalhe.ValorSeguro = cotacao.ValorSeguro;
            cotacaoDetalhe.ValorDesconto = cotacao.ValorDesconto;
            cotacaoDetalhe.DataEntregaDesejavel = cotacao.DataEntregaDesejavel;
            cotacaoDetalhe.PrazoMaximoCotacao = cotacao.PrazoMaximoCotacao;
            cotacaoDetalhe.Observacao = cotacao.Observacao;
            cotacaoDetalhe.NomeUsuarioCadastro = cotacao.NomeUsuarioCadastro;
            cotacaoDetalhe.DataCadastro = cotacao.DataCadastro;
            cotacaoDetalhe.NomeUsuarioAlteracao = cotacao.NomeUsuarioAlteracao;
            cotacaoDetalhe.DataAlteracao = cotacao.DataAlteracao;

            return cotacaoDetalhe;
        }

        private Material_Cotacao PreencherDadosMaterialCotacao(CotacaoRequest cotacaoRequest, int indice, int cotacaoId)
        {
            Material_Cotacao materialCotacao = new Material_Cotacao();


            materialCotacao.Material_Id = cotacaoRequest.MaterialCotacao[indice].Id;
            materialCotacao.Descricao = cotacaoRequest.MaterialCotacao[indice].Descricao;
            materialCotacao.Cotacao_Id = cotacaoId;
            materialCotacao.NomeFabricante = cotacaoRequest.MaterialCotacao[indice].NomeFabricante;
            materialCotacao.QuantidadeRequisitada = cotacaoRequest.MaterialCotacao[indice].QuantidadeRequisitada;
            materialCotacao.PrecoUnitario = cotacaoRequest.MaterialCotacao[indice].PrecoUnitario;
            materialCotacao.PercentualDesconto = cotacaoRequest.MaterialCotacao[indice].PercentualDesconto;
            materialCotacao.IpiIncluso = cotacaoRequest.MaterialCotacao[indice].IpiIncluso;
            materialCotacao.PercentualIpi = cotacaoRequest.MaterialCotacao[indice].PercentualIpi;
            materialCotacao.ValorIpi = cotacaoRequest.MaterialCotacao[indice].ValorIpi;
            materialCotacao.PercentualIcms = cotacaoRequest.MaterialCotacao[indice].PercentualIcms;
            materialCotacao.PrazoEntrega = cotacaoRequest.MaterialCotacao[indice].PrazoEntrega;
            materialCotacao.Marca = cotacaoRequest.MaterialCotacao[indice].Marca;
            materialCotacao.SubTotal = cotacaoRequest.MaterialCotacao[indice].SubTotal;
            materialCotacao.Ativo = false;

            return materialCotacao;
        }

        private Material_Cotacao AtualizarDadosMaterialCotacao(AtualizarCotacaoRequest cotacaoRequest, int indice)
        {
            Material_Cotacao materialCotacao = new Material_Cotacao();


            materialCotacao.Id = cotacaoRequest.MaterialCotacao[indice].Id;
            materialCotacao.Material_Id = cotacaoRequest.MaterialCotacao[indice].Material_Id;
            materialCotacao.Descricao = cotacaoRequest.MaterialCotacao[indice].Descricao;
            materialCotacao.Cotacao_Id = cotacaoRequest.Id;
            materialCotacao.NomeFabricante = cotacaoRequest.MaterialCotacao[indice].NomeFabricante;
            materialCotacao.QuantidadeRequisitada = cotacaoRequest.MaterialCotacao[indice].QuantidadeRequisitada;
            materialCotacao.PrecoUnitario = cotacaoRequest.MaterialCotacao[indice].PrecoUnitario;
            materialCotacao.PercentualDesconto = cotacaoRequest.MaterialCotacao[indice].PercentualDesconto;
            materialCotacao.IpiIncluso = cotacaoRequest.MaterialCotacao[indice].IpiIncluso;
            materialCotacao.PercentualIpi = cotacaoRequest.MaterialCotacao[indice].PercentualIpi;
            materialCotacao.ValorIpi = cotacaoRequest.MaterialCotacao[indice].ValorIpi;
            materialCotacao.PercentualIcms = cotacaoRequest.MaterialCotacao[indice].PercentualIcms;
            materialCotacao.PrazoEntrega = cotacaoRequest.MaterialCotacao[indice].PrazoEntrega;
            materialCotacao.Marca = cotacaoRequest.MaterialCotacao[indice].Marca;
            materialCotacao.SubTotal = cotacaoRequest.MaterialCotacao[indice].SubTotal;
            materialCotacao.Ativo = cotacaoRequest.MaterialCotacao[indice].Ativo;
            return materialCotacao;
        }

        private Cotacao PreencherDadosCotacao(int idFornecedor, int idMotivoCotacao, int idStatusCotacao,
                                              int idFrete, int idCondicaoPagamento, int idOutrasDespesas,
                                              CotacaoRequest cotacaoRequest)
        {
            Cotacao cotacao = new Cotacao();


            if (idFornecedor > 0)
            {
                cotacao.Fornecedor_Id = idFornecedor;
            }

            cotacao.IdCotacao = cotacaoRequest.ERPCotacao_Id;

            if (idMotivoCotacao > 0)
            {
                cotacao.Motivo_Id = idMotivoCotacao;
            }

            if (idStatusCotacao > 0)
            {
                cotacao.CotacaoStatus_Id = idStatusCotacao;
            }

            cotacao.Vendedor = cotacaoRequest.Vendedor;
            cotacao.DataPostagem = cotacaoRequest.DataPostagem;

            if (idCondicaoPagamento > 0)
            {
                cotacao.CondicoesPagamento_Id = idCondicaoPagamento;
            }

            if (idFrete > 0)
            {
                cotacao.Frete_Id = idFrete;
            }

            cotacao.OutrasDespesas = cotacaoRequest.OutrasDespesas;
            cotacao.ValorFrete = cotacaoRequest.ValorFrete;
            cotacao.ValorFreteForaNota = cotacaoRequest.ValorFreteForaNota;
            cotacao.ValorSeguro = cotacaoRequest.ValorSeguro;
            cotacao.ValorDesconto = cotacaoRequest.ValorDesconto;
            cotacao.PrazoMaximoCotacao = cotacaoRequest.PrazoMaximoCotacao;
            cotacao.DataEntregaDesejavel = cotacaoRequest.DataEntregaDesejavel;
            cotacao.Observacao = cotacaoRequest.Observacao;
            cotacao.NomeUsuarioCadastro = cotacaoRequest.NomeUsuarioCadastro;
            cotacao.DataCadastro = cotacaoRequest.DataCadastro;
            cotacao.NomeUsuarioAlteracao = cotacaoRequest.NomeUsuarioAlteracao;
            cotacao.DataAlteracao = cotacaoRequest.DataAlteracao;
            cotacao.Guid = Guid.NewGuid();

            return cotacao;
        }

        private Cotacao AtualizarDadosCotacao(int idFornecedor, int idMotivoCotacao, int idStatusCotacao,
                                              int idFrete, int idCondicaoPagamento, AtualizarCotacaoRequest cotacaoRequest)
        {
            Cotacao cotacao = new Cotacao();

            cotacao.Id = cotacaoRequest.Id;

            if (idFornecedor > 0)
            {
                cotacao.Fornecedor_Id = idFornecedor;
            }

            cotacao.IdCotacao = cotacaoRequest.ERPCotacao_Id;

            if (idMotivoCotacao > 0)
            {
                cotacao.Motivo_Id = idMotivoCotacao;
            }

            if (idStatusCotacao > 0)
            {
                cotacao.CotacaoStatus_Id = idStatusCotacao;
            }

            cotacao.Vendedor = cotacaoRequest.Vendedor;
            cotacao.DataPostagem = cotacaoRequest.DataPostagem;

            if (idCondicaoPagamento > 0)
            {
                cotacao.CondicoesPagamento_Id = idCondicaoPagamento;
            }

            if (idFrete > 0)
            {
                cotacao.Frete_Id = idFrete;
            }

            cotacao.OutrasDespesas = cotacaoRequest.OutrasDespesas;
            cotacao.ValorFrete = cotacaoRequest.ValorFrete;
            cotacao.ValorFreteForaNota = cotacaoRequest.ValorFreteForaNota;
            cotacao.ValorSeguro = cotacaoRequest.ValorSeguro;
            cotacao.ValorDesconto = cotacaoRequest.ValorDesconto;
            cotacao.PrazoMaximoCotacao = cotacaoRequest.PrazoMaximoCotacao;
            cotacao.DataEntregaDesejavel = cotacaoRequest.DataEntregaDesejavel;
            cotacao.Observacao = cotacaoRequest.Observacao;
            cotacao.NomeUsuarioCadastro = cotacaoRequest.NomeUsuarioCadastro;
            cotacao.DataCadastro = cotacaoRequest.DataCadastro;
            cotacao.NomeUsuarioAlteracao = cotacaoRequest.NomeUsuarioAlteracao;
            cotacao.DataAlteracao = cotacaoRequest.DataAlteracao;
            cotacao.Guid = cotacaoRequest.Guid;

            return cotacao;
        }

        private async Task<int> ListarIdFornecedorAsync(string cnpj)
        {
            int idFornecedor = 0;

            try
            {
                var dadosFornecedor = await _fornecedorServices.ListarDadosFornecedorAsync(cnpj);

                if (dadosFornecedor.Data != null)
                {
                    if (dadosFornecedor.Data.Executado == true)
                    {
                        if (dadosFornecedor.Data.fornecedor != null)
                        {
                            idFornecedor = dadosFornecedor.Data.fornecedor.Id;
                        }
                    }
                }
            }
            catch
            {
                return idFornecedor;
            }

            return idFornecedor;
        }
        private async Task<int> ListarIdStatusCotacaoAsync(string StatusCotacao)
        {
            int idStatus = 0;

            try
            {
                var dadosStatusCotacao = await _cotacaoStatusServices.ListarCotacaoStatusIdAsync(StatusCotacao);

                if (dadosStatusCotacao > 0)
                {
                    idStatus = dadosStatusCotacao;
                }
            }
            catch (Exception ex)
            {
                return idStatus;
            }

            return idStatus;
        }
        private async Task<int> ListarIdStatusCotacaoAsync(string IdCotacao, int idStatus)
        {
            int idcotacaoStatus_Id = 0;

            try
            {
                var dadosStatusCotacao = await _cotacaoStatusServices.ListarCotacaoStatusIdAsync(IdCotacao, idStatus);

                if (dadosStatusCotacao > 0)
                {
                    idcotacaoStatus_Id = dadosStatusCotacao;
                }
            }
            catch
            {
                return idcotacaoStatus_Id;
            }

            return idcotacaoStatus_Id;
        }
        private void RemoverStatusCotacao(string IdCotacao)
        {
            var IdStatusCotacao = _cotacaoStatusServices.ListarStatusIdAsync(IdCotacao).Result;
            if (IdStatusCotacao > 0)
            {
                _cotacaoStatusServices.ExcluirCotacaoStatusAsync(IdStatusCotacao);
            }
        }
        private void AtualizarStatusCotacao(int id, string IdCotacao, int idStatus)
        {
            Cotacao_Status cotacao_Status = new Cotacao_Status();
            cotacao_Status.Id = id;
            cotacao_Status.IdCotacao = IdCotacao;
            cotacao_Status.IdStatus = idStatus;
            cotacao_Status.DataStatus = DateTime.Now;

            _cotacaoStatusServices.AtualizarCotacaoStatusAsync(cotacao_Status);
        }
        private async Task<int> ListarIdMotivoCotacaoAsync(string motivo)
        {
            int idcotacaoMotivo_Id = 0;

            try
            {
                var dadosStatusCotacao = await _motivoServices.ListarCotacaoMotivoIdAsync(motivo);

                if (dadosStatusCotacao > 0)
                {
                    idcotacaoMotivo_Id = dadosStatusCotacao;
                }
            }
            catch (Exception ex)
            {
                return idcotacaoMotivo_Id;
            }

            return idcotacaoMotivo_Id;
        }
        private async Task<int> ListarIdMotivoCotacaoAsync(string IdCotacao, int Idmotivo)
        {
            int idcotacaoMotivo_Id = 0;

            try
            {
                var dadosStatusCotacao = await _motivoServices.ListarCotacaoMotivoIdAsync(IdCotacao, Idmotivo);

                if (dadosStatusCotacao > 0)
                {
                    idcotacaoMotivo_Id = dadosStatusCotacao;
                }
            }
            catch (Exception ex)
            {
                return idcotacaoMotivo_Id;
            }

            return idcotacaoMotivo_Id;
        }
        private void RemoverMotivoCotacao(string IdCotacao)
        {
            var IdMotivoCotacao = _motivoServices.ListarIdCotacaoMotivoAsync(IdCotacao).Result;
            if (IdMotivoCotacao > 0)
            {
                _motivoServices.ExcluirCotacaoMotivoAsync(IdMotivoCotacao);
            }
        }
        private void RemoverDadosCotacao(string IdCotacao)
        {
            RemoverStatusCotacao(IdCotacao);
            RemoverMotivoCotacao(IdCotacao);
            RemoverCotacaoDadosSolicitante(IdCotacao);
            RemoverCotacaoCondicaoPagamento(IdCotacao);
            RemoverCotacaoFrete(IdCotacao);
        }
        private async Task<int> ListarIdCotacaoDadosSolicitanteAsync(CotacaoDadosSolicitanteRequest cotacaoDadosSolicitanteRequest, string IdCotacao)
        {
            int idCotacaoDadosSolicitante = 0;

            try
            {
                idCotacaoDadosSolicitante = _cotacaoDadosSolicitanteServices.ListarIdCotacaoDadosSolicitanteAsync(IdCotacao).Result;

                if (idCotacaoDadosSolicitante == 0)
                {
                    idCotacaoDadosSolicitante = await _cotacaoDadosSolicitanteServices.InserirIdCotacaoDadosSolicitanteAsync(cotacaoDadosSolicitanteRequest);
                }

            }
            catch
            {
                return idCotacaoDadosSolicitante;
            }

            return idCotacaoDadosSolicitante;
        }
        private void RemoverCotacaoDadosSolicitante(string IdCotacao)
        {
            var idCotacaoDadosSolicitante = _cotacaoDadosSolicitanteServices.ListarIdCotacaoDadosSolicitanteAsync(IdCotacao).Result;
            if (idCotacaoDadosSolicitante > 0)
            {
                _cotacaoDadosSolicitanteServices.ExcluirCotacaoDadosSolicitanteAsync(idCotacaoDadosSolicitante);
            }
        }
        private async Task<bool> ListarIdCotacaoCondicaoPagamentoAsync(CondicaoPagamentoRequest condicaoPagamentoRequest)
        {
            bool condicaoPagamento = true;

            try
            {
                if (condicaoPagamentoRequest.Pagamentos != null && condicaoPagamentoRequest.Pagamentos.Any())
                {
                    foreach (var item in condicaoPagamentoRequest.Pagamentos)
                    {
                        int IdCotacaoCondicaoPagamento = await _condicaoPagamentoServices.ListarIdCotacaoCondicaoPagamentoAsync(item.IdCotacao, item.StatusCondicoesPagamento);

                        if (IdCotacaoCondicaoPagamento == 0)
                        {
                            Condicao_Pagamento pagamento = new Condicao_Pagamento();
                            pagamento.Id = item.Id;
                            pagamento.IdCotacao = item.IdCotacao;
                            pagamento.StatusCondicoesPagamento = item.StatusCondicoesPagamento;

                            await _condicaoPagamentoServices.InserirIdCondicaoPagamentoAsync(pagamento);
                        }
                    }
                }
            }
            catch
            {
                return false;
            }

            return condicaoPagamento;
        }
        private void RemoverCotacaoCondicaoPagamento(string IdCotacao)
        {
            var dadosCondicaoPagamento = _condicaoPagamentoServices.ListarIdCotacaoCondicaoPagamentoAsync(IdCotacao).Result;
            if (dadosCondicaoPagamento.Data.Pagamentos != null && dadosCondicaoPagamento.Data.Pagamentos.Any())
            {
                foreach (var item in dadosCondicaoPagamento.Data.Pagamentos)
                {
                    if (item.Id > 0)
                    {
                        _condicaoPagamentoServices.ExcluirCotacaoCondicaoPagamentoAsync(item.Id);
                    }
                }
            }
        }
        private async Task<bool> ListarIdFreteAsync(FreteRequest TipoFrete)
        {
            bool frete = true;

            try
            {
                if (TipoFrete.frete != null && TipoFrete.frete.Any())
                {
                    foreach (var item in TipoFrete.frete)
                    {
                        int IdFrete = await _ifreteServices.ListarIdCotacaoFreteAsync(item.IdCotacao, item.TipoFrete);

                        if (IdFrete == 0)
                        {
                            Frete freteDados = new Frete();
                            freteDados.Id = item.Id;
                            freteDados.IdCotacao = item.IdCotacao;
                            freteDados.TipoFrete = item.TipoFrete;

                            await _ifreteServices.InserirIdFreteAsync(freteDados);
                        }
                    }
                }
            }
            catch
            {
                return false;
            }

            return frete;
        }
        private void RemoverCotacaoFrete(string IdCotacao)
        {
            var dadoFrete = _ifreteServices.ListarIdFreteAsync(IdCotacao).Result;
            if (dadoFrete.Data.Frete != null && dadoFrete.Data.Frete.Any())
            {
                foreach (var item in dadoFrete.Data.Frete)
                {
                    if (item.Id > 0)
                    {
                        _ifreteServices.ExcluirCotacaoFreteAsync(item.Id);
                    }
                }
            }
        }

        private string WriteMessageNovaCotacao()
        {
            string html = File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "emails\\emails\\nova-cotacao.html"));
            return html;
        }

        #endregion

    }
}
