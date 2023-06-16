using AutoMapper;
using FluentResults;
using FluentValidation;
using Karaoke.Application.Common.Errors;
using Karaoke.Application.DTO;
using Karaoke.Core.Entities;
using Karaoke.Core.Exceptions;
using MediatR;

namespace Karaoke.Application.Songs.Requests.GetSong;

public static class GetSong
{
    public sealed class Request : IRequest<Result<SongDTO>>
    {
        public Request(Guid id)
        {
            Id = id;
        }

        public Request()
        {
        }

        public Guid Id { get; set; } = Guid.Empty;
    }

    internal sealed class Validator : AbstractValidator<Request>
    {
        public Validator()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty);
        }
    }

    internal sealed class Handler : IRequestHandler<Request, Result<SongDTO>>
    {
        private readonly IMapper _mapper;
        private readonly ISongsService _songsService;

        public Handler(ISongsService songsService, IMapper mapper)
        {
            _songsService = songsService;
            _mapper = mapper;
        }

        public async Task<Result<SongDTO>> Handle(Request request, CancellationToken cancellationToken)
        {
            try
            {
                var song = await _songsService.GetAsync(request.Id, cancellationToken);

                return Result.Ok(_mapper.Map<SongDTO>(song));
            }
            catch (EntityNotFoundException<Song> ex)
            {
                return Result.Fail(new EntityNotFoundError(ex.Message, request.Id));
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }
    }
}