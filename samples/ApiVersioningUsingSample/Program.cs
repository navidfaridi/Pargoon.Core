using Pargoon.ApiVersioning;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerService(builder.Configuration);

var app = builder.Build();

app.UseSwaggerVersioned(showSwaggerInAllEnv: true);

app.UseAuthorization();

app.MapControllers();

app.Run();
