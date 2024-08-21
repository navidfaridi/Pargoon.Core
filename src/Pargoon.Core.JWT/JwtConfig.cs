using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Pargoon.Core.JWT;


public static class JwtConfig
{
	static JwtSettings GetJwtSettings(IConfiguration configuration)
	{
		return new JwtSettings
		{
			Audience = configuration.GetSection("JwtSettings:Audience").Value ?? "DefaultAudience",
			Issuer = configuration.GetSection("JwtSettings:Issuer").Value ?? "DefaultIssuer",
			EncKey = configuration.GetSection("JwtSettings:EncKey").Value ?? "DefaultEncKey",
			Secret = configuration.GetSection("JwtSettings:Secret").Value ?? "DefaultSecret",
			ExpirationInMinutes = int.TryParse(configuration.GetSection("JwtSettings:ExpirationInMinutes").Value, out int expiration) ? expiration : 60,
			RefreshTokenExpirationInMinutes = int.TryParse(configuration.GetSection("JwtSettings:RefreshTokenExpirationInMinutes").Value, out int exp) ? exp : 24 * 60 * 2
		};
	}

	public static void ConfigureJwt(this IServiceCollection services, IConfiguration configuration)
	{
		services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

		var settings = services.BuildServiceProvider().GetRequiredService<IOptions<JwtSettings>>();
		var config = settings.Value;

		var isk = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.Secret));
		var tdk = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.EncKey));
		//AsymmetricSecurityKey
		//X509SecurityKey
		//SecurityKeyIdentifier 
		//JsonWebKey 
		var tvp = new TokenValidationParameters
		{
			ValidateIssuer = true,
			ValidateAudience = true,
			ValidateLifetime = true,
			ValidateIssuerSigningKey = true,

			ValidIssuer = config.Issuer,
			ValidAudience = config.Audience,
			IssuerSigningKey = isk,
			TokenDecryptionKey = tdk,
			ClockSkew = TimeSpan.FromMinutes(0)
		};
		services
			.AddAuthentication(o =>
			{
				o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
			.AddJwtBearer(opt =>
			{
				opt.TokenValidationParameters = tvp;
			});
	}
	public static string CreateToken(string username, Guid userUniqueId, List<Claim> claims, JwtSettings settings)
	{
		claims.Add(new Claim(ClaimTypes.Name, username));
		claims.Add(new Claim(ClaimTypes.NameIdentifier, userUniqueId.ToString()));
		var isk = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.Secret));
		var tdk = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.EncKey));

		var cred = new SigningCredentials(isk, SecurityAlgorithms.HmacSha256);
		var encryptingCredentials = new EncryptingCredentials(tdk,
			JwtConstants.DirectKeyUseAlg,
			SecurityAlgorithms.Aes256CbcHmacSha512);

		var jwtSecurityToken = new JwtSecurityTokenHandler()
			.CreateJwtSecurityToken(
				settings.Issuer,
				settings.Audience,
				new ClaimsIdentity(claims),
				notBefore: DateTime.UtcNow,
				expires: DateTime.UtcNow.AddMinutes(settings.ExpirationInMinutes),
				issuedAt: DateTime.UtcNow,
				signingCredentials: cred,
				encryptingCredentials: encryptingCredentials
		);

		var jwt = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

		return jwt;
	}
	public static string CreateToken(string username, Guid userUniqueId, List<string> roles, JwtSettings settings)
	{
		List<Claim> claims = new() {
					new Claim(ClaimTypes.Name, username),
					new Claim(ClaimTypes.NameIdentifier, userUniqueId.ToString()),
		};
		foreach (var item in roles)
			claims.Add(new Claim(ClaimTypes.Role, item));

		return CreateToken(username, userUniqueId, claims, settings);
	}
	public static string CreateToken(string username, Guid userUniqueId, List<string> roles, IConfiguration configuration)
	{
		var config = GetJwtSettings(configuration);
		return CreateToken(username, userUniqueId, roles, config);
	}

	public static string GetUsername(ClaimsPrincipal user)
	{
		if (user != null)
		{
			var nameClaim = user.FindFirst(ClaimTypes.Name);
			if (nameClaim != null)
				return nameClaim.Value;
		}
		return string.Empty;
	}
	public static string GetUserId(ClaimsPrincipal user)
	{
		if (user != null)
		{
			var nameClaim = user.FindFirst(ClaimTypes.NameIdentifier);
			if (nameClaim != null)
				return nameClaim.Value;
		}
		return string.Empty;
	}
	public static List<string> GetRoles(ClaimsPrincipal user)
	{
		var roles = new List<string>();
		if (user == null)
			return roles;

		foreach (var item in user.Claims.Where(u => u.Type == ClaimTypes.Role))
			roles.Add(item.Value);

		return roles;
	}
}