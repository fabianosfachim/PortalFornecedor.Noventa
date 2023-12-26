

namespace PortalFornecedor.Noventa.Domain.Model
{
    public class CotacaoDetalheFiltroResponse
    {
        public List<ListaFiltroCotacao> listaFiltroCotacaos { get; set; }
        public int totalPage { get; set; }
        public bool Executado { get; set; }
        public string MensagemRetorno { get; set; }
    }

    public class ListaFiltroCotacao
    {
        public int id { get; set; }
        public string solicitante { get; set; }
        public string localDestino { get; set; }
        public DateTime dataSolicitacao { get; set; }
        public DateTime dataEntrega { get; set; }
        public string motivo { get; set; }
        public string contato { get; set; }
        public string status { get; set; }
    }
}

