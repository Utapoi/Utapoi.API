using Utapoi.Core.Entities.Common;

namespace Utapoi.Core.Entities;

/// <summary>
///     Represents a tag.
/// </summary>
public sealed class Tag : Entity
{
    /// <summary>
    ///     Gets or sets the name of the tag.
    /// </summary>
    public string Name { get; set; } = string.Empty;
}