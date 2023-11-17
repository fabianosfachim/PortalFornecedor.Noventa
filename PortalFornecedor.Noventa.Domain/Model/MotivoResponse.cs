using PortalFornecedor.Noventa.Domain.Entities;

namespace PortalFornecedor.Noventa.Domain.Model
{
    public class MotivoResponse
    {
        public List<Motivo> Motivo { get; set; }
        public Motivo MotivoDados { get; set; }
        public bool Executado { get; set; }
        public string MensagemRetorno { get; set; }
    }
}
