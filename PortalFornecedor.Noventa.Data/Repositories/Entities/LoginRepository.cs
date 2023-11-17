using PortalFornecedor.Noventa.Data.Interfaces;
using PortalFornecedor.Noventa.Domain.Entities;

namespace PortalFornecedor.Noventa.Data.Repositories.Entities
{
    public class LoginRepository : EntityBaseRepository<Login>, ILoginRepository

    {
        private readonly ApplicationContext _db;
        public LoginRepository(ApplicationContext context) : base(context)
        {
            _db = context;
        }
    }
}
