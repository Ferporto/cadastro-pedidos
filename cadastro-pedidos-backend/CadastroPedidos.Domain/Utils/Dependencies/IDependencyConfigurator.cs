using Microsoft.Extensions.DependencyInjection;

namespace CadastroPedidos.Domain.Utils.Dependencies;

public interface IDependencyConfigurator
{
    void ConfigureDependencies(IServiceCollection services);
}
