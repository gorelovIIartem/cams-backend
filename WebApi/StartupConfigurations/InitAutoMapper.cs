using AutoMapper;
using BLL.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WebApi.StartupConfigurations.Interfaces;

namespace WebApi.StartupConfigurations
{
    public class InitAutoMapper : IInitializer
    {
        public void Setup(IServiceCollection services, IConfiguration configuration)
        {
            var logger = services.GetLogger<InitAutoMapper>();

            var mc = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MapperSettings());
            });
            var mapper = mc.CreateMapper();
            services.AddSingleton(mapper);

            logger.LogInformation("AutoMapper setuped.");
        }
    }
}
