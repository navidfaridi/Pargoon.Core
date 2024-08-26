using System.Text.Json.Serialization;

namespace Pargoon.Extensions.Linq;
public enum SortDirection : byte
{
    Asc = 1,
    Desc = 2
}
public class SortItem
{
    [JsonPropertyName("propertyName")]
    public string PropertyName { get; set; } = null!;
    [JsonPropertyName("direction")]
    public SortDirection Direction { get; set; } = SortDirection.Asc;
}
