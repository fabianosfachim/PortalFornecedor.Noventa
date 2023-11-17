
namespace PortalFornecedor.Noventa.Domain.Model
{
    public class DashBoardResponse
    {
        public int CotacoesPendentes { get; set; }
        public int CotacoesEnviadas { get; set; }
        public int OcsAprovadas { get; set; }
        public int OcsFinalizadas { get; set; }
        public List<ListaCotacoesPendentesDashBoard>? listaCotacoesPendentesDashBoards { get; set; }

        public List<ListaAtividadesRecentesDashBoard>? listaAtividadesRecentesDashBoards { get; set; }
        public bool Executado { get; set; }
        public string MensagemRetorno { get; set; }
    }

    public class ListaCotacoesPendentesDashBoard
    {
        public int Id { get; set; }
        public string solicitante { get; set; }
        public string localEntrega { get; set; }
        public DateTime dataSolicitacao { get; set; }
        public DateTime dataEntrega { get; set; }
        public int quantidadeItensRequisitados { get; set; }
    }

    public class ListaAtividadesRecentesDashBoard
    {
        public int Id { get; set; }
        public string solicitante { get; set; }
        public string localEntrega { get; set; }
        public DateTime dataEntrega { get; set; }
        public string acao { get; set; }
    }
}
