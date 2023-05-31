# Pargoon.Utility

Pargoon.Utility is a utility library that provides various helpful functions for string manipulation, encryption, and more.

## Features

- String manipulation utilities
- Encryption utilities
- Expiration-based encryption
- ObjectUtility
- RandomUtility
- RegXPattern
- TimeUtility


## Installation

You can install the Pargoon.Utility library using NuGet package manager. Run the following command in the NuGet Package Manager Console:


### RegXPattern
This part contain some useful regular expression patterns.

### StringUtility
Contain some extension methods for strings

### ArrayUtility
Contain some useful extension methods for merging arrays to string

### RandomUtils
Contain some useful function for generating random number and string



## Usage

### String Manipulation

```csharp
using Pargoon.Utility;
string[] names = { "John", "Jane", "Michael" };
string mergedNames = names.MergeToString(names);
Console.WriteLine(mergedNames); // Output: John, Jane, Michael

```

### Encryption

```csharp
string password = "MyPassword123";
string encryptedPassword = PasswordHelper.EncryptPasswordMd5(password);
Console.WriteLine(encryptedPassword); // Output: 5F4DCC3B5AA765D61D8327DEB882CF99

string message = "Hello, World!";
string passphrase = "MySecretKey";
string encryptedMessage = PasswordHelper.EncryptString(message, passphrase);
string decryptedMessage = PasswordHelper.DecryptString(encryptedMessage, passphrase);
Console.WriteLine(decryptedMessage); // Output: Hello, World!
```

### Expiration-based Encryption

```csharp
string message = "Confidential data";
string passphrase = "MySecretKey";
TimeSpan expiration = TimeSpan.FromHours(1);

string encryptedWithExpiration = PasswordHelper.EncryptWithExpiration(message, passphrase, expiration);
string decryptedWithExpiration = PasswordHelper.DecryptWithExpiration(encryptedWithExpiration, passphrase);
Console.WriteLine(decryptedWithExpiration); // Output: Confidential data (within 1 hour of encryption)
```


## Contributing

Contributions to Pargoon.Utility are welcome! If you find any bugs, have feature requests, or want to contribute code improvements, please open an issue or submit a pull request.

## License

This project is licensed under the MIT License.
