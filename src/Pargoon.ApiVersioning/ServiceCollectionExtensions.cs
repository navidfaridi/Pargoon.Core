#pragma warning disable 414, CS3021, CS1591
using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Pargoon.ApiVersioning;
/// <summary>
/// Service Configuration Extensions
/// </summary>
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSwaggerService(this IServiceCollection services, IConfiguration configuration, string xmlPath = "")
    {
        services.Configure<ApiInfo>(configuration.GetSection(ApiInfo.SectionName));
        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, SwaggerConfigOptions>();

        services.AddSwaggerGen(options =>
        {
            if (!string.IsNullOrEmpty(xmlPath))
                options.IncludeXmlComments(xmlPath);
            OpenApiSecurityScheme securityScheme = new OpenApiSecurityScheme
            {
                Name = "Authorization",
                BearerFormat = "JWT",
                Scheme = "bearer",
                Description = "Specify the authorization token.",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http
            };
            options.AddSecurityDefinition("Bearer", securityScheme);
            OpenApiSecurityRequirement securityRequirement = new OpenApiSecurityRequirement {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                Array.Empty<string>()
            } };
            options.AddSecurityRequirement(securityRequirement);
            options.EnableAnnotations();
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
    /// <summary>
    /// Configure swagger middleware
    /// </summary>
    /// <param name="app"></param>
    /// <param name="showSwaggerInAllEnv"></param>
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
                options.DocumentTitle = $"{app.Environment.ApplicationName} - Swagger UI";
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
