namespace Pargoon.Extensions.Linq;
public enum SortDirection : byte
{
    Asc = 1,
    Desc = 2
}
public class SortItem
{
    public string PropertyName { get; set; } = null!;
    public SortDirection Direction { get; set; } = SortDirection.Asc;
}
