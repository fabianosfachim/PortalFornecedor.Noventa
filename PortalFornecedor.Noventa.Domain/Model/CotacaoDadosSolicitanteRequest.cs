

namespace PortalFornecedor.Noventa.Domain.Model
{
    public class CotacaoDadosSolicitanteRequest
    {
        public string? IdCotacao { get; set; }
        public string? DataSolicitacao { get; set; }
        public string? Nome { get; set; }
        public string? CNPJ { get; set; }
        public string? DataEntrega { get; set; }
        public string? Endereco { get; set; }
        public string? CEP { get; set; }
        public string? Cidade { get; set; }
        public string? Estado { get; set; }
        public string? Contato { get; set; }
        public string? Email { get; set; }
    }
}
