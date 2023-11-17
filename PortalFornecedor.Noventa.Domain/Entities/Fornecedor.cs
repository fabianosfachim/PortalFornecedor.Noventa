

namespace PortalFornecedor.Noventa.Domain.Entities
{
    public class Fornecedor : EntityBase
    {
        public int Id { get; set; }
        public string? CNPJ { get; set; }
        public string? RazaoSocial { get; set; }
        public string? CEP { get; set; }
        public string? Logradouro { get; set; }
        public string? Numero { get; set; }
        public string? Complemento { get; set; }
        public string? Cidade { get; set; }
        public int? IdEstado { get; set; }
        public string? NomeUsuarioCadastro { get; set; }
        public DateTime? DataCadastro { get; set; }
        public string? NomeUsuarioAlteracao { get; set; }
        public DateTime? DataAlteracao { get; set; }

    }
}
