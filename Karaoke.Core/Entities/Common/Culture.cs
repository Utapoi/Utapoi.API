using System.Globalization;

namespace Karaoke.Core.Entities.Common;

public class Culture : Entity
{
    public Culture()
    {
    }

    public Culture(string tag)
    {
        Tag = tag;
        Name = CultureInfo.GetCultureInfo(tag).NativeName;
    }

    public string Name { get; set; } = string.Empty;

    public string Tag { get; set; } = string.Empty;
}