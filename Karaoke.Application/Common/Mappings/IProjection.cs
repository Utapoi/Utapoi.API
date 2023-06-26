using AutoMapper;

namespace Karaoke.Application.Common.Mappings;

public interface IProjection<TSource, TDest>
{
    void Projection(Profile profile)
    {
        var m = profile.CreateProjection<TSource, TDest>();

        ConfigureProjection(m);
    }

    /// <summary>
    ///    Configures the projection with more options.
    /// </summary>
    void ConfigureProjection(IProjectionExpression<TSource, TDest> projection)
    {
        // Ignore.
    }
}
