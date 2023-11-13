using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Pargoon.ApiVersioning;

public static class ServiceCollectionExtensions
{
  public static IServiceCollection AddSwaggerService(this IServiceCollection services)
  {
	services.AddTransient<IConfigureOptions<SwaggerGenOptions>, SwaggerConfigOptions>();
	services.AddSwaggerGen(options =>
	{
	  options.DocumentFilter<SwaggerDocumentFilter>();
	});
	services.AddApiVersioning(options =>
	{
	  options.DefaultApiVersion = new ApiVersion(1, 0);
	  options.AssumeDefaultVersionWhenUnspecified = true;
	  options.ReportApiVersions = true;
	}).AddApiExplorer(options =>
	{
	  options.SubstituteApiVersionInUrl = true;
	});
	return services;
  }

  public static void UseSwaggerVersioned(this WebApplication app)
  {
	if (app.Environment.IsDevelopment())
	{
	  var versionDescProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
	  app.UseSwagger();
	  app.UseSwaggerUI(options =>
	  {
		foreach (var desc in versionDescProvider.ApiVersionDescriptions)
		{
		  options.SwaggerEndpoint($"/swagger/{desc.GroupName}/swagger.json", $"{app.Environment.ApplicationName} - {desc.GroupName.ToUpper()}");
		}
	  });
	}
  }
}
