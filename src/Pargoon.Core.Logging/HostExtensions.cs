namespace Pargoon.Core.Logging;

using System;
using System.Threading.Tasks;
using global::Serilog;
using global::Serilog.Context;
using global::Serilog.Events;
using global::Serilog.Exceptions;
using global::Serilog.Formatting.Elasticsearch;
using global::Serilog.Sinks.Elasticsearch;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Pargoon.Core.Logging;

public static class HostExtensions
{
  public static IHostBuilder UseSerilog(this IHostBuilder hostBuilder, string serviceName, string serviceIndex) =>
	  hostBuilder.UseSerilog((context, services, loggerConfiguration) =>
	  {
		var loggingOptions = services.GetRequiredService<IOptions<LoggingOptions>>().Value;
		loggerConfiguration
			  .MinimumLevel.Information()
			  .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
			  .MinimumLevel.Override("Microsoft.AspNetCore.Mvc", LogEventLevel.Warning)
			  .MinimumLevel.Override("Microsoft.AspNetCore.Routing", LogEventLevel.Warning)
			  .MinimumLevel.Override("Microsoft.AspNetCore.Cors", LogEventLevel.Warning)
			  .MinimumLevel.Override("Microsoft.AspNetCore.Hosting.Diagnostics", LogEventLevel.Warning)
			  .Enrich.WithProperty("Service", serviceName)
			  .Enrich.With<ActivityEnricher>()
			  .Enrich.WithExceptionDetails()
			  .ReadFrom.Configuration(context.Configuration)
			  .ReadFrom.Services(services)
			  .Enrich.FromLogContext()
			  .WriteTo.Console()
			  .WriteTo.Elasticsearch(
				  new ElasticsearchSinkOptions(new Uri(loggingOptions.ElasticUri))
				  {
					AutoRegisterTemplate = true,
					IndexFormat = $"{serviceIndex}-{DateTime.UtcNow:yyyy-MM}",
					AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv8,
					CustomFormatter = new ElasticsearchJsonFormatter(false, Environment.NewLine),
					ModifyConnectionSettings = x =>
						  x.BasicAuthentication(loggingOptions.ElasticUser, loggingOptions.ElasticPassword),
				  });
	  });


  public static async Task PushClientIpToLog(HttpContext context, Func<Task> next)
  {
	using (LogContext.PushProperty("IPAddress", context.Connection.RemoteIpAddress))
	{
	  await next();
	}
  }
}
