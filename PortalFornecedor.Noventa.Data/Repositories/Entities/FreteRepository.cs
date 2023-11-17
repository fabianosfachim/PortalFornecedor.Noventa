using PortalFornecedor.Noventa.Data.Interfaces;
using PortalFornecedor.Noventa.Domain.Entities;

namespace PortalFornecedor.Noventa.Data.Repositories.Entities
{
    public class FreteRepository : EntityBaseRepository<Frete>, IFreteRepository
    {
        private readonly ApplicationContext _db;
        public FreteRepository(ApplicationContext context) : base(context)
        {
            _db = context;
        }
    }
}
