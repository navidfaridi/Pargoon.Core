﻿using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Pargoon.ApiVersioning;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddSwaggerService(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddTransient<IConfigureOptions<SwaggerGenOptions>, SwaggerConfigOptions>();
		var version = "1.0.0.0";
		if (configuration.GetSection(ApiInfo.SectionName) != null)
			version = configuration[$"{ApiInfo.SectionName}:{nameof(ApiInfo.ApiVersion)}"];

		var desc = "";
		if (configuration.GetSection(ApiInfo.SectionName) != null)
			desc = configuration[$"{ApiInfo.SectionName}:{nameof(ApiInfo.ApiDescription)}"];

		services.AddSwaggerGen(options =>
	{
		options.SwaggerDoc("Version1", new OpenApiInfo { Title = version, Version = "Version1", Description = desc });
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
