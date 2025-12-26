# JadeEncryption: .NET 8 Encryption Library

[![.NET 8](https://img.shields.io/badge/.NET-8-blue.svg)]([https://aka.ms/new-console-template](https://aka.ms/new-console-template))
[![.NET 9](https://img.shields.io/badge/.NET-9-blue.svg)]([https://aka.ms/new-console-template](https://aka.ms/new-console-template))
[![.NET 10](https://img.shields.io/badge/.NET-10-blue.svg)]([https://aka.ms/new-console-template](https://aka.ms/new-console-template))
[![Nuget](https://img.shields.io/nuget/v/JadeEncryption.svg)](https://www.nuget.org/packages/JadeEncryption)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

JadeEncryption provides easy-to-use methods for both one-way (hashing) and two-way (reversible) encryption within your .NET 8 projects.


## Important: Migrating from Jaded Encryption
**Those previously using Jaded Encryption can migrate to this by changing the below reference**
```
using JadeEncryption;
```

To

```
using JadeEncryption;
```


## Features

* **One-Way Encryption (Hashing):**
   * Securely hash sensitive data (e.g., passwords) for storage.
   * Verification to ensure data integrity.
* **Two-Way Encryption:**
   * Encrypt and decrypt confidential information.
   * Supports various key sizes (128, 192, 256 bits).
   * Utilizes AES (Advanced Encryption Standard) for robustness.

## Getting Started

1. **Install the Package:**
   ```bash
   dotnet add package JadeEncryption

2. **Reference the Package:**
    Add a using statement in your code:
    ```csharp
    using JadeEncryption;


## Demo: One-Way Encryption (Hashing)

1. **Create an instance of OnewayEncryption:**
    ```csharp
    OnewayEncryption oneWayEncryption = new OnewayEncryption();
    ```
    Or specify a custom iteration count (recommended: 10,000+ for new projects):
    ```csharp
    OnewayEncryption oneWayEncryption = new OnewayEncryption(10000);
    ```

    > **Security Note:**
    > The default iteration count is 10 for backward compatibility. For new projects, use a higher value (e.g., 10,000 or more) for better security.

2. **Hash the data:**
    The salt is included in the output, so you do not need to store it separately.
    ```csharp
    string hashedString = oneWayEncryption.HashData("This string is not encrypted");
    ```

3. **Verify the hash:**
    Pass the original hash and the string to check:
    ```csharp
    if(oneWayEncryption.VerifyHash(hashedString, "This string is not encrypted"))
    {
        Console.WriteLine("Hash is verified which is the expected result");
    }
    ```



## Demo: Two-Way Encryption (AES)

You need a 16, 24, or 32 byte key and a 16 byte IV. Use the KeyGen class to generate these. **Generate once and store securely** (e.g., environment variables, Azure Key Vault). Do not generate a new key/IV for every operation.

> **Important:**
> Do NOT call the key/IV generator every time you encrypt or decrypt. The key and IV must remain the same for decryption to work.

1. **Generate or provide AES keys:**
    ```csharp
    string key = KeyGen.GenerateAesKey(KeySize.KeySize_256);
    string iv = KeyGen.GenerateIv();
    ```

2. **Instantiate the TwoWayEncryption class:**
    ```csharp
    TwoWayEncryption twoWayEncrypt = new TwoWayEncryption(key, iv);
    ```

3. **Encrypt data:**
    ```csharp
    string twoWayEncryptedString = twoWayEncrypt.Encrypt("HELLO WORLD");
    ```

4. **Decrypt data:**
    ```csharp
    string twoWayDecryptedString = twoWayEncrypt.Decrypt(twoWayEncryptedString);
    ```

> **Security Note:**
> Never hardcode keys/IVs in your source code. Store them securely using environment variables or a secrets manager.

## Security Best Practices

- Use a high iteration count (10,000+) for PBKDF2 hashing.
- Store encryption keys and IVs securely (never in source code).
- Rotate keys periodically and follow your organization's security policies.
- Do not use the same key/IV pair for multiple applications.
- Always validate user input and handle exceptions securely.

## Important Note
One-way and two-way encryption are not interchangeable. You cannot decrypt a hash produced by the one-way method.


## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

---

Happy Coding!
