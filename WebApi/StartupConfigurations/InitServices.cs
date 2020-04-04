using BLL.Interfaces;
using BLL.Services;
using BLL.Interfaces.DTOInterfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WebApi.StartupConfigurations.Interfaces;
using BLL.Services.DTOServices;

namespace WebApi.StartupConfigurations
{
    public class InitServices : IInitializer
    {
        public void Setup(IServiceCollection services, IConfiguration configuration)
        {
            var logger = services.GetLogger<InitServices>();

            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<IGroupService, GroupService>();
            services.AddScoped<IDeviceService, DeviceService>();
            services.AddScoped<IDeviceLogService, DeviceLogService>();

            logger.LogInformation("Services setuped.");
        }
    }
}
