using PortalFornecedor.Noventa.Data.Interfaces;
using PortalFornecedor.Noventa.Domain.Entities;


namespace PortalFornecedor.Noventa.Data.Repositories.Entities
{
    public class RecuperarDadosAcessoRepository : EntityBaseRepository<Recuperar_Dados_Acesso>, IRecuperarDadosAcessoRepository
    {
        private readonly ApplicationContext _db;
        public RecuperarDadosAcessoRepository(ApplicationContext context) : base(context)
        {
            _db = context;
        }
    }
}
