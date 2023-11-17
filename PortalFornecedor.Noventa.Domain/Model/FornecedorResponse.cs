using PortalFornecedor.Noventa.Domain.Entities;

namespace PortalFornecedor.Noventa.Domain.Model
{
    public class FornecedorResponse
    {
        public Fornecedor fornecedor { get; set; }
        public List<Fornecedor> listaFornecedor { get; set; }
        public bool Executado { get; set; }
        public string MensagemRetorno { get; set; }
    }
}
