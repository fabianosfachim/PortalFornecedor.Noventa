

using PortalFornecedor.Noventa.Domain.Entities;

namespace PortalFornecedor.Noventa.Domain.Model
{
    public class FreteResponse
    {
        public List<Frete> Frete { get; set; }
        public bool Executado { get; set; }
        public string MensagemRetorno { get; set; }
        public Frete FreteDados { get; set; }
    }
}
