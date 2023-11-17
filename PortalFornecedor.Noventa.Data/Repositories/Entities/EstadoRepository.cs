using PortalFornecedor.Noventa.Data.Interfaces;
using PortalFornecedor.Noventa.Domain.Entities;

namespace PortalFornecedor.Noventa.Data.Repositories.Entities
{
    public class EstadoRepository : EntityBaseRepository<Estado>, IEstadoRepository

    {
        private readonly ApplicationContext _db;
        public EstadoRepository(ApplicationContext context) : base(context)
        {
            _db = context;
        }
    }
}
