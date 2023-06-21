using Karaoke.Application.Common.Requests;
using Karaoke.Application.Files;
using Karaoke.Application.Persistence;
using Karaoke.Core.Entities;
using Karaoke.Core.Extensions;
using Karaoke.Core.Interfaces;
using Karaoke.Core.Storage;
using Microsoft.EntityFrameworkCore;

namespace Karaoke.Infrastructure.Files;

public class FilesService : IFilesService
{
    private readonly IKaraokeDbContext _context;

    private readonly Storage _storage;

    public FilesService(IKaraokeDbContext context, Storage storage)
    {
        _context = context;
        _storage = storage.GetStorageForDirectory(@"files");
    }

    public async Task<NamedFile> CreateAsync(FileRequest request, CancellationToken cancellationToken = default)
    {
        var hash = request.File.ComputeSHA2Hash();
        var file = await _context.Files
            .FirstOrDefaultAsync(f => f.Hash == hash, cancellationToken);

        if (file != null && CheckFileExistsAndMatchesHash(file))
        {
            return file;
        }

        file ??= new NamedFile
        {
            Name = request.FileName,
            Size = request.File.Length,
            Extension = MimeTypeToExtension(request.FileType),
            MimeType = request.FileType,
            Hash = hash
        };

        await CreatePhysicalFileAsync(file, request.File, cancellationToken);
        await _context.Files.AddAsync(file, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return file;
    }

    public Task<NamedFile> CreateAsync(LocalizedFileRequest request, CancellationToken cancellationToken = default)
    {
        return CreateAsync(
            new FileRequest
            {
                File = request.File,
                FileType = request.FileType,
                FileName = request.FileName
            },
            cancellationToken
        );
    }

    private async Task CreatePhysicalFileAsync(
        IFileInfo file,
        byte[] bytes,
        CancellationToken cancellationToken = default
    )
    {
        var path = file.GetStoragePath();

        if (CheckFileExistsAndMatchesHash(file))
        {
            return;
        }

        // Note(Mikyan): Do we really want to delete the file if it doesn't match the hash?
        // Can this even happen?
        if (_storage.Exists(path))
        {
            _storage.Delete(path);
        }

        await using var stream = _storage.CreateFileSafely(path);
        await stream.WriteAsync(bytes, cancellationToken);
    }

    private bool CheckFileExistsAndMatchesHash(IFileInfo file)
    {
        var path = file.GetStoragePath();

        if (!_storage.Exists(path))
        {
            return false;
        }

        using var stream = _storage.GetStream(path);
        var hash = stream?.ComputeSHA2Hash();

        return hash == file.Hash;
    }

    private static string MimeTypeToExtension(string mimeType)
    {
        return mimeType switch
        {
            "image/jpeg" => ".jpg",
            "image/png" => ".png",
            "image/gif" => ".gif",
            "audio/mpeg" => ".mp3",
            "video/mp4" => ".mp4",
            "video/webm" => ".webm",
            "audio/ogg" => ".ogg",
            "audio/wav" => ".wav",
            "audio/flac" => ".flac",
            "text/plain" => ".txt",
            "subtitle/ass" => ".ass",
            _ => throw new ArgumentOutOfRangeException(nameof(mimeType), mimeType, null)
        };
    }
}