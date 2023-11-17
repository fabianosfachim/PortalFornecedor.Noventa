using PortalFornecedor.Noventa.Data.Interfaces;
using PortalFornecedor.Noventa.Domain.Entities;

namespace PortalFornecedor.Noventa.Data.Repositories.Entities
{
    public class StatusCotacaoRepository : EntityBaseRepository<Cotacao_Status>, IStatusCotacaoRepository

    {
        private readonly ApplicationContext _db;
        public StatusCotacaoRepository(ApplicationContext context) : base(context)
        {
            _db = context;
        }
    }
}



