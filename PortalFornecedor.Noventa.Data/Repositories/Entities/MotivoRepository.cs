using PortalFornecedor.Noventa.Data.Interfaces;
using PortalFornecedor.Noventa.Domain.Entities;


namespace PortalFornecedor.Noventa.Data.Repositories.Entities
{
    public class MotivoRepository : EntityBaseRepository<Motivo>, IMotivoRepository
    {
        private readonly ApplicationContext _db;
        public MotivoRepository(ApplicationContext context) : base(context)
        {
            _db = context;
        }

    }
}
