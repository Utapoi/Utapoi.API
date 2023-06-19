using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karaoke.Application.Identity.Common;

public static class Scopes
{
    public static class Create
    {
        public const string Albums = "Create.Albums";

        public const string Singers = "Create.Singers";

        public const string Songs = "Create.Songs";
    }

    public static class Edit
    {
        public const string Albums = "Edit.Albums";

        public const string Singers = "Edit.Singers";

        public const string Songs = "Edit.Songs";
    }
    
    public static class Read
    {
        public const string Albums = "Read.Albums";

        public const string Singers = "Read.Singers";

        public const string Songs = "Read.Songs";
    }

    public static IEnumerable<string> GetAll()
    {
        return GetCreateScopes()
            .Concat(GetEditScopes())
            .Concat(GetReadScopes());
    }

    public static IEnumerable<string> GetCreateScopes()
        => new[]
        {
            Create.Albums,
            Create.Singers,
            Create.Songs
        };
    
    public static IEnumerable<string> GetEditScopes()
        => new[]
        {
            Edit.Albums,
            Edit.Singers,
            Edit.Songs
        };

    public static IEnumerable<string> GetReadScopes()
        => new[]
        {
            Read.Albums,
            Read.Singers,
            Read.Songs
        };
    
    
}