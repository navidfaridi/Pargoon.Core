using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Pargoon.ApiVersioning;

public class SwaggerConfigOptions : IConfigureOptions<SwaggerGenOptions>
{
  private readonly IApiVersionDescriptionProvider _apiVersionDescriptionProvider;

  public SwaggerConfigOptions(IApiVersionDescriptionProvider apiVersionDescriptionProvider)
  {
	_apiVersionDescriptionProvider = apiVersionDescriptionProvider;
  }

  public void Configure(SwaggerGenOptions options)
  {
	foreach (var desc in _apiVersionDescriptionProvider.ApiVersionDescriptions)
	{
	  options.SwaggerDoc(desc.GroupName, new OpenApiInfo
	  {
		Title = $"BookMe API {desc.ApiVersion}",
		Version = desc.ApiVersion.ToString()
	  });
	}
  }
}