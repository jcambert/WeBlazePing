namespace WeMediatCrud;

public interface IBaseCommand<out TEntity, TKey,  TResponse> : IRequest<TResponse>
    where TEntity : class, IBaseEntity<TKey>, new()
    //where TResponse:class
{ }
public interface IValidateableBaseCommand< TEntity, TKey, TResponse> : IRequest<ValidateableResponse< TResponse>>
    where TEntity : class, IBaseEntity<TKey>, new()
    where TResponse:class
{ }

public abstract class BaseCommandHandler<TContext, TEntity, TKey, TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
where TContext : DbContext
where TEntity : class, IBaseEntity<TKey>, new()
where TCommand : class, IBaseCommand<TEntity, TKey, TResponse>, new()
    //where TResponse : class
{
    protected readonly IRepository<TContext, TEntity, TKey> Repository;
    protected readonly IMapper Mapper;

    public BaseCommandHandler(IRepository<TContext, TEntity, TKey> repository, IMapper mapper)
    {
        this.Repository = repository;
        this.Mapper = mapper;
    }
    public abstract Task<TResponse> Handle(TCommand request, CancellationToken cancellationToken);

}

public abstract class BaseValidateableCommandHandler<TContext, TEntity, TKey, TCommand, TResponse> : IRequestHandler<TCommand, ValidateableResponse<TResponse>>
    where TContext : DbContext
    where TEntity : class, IBaseEntity<TKey>, new()
    where TCommand : class, IValidateableBaseCommand<TEntity, TKey, TResponse>, IValidateable, new()
    where TResponse:class
{
    protected readonly IRepository<TContext, TEntity, TKey> Repository;
    protected readonly IMapper Mapper;
    public BaseValidateableCommandHandler(IRepository<TContext, TEntity, TKey> repository, IMapper mapper)
    {
        this.Repository = repository;
        this.Mapper = mapper;
    }

    public abstract Task<ValidateableResponse<TResponse>> Handle(TCommand request, CancellationToken cancellationToken);


}

