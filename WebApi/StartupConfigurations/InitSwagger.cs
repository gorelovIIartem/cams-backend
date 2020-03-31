using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using WebApi.StartupConfigurations.Interfaces;

namespace WebApi.StartupConfigurations
{
	public class InitSwagger : IInitializer
	{
		public void Setup(IServiceCollection services, IConfiguration configuration)
		{
			var logger = services.GetLogger<InitSwagger>();

			services.AddSwaggerGen(opt =>
			{
				opt.SwaggerDoc("v1", new OpenApiInfo
				{
					Title = "Authorization and Authentication Server Web Api",
					Version = "v1"
				});

				opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
				{
					Description = "JWT Authorization header using the bearer scheme",
					Name = "Authorization",
					In = ParameterLocation.Header,
					Type = SecuritySchemeType.ApiKey,
					Scheme = "Bearer"
				});

				opt.AddSecurityRequirement(new OpenApiSecurityRequirement
				{
					{
						new OpenApiSecurityScheme()
						{
							Reference = new OpenApiReference()
							{
								Type = ReferenceType.SecurityScheme,
								Id = "Bearer"
							},
							Scheme = "apiKey",
							Name = "Bearer",
							In = ParameterLocation.Header
						},
						new List<string>()
					}
				});
			});
			logger.LogInformation("Swagger setuped.");
		}
	}
}
