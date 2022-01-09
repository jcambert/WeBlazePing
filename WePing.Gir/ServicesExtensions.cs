using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WePing.Gir.Checkings;

namespace WePing.Gir
{
    public static class ServicesExtensions
    {
        public static IServiceCollection  AddGir(this IServiceCollection services, string connectionString)
        {
           
            services.AddDbContext<GirContext>(opt => opt.UseNpgsql(connectionString), ServiceLifetime.Transient);
            services.AddDbContextFactory<GirContext>(opt => opt.UseNpgsql(connectionString), ServiceLifetime.Transient);
            
            services.AddScoped<ITryAddJoueur, TryAddJoueurIntoEquipe>();

           
           
            services.AddScoped(typeof(IRequestHandler<ListQuery<Rencontre,Guid>, (int, IReadOnlyList<Rencontre>)>), typeof(ListQueryHandler<GirContext,Rencontre,Guid,ListQuery<Rencontre,Guid>>));

            services.AddValidatorsFromAssembly(typeof(GirContext).Assembly);

            return services;
        }
    }
}
