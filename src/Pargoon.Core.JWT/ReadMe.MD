# Pargoon.Core.JWT

The `Pargoon.Core.JWT` package provides utilities for configuring and generating JWT (JSON Web Tokens) in ASP.NET Core applications.

## Installation

You can install the `Pargoon.Core.JWT` package via NuGet Package Manager or by using the following command in your project:

```bash
dotnet add package Pargoon.Core.JWT
```

## Configuration

1. **Add JWT settings to `appsettings.json`**:

    ```json
    {
      "JwtSettings": {
        "Audience": "YourAudience",
        "Issuer": "YourIssuer",
        "EncKey": "YourEncryptionKey",
        "Secret": "YourSecretKey",
        "ExpirationInMinutes": 60,
        "RefreshTokenExpirationInMinutes":2460
      }
    }
    ```

2. **Configure services in `Startup.cs` or `Program.cs`**:

    ```csharp
        // configuration in Program.cs
        builder.Services.ConfigureJwt(builder.Configuration); // will config services.Configure<JwtSettings>(Configuration.GetSection("JwtSettings"));
        // Other services
    
    ```

## Usage

### Injecting `JwtSettings`

You can inject `JwtSettings` into any service or controller in your application:

```csharp
using Microsoft.Extensions.Options;

public class MyService
{
    private readonly JwtSettings _jwtSettings;

    public MyService(IOptions<JwtSettings> jwtSettings)
    {
        _jwtSettings = jwtSettings.Value;
    }

    public void MyMethod()
    {
        var issuer = _jwtSettings.Issuer;
        // Use the settings as needed
    }
}
```

### Generating a JWT Token

Use the `JwtConfig` class to generate JWT tokens. Here's an example of a service that generates tokens:

```csharp
using System;
using System.Collections.Generic;

public class TokenService
{
    private readonly JwtSettings _jwtSettings;

    public TokenService(IOptions<JwtSettings> jwtSettings)
    {
        _jwtSettings = jwtSettings.Value;
    }

    public string GenerateToken(string username, Guid userUniqueId, List<string> roles)
    {
        return JwtConfig.CreateToken(username, userUniqueId, roles, _jwtSettings);
    }
}
```

And then use this `TokenService` in your controller:

```csharp
[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly TokenService _tokenService;

    public AuthController(TokenService tokenService)
    {
        _tokenService = tokenService;
    }

    [HttpPost("login")]
    public IActionResult Login(string username, Guid userUniqueId, List<string> roles)
    {
        var token = _tokenService.GenerateToken(username, userUniqueId, roles);
        return Ok(new { Token = token });
    }
}
```

## Contributing

Contributions are welcome! Please open an issue or submit a pull request on [GitHub](https://github.com/navidfaridi/Pargoon.Core/tree/main/src/Pargoon.Core.JWT).

## License

This project is licensed under the MIT License. See the LICENSE file for more details.

