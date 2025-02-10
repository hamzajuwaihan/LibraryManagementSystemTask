namespace LibraryManagementSystem.Domain.Shared.Entities;

/// <summary>
/// Represents an auditable entity that tracks the creator and last updater.
/// </summary>
public abstract class AuditableEntity : BaseEntity
{
    /// <summary>
    /// Gets or sets the identifier of the user who created the entity.
    /// </summary>
    public Guid? CreatedBy { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the user who last updated the entity.
    /// </summary>
    public Guid? UpdatedBy { get; set; }
}