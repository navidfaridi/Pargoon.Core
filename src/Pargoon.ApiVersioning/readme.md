# API Versioning
---
Enable Swagger to Explorer Versioned API for dotnet 8.0 or above
---
```
using Noyanet.Core.ApiVersioning;

.
.
.
builder.Services.AddSwaggerService(builder.Configuration);

.
.
.
var app = builder.Build();

// added showSwaggerInAllEnv parameter to allow enable swagger in all environments, it is false by default
// you can add this optional param to enable swagger in staging or production environment
app.UseSwaggerVersioned(showSwaggerInAllEnv:true);  
```

from the version 8.0.0 we added ApiInfo class to the package which will read some data from appsettings.json if exists.

```
public class ApiInfo
{
    public const string SectionName = "ApiInfo";
    public string ApiVersion { get; set; } = string.Empty;
    public string BuildVersion { get; set; } = string.Empty;
    public string ApiName { get; set; } = string.Empty;
    public string ApiBadge { get; set; } = string.Empty;
}
```

this class properties will fill from appsettings.json like below:

```
...

    "ApiInfo": {
        "ApiVersion": "1.1.0",
        "BuildNumber": "1125",
        "ApiName": "my sample Api",
        "ApiBadge": "Api Badge"
    },

 ...
```