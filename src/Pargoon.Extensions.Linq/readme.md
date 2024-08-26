# Pargoon.Extensions.Linq

## Overview

`Pargoon.Extensions.Linq` is a C# library that provides a set of extension methods for enhancing `IQueryable<T>` and `IEnumerable<T>` operations. These extensions focus on conditional filtering (`WhereIf`) and dynamic sorting (`Sorting`), allowing developers to build more flexible and maintainable LINQ queries.

## Features

- **Conditional Filtering**: Easily apply filters to queries based on runtime conditions.
- **Dynamic Sorting**: Sort collections dynamically based on property names and sort directions, with support for multiple sorting criteria.
- **Supports Both `IQueryable<T>` and `IEnumerable<T>`**: Although the library is optimized for `IQueryable<T>`, it also provides fallback support for `IEnumerable<T>` by converting to `IQueryable<T>`.

## Installation

To use the `Pargoon.Extensions.Linq` library, simply include the source file in your project or compile the code into a DLL and reference it in your project.

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

The `Sorting` extension methods provide a way to sort collections based on property names and sort directions at runtime.

#### Single Property Sorting

```csharp
using Pargoon.Extensions.Linq;

var sortedData = data.Sorting("PropertyName", SortDirection.Asc);
```

- `PropertyName`: The name of the property to sort by.
- `SortDirection.Asc`: Sort direction (ascending or descending).

#### Multiple Property Sorting

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

Here's a complete example of using `Pargoon.Extensions.Linq` in a typical scenario:

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
- The filtered results are then sorted by `FirstName` in ascending order and `LastName` in descending order.

## Contributing

Contributions are welcome! If you find a bug or have a feature request, please open an issue or submit a pull request.

## License

This library is licensed under the MIT License. See the LICENSE file for more details.

---

With `Pargoon.Extensions.Linq`, you can build more dynamic and flexible LINQ queries, improving both the readability and maintainability of your code.