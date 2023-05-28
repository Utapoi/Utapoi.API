namespace Karaoke.Core.Entities;

public class AuditableEntity
{
    public DateTime Created { get; set; }

    public Guid CreatedBy { get; set; } = Guid.Empty;

    public DateTime? LastModified { get; set; }

    public Guid LastModifiedBy { get; set; } = Guid.Empty;
}