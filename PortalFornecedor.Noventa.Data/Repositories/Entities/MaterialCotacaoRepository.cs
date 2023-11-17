using PortalFornecedor.Noventa.Data.Interfaces;
using PortalFornecedor.Noventa.Domain.Entities;

namespace PortalFornecedor.Noventa.Data.Repositories.Entities
{
    public class MaterialCotacaoRepository : EntityBaseRepository<Material_Cotacao>, IMaterialCotacaoRepository

    {
        private readonly ApplicationContext _db;
        public MaterialCotacaoRepository(ApplicationContext context) : base(context)
        {
            _db = context;
        }
    }
}
