# Pargoon.Exceptions

**Pargoon.Exceptions** is a .NET Standard library designed to simplify exception handling in .NET applications. It provides a set of classes to help manage errors in a consistent and structured manner.

## Features

- **AppError**: A simple class to encapsulate error details such as HTTP status code, error code, and error message.
- **AppException**: A custom exception class that extends the base `Exception` class, allowing for more detailed error information including HTTP status codes and custom error codes.

## Installation

To install this library, you can use the following command in the NuGet Package Manager:

```sh
Install-Package Pargoon.Exceptions
```

Or add it to your project file:

```xml
<PackageReference Include="Pargoon.Exceptions" Version="1.0.0" />
```

## Usage

### AppError Class

The `AppError` class is designed to hold error details such as `StatusCode`, `ErrorCode`, and `ErrorMessage`. You can use this class to define the error that you want to handle.

```csharp
var error = new AppError
{
    StatusCode = HttpStatusCode.BadRequest,
    ErrorCode = "400",
    ErrorMessage = "Invalid request data."
};
```

### AppException Class

The `AppException` class is a custom exception that can be thrown in your application to handle errors more effectively. This class allows you to pass error details when an exception occurs.

```csharp
throw new AppException(HttpStatusCode.BadRequest, "Invalid request data.", "400");
```

Alternatively, you can create an `AppException` from an `AppError` object:

```csharp
throw new AppException(error);
```

### Example

Here is a simple example of how to use the Pargoon.Exceptions library:

```csharp
try
{
    // Some code that may throw an exception
    throw new AppException(HttpStatusCode.NotFound, "Resource not found", "404");
}
catch (AppException ex)
{
    Console.WriteLine($"Error: {ex.ErrorCode}, Status: {ex.Status}, Message: {ex.Message}");
}
```

## Contributing

Contributions are welcome! Please open an issue or submit a pull request if you would like to contribute to this project.

## License

This project is licensed under the MIT License. See the LICENSE file for more details.
