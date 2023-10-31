﻿using AutoMapper;
using Utapoi.Application.Common.Mappings;
using Utapoi.Core.Entities;
using Utapoi.Core.Extensions;

namespace Utapoi.Application.Songs.Requests.GetSongsForSinger;

public static partial class GetSongsForSinger
{
    public struct AlbumDTO : IProjection<Album, AlbumDTO>
    {
        public Guid Id { get; set; }

        public IReadOnlyCollection<LocalizedString> Titles { get; set; } = new List<LocalizedString>();

        public string Cover { get; set; } = string.Empty;

        public AlbumDTO()
        {
        }

        public readonly void ConfigureProjection(IProjectionExpression<Album, AlbumDTO> projection)
        {
            projection.ForMember(
               d => d.Cover,
               opt => opt.MapFrom(s => s.Cover!.GetUrl())
            );
        }
    }

    public sealed class Response : IProjection<Song, Response>
    {
        public Guid Id { get; set; }

        public IReadOnlyCollection<LocalizedString> Titles { get; set; } = new List<LocalizedString>();

        public string Cover { get; set; } = string.Empty;

        public IReadOnlyCollection<AlbumDTO> Albums { get; set; } = new List<AlbumDTO>();
    }
}