using System.Security.Cryptography;
using System.Text;

namespace Utapoi.Core.Extensions;

public static class HashExtensions
{
    private static string ToLowercaseHex(this byte[] bytes)
    {
        return string.Create(bytes.Length * 2, bytes, (span, b) =>
        {
            for (var i = 0; i < b.Length; i++)
            {
                _ = b[i].TryFormat(span[(i * 2)..], out _, "x2");
            }
        });
    }

    /// <summary>
    ///     Gets a SHA-2 (256bit) hash for the given stream, seeking the stream before and after.
    /// </summary>
    /// <param name="stream">The stream to create a hash from.</param>
    /// <returns>A lower-case hex string representation of the hash (64 characters).</returns>
    public static string ComputeSHA2Hash(this Stream stream)
    {
        string hash;

        stream.Seek(0, SeekOrigin.Begin);

        using (var alg = SHA256.Create())
        {
            hash = alg.ComputeHash(stream).ToLowercaseHex();
        }

        stream.Seek(0, SeekOrigin.Begin);

        return hash;
    }

    /// <summary>
    ///     Gets a SHA-2 (256bit) hash for the given string.
    /// </summary>
    /// <param name="str">The string to create a hash from.</param>
    /// <returns>A lower-case hex string representation of the hash (64 characters).</returns>
    public static string ComputeSHA2Hash(this string str)
    {
        return SHA256.HashData(Encoding.UTF8.GetBytes(str)).ToLowercaseHex();
    }

    public static string ComputeSHA2Hash(this byte[] bytes)
    {
        return SHA256.HashData(bytes).ToLowercaseHex();
    }

    public static string ComputeMD5Hash(this Stream stream)
    {
        string hash;

        stream.Seek(0, SeekOrigin.Begin);
        using (var md5 = MD5.Create())
        {
            hash = md5.ComputeHash(stream).ToLowercaseHex();
        }

        stream.Seek(0, SeekOrigin.Begin);

        return hash;
    }

    public static string ComputeMD5Hash(this byte[] bytes)
    {
        return MD5.HashData(bytes).ToLowercaseHex();
    }

    public static string ComputeMD5Hash(this string input)
    {
        return MD5.HashData(Encoding.UTF8.GetBytes(input)).ToLowercaseHex();
    }
}