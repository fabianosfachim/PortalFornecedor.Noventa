using PortalFornecedor.Noventa.Data.Interfaces;
using PortalFornecedor.Noventa.Domain.Entities;

namespace PortalFornecedor.Noventa.Data.Repositories.Entities
{
    public class CotacaoRepository : EntityBaseRepository<Cotacao>, ICotacaoRepository

    {
        private readonly ApplicationContext _db;
        public CotacaoRepository(ApplicationContext context) : base(context)
        {
            _db = context;
        }
    }
}
