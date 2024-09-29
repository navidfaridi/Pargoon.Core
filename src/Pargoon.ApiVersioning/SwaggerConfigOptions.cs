#pragma warning disable 414, CS3021, CS1591

using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Pargoon.ApiVersioning;

public class SwaggerConfigOptions : IConfigureOptions<SwaggerGenOptions>
{
    private readonly IApiVersionDescriptionProvider _apiVersionDescriptionProvider;
    private readonly ApiInfo _apiInfo;
    public SwaggerConfigOptions(IApiVersionDescriptionProvider apiVersionDescriptionProvider,
        IOptions<ApiInfo> apiInfo)
    {
        _apiVersionDescriptionProvider = apiVersionDescriptionProvider;
        _apiInfo = apiInfo.Value;
    }
    public void Configure(SwaggerGenOptions options)
    {
        foreach (var desc in _apiVersionDescriptionProvider.ApiVersionDescriptions)
        {
            options.SwaggerDoc(desc.GroupName, new OpenApiInfo
            {
                Title = $"{_apiInfo.ApiName} - {_apiInfo.ApiVersion}:{_apiInfo.BuildNumber} : {desc.ApiVersion}",
                Version = _apiInfo.ApiBadge
            });
        }
    }
}