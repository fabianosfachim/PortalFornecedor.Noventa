using PortalFornecedor.Noventa.Domain.Entities;

namespace PortalFornecedor.Noventa.Domain.Model
{
    public class OutrasDespesasResponse
    {
        public List<Outras_Despesas> OutrasDespesas { get; set; }
        public bool Executado { get; set; }
        public string MensagemRetorno { get; set; }
        public Outras_Despesas OutrasDespesasDados { get; set; }
    }
}
