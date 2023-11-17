
using PortalFornecedor.Noventa.Domain.Entities;

namespace PortalFornecedor.Noventa.Domain.Model
{
    public class CotacaoResponse
    {
        public ListarDadosCotacao listarDadosCotacao { get; set; }
        public List<ResumoCotacao> resumoCotacao { get; set; }
        public bool Executado { get; set; }
        public string MensagemRetorno { get; set; }
    }

    public class ListarDadosCotacao
    {
        public Cotacao_Dados_Solicitante dadosSolicitante { get; set; }
        public CotacaoDetalhe cotacao { get; set; }
        public ResumoCotacao resumoCotacao { get; set; }
        public List<Material_Cotacao> material { get; set; }
    }

    public class CotacaoDetalhe
    {
        public int Id { get; set; }
        public int Fornecedor_Id { get; set; }
        public string NomeFornecedor { get; set; }
        public string IdCotacao { get; set; }
        public int Motivo_Id { get; set; }
        public string NomeMotivo { get; set; }
        public int CotacaoStatus_Id { get; set; }
        public string NomeStatus { get; set; }
        public string? Vendedor { get; set; }
        public DateTime? DataPostagem { get; set; }
        public int? CondicoesPagamento_Id { get; set; }
        public string NomeCondicaoPagamento { get; set; }
        public int? Frete_Id { get; set; }
        public string NomeFrete { get; set; }
        public int? OutrasDespesas_Id { get; set; }
        public string NomeOutrasDespesas { get; set; }
        public decimal? ValorFrete { get; set; }
        public decimal? ValorFreteForaNota { get; set; }
        public decimal? ValorSeguro { get; set; }
        public decimal? ValorDesconto { get; set; }
        public string? Observacao { get; set; }
        public string NomeUsuarioCadastro { get; set; }
        public DateTime DataCadastro { get; set; }
        public string? NomeUsuarioAlteracao { get; set; }
        public DateTime? DataAlteracao { get; set; }
    }

    public class ResumoCotacao
    {
        public decimal subTotalItens { get; set; }
        public decimal valorFrete { get; set; }
        public decimal valorSeguro { get; set; }
        public decimal ValorDesconto { get; set;}
        public string outrasDespesas { get; set; }
        public string formaPagamento { get; set; }
        public decimal valorFinalCotacao { get; set; }
    }

}
