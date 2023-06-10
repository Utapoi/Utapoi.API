using AutoMapper;
using FluentResults;
using Karaoke.Application.DTO;
using MediatR;

namespace Karaoke.Application.Albums.Requests.GetAlbums;

public static class GetAlbums
{
    public class Request : IRequest<Result<IEnumerable<AlbumDTO>>>
    {

    internal sealed class Handler : IRequestHandler<Request, Result<IEnumerable<AlbumDTO>>>
    {
        private readonly IAlbumsService _albumsService;
        private readonly IMapper _mapper;

        public Handler(IMapper mapper, IAlbumsService albumsService)
        {
            _mapper = mapper;
            _albumsService = albumsService;
        }

        public async Task<Result<IEnumerable<AlbumDTO>>> Handle(Request request, CancellationToken cancellationToken)
        {
            var albums = await _albumsService.GetAllAsync(cancellationToken);

            return Result.Ok(_mapper.Map<IEnumerable<AlbumDTO>>(albums));
        }
    }
}