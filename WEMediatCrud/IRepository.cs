using AutoFilterer.Types;
using Microsoft.EntityFrameworkCore.Query;

namespace WeMediatCrud;

//@see https://github.com/furkandeveloper/EasyRepository.EFCore/blob/master/src/EasyRepository.EFCore.Generic/Repository.cs
public interface IRepository<TContext, TEntity, TKey>:IDisposable
where TContext : DbContext
where TEntity : class, IBaseEntity<TKey>, new()
{
    /// <summary>
    /// This method takes <see cref="{TEntity}"/> and performs entity insert operation. In additional this methods returns <see cref="{TEntity}"/>
    /// </summary>
    /// <typeparam name="TEntity">
    /// Type of Entity <see cref="{TEntity}"/>
    /// </typeparam>
    /// <param name="entity">
    /// The entity to be added
    /// </param>
    /// <returns>
    /// Returns <see cref="{TEntity}"/>
    /// </returns>
    TEntity Add(TEntity entity);
    /// <summary>
    /// This method takes <see cref="{TEntity}"/> and performs entity insert async. In additional this methods returns <see cref="Task{TEntity}"/> 
    /// </summary>
    /// <typeparam name="TEntity">
    /// Type of Entity <see cref="{TEntity}"/>
    /// </typeparam>
    /// <param name="entity">
    /// The entity to be added
    /// </param>
    /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>
    /// Returns <see cref="Task{TEntity}"/>
    /// </returns>
    Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);
    /// <summary>
    /// This methods takes <see cref="IEnumerable{TEntity}"/> and performs entity insert range operation. In additional this methods returns <see cref="IEnumerable{TEntity}"/>
    /// </summary>
    /// <typeparam name="TEntity">
    /// Type of entity
    /// </typeparam>
    /// <param name="entities">
    /// The entity to be added
    /// </param>
    /// <returns>
    /// Returns <see cref="IEnumerable{TEntity}"/>
    /// </returns>
    IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities);
    /// <summary>
    /// This method takes <see cref="IEnumerable{TEntity}"/> and performs entity insert range operation async version. In additional this methods returns <see cref="IEnumerable{TEntity}"/>
    /// </summary>
    /// <typeparam name="TEntity">
    /// Type of Entity
    /// </typeparam>
    /// <param name="entities">
    /// The entities to be added
    /// </param>
    /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>
    /// Returns <see cref="Task{IEnumerable{TEntity}}"/>
    /// </returns>
    Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

    bool Any(Expression<Func<TEntity, bool>> anyExpression);
    Task<bool> AnyAsync(Expression<Func<TEntity, bool>> anyExpression, CancellationToken cancellationToken = default);
    int Count();
    int Count(Expression<Func<TEntity, bool>> whereExpression);
    int Count<TFilter>(TFilter filter) where TFilter : FilterBase;
    Task<int> CountAsync(CancellationToken cancellationToken = default);
    Task<int> CountAsync(Expression<Func<TEntity, bool>> whereExpression, CancellationToken cancellationToken = default);
    Task<int> CountAsync<TFilter>(TFilter filter, CancellationToken cancellationToken = default) where TFilter : FilterBase;
    TEntity GetById(bool asNoTracking, object id);
    TEntity GetById(bool asNoTracking, object id, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeExpression);
    TProjected GetById<TProjected>(bool asNoTracking, object id, Expression<Func<TEntity, TProjected>> projectExpression);
    TProjected GetById<TProjected>(bool asNoTracking, object id, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeExpression, Expression<Func<TEntity, TProjected>> projectExpression);
    Task<TEntity> GetByIdAsync(bool asNoTracking, object id, CancellationToken cancellationToken = default);
    Task<TEntity> GetByIdAsync(bool asNoTracking, object id, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeExpression, CancellationToken cancellationToken = default);
    Task<TProjected> GetByIdAsync<TProjected>(bool asNoTracking, object id, Expression<Func<TEntity, TProjected>> projectExpression, CancellationToken cancellationToken = default);
    Task<TProjected> GetByIdAsync<TProjected>(bool asNoTracking, object id, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeExpression, Expression<Func<TEntity, TProjected>> projectExpression, CancellationToken cancellationToken = default);
    /// <summary>
    /// This method  returns List of Entity without filter. <see cref="List{TEntity}"/>
    /// </summary>
    /// <typeparam name="TEntity">
    /// Type of Entity
    /// </typeparam>
    /// <param name="asNoTracking">
    /// Do you want the entity to be tracked by EF Core? Default value : false <see cref="bool"/>
    /// </param>
    /// <returns>
    /// Returns <see cref="List{TEntity}"/>
    /// </returns>
    List<TEntity> GetMultiple(bool asNoTracking);

    List<TEntity> GetMultiple(bool asNoTracking, Expression<Func<TEntity, bool>> whereExpression);
    List<TEntity> GetMultiple(bool asNoTracking, Expression<Func<TEntity, bool>> whereExpression, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeExpression);
    List<TEntity> GetMultiple(bool asNoTracking, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeExpression);
    List<TProjected> GetMultiple<TFilter, TProjected>(bool asNoTracking, TFilter filter, Expression<Func<TEntity, TProjected>> projectExpression) where TFilter : FilterBase;
    List<TProjected> GetMultiple<TFilter, TProjected>(bool asNoTracking, TFilter filter, Expression<Func<TEntity, TProjected>> projectExpression, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeExpression) where TFilter : FilterBase;
    List<TEntity> GetMultiple<TFilter>(bool asNoTracking, TFilter filter) where TFilter : FilterBase;
    List<TEntity> GetMultiple<TFilter>(bool asNoTracking, TFilter filter, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeExpression) where TFilter : FilterBase;
    List<TProjected> GetMultiple<TProjected>(bool asNoTracking, Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, TProjected>> projectExpression);
    List<TProjected> GetMultiple<TProjected>(bool asNoTracking, Expression<Func<TEntity, bool>> whereExpression, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeExpression, Expression<Func<TEntity, TProjected>> projectExpression);
    /// <summary>
    /// This method provides without filter get all entity but you can convert it to any object you want.
    /// In additional this method takes <see cref="Expression{Func}"/> returns <see cref="List{TProjected}"/>
    /// </summary>
    /// <typeparam name="TEntity">
    /// Type of Entity
    /// </typeparam>
    /// <typeparam name="TProjected">
    /// Type of projected object
    /// </typeparam>
    /// <param name="projectExpression">
    /// Select expression
    /// </param>
    /// <param name="asNoTracking">
    /// Do you want the entity to be tracked by EF Core? Default value : false <see cref="bool"/>
    /// </param>
    /// <returns>
    /// Returns <see cref="List{TProjected}"/>
    /// </returns>
    List<TProjected> GetMultiple<TProjected>(bool asNoTracking, Expression<Func<TEntity, TProjected>> projectExpression);
    /// <summary>
    /// This method  returns List of Entity without filter async version. <see cref="List{TEntity}"/>
    /// </summary>
    /// <typeparam name="TEntity">
    /// Type of Entity
    /// </typeparam>
    /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <param name="asNoTracking">
    /// Do you want the entity to be tracked by EF Core? Default value : false <see cref="bool"/>
    /// </param>
    /// <returns>
    /// Returns <see cref="List{TEntity}"/>
    /// </returns>
    Task<List<TEntity>> GetMultipleAsync(bool asNoTracking, CancellationToken cancellationToken = default);
    Task<List<TEntity>> GetMultipleAsync(bool asNoTracking, Expression<Func<TEntity, bool>> whereExpression, CancellationToken cancellationToken = default);
    Task<List<TEntity>> GetMultipleAsync(bool asNoTracking, Expression<Func<TEntity, bool>> whereExpression, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeExpression, CancellationToken cancellationToken = default);
    Task<List<TEntity>> GetMultipleAsync(bool asNoTracking, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeExpression, CancellationToken cancellationToken = default);
    Task<List<TProjected>> GetMultipleAsync<TFilter, TProjected>(bool asNoTracking, TFilter filter, Expression<Func<TEntity, TProjected>> projectExpression, CancellationToken cancellationToken = default) where TFilter : FilterBase;
    Task<List<TProjected>> GetMultipleAsync<TFilter, TProjected>(bool asNoTracking, TFilter filter, Expression<Func<TEntity, TProjected>> projectExpression, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeExpression, CancellationToken cancellationToken = default) where TFilter : FilterBase;
    Task<List<TEntity>> GetMultipleAsync<TFilter>(bool asNoTracking, TFilter filter, CancellationToken cancellationToken = default) where TFilter : FilterBase;
    Task<List<TEntity>> GetMultipleAsync<TFilter>(bool asNoTracking, TFilter filter, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeExpression, CancellationToken cancellationToken = default) where TFilter : FilterBase;
    Task<List<TProjected>> GetMultipleAsync<TProjected>(bool asNoTracking, Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, TProjected>> projectExpression, CancellationToken cancellationToken = default);
    Task<List<TProjected>> GetMultipleAsync<TProjected>(bool asNoTracking, Expression<Func<TEntity, bool>> whereExpression, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeExpression, Expression<Func<TEntity, TProjected>> projectExpression, CancellationToken cancellationToken = default);
    /// <summary>
    /// This method takes <see cref="Expression{Func}"/> and <see cref="CancellationToken"/>. This method performs without filter get all entity but you can convert it to any object you want
    /// </summary>
    /// <typeparam name="TEntity">
    /// Type of entity
    /// </typeparam>
    /// <typeparam name="TProjected">
    /// Type of projected object
    /// </typeparam>
    /// <param name="projectExpression">
    /// Select expression
    /// </param>
    /// <param name="asNoTracking">
    /// Do you want the entity to be tracked by EF Core? Default value : false <see cref="bool"/>
    /// </param>
    /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>
    /// Returns <see cref="List{TProjected}"/>
    /// </returns>
    Task<List<TProjected>> GetMultipleAsync<TProjected>(bool asNoTracking, Expression<Func<TEntity, TProjected>> projectExpression, CancellationToken cancellationToken = default);
    /// <summary>
    /// This method provides entity queryable version. In additional this method returns <see cref="IQueryable{TEntity}"/>
    /// </summary>
    /// <typeparam name="TEntity">
    /// Type of Entity
    /// </typeparam>
    /// <returns>
    /// Returns <see cref="IQueryable{TEntity}"/>
    /// </returns>
    IQueryable<TEntity> GetQueryable();
    /// <summary>
    /// This method takes <see cref="Expression{TDelegate}"/> and apply filter to data source. In additional returns <see cref="IQueryable{TEntity}"/>
    /// </summary>
    /// <typeparam name="TEntity">
    /// Type of Entity
    /// </typeparam>
    /// <param name="filter">
    /// The filter to apply on the Entity.
    /// </param>
    /// <returns>
    /// Returns <see cref="IQueryable{TEntity}"/>
    /// </returns>
    IQueryable<TEntity> GetQueryable(Expression<Func<TEntity, bool>> filter);
    TEntity GetSingle(bool asNoTracking, Expression<Func<TEntity, bool>> whereExpression);
    TEntity GetSingle(bool asNoTracking, Expression<Func<TEntity, bool>> whereExpression, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeExpression);
    TEntity GetSingle<TFilter>(bool asNoTracking, TFilter filter) where TFilter : FilterBase;
    TEntity GetSingle<TFilter>(bool asNoTracking, TFilter filter, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeExpression) where TFilter : FilterBase;
    TProjected GetSingle<TProjected, TFilter>(bool asNoTracking, TFilter filter, Expression<Func<TEntity, TProjected>> projectExpression) where TFilter : FilterBase;
    TProjected GetSingle<TProjected, TFilter>(bool asNoTracking, TFilter filter, Expression<Func<TEntity, TProjected>> projectExpression, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeExpression) where TFilter : FilterBase;
    TProjected GetSingle<TProjected>(bool asNoTracking, Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, TProjected>> projectExpression);
    TProjected GetSingle<TProjected>(bool asNoTracking, Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, TProjected>> projectExpression, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeExpression);
    Task<TEntity> GetSingleAsync(bool asNoTracking, Expression<Func<TEntity, bool>> whereExpression, CancellationToken cancellationToken = default);
    Task<TEntity> GetSingleAsync(bool asNoTracking, Expression<Func<TEntity, bool>> whereExpression, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeExpression, CancellationToken cancellationToken = default);
    Task<TEntity> GetSingleAsync<TFilter>(bool asNoTracking, TFilter filter, CancellationToken cancellationToken = default) where TFilter : FilterBase;
    Task<TEntity> GetSingleAsync<TFilter>(bool asNoTracking, TFilter filter, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeExpression, CancellationToken cancellationToken = default) where TFilter : FilterBase;
    Task<TProjected> GetSingleAsync<TProjected, TFilter>(bool asNoTracking, TFilter filter, Expression<Func<TEntity, TProjected>> projectExpression, CancellationToken cancellationToken = default) where TFilter : FilterBase;
    Task<TProjected> GetSingleAsync<TProjected, TFilter>(bool asNoTracking, TFilter filter, Expression<Func<TEntity, TProjected>> projectExpression, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeExpression, CancellationToken cancellationToken = default) where TFilter : FilterBase;
    Task<TProjected> GetSingleAsync<TProjected>(bool asNoTracking, Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, TProjected>> projectExpression, CancellationToken cancellationToken = default);
    Task<TProjected> GetSingleAsync<TProjected>(bool asNoTracking, Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, TProjected>> projectExpression, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeExpression, CancellationToken cancellationToken = default);
    /// <summary>
    /// This method takes <see cref="Object"/> and performs entity hard delete operation
    /// </summary>
    /// <param name="id">
    /// PK of Entity
    /// </param>
    void HardDelete(object id);
    /// <summary>
    /// This method takes <see cref="{TEntity}"/> and performs entity hard delete operation.
    /// </summary>
    /// <typeparam name="TEntity">
    /// Type of entity
    /// </typeparam>
    /// <param name="entity">
    /// The entity to be deleted
    /// </param>
    void HardDelete(TEntity entity);
    /// <summary>
    /// This method takes <see cref="Object"/> an <see cref="CancellationToken"/>. This method performs hard delete operation async version. In additional returns <see cref="Task"/>
    /// </summary>
    /// <param name="id">
    /// Pk of Entity
    /// </param>
    /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>
    /// Returns <see cref="Task"/>
    /// </returns>
    Task HardDeleteAsync(object id, CancellationToken cancellationToken = default);
    /// <summary>
    /// This method takes <see cref="{TEntity}"/> and <see cref="CancellationToken"/>. This method performs entity hard delete operation. In additional this methods returns <see cref="Task"/>
    /// </summary>
    /// <typeparam name="TEntity">
    /// Type of entity
    /// </typeparam>
    /// <param name="entity">
    /// The entity to be deleted
    /// </param>
    /// <returns>
    /// Returns <see cref="Task"/>
    /// </returns>
    Task HardDeleteAsync(TEntity entity, CancellationToken cancellationToken = default);
    /// <summary>
    /// This method takes <see cref="{TEntity}"/> performs replace operation. In additional returns <see cref="{TEntity}"/>
    /// </summary>
    /// <typeparam name="TEntity">
    /// Type of Entity
    /// </typeparam>
    /// <param name="entity">
    /// The entity to be replaced
    /// </param>
    TEntity Replace(TEntity entity);
    /// <summary>
    /// This method takes <see cref="{TEntity}"/> and <see cref="CancellationToken"/> performs replace operation async version. In additional this methods returns <see cref="Task{TEntity}"/>
    /// </summary>
    /// <typeparam name="TEntity">
    /// Type of Entity
    /// </typeparam>
    /// <param name="entity">
    /// The entity to be replaced
    /// </param>
    /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>
    /// Returns <see cref="Task"/>
    /// </returns>
    Task<TEntity> ReplaceAsync(TEntity entity, CancellationToken cancellationToken = default);
    /// <summary>
    /// This method takes <see cref="{TKey}"/> and performs soft delete operation
    /// </summary>
    /// <typeparam name="TEntity">
    /// Type of entity
    /// </typeparam>
    /// <typeparam name="TKey">
    /// Type of Primary Key
    /// </typeparam>
    /// <param name="id">
    /// PK of Entity
    /// </param>
    void SoftDelete(TEntity entity);
    /// <summary>
    /// This method takes <see cref="{TKey}"/> and performs soft delete operation by id.
    /// </summary>
    /// <typeparam name="TEntity">
    /// Type of entity
    /// </typeparam>
    /// <typeparam name="TKey">
    /// Type of Primary Key
    /// </typeparam>
    /// <param name="id">
    /// PK of Entity
    /// </param>
    void SoftDelete(TKey id);
    /// <summary>
    /// This method takes <see cref="{TEntity}"/> performs soft delete operation. In additional this method returns <see cref="Task"/>
    /// </summary>
    /// <typeparam name="TEntity">
    /// Type of Entity
    /// </typeparam>
    /// <typeparam name="TKey">
    /// Type of Primary Key
    /// </typeparam>
    /// <param name="entity">
    /// The entity to be deleted
    /// </param>
    /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>
    /// Returns <see cref="Task"/>
    /// </returns>
    Task SoftDeleteAsync(TEntity entity, CancellationToken cancellationToken = default);
    /// <summary>
    /// This method takes <see cref="{TKey}"/> and <see cref="CancellationToken"/>. This method performs soft delete operation by id async version. In additional this method returns <see cref="Task"/>
    /// </summary>
    /// <typeparam name="TEntity">
    /// Type of Entity
    /// </typeparam>
    /// <typeparam name="TKey">
    /// Type of Primary Key
    /// </typeparam>
    /// <param name="id">
    /// PK of Entity
    /// </param>
    /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>
    /// Returns <see cref="Task"/>
    /// </returns>
    Task SoftDeleteAsync(TKey id, CancellationToken cancellationToken = default);
    /// <summary>
    /// This method takes <see cref="{TEntity}"/> performs update operation. In additional returns <see cref="{TEntity}"/>
    /// </summary>
    /// <typeparam name="TEntity">
    /// Type of Entity
    /// </typeparam>
    /// <param name="entity">
    /// The entity to be updated
    /// </param>
    TEntity Update(TEntity entity);
    /// <summary>
    /// This method takes <see cref="{TEntity}"/> and <see cref="CancellationToken"/> performs update operation async version. In additional this methods returns <see cref="Task{TEntity}"/>
    /// </summary>
    /// <typeparam name="TEntity">
    /// Type of Entity
    /// </typeparam>
    /// <param name="entity">
    /// The entity to be updated
    /// </param>
    /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>
    /// Returns <see cref="Task"/>
    /// </returns>
    Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
    /// <summary>
    /// This method takes <see cref="IEnumerable{TEntity}"/> performs update operation. In additional returns <see cref="IEnumerable{TEntity}"/>
    /// </summary>
    /// <typeparam name="TEntity">
    /// Type of Entity
    /// </typeparam>
    /// <param name="entities">
    /// The entities to be updated
    /// </param>
    IEnumerable<TEntity> UpdateRange(IEnumerable<TEntity> entities);
    /// <summary>
    /// This method takes <see cref="IEnumerable{TEntity}"/> and <see cref="CancellationToken"/> performs update operation async version. In additional this methods returns <see cref="Task{TResult}"/>
    /// </summary>
    /// <typeparam name="TEntity">
    /// Type of Entity
    /// </typeparam>
    /// <param name="entities">
    /// The entities to be updated
    /// </param>
    /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>
    /// Returns <see cref="Task{TResult}"/>
    /// </returns>
    Task<IEnumerable<TEntity>> UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
}
