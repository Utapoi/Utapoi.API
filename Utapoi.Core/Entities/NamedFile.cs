using Utapoi.Core.Entities.Common;
using Utapoi.Core.Interfaces;

namespace Utapoi.Core.Entities;

public class NamedFile : Entity, IFileInfo
{
    public string Name { get; set; } = string.Empty;

    public string Extension { get; set; } = string.Empty;

    public string MimeType { get; set; } = string.Empty;

    public long Size { get; set; }

    public string Hash { get; set; } = string.Empty;
}