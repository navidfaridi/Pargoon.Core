# Pargoon.Core

Pargoon.Core is a .NET library that provides a set of utility classes for managing operation results in applications. The library offers a standardized mechanism for handling success and failure statuses, along with associated data and error messages, using the `TResult` and `PocoResult` classes.

## Features

- **PocoResult Class**: A base class that includes the status code (`StatusCode`), a numeric code (`Code`), and a description (`Description`).
- **TResult Class**: The main class used to manage operation results, which can be initialized directly or using an HTTP status code.
- **Derived Classes**: Specific classes like `BadRequestDataResult`, `UnauthorizedDataResult`, etc., designed to handle specific HTTP statuses.
- **TResult<T>**: A generic version of `TResult` that also holds data related to the operation, in addition to managing the status.

## Installation

To install this library, you can use the NuGet Package Manager in Visual Studio:

```sh
Install-Package Pargoon.Core
```

Or via the .NET CLI:

```sh
dotnet add package Pargoon.Core
```

## Usage

After installation, you can use the classes provided by this library to manage operation results in your applications. Below are some examples:

### Using the `TResult` Class

```csharp
using Pargoon.Core;

public class Example
{
    public TResult ProcessData(int value)
    {
        if (value < 0)
        {
            return new BadRequestDataResult("Value cannot be negative");
        }

        return new TResult(200, "Processing successful");
    }

    public void Run()
    {
        var result = ProcessData(10);

        if (result.IsSuccess)
        {
            Console.WriteLine("Success: " + result.Description);
        }
        else
        {
            Console.WriteLine("Failure: " + result.Description);
        }
    }
}
```

### Using the `TResult<T>` Class

```csharp
using Pargoon.Core;

public class Example
{
    public TResult<string> ProcessData(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return TResult<string>.Fail(400, "Input cannot be null or empty");
        }

        return TResult<string>.Success(input.ToUpper(), "Data processed successfully");
    }

    public void Run()
    {
        var result = ProcessData("hello");

        if (result.IsSuccess)
        {
            Console.WriteLine("Processed Data: " + result.Data);
        }
        else
        {
            Console.WriteLine("Error: " + result.Description);
        }
    }
}
```

## Derived Classes

Pargoon.Core includes several derived classes from `TResult`, specifically designed to handle common HTTP statuses:

- **`BadRequestDataResult`**: For handling HTTP 400 Bad Request.
- **`UnauthorizedDataResult`**: For handling HTTP 401 Unauthorized.
- **`ForbiddenDataResult`**: For handling HTTP 403 Forbidden.
- **`NotFoundDataResult`**: For handling HTTP 404 Not Found.
- **`InternalServerErrorResult`**: For handling HTTP 500 Internal Server Error.

These classes allow you to easily manage different statuses and provide appropriate error messages.

## Contribution

Contributions are welcome! If you find a bug, have a feature request, or want to contribute to the project, feel free to open an issue or submit a pull request.

## License

This project is licensed under the MIT License. For more details, see the [LICENSE](LICENSE) file.

## Contact

For any questions or support, please reach out to [Navid Faridi](https://github.com/navidfaridi).

---

This `README.md` provides an overview of the Pargoon.Core library, including explanations, usage instructions, and contribution guidelines.