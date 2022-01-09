namespace WeMediatCrud;

public interface IGetQueryCommand< TEntity,TKey> : IBaseCommand<TEntity, TKey,TEntity>
    where TEntity : class, IBaseEntity<TKey>, new()
{
    
    TKey Id { get; }
}
internal class GetQueryCommand<TEntity, TKey> : IGetQueryCommand<TEntity, TKey>
    where TEntity : class, IBaseEntity<TKey>, new()
{
    public TKey Id { get; set; }
}
public  class GetQueryHandler<TContext, TEntity, TKey, TCommand> : BaseCommandHandler<TContext, TEntity, TKey, TCommand,TEntity>
where TContext : DbContext
where TEntity : class, IBaseEntity<TKey>, new()
where TCommand : class, IGetQueryCommand<TEntity, TKey>, new()
{
 

    protected GetQueryHandler(IRepository<TContext, TEntity, TKey> repository, IMapper mapper) : base(repository, mapper)
    {
    }

    public override async Task<TEntity> Handle(TCommand request, CancellationToken cancellationToken)
    =>await Repository.GetByIdAsync(true,request.Id); 

}
