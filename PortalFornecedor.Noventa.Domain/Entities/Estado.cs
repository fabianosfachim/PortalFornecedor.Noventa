namespace PortalFornecedor.Noventa.Domain.Entities
{
    public class Estado : EntityBase
    {
        public int Id { get; set; }
        public string? NomeEstado { get; set; }
        public string? SiglaEstado { get; set; }
    }
}
