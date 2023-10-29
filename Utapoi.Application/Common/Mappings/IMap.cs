using AutoMapper;

namespace Utapoi.Application.Common.Mappings;

/// <summary>
///    Provides a mapping interface for AutoMapper with automatic configuration.
/// </summary>
public interface IMap<TSource, TDest>
{
    void Mapping(Profile profile)
    {
        var m = profile.CreateMap<TSource, TDest>();

        ConfigureMapping(m);
    }

    /// <summary>
    ///    Configures the mapping with more options.
    /// </summary>
    void ConfigureMapping(IMappingExpression<TSource, TDest> map)
    {
        // Ignore.
    }
}