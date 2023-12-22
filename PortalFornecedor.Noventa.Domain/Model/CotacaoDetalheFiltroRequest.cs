

namespace PortalFornecedor.Noventa.Domain.Model
{
    public class CotacaoDetalheFiltroRequest
    {
        public int IdFornecedor { get; set; }
        public string? solicitacao { get; set; }
        public List<int>? statusId { get; set; }
        public List<int>? motivoId { get; set; }
        public DateTime? dataInicio { get; set; }
        public DateTime? dataTermino { get; set; }

    }

}
