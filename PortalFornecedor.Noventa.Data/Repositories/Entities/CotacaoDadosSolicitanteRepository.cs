using PortalFornecedor.Noventa.Data.Interfaces;
using PortalFornecedor.Noventa.Domain.Entities;

namespace PortalFornecedor.Noventa.Data.Repositories.Entities
{
    public class CotacaoDadosSolicitanteRepository : EntityBaseRepository<Cotacao_Dados_Solicitante>, ICotacaoDadosSolicitanteRepository
    {
        private readonly ApplicationContext _db;
        public CotacaoDadosSolicitanteRepository(ApplicationContext context) : base(context)
        {
            _db = context;
        }
    }
}
