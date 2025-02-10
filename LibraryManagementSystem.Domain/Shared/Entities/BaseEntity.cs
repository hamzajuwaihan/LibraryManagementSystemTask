namespace LibraryManagementSystem.Domain.Shared.Entities;

/// <summary>
/// Represents the base entity that all other entities inherit from.
/// </summary>
public abstract class BaseEntity
{
    /// <summary>
    /// Gets or sets the unique identifier for the entity.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the timestamp when the entity was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the timestamp when the entity was last updated.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }
}