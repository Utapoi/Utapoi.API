﻿using AutoMapper;
using Utapoi.Application.Common.Mappings;
using Utapoi.Application.DTO.Albums;
using Utapoi.Core.Entities;
using Utapoi.Core.Extensions;

namespace Utapoi.Application.DTO;

/// <summary>
///     Represents a singer.
/// </summary>
public sealed class SingerDTO : IMap<Singer, SingerDTO>
{
    public string Id { get; set; } = string.Empty;

    public IEnumerable<LocalizedString> Names { get; set; } = new List<LocalizedString>();

    public IEnumerable<LocalizedString> Nicknames { get; set; } = new List<LocalizedString>();

    public IEnumerable<AlbumDTO> Albums { get; set; } = new List<AlbumDTO>();

    public IEnumerable<SongDTO> Songs { get; set; } = new List<SongDTO>();

    public string ProfilePicture { get; set; } = string.Empty;

    public void ConfigureMapping(IMappingExpression<Singer, SingerDTO> map)
    {
        map.ForMember(
            d => d.Id,
            o => o.MapFrom(
                s => s.Id.ToString()
            )
        );

        map.ForMember(
            d => d.ProfilePicture,
            o => o.MapFrom(
                s => s.ProfilePicture.GetUrl()
            )
        );
    }
}