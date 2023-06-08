namespace Karaoke.Core.Interfaces;

public interface IFileInfo
{
    string Hash { get; set; }

    string Extension { get; set; }
}