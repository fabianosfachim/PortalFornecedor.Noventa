using PortalFornecedor.Noventa.Data.Interfaces;
using PortalFornecedor.Noventa.Domain.Entities;

namespace PortalFornecedor.Noventa.Data.Repositories.Entities
{
    public class CondicaoPagamentoRepository : EntityBaseRepository<Condicao_Pagamento>, ICondicaoPagamentoRepository
    {
        private readonly ApplicationContext _db;
        public CondicaoPagamentoRepository(ApplicationContext context) : base(context)
        {
            _db = context;
        }
    }
}
