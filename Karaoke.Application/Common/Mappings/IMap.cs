using AutoMapper;

namespace Karaoke.Application.Common.Mappings;

public interface IMap<TSource, TDest>
{
    void Mapping(Profile profile)
    {
        var m = profile.CreateMap<TSource, TDest>();

        ConfigureMapping(m);
    }

    void ConfigureMapping(IMappingExpression<TSource, TDest> map)
    {
        // Ignore.
    }
}