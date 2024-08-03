# Pargoon.TagHelpers

[![NuGet Version](https://img.shields.io/nuget/v/Pargoon.TagHelpers.svg?style=flat)](https://www.nuget.org/packages/Pargoon.TagHelpers)
[![NuGet Downloads](https://img.shields.io/nuget/dt/Pargoon.TagHelpers.svg?style=flat)](https://www.nuget.org/packages/Pargoon.TagHelpers)

Pargoon.TagHelpers is a library for ASP.NET Core that adds Ajax capabilities to `a` and `form` tags in Razor pages. This library provides functionality similar to the Ajax Helper in the .NET Framework, making it easy to send Ajax requests.

## Features

- **AjaxAnchorTagHelper**: Adds Ajax capabilities to `a` tags.
- **AjaxFormTagHelper**: Adds Ajax capabilities to `form` tags.

## Installation

You can install the `Pargoon.TagHelpers` package via NuGet Package Manager:

```sh
Install-Package Pargoon.TagHelpers
```

Or via .NET CLI:

```sh
dotnet add package Pargoon.TagHelpers
```

## Usage

To use these Tag Helpers, first register the library in your `_ViewImports.cshtml` file:

```csharp
@addTagHelper *, Pargoon.TagHelpers
```

### AjaxAnchorTagHelper

This Tag Helper adds Ajax capabilities to `a` tags.

Example:

```html
<a asp-ajax="true" asp-ajax-method="GET" asp-ajax-update="#result" href="/Home/GetData">Get Data</a>
```

- `asp-ajax="true"`: Enables Ajax functionality.
- `asp-ajax-method`: Specifies the HTTP method (e.g., GET, POST).
- `asp-ajax-update`: CSS selector where the Ajax response will be placed.

### AjaxFormTagHelper

This Tag Helper adds Ajax capabilities to `form` tags.

Example:

```html
<form asp-ajax="true" asp-ajax-update="#result" asp-controller="Home" asp-action="SubmitForm">
    <input type="text" name="name" />
    <button type="submit">Submit</button>
</form>
```

- `asp-ajax="true"`: Enables Ajax functionality.
- `asp-ajax-update`: CSS selector where the Ajax response will be placed.
- `asp-controller` and `asp-action`: Specifies the controller and action to which the form will be submitted.

## Contributing

Contributions are welcome! Please check out the [contributing guidelines](https://github.com/navidfaridi/Pargoon.Core/blob/main/CONTRIBUTING.md) for more information.

## License

This project is licensed under the MIT License. See the [LICENSE](https://github.com/navidfaridi/Pargoon.Core/blob/main/LICENSE) file for details.

## Source Code

The source code for this library can be found on GitHub: [Pargoon.TagHelpers Source Code](https://github.com/navidfaridi/Pargoon.Core/tree/main/src/Pargoon.TagHelpers)

## Contact

For any questions or suggestions, feel free to create an issue on the GitHub repository or contact the maintainer.

---

Made with ❤️ by Navid Faridi.
