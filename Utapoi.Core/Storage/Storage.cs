using System.Diagnostics.Contracts;

namespace Utapoi.Core.Storage;

public abstract class Storage
{
    protected Storage(string path, string? subfolder = null)
    {
        static string FileNameStrip(string entry)
        {
            return Path.GetInvalidFileNameChars()
                .Aggregate(entry, (current, c) => current.Replace(c.ToString(), string.Empty));
        }

        BasePath = path;

        if (BasePath == null)
        {
            throw new InvalidOperationException($"{nameof(BasePath)} not correctly initialized!");
        }

        if (!string.IsNullOrEmpty(subfolder))
        {
            BasePath = Path.Combine(BasePath, FileNameStrip(subfolder));
        }
    }

    protected string BasePath { get; }

    public abstract string GetFullPath(string path, bool createIfNotExisting = false);

    public abstract bool Exists(string path);

    public abstract bool ExistsDirectory(string path);

    public abstract void DeleteDirectory(string path);

    public abstract void Delete(string path);

    public abstract IEnumerable<string> GetDirectories(string path);

    public abstract IEnumerable<string> GetFiles(string path, string pattern = "*");

    public virtual Storage GetStorageForDirectory(string path)
    {
        if (string.IsNullOrEmpty(path))
        {
            throw new ArgumentException("Must be non-null and not empty string", nameof(path));
        }

        if (!path.EndsWith(Path.DirectorySeparatorChar))
        {
            path += Path.DirectorySeparatorChar;
        }

        // create non-existing path.
        var fullPath = GetFullPath(path, true);

        return (Storage)Activator.CreateInstance(GetType(), fullPath)!;
    }

    public abstract void Move(string from, string to);

    [Pure]
    public Stream CreateFileSafely(string path)
    {
        var temporaryPath = Path.Combine(Path.GetDirectoryName(path) ?? string.Empty,
            $"_{Path.GetFileName(path)}_{Guid.NewGuid()}");

        return new SafeWriteStream(temporaryPath, path, this);
    }

    /// <summary>
    ///     Retrieve a stream from an underlying file inside this storage.
    /// </summary>
    /// <param name="path">The path of the file.</param>
    /// <param name="access">The access requirements.</param>
    /// <param name="mode">The mode in which the file should be opened.</param>
    /// <returns>A stream associated with the requested path.</returns>
    [Pure]
    public abstract Stream? GetStream(string path, FileAccess access = FileAccess.Read,
        FileMode mode = FileMode.OpenOrCreate);

    private sealed class SafeWriteStream : FileStream
    {
        private readonly string _finalPath;
        private readonly Storage _storage;
        private readonly string _temporaryPath;

        private bool _isDisposed;

        public SafeWriteStream(string temporaryPath, string finalPath, Storage storage)
            : base(storage.GetFullPath(temporaryPath, true), FileMode.Create, FileAccess.Write)
        {
            _temporaryPath = temporaryPath;
            _finalPath = finalPath;
            _storage = storage;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (_isDisposed)
            {
                return;
            }

            _storage.Delete(_finalPath);
            _storage.Move(_temporaryPath, _finalPath);
            _isDisposed = true;
        }
    }
}