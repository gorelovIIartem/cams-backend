using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace WebApi
{
	public static class LoggerExtensions
	{
		public static ILogger<T> GetLogger<T>(this IServiceCollection serviceCollection)
			=> serviceCollection
				.BuildServiceProvider()
				.GetRequiredService<ILoggerFactory>()
				.CreateLogger<T>();
	}
}
