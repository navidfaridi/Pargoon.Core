using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Pargoon.Core;

public class TPagedResult<T>
{
    [JsonPropertyName("items")]
    public List<T> Items { get; set; } = new List<T>();

    [JsonPropertyName("pageSize")]
    public int PageSize { get; set; } = 10;

    [JsonPropertyName("pageIndex")]
    public int PageIndex { get; set; } = 0;

    [JsonPropertyName("pageCount")]
    public int PageCount
    {
        get
        {
            return (TotalRecords + PageSize - 1) / PageSize;
        }
    }

    [JsonPropertyName("totalRecords")]
    public int TotalRecords { get; set; }
}