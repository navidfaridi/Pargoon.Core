using System.Runtime.Serialization;

namespace Pargoon.Core;

public class TPagination<T>
{
	private int _totalCount = 0;
	[DataMember(Name = "data")]
	public T? Data { get; set; }
	public int PageSize { get; set; } = 10;
	public int PageCount { get; set; } = 0;
	public int TotalCount
	{
		get
		{
			return _totalCount;
		}
		set
		{
			_totalCount = value;
			PageCount = _totalCount / PageSize;
			if (_totalCount > 0 && PageCount == 0)
				PageCount = 1;
		}
	}
}
