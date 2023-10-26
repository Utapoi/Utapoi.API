using FluentResults;
using Karaoke.Application.Common;
using Karaoke.Application.Common.Requests;

namespace Karaoke.Application.Singers.Commands.CreateSinger
{
    public static partial class CreateSinger
    {
        /// <summary>
        /// Represents a command to create a singer.
        /// </summary>
        public sealed class Command : ICommand<Result<Response>>
        {
            public IEnumerable<LocalizedStringRequest> Names { get; set; } = new List<LocalizedStringRequest>();

            public IEnumerable<LocalizedStringRequest> Nicknames { get; set; } = new List<LocalizedStringRequest>();

            public IEnumerable<LocalizedStringRequest> Descriptions { get; set; } = new List<LocalizedStringRequest>();

            public IEnumerable<LocalizedStringRequest> Activities { get; set; } = new List<LocalizedStringRequest>();

            public DateTime? Birthday { get; set; }

            public string BloodType { get; set; } = string.Empty;

            public float Height { get; set; }

            public string Nationality { get; set; } = string.Empty;

            public FileRequest? ProfilePictureFile { get; set; }

            public FileRequest? CoverFile { get; set; }
        }
    }
}