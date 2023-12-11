using PortalFornecedor.Noventa.Domain.Entities;

namespace PortalFornecedor.Noventa.Domain.Model
{
    public class LoginResponse
    {
        public Login login { get; set; }
        public Fornecedor fornecedor { get; set; }
        public bool Executado { get; set; }
        public string MensagemRetorno { get; set; }
    }
}
