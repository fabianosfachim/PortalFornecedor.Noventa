using PortalFornecedor.Noventa.Domain.Entities;

namespace PortalFornecedor.Noventa.Domain.Model
{
    public class CotacaoRequest
    {
        public string ERPCotacao_Id { get; set; }
        public string CotacaoStatusDescricao { get; set; }
        public string motivo { get; set; }
        public string CNPJ { get; set; }
        public CotacaoDadosSolicitanteRequest cotacaoDadosSolicitante { get; set; }
        public string? Vendedor { get; set; }
        public DateTime? DataPostagem { get; set; }
        public CondicaoPagamentoRequest CondicoesPagamento { get;set; }
        public FreteRequest TipoFrete { get; set; }
        public OutrasDespesasRequest? OutrasDespesas { get;set; }
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
