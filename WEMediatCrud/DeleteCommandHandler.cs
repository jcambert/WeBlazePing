namespace WeMediatCrud;

public interface IDeleteCommand<out TEntity,TKey> : IBaseCommand<TEntity, TKey,Unit> 
    where TEntity : class, IBaseEntity<TKey>, new()
    
{
    TKey Id { get; }
}
internal class DeleteCommand<TEntity, TKey> : IDeleteCommand<TEntity, TKey>
    where TEntity : class, IBaseEntity<TKey>, new()
{
    public TKey Id { get; set; }
}
public class DeleteCommandHandler<TContext, TEntity, TKey, TCommand> : BaseCommandHandler<TContext, TEntity, TKey, TCommand,Unit>
where TContext : DbContext
where TEntity : class, IBaseEntity<TKey>, new()
where TCommand : class, IDeleteCommand<TEntity, TKey>, new()

{
 

    protected DeleteCommandHandler(IRepository<TContext, TEntity, TKey> repository, IMapper mapper) : base(repository, mapper)
    {
    }

    public override async Task<Unit> Handle(TCommand request, CancellationToken cancellationToken)
    {
        await Repository.SoftDeleteAsync(request.Id);
        

        return Unit.Value;
    }
}
