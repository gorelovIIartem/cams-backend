using DAL.ApplicationDbContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Linq;
using WebApi.StartupConfigurations.Interfaces;

namespace WebApi.StartupConfigurations
{
    public class InitIdentityRoles : IInitializer
    {
        public void Setup(IServiceCollection services, IConfiguration configuration)
        {
            var logger = services.GetLogger<InitIdentityRoles>();

            string[] roles = configuration.GetSection("IdentityRoles").Get<string[]>();
            if (roles.Length == 0)
                return;

            DbContextOptionsBuilder dbContextOptionsBuilder = new DbContextOptionsBuilder();
            dbContextOptionsBuilder.UseSqlServer(configuration.GetConnectionString("CamsDb"));
            using CamsDbContext camsDbContext = new CamsDbContext(dbContextOptionsBuilder.Options);

            if (!camsDbContext.DatabaseExists())
            {
                logger.LogError($"Identity roles setup. Database CamsDb not exist!");
                return;
            }

            var dbRoles = camsDbContext.Roles.Select(dbRole => dbRole.Name);
            var newRoles = roles.Except(dbRoles).ToList();
            if (newRoles.Count == 0)
                return;

            newRoles.ForEach(r => camsDbContext.Roles.Add(new IdentityRole()
            {
                Name = r,
                NormalizedName = r.ToUpper()
            }));
            camsDbContext.SaveChanges();
            logger.LogInformation("Identity roles setuped.");
        }
    }
}
