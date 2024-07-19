namespace Pargoon.ApiVersioning;

public class ApiInfo
{
    public const string SectionName = "ApiInfo";
    public string ApiVersion { get; set; } = string.Empty;
    public string ApiName { get; set; } = string.Empty;
    public string ApiBadge { get; set; } = string.Empty;
}