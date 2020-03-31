using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using WebApi.StartupConfigurations.Interfaces;

namespace WebApi.StartupConfigurations.Defaults
{
	public static class StartupConfigurationExtensions
	{
		public static void ExecuteAllAssemblyInitializers(this IServiceCollection services, IConfiguration configuration)
		{
			var setups = typeof(Startup).Assembly.ExportedTypes.Where(et =>
											typeof(IInitializer)
											.IsAssignableFrom(et) && !et.IsInterface && !et.IsAbstract)
											.Select(Activator.CreateInstance)
											.Cast<IInitializer>()
											.ToList();

			setups.ForEach(setup => setup.Setup(services, configuration));
		}
	}
}
