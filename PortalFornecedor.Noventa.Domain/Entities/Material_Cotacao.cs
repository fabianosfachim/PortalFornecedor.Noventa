

namespace PortalFornecedor.Noventa.Domain.Entities
{
    public class Material_Cotacao : EntityBase
    {
        public int Id { get; set; }
        public int Material_Id { get; set; }
        public string? Descricao { get; set; }
        public int Cotacao_Id { get; set; }
        public string? NomeFabricante { get; set; }
        public int? QuantidadeRequisitada { get; set; }
        public decimal? PrecoUnitario { get; set; }
        public decimal? PercentualDesconto { get; set; }
        public bool? IpiIncluso { get; set; }
        public decimal? PercentualIpi { get; set; }
        public decimal? ValorIpi { get; set; }
        public decimal? PercentualIcms { get; set; }
        public int? PrazoEntrega { get; set; }
        public string? Marca { get; set; }
        public decimal? SubTotal { get; set; }
        public bool Ativo { get; set; }


    }
}
