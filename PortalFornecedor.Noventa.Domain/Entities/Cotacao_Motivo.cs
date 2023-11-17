

namespace PortalFornecedor.Noventa.Domain.Entities
{
    public class Cotacao_Motivo : EntityBase
    {
        public int Id { get; set; }
        public string IdCotacao { get; set; }
        public int IdMotivo { get; set; }
    }
}
