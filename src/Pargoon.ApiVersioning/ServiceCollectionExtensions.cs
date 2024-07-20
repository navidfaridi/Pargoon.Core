﻿using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Pargoon.ApiVersioning;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSwaggerService(this IServiceCollection services, string xmlPath)
    {

        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, SwaggerConfigOptions>();

        services.AddSwaggerGen(options =>
        {
            options.IncludeXmlComments(xmlPath);
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

    public static void UseSwaggerVersioned(this WebApplication app, bool showSwaggerInAllEnv = false)
    {
        if (app.Environment.IsDevelopment() || showSwaggerInAllEnv)
        {
            var versionDescProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                foreach (var desc in versionDescProvider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint($"/swagger/{desc.GroupName}/swagger.json", $"{app.Environment.ApplicationName} - {desc.GroupName.ToUpper()}");
                }

                options.RoutePrefix = "swagger";
                options.DocumentTitle = $"{app.Environment.ApplicationName} API - Swagger UI";
                options.InjectStylesheet("/swagger-ui/custom.css");
                options.InjectJavascript("/swagger-ui/custom.js");
                options.EnableValidator(null);
                options.DisplayRequestDuration();
                options.DocExpansion(DocExpansion.List);

                options.OAuthClientId("swagger");
                options.OAuthClientSecret("secret");
                options.OAuthRealm("");
                options.OAuthAppName("Swagger");
                options.OAuthScopeSeparator(" ");
                options.OAuthUsePkce();
            });
        }
    }

}
