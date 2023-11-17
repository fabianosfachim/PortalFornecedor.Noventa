
using PortalFornecedor.Noventa.Domain.Entities;

namespace PortalFornecedor.Noventa.Domain.Model
{
    public class CondicaoPagamentoResponse
    {
        public List<Condicao_Pagamento> Pagamentos { get; set; }
        public bool Executado { get; set; }
        public string MensagemRetorno { get; set; }
        public Condicao_Pagamento PagamentosDados { get; set; }
    }
}
