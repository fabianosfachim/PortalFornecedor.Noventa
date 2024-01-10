using PortalFornecedor.Noventa.Application;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PortalFornecedor.Noventa.Application.Services.Interfaces;
using PortalFornecedor.Noventa.Data.Interfaces;
using PortalFornecedor.Noventa.Data.Repositories.Entities;
using PortalFornecedor.Noventa.Data.Repositories;


namespace PortalFornecedor.Noventa.Ioc
{
    public static class Register
    {
        public static void RegisterDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {

            #region DependencyInjection

            //Services
            services.AddTransient<IEstadoServices, EstadoServices>();
            services.AddTransient<ILoginServices, LoginServices>();
            services.AddTransient<IFornecedorServices, FornecedorServices>();
            services.AddTransient<IFiltrosServices, FiltrosServices>();
            services.AddTransient<ICotacaoStatusServices, CotacaoStatusServices>();
            services.AddTransient<ICotacaoDadosSolicitanteRepository, CotacaoDadosSolicitanteRepository>();
            services.AddTransient<ICondicaoPagamentoServices, CondicaoPagamentoServices>();
            services.AddTransient<IfreteServices, FreteServices>();
            services.AddTransient<ICotacaoServices, CotacaoServices>();

            //Repository//
            services.AddSingleton(typeof(IEntityRepository<>), typeof(EntityBaseRepository<>));

            //Entity
            services.AddTransient<IEstadoRepository, EstadoRepository>();
            services.AddTransient<ILoginRepository, LoginRepository>();
            services.AddTransient<IFornecedorRepository, FornecedorRepository>();
            services.AddTransient<IStatusRepository, StatusRepository>();
            services.AddTransient<IStatusCotacaoRepository, StatusCotacaoRepository>();
            services.AddTransient<ICotacaoDadosSolicitanteServices, CotacaoDadosSolicitanteServices>();
            services.AddTransient<ICondicaoPagamentoRepository, CondicaoPagamentoRepository>();
            services.AddTransient<IFreteRepository, FreteRepository>();
            services.AddTransient<ICotacaoRepository, CotacaoRepository>();
            services.AddTransient<IMaterialCotacaoRepository, MaterialCotacaoRepository>();
            services.AddTransient<IRecuperarDadosAcessoRepository, RecuperarDadosAcessoRepository>();
            #endregion
        }
    }
}