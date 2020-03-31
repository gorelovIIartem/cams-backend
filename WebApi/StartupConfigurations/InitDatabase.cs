using DAL.ApplicationDbContext;
using DAL.Interfaces;
using DAL.Models;
using DAL.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WebApi.StartupConfigurations.Interfaces;

namespace WebApi.StartupConfigurations
{
    public class InitDatabase : IInitializer
    {
        public void Setup(IServiceCollection services, IConfiguration configuration)
        {
            var logger = services.GetLogger<InitDatabase>();

            services.AddDbContext<CamsDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("CamsDb"))
            );

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<CamsDbContext>()
                .AddUserManager<UserManager<User>>()
                .AddRoleManager<RoleManager<IdentityRole>>();

            services.AddScoped<DbContext, CamsDbContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            logger.LogInformation("Database setuped.");
        }
    }
}
