using System;
using System.Threading.Tasks;
using global::Serilog;
using global::Serilog.Context;
using global::Serilog.Events;
using global::Serilog.Formatting.Elasticsearch;
using global::Serilog.Sinks.Elasticsearch;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;

namespace Pargoon.Serilog
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddSerilog(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<LoggingOptions>(configuration.GetSection(nameof(LoggingOptions)));
            services.AddLogging(b => b.AddSerilog(Log.Logger));
            return services;
        }
    }
    public class LoggingOptions
    {
        public string ElasticUri { get; set; }
        public string ElasticUser { get; set; }
        public string ElasticPassword { get; set; }
    }

    public static class HostExtensions
    {
        public static IHostBuilder UseSerilog(this IHostBuilder hostBuilder, string serviceName) =>
            hostBuilder.UseSerilog((context, services, loggerConfiguration) =>
            {
                var loggingOptions = services.GetRequiredService<IOptions<LoggingOptions>>().Value;
                loggerConfiguration
                    .Enrich.FromLogContext()
                    .Enrich.WithMachineName()
                    .Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName)
                    .Enrich.WithProperty("Application", context.HostingEnvironment.ApplicationName)
                    .MinimumLevel.Information()
                    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                    .MinimumLevel.Override("System", LogEventLevel.Warning)
                    //.MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
                    //.MinimumLevel.Override("Microsoft.AspNetCore.Mvc", LogEventLevel.Warning)
                    //.MinimumLevel.Override("Microsoft.AspNetCore.Routing", LogEventLevel.Warning)
                    //.MinimumLevel.Override("Microsoft.AspNetCore.Cors", LogEventLevel.Warning)
                    //.MinimumLevel.Override("Microsoft.AspNetCore.Hosting.Diagnostics", LogEventLevel.Warning)
                    .Enrich.WithProperty("Service", serviceName)
                    .ReadFrom.Configuration(context.Configuration)
                    //.ReadFrom.Services(services)
                    .WriteTo.Console()
                    .WriteTo.Elasticsearch(
                        new ElasticsearchSinkOptions(new Uri(loggingOptions.ElasticUri))
                        {
                            IndexFormat = $"applogs-{serviceName}-{context.HostingEnvironment.EnvironmentName?.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}",
                            AutoRegisterTemplate = true,
                            NumberOfShards = 2,
                            NumberOfReplicas = 1,
                            AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv7,
                            CustomFormatter = new ElasticsearchJsonFormatter(false, Environment.NewLine),
                            ////ModifyConnectionSettings = x => x.BasicAuthentication(loggingOptions.ElasticUser, loggingOptions.ElasticPassword),
                        });
                //                    .WriteTo.Console(outputTemplate: "{Level:u3} {SourceContext:l} {Message:lj}{NewLine}{Exception}");
            });


        public static async Task PushClientIpToLog(HttpContext context, Func<Task> next)
        {
            using (LogContext.PushProperty("IPAddress", context.Connection.RemoteIpAddress))
            {
                await next();
            }
        }
    }
}
