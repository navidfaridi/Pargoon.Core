using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Pargoon.Extensions.Linq;

public class TPaginationResult<T>
{
    [DataMember(Name = "data")]
    [JsonPropertyName("data")]
    public List<T> Data { get; set; } = new List<T>();

    [JsonPropertyName("pageSize")]
    public int PageSize { get; set; } = 10;

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

public class TListRequest<T>
{
    [JsonPropertyName("pageSize")]
    public int PageSize { get; set; } = 10;
    [JsonPropertyName("pageIndex")]
    public int PageIndex { get; set; } = 0;
    [JsonPropertyName("sortItems")]
    public List<SortItem> SortItems { get; set; } = new List<SortItem>();
}