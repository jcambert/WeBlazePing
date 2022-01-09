namespace WeMediatCrud;

public interface IUpdateCommand<TEntity,TKey> :
    IBaseCommand<TEntity, TKey,TEntity> where TEntity : class, IBaseEntity<TKey>, new() 
{
    TKey Id { get; }
}
internal class UpdateCommand<TEntity, TKey> : IUpdateCommand<TEntity, TKey>
    where TEntity : class, IBaseEntity<TKey>, new()
{
    public TKey Id { get; set; }
}
public class UpdateCommandHandler<TContext, TEntity, TKey, TCommand> : BaseCommandHandler<TContext, TEntity, TKey, TCommand,TEntity>
where TContext : DbContext
where TEntity : class, IBaseEntity<TKey>, new()
where TCommand : class, IUpdateCommand<TEntity, TKey>, new()
{


    protected UpdateCommandHandler(IRepository<TContext, TEntity, TKey> repository, IMapper mapper) : base(repository, mapper)
    {
        
    }

    public override async Task<TEntity> Handle(TCommand request, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<TCommand, TEntity>(request);
        await Repository.UpdateAsync(entity);
        

        return entity;
    }
}

