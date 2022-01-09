namespace WeMediatCrud;
public interface IListQuery<TEntity, TKey> : IBaseCommand<TEntity, TKey, (int, IReadOnlyList<TEntity>)> 
    where TEntity : class, IBaseEntity<TKey>, new()
{
    int PageIndex { get; set; }

    int PageSize { get; set; }
}
public class ListQuery<TEntity, TKey> : IListQuery<TEntity, TKey>
    where TEntity : class, IBaseEntity<TKey>, new()
{
    public int PageIndex { get; set; }

    public int PageSize { get; set; }
}

public  class ListQueryHandler<TContext, TEntity, TKey, TCommand> : BaseCommandHandler<TContext, TEntity, TKey, TCommand,(int,IReadOnlyList<TEntity>)>
where TContext : DbContext
where TEntity : class, IBaseEntity<TKey>, new()
where TCommand : class, IListQuery<TEntity, TKey>, new()
{
   

    public ListQueryHandler(IRepository<TContext, TEntity, TKey> repository, IMapper mapper) : base(repository, mapper)
    {
    }

    public override async Task<(int, IReadOnlyList<TEntity>)> Handle(TCommand request, CancellationToken cancellationToken)
    {
        var query = Repository.GetQueryable();

        query = query.Skip(request.PageIndex * request.PageSize);

        if (request.PageSize > 0)
        {
            query = query.Take(request.PageSize);
        }
        
        var res= await query.ToListAsync(cancellationToken);
        return (res.Count, res);
    }
}