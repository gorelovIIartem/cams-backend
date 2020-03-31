using BLL.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebApi.StartupConfigurations.Interfaces;

namespace WebApi.StartupConfigurations
{
	public class InitJwt : IInitializer
	{
		public void Setup(IServiceCollection services, IConfiguration configuration)
		{
			var logger = services.GetLogger<InitJwt>();

			var jwtSettings = new JwtSettings();
			configuration.Bind(nameof(JwtSettings), jwtSettings);
			services.AddSingleton(jwtSettings);

			services.AddAuthentication(opt =>
			{
				opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
				opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
				.AddJwtBearer(opt =>
				{
					opt.SaveToken = true;
					opt.TokenValidationParameters = new TokenValidationParameters
					{
						ValidateIssuerSigningKey = true,
						IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Secret)),
						ValidateIssuer = false,
						ValidateAudience = false,
						RequireExpirationTime = false,
						ValidateLifetime = true
					};
				});
			logger.LogInformation("JWT setuped.");
		}
	}
}
