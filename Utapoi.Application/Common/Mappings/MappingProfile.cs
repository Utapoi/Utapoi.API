using System.Reflection;
using AutoMapper;

namespace Utapoi.Application.Common.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
        ApplyProjectionsFromAssembly(Assembly.GetExecutingAssembly());
    }

    /// <summary>
    ///   Apply projections from the given assembly.
    /// </summary>
    /// <param name="assembly">The assembly to scan for projections.</param>
    private void ApplyProjectionsFromAssembly(Assembly assembly)
    {
        var mapFromType = typeof(IProjection<,>);

        const string mappingMethodName = nameof(IProjection<object, object>.Projection);

        bool HasInterface(Type t)
        {
            return t.IsGenericType && t.GetGenericTypeDefinition() == mapFromType;
        }

        var types = assembly.GetExportedTypes().Where(t => t.GetInterfaces().Any(HasInterface)).ToList();

        var argumentTypes = new[] { typeof(Profile) };

        foreach (var type in types)
        {
            var instance = Activator.CreateInstance(type);

            var methodInfo = type.GetMethod(mappingMethodName);

            if (methodInfo != null)
            {
                methodInfo.Invoke(instance, new object[] { this });
            }
            else
            {
                var interfaces = type.GetInterfaces().Where(HasInterface).ToList();

                if (interfaces.Count <= 0)
                {
                    continue;
                }

                foreach (var interfaceMethodInfo in interfaces.Select(@interface =>
                             @interface.GetMethod(mappingMethodName, argumentTypes)))
                {
                    interfaceMethodInfo?.Invoke(instance, new object[] { this });
                }
            }
        }
    }

    /// <summary>
    ///    Apply mappings from the given assembly.
    /// </summary>
    /// <param name="assembly">The assembly to scan for mappings.</param>
    private void ApplyMappingsFromAssembly(Assembly assembly)
    {
        var mapFromType = typeof(IMap<,>);

        const string mappingMethodName = nameof(IMap<object, object>.Mapping);

        bool HasInterface(Type t)
        {
            return t.IsGenericType && t.GetGenericTypeDefinition() == mapFromType;
        }

        var types = assembly.GetExportedTypes().Where(t => t.GetInterfaces().Any(HasInterface)).ToList();

        var argumentTypes = new[] { typeof(Profile) };

        foreach (var type in types)
        {
            var instance = Activator.CreateInstance(type);

            var methodInfo = type.GetMethod(mappingMethodName);

            if (methodInfo != null)
            {
                methodInfo.Invoke(instance, new object[] { this });
            }
            else
            {
                var interfaces = type.GetInterfaces().Where(HasInterface).ToList();

                if (interfaces.Count <= 0)
                {
                    continue;
                }

                foreach (var interfaceMethodInfo in interfaces.Select(@interface =>
                             @interface.GetMethod(mappingMethodName, argumentTypes)))
                {
                    interfaceMethodInfo?.Invoke(instance, new object[] { this });
                }
            }
        }
    }
}