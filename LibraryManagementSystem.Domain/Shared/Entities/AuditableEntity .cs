﻿namespace LibraryManagementSystem.Domain.Shared.Entities;

public abstract class AuditableEntity : BaseEntity
{
    public Guid? CreatedBy { get; set; }
    public Guid? UpdatedBy { get; set; }
}