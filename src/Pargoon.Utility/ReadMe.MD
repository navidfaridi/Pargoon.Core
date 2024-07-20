# Pargoon.Utility

## Overview

Pargoon.utility is a collection of utility libraries aimed at simplifying common programming tasks. The utilities cover a wide range of functions including string manipulation, array handling, regular expressions, and random number generation.

## Pargoon.Utility

The `Pargoon.Utility` package provides various utility functions for working with regular expressions, strings, arrays, and random numbers. It is designed to be simple and effective for everyday programming needs.

### Installation

You can install the package via NuGet using the following command:

```bash
dotnet add package Pargoon.Utility --version 1.1.2
```

Or update your project file with:

```xml
<PackageReference Include="Pargoon.Utility" Version="1.1.2" />
```

### Classes and Methods

#### RegXPattern
This class contains useful regular expression patterns for common validation and matching tasks.

#### StringUtility
Provides extension methods for string manipulation, such as trimming, padding, and converting cases.

#### ArrayUtility
Includes extension methods for working with arrays, such as merging arrays into strings.

#### RandomUtils
Offers functions to generate random numbers and strings, useful for testing and simulations.

#### Encryption (New)
Provides methods for encrypting and decrypting data to enhance security.

#### ObjectUtility (New)
Includes helper methods for working with objects, such as deep cloning and property manipulation.

#### TimeUtility (New)
Contains functions for working with date and time, such as formatting and converting time zones.

### Usage Examples

#### Generating a Random String
```csharp
using Pargoon.Utility;

string randomStr = RandomUtils.RandomString(10);
Console.WriteLine(randomStr);  // Outputs a random string of length 10
```

#### Merging Arrays into a String
```csharp
using Pargoon.Utility;

string[] array = { "one", "two", "three" };
string result = ArrayUtility.MergeToString(array, ", ");
Console.WriteLine(result);  // Outputs: "one, two, three"
```

#### Using Regular Expression Patterns
```csharp
using Pargoon.Utility;

bool isValidEmail = Regex.IsMatch("example@example.com", RegXPattern.EmailPattern);
Console.WriteLine(isValidEmail);  // Outputs: True
```

### License

This project is licensed under the MIT License. See the [LICENSE](../LICENSE) file for details.

---

For more information and updates, visit the [Pargoon.Core GitHub repository](https://github.com/navidfaridi/Pargoon.Core) and the [NuGet package page](https://www.nuget.org/packages/Pargoon.Utility)【20†source】【21†source】.