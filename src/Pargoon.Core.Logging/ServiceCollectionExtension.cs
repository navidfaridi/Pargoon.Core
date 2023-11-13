namespace Pargoon.Core.Logging;

using global::Serilog;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

public static class ServiceCollectionExtension
{
  public static IServiceCollection AddSerilog(this IServiceCollection services, IConfiguration configuration)
  {
	services.Configure<LoggingOptions>(configuration.GetSection(nameof(LoggingOptions)));
	services.AddLogging(b => b.ClearProviders().AddSerilog(Log.Logger));
	return services;
  }
}
