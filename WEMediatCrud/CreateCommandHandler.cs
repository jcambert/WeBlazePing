
namespace WeMediatCrud;
public interface ICreateCommand< TEntity,TKey> : IBaseCommand<TEntity,TKey,TEntity>
    where TEntity : class, IBaseEntity<TKey>, new()
{ }
internal class CreateCommand<TEntity, TKey> : ICreateCommand<TEntity, TKey>
     where TEntity : class, IBaseEntity<TKey>, new()
{

}

public interface IValidateableCreateCommand< TEntity, TKey> : IValidateableBaseCommand<TEntity, TKey,  TEntity>
    where TEntity : class, IBaseEntity<TKey>, new()
{ }

public class CreateCommandHandler<TContext, TEntity, TKey, TCommand> : BaseCommandHandler<TContext, TEntity, TKey, TCommand, TEntity>
where TContext : DbContext
where TEntity : class, IBaseEntity<TKey>, new()
where TCommand : class, ICreateCommand<TEntity,TKey>, new()

{
    public CreateCommandHandler(IRepository<TContext, TEntity, TKey> repository, IMapper mapper):base(repository, mapper) { 
    
    }

    public override async Task<TEntity> Handle(TCommand request, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<TCommand, TEntity>(request);
        await Repository.AddAsync(entity);

        return entity ;
    }
}

public class ValidateableCreateCommandHandler<TContext, TEntity, TKey, TCommand> : BaseValidateableCommandHandler<TContext, TEntity, TKey, TCommand, TEntity>
where TContext : DbContext
where TEntity : class, IBaseEntity<TKey>, new()
where TCommand : class, IValidateableCreateCommand< TEntity, TKey>,IValidateable, new()

{
    public ValidateableCreateCommandHandler(IRepository<TContext, TEntity, TKey> repository, IMapper mapper) : base(repository, mapper)
    {

    }

    
    public override async Task<ValidateableResponse<TEntity>> Handle(TCommand request, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<TCommand, TEntity>(request);
        await Repository.AddAsync(entity);

        return new ValidateableResponse<TEntity>(entity);
    }
}