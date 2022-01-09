using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
namespace WePing.Service.Spid
{
    public static class ComponentServiceCollectionExtensions
    {

        public static IServiceCollection AddSpid(this IServiceCollection services, Action<SpidOptions> configure)
        {

            services.AddSingleton<ISpidAuthenticatorBuilder, SpidAuthenticatorBuilder>(sp =>
            {


                var option = sp.GetRequiredService<IOptions<SpidOptions>>();
                var opt = option.Value;

                configure.Invoke(opt);
                return new SpidAuthenticatorBuilder(opt.AppId, opt.Password, opt.Serie);
            });
            services.AddHttpClient(SpidOptions.SPID, (sp, c) =>
            {
                var opt = sp.GetRequiredService<IOptions<SpidOptions>>().Value;
                c.BaseAddress = new Uri(opt.EndPoint);
            });
            services.AddScoped<ISpidRequest, SpidRequest>();
            services.AddScoped<IQueryExecutor, QueryExecutor>();
            services.AddScoped<ICalculateurPoints, CalculateurPoints>();

            
            return services;
        }
    }
}