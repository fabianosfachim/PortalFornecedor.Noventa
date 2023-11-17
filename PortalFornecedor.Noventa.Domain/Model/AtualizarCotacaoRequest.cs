
using PortalFornecedor.Noventa.Domain.Entities;

namespace PortalFornecedor.Noventa.Domain.Model
{
    public class AtualizarCotacaoRequest
    {
        public int Id { get; set; }
        public int Fornecedor_Id { get; set; }
        public string ERPCotacao_Id { get; set; }
        public int Motivo_Id { get; set; }
        public int CotacaoStatus_Id { get; set; }
        public string? Vendedor { get; set; }
        public DateTime? DataPostagem { get; set; }
        public int CondicoesPagamento_Id { get; set; }
        public int Frete_Id { get; set; }
        public int? OutrasDespesas_Id { get; set; }
        public int? ValorFrete { get; set; }
        public int? ValorFreteForaNota { get; set; }
        public int? ValorSeguro { get; set; }
        public int? ValorDesconto { get; set; }
        public string? Observacao { get; set; }
        public string NomeUsuarioCadastro { get; set; }
        public DateTime DataCadastro { get; set; }
        public string? NomeUsuarioAlteracao { get; set; }
        public DateTime? DataAlteracao { get; set; }

        public List<Material_Cotacao> MaterialCotacao { get; set; }
    }
}
