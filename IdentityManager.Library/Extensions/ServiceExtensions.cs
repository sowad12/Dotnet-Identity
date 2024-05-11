

using IdentityManager.Library.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Text.RegularExpressions;

namespace IdentityManager.Library.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services, Assembly[] assemblies)
        {
            foreach (Assembly assembly in assemblies)
            {
                System.Collections.Generic.IEnumerable<TypeInfo> classTypes = assembly.ExportedTypes.Select(t => t.GetTypeInfo()).Where(t => t.IsClass && !t.IsAbstract);

                foreach (var type in classTypes)
                {
                    IEnumerable<TypeInfo> interfaces = type.ImplementedInterfaces.Select(i => i.GetTypeInfo());

                    foreach (var handlerType in interfaces.Where(x => x.GetInterface(nameof(IService)) != null
                                                || x.GetInterface(nameof(IManager)) != null
                                                || x.GetInterface(nameof(IRepository)) != null))
                    {
                        services.AddTransient(handlerType.AsType(), type.AsType());
                    }
                }
            }

            return services;
        }

        public static MatchCollection GetMacros(this string str)
        {
            return Regex.Matches(str, "{{[\\w\\s-\'()$/]+}}");
        }
    }
}
