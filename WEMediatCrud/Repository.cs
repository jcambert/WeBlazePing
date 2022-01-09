using AutoFilterer.Extensions;
using AutoFilterer.Types;
using Microsoft.EntityFrameworkCore.Query;
using System.Globalization;

namespace WeMediatCrud;

public class Repository<TContext, TEntity, TKey> : IRepository<TContext, TEntity, TKey>
    where TContext : DbContext
    where TEntity : class, IBaseEntity<TKey>, new()
{
    private readonly DbContext db;
    public Repository(IDbContextFactory<TContext> context, ILogger<Repository<TContext, TEntity, TKey>> logger)
    {
        this.ContextFactory = context;
        this.db = CreateDbContext();
    }

    #region Privates Methods
    private Expression<Func<TEntity, bool>> GenerateExpression(object id)
    {
        

            var type = db.Model.FindEntityType(typeof(TEntity));
            string pk = type.FindPrimaryKey().Properties.Select(s => s.Name).FirstOrDefault();
            Type pkType = type.FindPrimaryKey().Properties.Select(p => p.ClrType).FirstOrDefault();

            object value = Convert.ChangeType(id, pkType, CultureInfo.InvariantCulture);

            ParameterExpression pe = Expression.Parameter(typeof(TEntity), "entity");
            MemberExpression me = Expression.Property(pe, pk);
            ConstantExpression constant = Expression.Constant(value, pkType);
            BinaryExpression body = Expression.Equal(me, constant);
            Expression<Func<TEntity, bool>> expression = Expression.Lambda<Func<TEntity, bool>>(body, new[] { pe });

            return expression;
        
    }

    private IQueryable<TEntity> FindQueryable(bool asNoTracking)
    {
        var queryable = GetQueryable();
        if (asNoTracking)
        {
            queryable = queryable.AsNoTracking();
        }
        return queryable;
    }
    #endregion

    #region Protected Methods
    protected readonly IDbContextFactory<TContext> ContextFactory;
    private bool disposedValue;

    //
    // Résumé :
    //     Creates a new Microsoft.EntityFrameworkCore.DbContext instance.
    //     The caller is responsible for disposing the context; it will not be disposed
    //     by any dependency injection container.
    //
    // Retourne :
    //     A new context instance.
    protected TContext CreateDbContext() => ContextFactory.CreateDbContext();

    //
    // Résumé :
    //     Creates a new Microsoft.EntityFrameworkCore.DbContext instance in an async context.
    //     The caller is responsible for disposing the context; it will not be disposed
    //     by any dependency injection container.
    //
    // Paramètres :
    //   cancellationToken:
    //     A System.Threading.CancellationToken to observe while waiting for the task to
    //     complete.
    //
    // Retourne :
    //     A task containing the created context that represents the asynchronous operation.
    //
    // Exceptions :
    //   T:System.OperationCanceledException:
    //     If the System.Threading.CancellationToken is canceled.
    Task<TContext> CreateDbContextAsync(CancellationToken cancellationToken = default(CancellationToken))
    => ContextFactory.CreateDbContextAsync(cancellationToken);
    #endregion

    public TEntity Add(TEntity entity)
    {
        if (entity is ICreateDateEntity)
            (entity as ICreateDateEntity).CreationDate = DateTime.UtcNow;
        
            db.Set<TEntity>().Add(entity);
            db.SaveChanges();
        
        return entity;
    }

    public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        if (entity is ICreateDateEntity)
            (entity as ICreateDateEntity).CreationDate = DateTime.UtcNow;
       
            await db.Set<TEntity>().AddAsync(entity, cancellationToken);
            await db.SaveChangesAsync();
        
        return entity;
    }


    public IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities)
    {
        entities.Where(e => e is ICreateDateEntity).Select(e => e as ICreateDateEntity).ToList().ForEach(x => x.CreationDate = DateTime.UtcNow);
       
            db.Set<TEntity>().AddRange(entities);
            db.SaveChanges();

        
        return entities;
    }

    public async Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        entities.Where(e => e is ICreateDateEntity).Select(e => e as ICreateDateEntity).ToList().ForEach(x => x.CreationDate = DateTime.UtcNow);
      
            await db.Set<TEntity>().AddRangeAsync(entities);
            await db.SaveChangesAsync();

        
        return entities;
    }


    public void HardDelete(TEntity entity)
    {
       
            db.Set<TEntity>().Remove(entity);
            db.SaveChanges();
        
    }

    public async Task HardDeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        
            db.Set<TEntity>().Remove(entity);
            await db.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

    }

    public void HardDelete(object id)
    {
       
            var entity = db.Set<TEntity>().Find(id);
            db.Set<TEntity>().Remove(entity);
            db.SaveChanges();

        
    }

    public async Task HardDeleteAsync(object id, CancellationToken cancellationToken = default)
    {
        
            var entity = await db.Set<TEntity>().FirstOrDefaultAsync(GenerateExpression(id), cancellationToken).ConfigureAwait(false);
            db.Set<TEntity>().Remove(entity);
            await db.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        
    }



    public void SoftDelete(TEntity entity)
    {
        if (entity is ISoftDeleteEntity)
        {
            (entity as ISoftDeleteEntity).IsDeleted = true;
            (entity as ISoftDeleteEntity).DeletionDate = DateTime.UtcNow;

        }
        Replace(entity);
    }

    public async Task SoftDeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        
            if (entity is ISoftDeleteEntity)
            {
                (entity as ISoftDeleteEntity).IsDeleted = true;
                (entity as ISoftDeleteEntity).DeletionDate = DateTime.UtcNow;

            }
            await ReplaceAsync(entity, cancellationToken).ConfigureAwait(false);
        
    }

    public void SoftDelete(TKey id)
    {
        
            var entity = db.Set<TEntity>().FirstOrDefault(GenerateExpression(id));
            if (entity is ISoftDeleteEntity)
            {
                (entity as ISoftDeleteEntity).IsDeleted = true;
                (entity as ISoftDeleteEntity).DeletionDate = DateTime.UtcNow;

            }
            Replace(entity);

        
    }

    public async Task SoftDeleteAsync(TKey id, CancellationToken cancellationToken = default)
    {
        
            var entity = await db.Set<TEntity>().FirstOrDefaultAsync(GenerateExpression(id), cancellationToken).ConfigureAwait(false); ;
            if (entity is ISoftDeleteEntity)
            {
                (entity as ISoftDeleteEntity).IsDeleted = true;
                (entity as ISoftDeleteEntity).DeletionDate = DateTime.UtcNow;

            }
            await ReplaceAsync(entity, cancellationToken).ConfigureAwait(false);

        
    }

    public TEntity Update(TEntity entity)
    {
        
            if (entity is IUpdateDateEntity)
                (entity as IUpdateDateEntity).ModificationDate = DateTime.UtcNow;
            db.Set<TEntity>().Update(entity);
            db.SaveChanges();
            return entity;
        
    }

    public async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        
            if (entity is IUpdateDateEntity)
                (entity as IUpdateDateEntity).ModificationDate = DateTime.UtcNow;
            db.Set<TEntity>().Update(entity);
            await db.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return entity;

        
    }

    public IEnumerable<TEntity> UpdateRange(IEnumerable<TEntity> entities)
    {
        entities.Where(e => e is IUpdateDateEntity).Select(e => e as IUpdateDateEntity).ToList().ForEach(a => a.ModificationDate = DateTime.UtcNow);
        
            db.Set<TEntity>().UpdateRange(entities);
            db.SaveChanges();

        
        return entities;
    }

    public async Task<IEnumerable<TEntity>> UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {

        entities.Where(e => e is IUpdateDateEntity).Select(e => e as IUpdateDateEntity).ToList().ForEach(a => a.ModificationDate = DateTime.UtcNow);
        
            db.Set<TEntity>().UpdateRange(entities);
            await db.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return entities;

        
    }


    public TEntity Replace(TEntity entity)
    {
        if (entity is IUpdateDateEntity)
            (entity as IUpdateDateEntity).ModificationDate = DateTime.UtcNow;
        
            db.Entry(entity).State = EntityState.Modified;
            db.SaveChanges();
            return entity;

    }

    public async Task<TEntity> ReplaceAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        if (entity is IUpdateDateEntity)
            (entity as IUpdateDateEntity).ModificationDate = DateTime.UtcNow;
      
            db.Entry(entity).State = EntityState.Modified;
            await db.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return entity;
        
    }



    public IQueryable<TEntity> GetQueryable()
    => db.Set<TEntity>().AsQueryable();

    

    public IQueryable<TEntity> GetQueryable(Expression<Func<TEntity, bool>> filter)
    => db.Set<TEntity>().Where(filter);

    public List<TEntity> GetMultiple(bool asNoTracking)
    => FindQueryable(asNoTracking).ToList();


    public async Task<List<TEntity>> GetMultipleAsync(bool asNoTracking, CancellationToken cancellationToken = default)
    => await FindQueryable(asNoTracking).ToListAsync(cancellationToken).ConfigureAwait(false);


    public List<TProjected> GetMultiple<TProjected>(bool asNoTracking, Expression<Func<TEntity, TProjected>> projectExpression)
    => FindQueryable(asNoTracking).Select(projectExpression).ToList();

    public async Task<List<TProjected>> GetMultipleAsync<TProjected>(bool asNoTracking, Expression<Func<TEntity, TProjected>> projectExpression, CancellationToken cancellationToken = default)
    => await FindQueryable(asNoTracking).Select(projectExpression).ToListAsync(cancellationToken).ConfigureAwait(false);

    public List<TEntity> GetMultiple(bool asNoTracking, Expression<Func<TEntity, bool>> whereExpression)
    => FindQueryable(asNoTracking).Where(whereExpression).ToList();

    public async Task<List<TEntity>> GetMultipleAsync(bool asNoTracking, Expression<Func<TEntity, bool>> whereExpression, CancellationToken cancellationToken = default)
    => await FindQueryable(asNoTracking).Where(whereExpression).ToListAsync(cancellationToken).ConfigureAwait(false);

    public List<TProjected> GetMultiple<TProjected>(bool asNoTracking, Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, TProjected>> projectExpression)
    => FindQueryable(asNoTracking).Where(whereExpression).Select(projectExpression).ToList();

    public async Task<List<TProjected>> GetMultipleAsync<TProjected>(bool asNoTracking, Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, TProjected>> projectExpression, CancellationToken cancellationToken = default)
    => await FindQueryable(asNoTracking).Where(whereExpression).Select(projectExpression).ToListAsync(cancellationToken).ConfigureAwait(false);

    public List<TEntity> GetMultiple(bool asNoTracking, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeExpression)
    => includeExpression(FindQueryable(asNoTracking)).ToList();


    public async Task<List<TEntity>> GetMultipleAsync(bool asNoTracking, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeExpression, CancellationToken cancellationToken = default)
    => await (includeExpression(FindQueryable(asNoTracking))).ToListAsync(cancellationToken).ConfigureAwait(false);


    public List<TEntity> GetMultiple(bool asNoTracking, Expression<Func<TEntity, bool>> whereExpression, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeExpression)
    => includeExpression(FindQueryable(asNoTracking).Where(whereExpression)).ToList();


    public async Task<List<TEntity>> GetMultipleAsync(bool asNoTracking, Expression<Func<TEntity, bool>> whereExpression, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeExpression, CancellationToken cancellationToken = default)
    => await (includeExpression(FindQueryable(asNoTracking).Where(whereExpression))).ToListAsync(cancellationToken).ConfigureAwait(false);


    public List<TProjected> GetMultiple<TProjected>(bool asNoTracking, Expression<Func<TEntity, bool>> whereExpression, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeExpression, Expression<Func<TEntity, TProjected>> projectExpression)
    => includeExpression(FindQueryable(asNoTracking).Where(whereExpression)).Select(projectExpression).ToList();


    public async Task<List<TProjected>> GetMultipleAsync<TProjected>(bool asNoTracking, Expression<Func<TEntity, bool>> whereExpression, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeExpression, Expression<Func<TEntity, TProjected>> projectExpression, CancellationToken cancellationToken = default)
    => await (includeExpression(FindQueryable(asNoTracking).Where(whereExpression))).Select(projectExpression).ToListAsync(cancellationToken).ConfigureAwait(false);


    public List<TEntity> GetMultiple<TFilter>(bool asNoTracking, TFilter filter) where TFilter : FilterBase
    => FindQueryable(asNoTracking).ApplyFilter(filter).ToList();

    public async Task<List<TEntity>> GetMultipleAsync<TFilter>(bool asNoTracking, TFilter filter, CancellationToken cancellationToken = default) where TFilter : FilterBase
    => await FindQueryable(asNoTracking).ApplyFilter(filter).ToListAsync(cancellationToken).ConfigureAwait(false);

    public List<TEntity> GetMultiple<TFilter>(bool asNoTracking, TFilter filter, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeExpression) where TFilter : FilterBase
    => includeExpression(FindQueryable(asNoTracking).ApplyFilter(filter)).ToList();


    public async Task<List<TEntity>> GetMultipleAsync<TFilter>(bool asNoTracking, TFilter filter, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeExpression, CancellationToken cancellationToken = default) where TFilter : FilterBase
    => await (includeExpression(FindQueryable(asNoTracking).ApplyFilter(filter))).ToListAsync(cancellationToken).ConfigureAwait(false);


    public List<TProjected> GetMultiple<TFilter, TProjected>(bool asNoTracking, TFilter filter, Expression<Func<TEntity, TProjected>> projectExpression) where TFilter : FilterBase
    => FindQueryable(asNoTracking).ApplyFilter(filter).Select(projectExpression).ToList();

    public async Task<List<TProjected>> GetMultipleAsync<TFilter, TProjected>(bool asNoTracking, TFilter filter, Expression<Func<TEntity, TProjected>> projectExpression, CancellationToken cancellationToken = default) where TFilter : FilterBase
    => await FindQueryable(asNoTracking).ApplyFilter(filter).Select(projectExpression).ToListAsync(cancellationToken).ConfigureAwait(false);

    public List<TProjected> GetMultiple<TFilter, TProjected>(bool asNoTracking, TFilter filter, Expression<Func<TEntity, TProjected>> projectExpression, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeExpression) where TFilter : FilterBase
    => (includeExpression(FindQueryable(asNoTracking).ApplyFilter(filter))).Select(projectExpression).ToList();

    public async Task<List<TProjected>> GetMultipleAsync<TFilter, TProjected>(bool asNoTracking, TFilter filter, Expression<Func<TEntity, TProjected>> projectExpression, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeExpression, CancellationToken cancellationToken = default) where TFilter : FilterBase
    => await (includeExpression(FindQueryable(asNoTracking).ApplyFilter(filter))).Select(projectExpression).ToListAsync(cancellationToken).ConfigureAwait(false);


    public TEntity GetSingle(bool asNoTracking, Expression<Func<TEntity, bool>> whereExpression)
    => FindQueryable(asNoTracking).Where(whereExpression).FirstOrDefault();

    public async Task<TEntity> GetSingleAsync(bool asNoTracking, Expression<Func<TEntity, bool>> whereExpression, CancellationToken cancellationToken = default)
    => await (FindQueryable(asNoTracking).Where(whereExpression)).FirstOrDefaultAsync(cancellationToken).ConfigureAwait(false);


    public TEntity GetSingle(bool asNoTracking, Expression<Func<TEntity, bool>> whereExpression, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeExpression)
    => includeExpression(FindQueryable(asNoTracking).Where(whereExpression)).FirstOrDefault();


    public async Task<TEntity> GetSingleAsync(bool asNoTracking, Expression<Func<TEntity, bool>> whereExpression, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeExpression, CancellationToken cancellationToken = default)
    => await (includeExpression(FindQueryable(asNoTracking).Where(whereExpression))).FirstOrDefaultAsync(cancellationToken).ConfigureAwait(false);


    public TProjected GetSingle<TProjected>(bool asNoTracking, Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, TProjected>> projectExpression)
    => FindQueryable(asNoTracking).Where(whereExpression).Select(projectExpression).FirstOrDefault();

    public async Task<TProjected> GetSingleAsync<TProjected>(bool asNoTracking, Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, TProjected>> projectExpression, CancellationToken cancellationToken = default)
    => await FindQueryable(asNoTracking).Where(whereExpression).Select(projectExpression).FirstOrDefaultAsync(cancellationToken).ConfigureAwait(false);

    public TProjected GetSingle<TProjected>(bool asNoTracking, Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, TProjected>> projectExpression, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeExpression)
    => includeExpression(FindQueryable(asNoTracking).Where(whereExpression)).Select(projectExpression).FirstOrDefault();

    public async Task<TProjected> GetSingleAsync<TProjected>(bool asNoTracking, Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, TProjected>> projectExpression, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeExpression, CancellationToken cancellationToken = default)
    => await (includeExpression(FindQueryable(asNoTracking).Where(whereExpression))).Select(projectExpression).FirstOrDefaultAsync(cancellationToken).ConfigureAwait(false);


    public TEntity GetSingle<TFilter>(bool asNoTracking, TFilter filter) where TFilter : FilterBase
    => FindQueryable(asNoTracking).ApplyFilter(filter).FirstOrDefault();



    public async Task<TEntity> GetSingleAsync<TFilter>(bool asNoTracking, TFilter filter, CancellationToken cancellationToken = default) where TFilter : FilterBase
    => await (FindQueryable(asNoTracking).ApplyFilter(filter)).FirstOrDefaultAsync(cancellationToken).ConfigureAwait(false);


    public TEntity GetSingle<TFilter>(bool asNoTracking, TFilter filter, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeExpression) where TFilter : FilterBase
    => includeExpression(FindQueryable(asNoTracking).ApplyFilter(filter)).FirstOrDefault();


    public async Task<TEntity> GetSingleAsync<TFilter>(bool asNoTracking, TFilter filter, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeExpression, CancellationToken cancellationToken = default) where TFilter : FilterBase
    => await (includeExpression(FindQueryable(asNoTracking).ApplyFilter(filter))).FirstOrDefaultAsync(cancellationToken).ConfigureAwait(false);

    public TProjected GetSingle<TProjected, TFilter>(bool asNoTracking, TFilter filter, Expression<Func<TEntity, TProjected>> projectExpression) where TFilter : FilterBase
    => FindQueryable(asNoTracking).ApplyFilter(filter).Select(projectExpression).FirstOrDefault();



    public async Task<TProjected> GetSingleAsync<TProjected, TFilter>(bool asNoTracking, TFilter filter, Expression<Func<TEntity, TProjected>> projectExpression, CancellationToken cancellationToken = default) where TFilter : FilterBase
    => await (FindQueryable(asNoTracking).ApplyFilter(filter)).Select(projectExpression).FirstOrDefaultAsync(cancellationToken).ConfigureAwait(false);

    public TProjected GetSingle<TProjected, TFilter>(bool asNoTracking, TFilter filter, Expression<Func<TEntity, TProjected>> projectExpression, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeExpression) where TFilter : FilterBase
    => includeExpression(FindQueryable(asNoTracking).ApplyFilter(filter)).Select(projectExpression).FirstOrDefault();


    public async Task<TProjected> GetSingleAsync<TProjected, TFilter>(bool asNoTracking, TFilter filter, Expression<Func<TEntity, TProjected>> projectExpression, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeExpression, CancellationToken cancellationToken = default) where TFilter : FilterBase
    => await (includeExpression(FindQueryable(asNoTracking).ApplyFilter(filter))).Select(projectExpression).FirstOrDefaultAsync(cancellationToken).ConfigureAwait(false);


    public TEntity GetById(bool asNoTracking, object id)
    => db.Set<TEntity>().FirstOrDefault(GenerateExpression(id));
      

    public async Task<TEntity> GetByIdAsync(bool asNoTracking, object id, CancellationToken cancellationToken = default)
    => await FindQueryable(asNoTracking).FirstOrDefaultAsync(GenerateExpression(id), cancellationToken).ConfigureAwait(false);


    public TEntity GetById(bool asNoTracking, object id, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeExpression)
    => includeExpression(FindQueryable(asNoTracking).Where(GenerateExpression(id))).FirstOrDefault();

    public async Task<TEntity> GetByIdAsync(bool asNoTracking, object id, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeExpression, CancellationToken cancellationToken = default)
    => await (includeExpression(FindQueryable(asNoTracking).Where(GenerateExpression(id)))).FirstOrDefaultAsync(cancellationToken).ConfigureAwait(false);


    public TProjected GetById<TProjected>(bool asNoTracking, object id, Expression<Func<TEntity, TProjected>> projectExpression)
    => FindQueryable(asNoTracking).Where(GenerateExpression(id)).Select(projectExpression).FirstOrDefault();


    public async Task<TProjected> GetByIdAsync<TProjected>(bool asNoTracking, object id, Expression<Func<TEntity, TProjected>> projectExpression, CancellationToken cancellationToken = default)
    => await (FindQueryable(asNoTracking).Where(GenerateExpression(id))).Select(projectExpression).FirstOrDefaultAsync(cancellationToken).ConfigureAwait(false);


    public TProjected GetById<TProjected>(bool asNoTracking, object id, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeExpression, Expression<Func<TEntity, TProjected>> projectExpression)
    => includeExpression(FindQueryable(asNoTracking).Where(GenerateExpression(id))).Select(projectExpression).FirstOrDefault();


    public async Task<TProjected> GetByIdAsync<TProjected>(bool asNoTracking, object id, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeExpression, Expression<Func<TEntity, TProjected>> projectExpression, CancellationToken cancellationToken = default)
    => await (includeExpression(FindQueryable(asNoTracking).Where(GenerateExpression(id)))).Select(projectExpression).FirstOrDefaultAsync(cancellationToken).ConfigureAwait(false);


    public bool Any(Expression<Func<TEntity, bool>> anyExpression)
    =>db.Set<TEntity>().Any(anyExpression);

    public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> anyExpression, CancellationToken cancellationToken = default)
    => await db.Set<TEntity>().AnyAsync(anyExpression, cancellationToken).ConfigureAwait(false);


    public int Count()
    => db.Set<TEntity>().Count();
        

    public async Task<int> CountAsync(CancellationToken cancellationToken = default)
    => await db.Set<TEntity>().CountAsync(cancellationToken);
        
    

    public int Count(Expression<Func<TEntity, bool>> whereExpression)
    => db.Set<TEntity>().Where(whereExpression).Count();

    public async Task<int> CountAsync(Expression<Func<TEntity, bool>> whereExpression, CancellationToken cancellationToken = default)
    =>await db.Set<TEntity>().Where(whereExpression).CountAsync(cancellationToken);

    public int Count<TFilter>(TFilter filter) where TFilter : FilterBase
    =>db.Set<TEntity>().ApplyFilter(filter).Count();

    public async Task<int> CountAsync<TFilter>(TFilter filter, CancellationToken cancellationToken = default) where TFilter : FilterBase
    => await db.Set<TEntity>().CountAsync(cancellationToken).ConfigureAwait(false);

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                // TODO: supprimer l'état managé (objets managés)
                db.Dispose();
            }

            // TODO: libérer les ressources non managées (objets non managés) et substituer le finaliseur
            // TODO: affecter aux grands champs une valeur null
            disposedValue = true;
        }
    }

    // // TODO: substituer le finaliseur uniquement si 'Dispose(bool disposing)' a du code pour libérer les ressources non managées
    // ~Repository()
    // {
    //     // Ne changez pas ce code. Placez le code de nettoyage dans la méthode 'Dispose(bool disposing)'
    //     Dispose(disposing: false);
    // }

    public void Dispose()
    {
        // Ne changez pas ce code. Placez le code de nettoyage dans la méthode 'Dispose(bool disposing)'
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}

