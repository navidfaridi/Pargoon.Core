using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Pargoon.Core;

public class TListRequest
{
    [JsonPropertyName("pageSize")]
    public int PageSize { get; set; } = 10;
    [JsonPropertyName("pageIndex")]
    public int PageIndex { get; set; } = 0;
    [JsonPropertyName("sortItems")]
    public List<SortItem> SortItems { get; set; } = new List<SortItem>();
}