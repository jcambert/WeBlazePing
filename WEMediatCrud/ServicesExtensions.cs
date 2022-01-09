
using System.Reflection;


namespace WeMediatCrud;

public static class ServicesExtensions
{
    public static void AddMediatCrud(this IServiceCollection services,List<Assembly> handlerTypes,Action<DbContextOptionsBuilder> optionDbContextAction=null)
        
    {
        services.AddScoped(typeof(IRepository<,,>), typeof(Repository<,,>));
        services.AddScoped(typeof(IPipelineBehavior<,>),typeof(ValidationPipeline<,>));
        services.AddTransient(typeof(IUpdateCommand<,>), typeof(UpdateCommand<,>));
        services.AddTransient(typeof(IGetQueryCommand<,>), typeof(GetQueryCommand<,>));
        services.AddTransient(typeof(ICreateCommand<,>), typeof(CreateCommand<,>));
        services.AddTransient(typeof(IDeleteCommand<,>), typeof(DeleteCommand<,>));
        services.AddTransient(typeof(IListQuery<,>), typeof(ListQuery<,>));

        

        services.AddMediatR(handlerTypes.ToArray());

        services.AddAutoMapper(a =>
        {

        });
        services.AddScoped(typeof(IMediatorAction),typeof(MediatorAction));

    }
}
