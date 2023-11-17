using PortalFornecedor.Noventa.Data.Interfaces;
using PortalFornecedor.Noventa.Domain.Entities;

namespace PortalFornecedor.Noventa.Data.Repositories.Entities
{
    public class StatusRepository : EntityBaseRepository<Status>, IStatusRepository
    {
        private readonly ApplicationContext _db;
        public StatusRepository(ApplicationContext context) : base(context)
        {
            _db = context;
        }
    }
}
