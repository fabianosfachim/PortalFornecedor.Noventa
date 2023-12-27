using PortalFornecedor.Noventa.Domain.Entities;

namespace PortalFornecedor.Noventa.Domain.Model
{
    public class StatusResponse
    {
        public List<Status> Status { get; set; }
        public Status StatusDados { get; set; }
        public StatusDashBoard statusDashBoard { get; set; }
        public bool Executado { get; set; }
        public string MensagemRetorno { get; set; }
    }
}
