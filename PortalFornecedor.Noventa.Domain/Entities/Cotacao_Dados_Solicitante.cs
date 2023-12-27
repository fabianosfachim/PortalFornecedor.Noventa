
namespace PortalFornecedor.Noventa.Domain.Entities
{
    public class Cotacao_Dados_Solicitante : EntityBase
    {
        public int Id { get; set; }
        public string IdCotacao { get; set; }
        public DateTime? DataSolicitacao { get; set; }
        public string? Nome { get; set; }
        public string? CNPJ { get; set; }
        public DateTime? DataEntrega { get; set; }
        public string? Endereco { get; set; }
        public string? CEP { get; set; }
        public string? Cidade { get; set; }
        public string? Estado { get; set; }
        public string? Contato { get; set; }
        public string? Email { get; set; }
        public string? Telefone { get; set; }
    }
}
