# Pargoon.Extensions.Linq

## Overview

`Pargoon.Extensions.Linq` is a C# library that enhances LINQ operations by providing a set of extension methods. These methods allow for conditional filtering and dynamic sorting of `IQueryable<T>` collections, making it easier to build flexible and maintainable queries.

## Features

- **Conditional Filtering**: Use `WhereIf` to apply filters only when a specified condition is true.
- **Dynamic Sorting**: Sort collections based on property names and directions specified at runtime, with support for sorting on multiple properties.
- **Supports Custom Comparers**: Sorting methods support custom comparers for advanced sorting scenarios.

## Installation

To use the `Pargoon.Extensions.Linq` library, add the source file to your project or compile it into a DLL and reference it in your project.

## Usage

### 1. Conditional Filtering (`WhereIf`)

The `WhereIf` extension method allows you to apply a filter to a query only if a specified condition is true.

```csharp
using Pargoon.Extensions.Linq;

// Applying a filter conditionally
var filteredData = data.WhereIf(isActive, x => x.IsActive);
```

- `isActive`: A boolean condition.
- `x => x.IsActive`: The predicate to apply if `isActive` is true.

### 2. Dynamic Sorting (`Sorting`)

The `Sorting` extension methods enable sorting collections based on property names and sort directions at runtime.

#### Single Property Sorting

```csharp
using Pargoon.Extensions.Linq;

var sortedData = data.Sorting("PropertyName", SortDirection.Asc);
```

- `PropertyName`: The name of the property to sort by.
- `SortDirection.Asc`: The direction to sort in (ascending or descending).

#### Multiple Property Sorting

You can sort by multiple properties by providing a list of `SortItem` objects.

```csharp
using Pargoon.Extensions.Linq;

var sortItems = new List<SortItem>
{
    new SortItem { PropertyName = "FirstName", Direction = SortDirection.Asc },
    new SortItem { PropertyName = "LastName", Direction = SortDirection.Desc }
};

var sortedData = data.Sorting(sortItems);
```

- `sortItems`: A list of `SortItem` objects specifying the properties and directions to sort by.

### 3. Custom Comparers

If you need to use a custom comparer for sorting, you can pass it as an optional parameter:

```csharp
using Pargoon.Extensions.Linq;

var sortedData = data.Sorting("PropertyName", SortDirection.Asc, comparer: new CustomComparer());
```

- `CustomComparer`: Your custom `IComparer<object>` implementation.

## Example

Here's a complete example using `Pargoon.Extensions.Linq` in a typical scenario:

```csharp
using Pargoon.Extensions.Linq;

var isActive = true;
var sortItems = new List<SortItem>
{
    new SortItem { PropertyName = "FirstName", Direction = SortDirection.Asc },
    new SortItem { PropertyName = "LastName", Direction = SortDirection.Desc }
};

var query = dbContext.Users
    .WhereIf(isActive, x => x.IsActive)
    .Sorting(sortItems);

var result = query.ToList();
```

In this example:

- Users are filtered by `IsActive` only if `isActive` is `true`.
- The filtered results are sorted by `FirstName` in ascending order and `LastName` in descending order.

## Contributing

Contributions are welcome! If you find a bug or have a feature request, please open an issue or submit a pull request.

## License

This library is licensed under the MIT License. See the LICENSE file for more details.

---

With `Pargoon.Extensions.Linq`, you can build dynamic and flexible LINQ queries that improve the readability and maintainability of your code.