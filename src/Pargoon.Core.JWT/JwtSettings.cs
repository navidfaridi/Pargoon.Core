namespace Pargoon.Core.JWT;

public class JwtSettings
{
	public const string SectionName = "JwtSettings";
	public string Secret { get; set; } = null!;
	public string EncKey { get; set; } = null!;
	public string Issuer { get; set; } = null!;
	public string Audience { get; set; } = null!;
}
