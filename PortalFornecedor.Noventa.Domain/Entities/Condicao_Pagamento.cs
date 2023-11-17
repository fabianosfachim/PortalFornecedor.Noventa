namespace PortalFornecedor.Noventa.Domain.Entities
{
    public class Condicao_Pagamento : EntityBase
    {
        public int Id { get; set; }
        public string IdCotacao { get; set; }
        public string StatusCondicoesPagamento { get; set; }

    }
}
