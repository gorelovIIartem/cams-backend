using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WebApi.StartupConfigurations.Interfaces
{
    public interface IInitializer
    {
        void Setup(IServiceCollection services, IConfiguration configuration);
    }
}
