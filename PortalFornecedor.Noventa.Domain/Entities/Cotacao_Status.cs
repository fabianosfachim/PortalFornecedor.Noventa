namespace PortalFornecedor.Noventa.Domain.Entities
{
    public class Cotacao_Status : EntityBase
    {
        public int Id { get; set; }
        public string IdCotacao { get; set; }
        public int IdStatus { get; set; }
        public DateTime DataStatus { get; set; }
    }
}
