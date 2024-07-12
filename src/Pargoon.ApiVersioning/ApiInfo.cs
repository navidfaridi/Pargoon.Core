namespace Pargoon.ApiVersioning;

public class ApiInfo
{
	public const string SectionName = "ApiInfo";
	public string ApiVersion { get; set; } = string.Empty;
	public string ApiDescription { get; set; } = string.Empty;
}