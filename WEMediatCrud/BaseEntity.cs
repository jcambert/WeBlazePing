
using System.ComponentModel.DataAnnotations;

namespace WeMediatCrud;

public interface IBaseEntity<TKey>
{
    [Key]
    TKey Id { get; set; }
}
public interface IBaseEntity : IBaseEntity<Guid>
{

}


/// <summary>
/// This interface implemented creation date for entity
/// </summary>
public interface ICreateDateEntity
{
    /// <summary>
    /// Creation Date
    /// </summary>
    public DateTime CreationDate { get; set; }
}

/// <summary>
/// This interface implemented Modification Date property for entity
/// </summary>
public interface IUpdateDateEntity
{
    /// <summary>
    /// Modification Date
    /// </summary>
    public DateTime? ModificationDate { get; set; }
}

/// <summary>
/// This interface implemented Deletion Date and Is Deleted property for entity
/// </summary>
public interface ISoftDeleteEntity
{
    /// <summary>
    /// Deletion Date
    /// </summary>
    public DateTime? DeletionDate { get; set; }

    /// <summary>
    /// Is Deleted
    /// </summary>
    public bool IsDeleted { get; set; }
}

/// <summary>
/// This abstraction implemented base properties for entities
/// </summary>
/// <typeparam name="TKey">
/// Primary Key type of the entity
/// </typeparam>
public abstract class BaseEntity<TKey> : IBaseEntity<TKey>, ICreateDateEntity, IUpdateDateEntity, ISoftDeleteEntity
{
    /// <summary>
    /// Creation Date <see cref="{DateTime}"/>
    /// </summary>
    public virtual DateTime CreationDate { get; set; }

    /// <summary>
    /// Primary Key <see cref="{TKey}"/>
    /// </summary>
    public virtual TKey Id { get; set; }

    /// <summary>
    /// Modification Date <see cref="{DateTime}"/>
    /// </summary>
    public virtual DateTime? ModificationDate { get; set; }

    /// <summary>
    /// Deletion Date <see cref="{DateTime}"/>
    /// </summary>
    public virtual DateTime? DeletionDate { get; set; }

    /// <summary>
    /// Is Deleted <see cref="{Boolean}"/>
    /// </summary>
    public virtual bool IsDeleted { get; set; }
}
public class BaseEntity:BaseEntity<Guid>
{
    public BaseEntity()
    {

    }
}
