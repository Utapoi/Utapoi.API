using AutoMapper;
using Utapoi.Application.Common.Mappings;
using Utapoi.Core.Entities;

namespace Utapoi.Application.DTO;

/// <summary>
///     The user data transfer object.
/// </summary>
public sealed class UserDTO : IMap<User, UserDTO>
{
    public string Id { get; set; } = string.Empty;

    public string Username { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string ProfilePicture { get; set; } = string.Empty;

    public ICollection<string> Roles { get; set; } = new List<string>();

    public void ConfigureMapping(IMappingExpression<User, UserDTO> map)
    {
        map.ForMember(
            x => x.Id,
            o => o.MapFrom(s => s.Id.ToString())
        );
    }
}