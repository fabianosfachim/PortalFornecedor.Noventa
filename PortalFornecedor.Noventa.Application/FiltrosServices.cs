using Microsoft.Extensions.Logging;
using PortalFornecedor.Noventa.Application.Services.Interfaces;
using PortalFornecedor.Noventa.Application.Services.Wrappers;
using PortalFornecedor.Noventa.Data.Interfaces;
using PortalFornecedor.Noventa.Domain.Entities;
using PortalFornecedor.Noventa.Domain.Model;
using System.Collections.Generic;

namespace PortalFornecedor.Noventa.Application
{
    public class FiltrosServices : IFiltrosServices
    {
        private readonly ILogger<FiltrosServices> _logger;
        private readonly IStatusRepository _statusRepository;
        private readonly IMotivoRepository _motivoRepository;
        private readonly ICotacaoRepository _cotacaoRepository;
        private readonly IMaterialCotacaoRepository _materialCotacaoRepository;
        private readonly ICotacaoDadosSolicitanteServices _cotacaoDadosSolicitanteServices;
        private readonly ICotacaoStatusServices _cotacaoStatusServices;

        public FiltrosServices(ILogger<FiltrosServices> logger,
                              IStatusRepository statusRepository,
                              IMotivoRepository motivoRepository,
                              ICotacaoRepository cotacaoRepository,
                              IMaterialCotacaoRepository materialCotacaoRepository,
                              ICotacaoDadosSolicitanteServices cotacaoDadosSolicitanteServices,
                              ICotacaoStatusServices cotacaoStatusServices)
        {
            _logger = logger;
            _statusRepository = statusRepository;
            _motivoRepository = motivoRepository;
            _cotacaoRepository = cotacaoRepository;
            _materialCotacaoRepository = materialCotacaoRepository;
            _cotacaoDadosSolicitanteServices = cotacaoDadosSolicitanteServices;
            _cotacaoStatusServices = cotacaoStatusServices;
        }

        public async Task<Response<StatusResponse>> ListarStatusAsync()
        {
            StatusResponse statusResponse = new StatusResponse();

            try
            {
                _logger.LogInformation("Iniciando o método   " +
                    $"{nameof(ListarStatusAsync)}   ");


                var status = await _statusRepository.GetAllAsync();
                statusResponse.Status = status.ToList();
                statusResponse.Executado = true;
                statusResponse.MensagemRetorno = "Consulta efetuada com sucesso";

                _logger.LogInformation("Finalizando o método   " +
                    $"{nameof(ListarStatusAsync)}   ");
            }
            catch (Exception ex)
            {
                _logger.LogError("Erro na execução do método " +
                    $"{nameof(ListarStatusAsync)}   " +
                    " Com o erro = " + ex.Message);

                statusResponse.Executado = false;
                statusResponse.MensagemRetorno = "Erro na consulta de lista de status";
            }

            return new Response<StatusResponse>(statusResponse, $"Lista Status.");
        }

        public async Task<Response<MotivoResponse>> ListarMotivoAsync()
        {
            MotivoResponse motivoResponse = new MotivoResponse();

            try
            {
                _logger.LogInformation("Iniciando o método   " +
                    $"{nameof(ListarMotivoAsync)}   ");


                var motivo = await _motivoRepository.GetAllAsync();
                motivoResponse.Motivo = motivo.ToList();
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

        public async Task<Response<DashBoardResponse>> ListarDadosDashBoardAsync(int idFornecedor, int idData, int peddingPage, int currentPage)
        {
            DashBoardResponse dashBoardResponse = new DashBoardResponse();
            int CotacoesPendentes = 0;
            int CotacoesEnviadas = 0;
            int pageLimit = 10;


            List<ListaCotacoesPendentesDashBoard> listaCotacoesPendentesDashBoards = new List<ListaCotacoesPendentesDashBoard>();
            List<ListaAtividadesRecentesDashBoard> listaAtividadesRecentesDashBoard = new List<ListaAtividadesRecentesDashBoard>();

            try
            {
                _logger.LogInformation("Iniciando o método   " +
                $"{nameof(ListarDadosDashBoardAsync)}   ");

                var cotacao = await _cotacaoRepository.GetAsync(x =>  x.Fornecedor_Id == idFornecedor);

                if(cotacao.Any()) 
                {
                  foreach(var item in cotacao) 
                    {
                        var dadosSolicitante = await _cotacaoDadosSolicitanteServices.ListarDadosSolicitanteAsync(item.IdCotacao);

                        if (dadosSolicitante != null)
                        {
                            var DadosStatus = _cotacaoStatusServices.ListarCotacaoAsync(item.CotacaoStatus_Id).Result;

                            if (DadosStatus != null)
                            {
                                if ((ValidarData(DateTime.Parse(dadosSolicitante.Data.solicitante.DataSolicitacao.Value.ToString("yyyy-MM-dd")), idData) == false) && DadosStatus.Data.statusDashBoard.Id == 1)
                                {
                                    continue;
                                }
                               
                                if(DadosStatus.Data.statusDashBoard.Id == 2)
                                {
                                    if ((ValidarData(DateTime.Parse(DadosStatus.Data.statusDashBoard.DataStatus.ToString("yyyy-MM-dd")), idData) == false))
                                    {
                                        continue;
                                    }
                                }

                                if (DadosStatus.Data.statusDashBoard.Id == 1)
                                {
                                    ListaCotacoesPendentesDashBoard objListaCotacoesPendentesDashBoard = new ListaCotacoesPendentesDashBoard();

                                    objListaCotacoesPendentesDashBoard.Id = item.Id;
                                    objListaCotacoesPendentesDashBoard.solicitante = dadosSolicitante.Data.solicitante.Nome;
                                    objListaCotacoesPendentesDashBoard.localEntrega = dadosSolicitante.Data.solicitante.Cidade + " (" + dadosSolicitante.Data.solicitante.Estado + ")";
                                    objListaCotacoesPendentesDashBoard.dataSolicitacao = dadosSolicitante.Data.solicitante.DataSolicitacao.Value;
                                    objListaCotacoesPendentesDashBoard.dataEntrega = dadosSolicitante.Data.solicitante.DataEntrega.Value;

                                    var materialCotacao = await _materialCotacaoRepository.GetAsync(x => x.Cotacao_Id == item.Id && x.Ativo == true);

                                    if (materialCotacao != null && materialCotacao.Any())
                                    {
                                        foreach (var itemCotacao in materialCotacao)
                                        {
                                            objListaCotacoesPendentesDashBoard.quantidadeItensRequisitados = objListaCotacoesPendentesDashBoard.quantidadeItensRequisitados + 1;
                                        }
                                    }

                                    CotacoesPendentes = CotacoesPendentes + 1;

                                    listaCotacoesPendentesDashBoards.Add(objListaCotacoesPendentesDashBoard);
                                }
                                else if (DadosStatus.Data.statusDashBoard.Id == 2)
                                {
                                    CotacoesEnviadas = CotacoesEnviadas + 1;
                                }
                                
                                if (DadosStatus.Data.statusDashBoard.Id != 1)
                                {
                                    ListaAtividadesRecentesDashBoard objListaAtividadesRecentesDashBoard = new ListaAtividadesRecentesDashBoard();
                                    
                                    objListaAtividadesRecentesDashBoard.Id = item.Id;
                                    objListaAtividadesRecentesDashBoard.solicitante = dadosSolicitante.Data.solicitante.Nome; 
                                    objListaAtividadesRecentesDashBoard.localEntrega = dadosSolicitante.Data.solicitante.Cidade + " (" + dadosSolicitante.Data.solicitante.Estado + ")";
                                    objListaAtividadesRecentesDashBoard.dataEntrega = dadosSolicitante.Data.solicitante.DataEntrega.Value;
                                    objListaAtividadesRecentesDashBoard.acao = "Cotação " + DadosStatus.Data.statusDashBoard.NomeStatus;
                                    objListaAtividadesRecentesDashBoard.DataSolicitacao = DadosStatus.Data.statusDashBoard.DataStatus;
                                    objListaAtividadesRecentesDashBoard.DataStatus = DadosStatus.Data.statusDashBoard.DataStatus;

                                    listaAtividadesRecentesDashBoard.Add(objListaAtividadesRecentesDashBoard);
                                }
                            }
                        }
                    }
                }

                if(listaCotacoesPendentesDashBoards.Count > 0)
                {
                    dashBoardResponse.CotacoesPendentesPageCount = listaCotacoesPendentesDashBoards.Count;

                    dashBoardResponse.listaCotacoesPendentesDashBoards = listaCotacoesPendentesDashBoards
                        .OrderBy(cot => cot.dataSolicitacao)
                        .Skip((pageLimit * peddingPage) - pageLimit)
                        .Take(pageLimit)
                        .ToList();
                }
                
                dashBoardResponse.CotacoesPendentes = CotacoesPendentes;
                dashBoardResponse.CotacoesEnviadas = CotacoesEnviadas;
                dashBoardResponse.OcsAprovadas = 0;
                dashBoardResponse.OcsFinalizadas = 0;

                if (listaAtividadesRecentesDashBoard.Count > 0)
                {
                    dashBoardResponse.CotacoesRecentesPageCount = listaAtividadesRecentesDashBoard.Count;

                    dashBoardResponse.listaAtividadesRecentesDashBoards = listaAtividadesRecentesDashBoard
                         .OrderByDescending(cot => cot.DataStatus)
                        .Skip((pageLimit * currentPage) - pageLimit)
                        .Take(pageLimit)
                        .ToList();
                }
                dashBoardResponse.Executado = true;
                dashBoardResponse.MensagemRetorno = "Consulta efetuada com sucesso";

                _logger.LogInformation("Finalizando o método   " +
                    $"{nameof(ListarDadosDashBoardAsync)}   ");
            }
            catch (Exception ex)
            {
                _logger.LogError("Erro na execução do método " +
                    $"{nameof(ListarDadosDashBoardAsync)}   " +
                    " Com o erro = " + ex.Message);

                dashBoardResponse.Executado = false;
                dashBoardResponse.MensagemRetorno = "Erro na consulta dos dados do Dashboard";
            }

            return new Response<DashBoardResponse>(dashBoardResponse, $"Lista dados do Dashboard.");
        }

        private bool ValidarData(DateTime dataSolicitacao, int idData)
        {
            bool retorno = true;
            DateTime dataAtual = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));

            if ((int)DashBoardRequest.Data.UltimaSemana == idData)
            {
                 retorno = Between(dataSolicitacao, dataAtual.AddDays(-7), dataAtual);
            }
            else if ((int)DashBoardRequest.Data.UltimaQuinzena == idData)
            {
                retorno = Between(dataSolicitacao, dataAtual.AddDays(-15), dataAtual);

            }
            if ((int)DashBoardRequest.Data.UltimaMensal == idData)
            {
                retorno = Between(dataSolicitacao, dataAtual.AddDays(-30), dataAtual);
            }
            if ((int)DashBoardRequest.Data.UltimoTrimeste == idData)
            {
                retorno = Between(dataSolicitacao, dataAtual.AddDays(-90), dataAtual);
            }
            if ((int)DashBoardRequest.Data.UltimoSemestre == idData)
            {
                retorno = Between(dataSolicitacao, dataAtual.AddDays(-180), dataAtual);
            }
            if ((int)DashBoardRequest.Data.UltimoAno == idData)
            {
                retorno = Between(dataSolicitacao, dataAtual.AddDays(-365), dataAtual);
            }

            return retorno;
        }

        private static bool Between(DateTime input, DateTime date1, DateTime date2)
        {
            bool retorno = true;

            if (date1 > input)
            {
                retorno = false;
            }
            else if (input <= date2)
            {
                retorno = true;
            }
            else
            {
                retorno = false;
            }

            return retorno;
        }
    }
}
