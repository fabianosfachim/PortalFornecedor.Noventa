

namespace PortalFornecedor.Noventa.Domain.Model
{
    public class CotacaoDetalheFiltroRequest
    {
        public int IdFornecedor { get; set; }
        public string? solicitacao { get; set; }
        public List<StatusFiltro>? statusId { get; set; }
        public List<MotivoFiltro>? motivoId { get; set; }
        public DateTime? dataInicio { get; set; }
        public DateTime? dataTermino { get; set; }

    }

    public class StatusFiltro
    {
        public int? statusId { get; set; }
    }

    public class MotivoFiltro
    {
        public int? motivoId { get; set; }
    }
}
