using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using PortalFornecedor.Noventa.Domain.Entities;

namespace PortalFornecedor.Noventa.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<Estado> Estado { get; set; }
        public DbSet<Login> Login { get; set; }
        public DbSet<Fornecedor> Fornecedor { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<Motivo> Motivo { get; set; }
        public DbSet<Cotacao_Status> Cotacao_Status { get; set; }
        public DbSet<Cotacao_Motivo> Cotacao_Motivo { get; set; }
        public DbSet<Cotacao_Dados_Solicitante> Cotacao_Dados_Solicitante { get; set; }
        public DbSet<Condicao_Pagamento> Condicao_Pagamento { get; set; }
        public DbSet<Frete> Frete { get; set; }
        public DbSet<Cotacao> Cotacao { get; set; }
        public DbSet<Material_Cotacao> Material_Cotacao { get; set; }

    }
}
