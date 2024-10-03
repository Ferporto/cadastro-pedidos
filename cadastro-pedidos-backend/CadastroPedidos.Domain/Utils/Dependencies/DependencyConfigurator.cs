using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CadastroPedidos.Domain.Utils.Dependencies;

public class DependencyConfigurator : IDependencyConfigurator
{
    private readonly Assembly _assembly;

    public DependencyConfigurator(Assembly assembly)
    {
        _assembly = assembly ?? throw new ArgumentNullException(nameof(assembly));
    }

    public void ConfigureDependencies(IServiceCollection services)
    {
        // Pega todos os tipos definidos no assembly atual
        var types = _assembly.GetTypes();

        // Registra cada tipo baseado na interface de ciclo de vida implementada
        foreach (var type in types)
        {
            var interfaces = type.GetInterfaces();

            // Verifica se implementa `ITransientDependency`
            if (interfaces.Contains(typeof(ITransientDependency)))
            {
                services.AddTransient(interfaces.First(i => i != typeof(ITransientDependency)), type);
            }

            // Verifica se implementa `IScopedDependency`
            if (interfaces.Contains(typeof(IScopedDependency)))
            {
                services.AddScoped(interfaces.First(i => i != typeof(IScopedDependency)), type);
            }

            // Verifica se implementa `ISingletonDependency`
            if (interfaces.Contains(typeof(ISingletonDependency)))
            {
                services.AddSingleton(interfaces.First(i => i != typeof(ISingletonDependency)), type);
            }
        }
    }
}
