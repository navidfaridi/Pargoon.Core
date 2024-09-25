namespace Pargoon.Core;

public enum SortDirection : byte
{
    Asc = 1,
    Desc = 2
}
public class SortItem
{
    public string SortPropertyName { get; set; } = string.Empty;
    public SortDirection SortDirection { get; set; } = SortDirection.Asc;
}
