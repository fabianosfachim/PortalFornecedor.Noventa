using PortalFornecedor.Noventa.Data.Interfaces;
using PortalFornecedor.Noventa.Domain.Entities;

namespace PortalFornecedor.Noventa.Data.Repositories.Entities
{
    public class FornecedorRepository : EntityBaseRepository<Fornecedor>, IFornecedorRepository
    {
        private readonly ApplicationContext _db;
        public FornecedorRepository(ApplicationContext context) : base(context)
        {
            _db = context;
        }
    }
}
