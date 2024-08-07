﻿using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Pargoon.ApiVersioning;

/// <summary>
/// SwaggerConfigOptions
/// </summary>
public class SwaggerConfigOptions : IConfigureOptions<SwaggerGenOptions>
{
    private readonly IApiVersionDescriptionProvider _apiVersionDescriptionProvider;
    private readonly IConfiguration _configuration;
    /// <summary>
    /// SwaggerConfigOptions Constructor
    /// </summary>
    /// <param name="apiVersionDescriptionProvider"></param>
    /// <param name="configuration"></param>
    public SwaggerConfigOptions(IApiVersionDescriptionProvider apiVersionDescriptionProvider, IConfiguration configuration)
    {
        _apiVersionDescriptionProvider = apiVersionDescriptionProvider;
        _configuration = configuration;
    }
    /// <summary>
    /// Configure SwaggerGenOptions
    /// </summary>
    /// <param name="options"></param>
    public void Configure(SwaggerGenOptions options)
    {
        var apiInfo = new ApiInfo();
        _configuration.GetSection(ApiInfo.SectionName).Bind(apiInfo);
        foreach (var desc in _apiVersionDescriptionProvider.ApiVersionDescriptions)
        {
            options.SwaggerDoc(desc.GroupName, new OpenApiInfo
            {
                Title = $"{apiInfo.ApiName} - {apiInfo.ApiVersion} : {desc.ApiVersion}",
                Version = apiInfo.ApiBadge
            });
        }
    }
}