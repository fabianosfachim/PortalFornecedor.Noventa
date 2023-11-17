

namespace PortalFornecedor.Noventa.Domain.Entities
{
    public class Frete : EntityBase
    {
        public int Id { get; set; }
        public string IdCotacao { get; set; }
        public string TipoFrete { get; set; }
    }
}
