using System.Globalization;
using Karaoke.Core.Entities.Common;

namespace Karaoke.Core.Entities;

public class Culture : Entity
{
    public Culture()
    {
    }

    public Culture(string tag)
    {
        Tag = tag;
        Name = CultureInfo.GetCultureInfo(tag).EnglishName;
    }

    public string Name { get; set; } = string.Empty;

    public string Tag { get; set; } = string.Empty;
}