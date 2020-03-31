using BLL.Interfaces;
using BLL.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WebApi.StartupConfigurations.Interfaces;

namespace WebApi.StartupConfigurations
{
    public class InitServices : IInitializer
    {
        public void Setup(IServiceCollection services, IConfiguration configuration)
        {
            var logger = services.GetLogger<InitServices>();

            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<IGroupService, GroupService>();

            logger.LogInformation("Services setuped.");
        }
    }
}
