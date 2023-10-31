using AutoMapper;
using FluentResults;
using MediatR;
using Utapoi.Application.DTO.Albums;

namespace Utapoi.Application.Albums.Requests.SearchAlbums;

public static class SearchAlbums
{
    public class Request : IRequest<Result<IEnumerable<AlbumDTO>>>
    {
        public string Input { get; set; } = string.Empty;
    }

    internal sealed class Handler : IRequestHandler<Request, Result<IEnumerable<AlbumDTO>>>
    {
        private readonly IAlbumsService _albumsService;

        private readonly IMapper _mapper;

        public Handler(IAlbumsService albumsService, IMapper mapper)
        {
            _albumsService = albumsService;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<AlbumDTO>>> Handle(Request request, CancellationToken cancellationToken)
        {
            var albums = await _albumsService.SearchAsync(request.Input, cancellationToken);

            return Result.Ok(_mapper.Map<IEnumerable<AlbumDTO>>(albums));
        }
    }
}