using FluentResults;
using Karaoke.Application.Common;
using Karaoke.Application.Common.Requests;

namespace Karaoke.Application.Singers.Commands.EditSinger;

public static partial class EditSinger
{
    public sealed class Command : ICommand<Result<Response>>
    {
        public Guid SingerId { get; set; }

        public IEnumerable<LocalizedStringRequest> Names { get; set; } = new List<LocalizedStringRequest>();

        public IEnumerable<LocalizedStringRequest> Nicknames { get; set; } = new List<LocalizedStringRequest>();

        public IEnumerable<LocalizedStringRequest> Descriptions { get; set; } = new List<LocalizedStringRequest>();

        public IEnumerable<LocalizedStringRequest> Activities { get; set; } = new List<LocalizedStringRequest>();

        public DateTime? Birthday { get; set; }

        public FileRequest? ProfilePictureFile { get; set; }
    }
}