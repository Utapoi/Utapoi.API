using System.Diagnostics.CodeAnalysis;

namespace Karaoke.Core.Storage;

public class NativeStorage : Storage
{
    public NativeStorage(string path)
        : base(path)
    {
    }

    public override bool Exists(string path)
    {
        return File.Exists(GetFullPath(path));
    }

    public override bool ExistsDirectory(string path)
    {
        return Directory.Exists(GetFullPath(path));
    }

    public override void DeleteDirectory(string path)
    {
        path = GetFullPath(path);

        if (Directory.Exists(path))
        {
            Directory.Delete(path, true);
        }
    }

    public override void Delete(string path)
    {
        path = GetFullPath(path);

        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }

    public override void Move(string from, string to)
    {
        File.Move(GetFullPath(from), GetFullPath(to));
    }

    public override IEnumerable<string> GetDirectories(string path)
    {
        return GetRelativePaths(Directory.GetDirectories(GetFullPath(path)));
    }

    public override IEnumerable<string> GetFiles(string path, string pattern = "*")
    {
        return GetRelativePaths(Directory.GetFiles(GetFullPath(path), pattern));
    }

    private IEnumerable<string> GetRelativePaths(IEnumerable<string> paths)
    {
        var basePath = Path.GetFullPath(GetFullPath(string.Empty));
        return paths.Select(Path.GetFullPath).Select(path =>
        {
            if (!path.StartsWith(basePath, StringComparison.OrdinalIgnoreCase))
            {
                throw new ArgumentException($"\"{path}\" does not start with \"{basePath}\" and is probably malformed");
            }

            return path.AsSpan(basePath.Length).TrimStart(Path.DirectorySeparatorChar).ToString();
        });
    }

    public override string GetFullPath(string path, bool createIfNotExisting = false)
    {
        path = path.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);

        var basePath = Path.GetFullPath(BasePath).TrimEnd(Path.DirectorySeparatorChar);
        var resolvedPath = Path.GetFullPath(Path.Combine(basePath, path));

        if (!resolvedPath.StartsWith(basePath, StringComparison.OrdinalIgnoreCase))
        {
            throw new ArgumentException(
                $"\"{resolvedPath}\" traverses outside of \"{basePath}\" and is probably malformed");
        }

        if (createIfNotExisting)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(resolvedPath) ?? string.Empty);
        }

        return resolvedPath;
    }

    public override Stream? GetStream(string path, FileAccess access = FileAccess.Read,
        FileMode mode = FileMode.OpenOrCreate)
    {
        path = GetFullPath(path, access != FileAccess.Read);

        if (string.IsNullOrEmpty(path))
        {
            throw new ArgumentNullException(nameof(path));
        }

        return access switch
        {
            FileAccess.Read => !File.Exists(path) ? null : File.Open(path, FileMode.Open, access, FileShare.Read),
            _ => new FileStream(path, mode, access)
        };
    }

    public override Storage GetStorageForDirectory([NotNull] string path)
    {
        ArgumentNullException.ThrowIfNull(path);

        if (path.Length > 0 && !path.EndsWith(Path.DirectorySeparatorChar))
        {
            path += Path.DirectorySeparatorChar;
        }

        var fullPath = GetFullPath(path, true);

        return (Storage)Activator.CreateInstance(GetType(), fullPath)!;
    }
}