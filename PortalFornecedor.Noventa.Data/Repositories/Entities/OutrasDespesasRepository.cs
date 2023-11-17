using PortalFornecedor.Noventa.Data.Interfaces;
using PortalFornecedor.Noventa.Domain.Entities;

namespace PortalFornecedor.Noventa.Data.Repositories.Entities
{
    public class OutrasDespesasRepository : EntityBaseRepository<Outras_Despesas>, IOutrasDespesasRepository
    {
        private readonly ApplicationContext _db;
        public OutrasDespesasRepository(ApplicationContext context) : base(context)
        {
            _db = context;
        }
    }
}
