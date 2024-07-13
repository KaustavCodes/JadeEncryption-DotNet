# JadedEncryption: .NET 8 Encryption Library

[![.NET 8](https://img.shields.io/badge/.NET-8-blue.svg)]([https://aka.ms/new-console-template](https://aka.ms/new-console-template))

JadedEncryption provides easy-to-use methods for both one-way (hashing) and two-way (reversible) encryption within your .NET 8 projects.

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
   dotnet add package JadedEncryption

2. **Referenc the Package:**
    Add a using statement in your package
    ```bash
    using JadedEncryption;

## Demo for One way Encryption ideally for passwords
1. **Create an instance of OnewayEncryption class:**
    ```bash
    OnewayEncryption oneWayEncryption = new OnewayEncryption();

2. **Encrypt the data:**
    The application will include the salt so no need to separately store the salt.
    ```bash
    string encrypedString = oneWayEncryption.HashData("This string is not encrypted");

3. **Verify the hash against a probably match. Ideally passwords**
    Here you pass in the original hashed string as the first argument and the second parameter is the string that should match.
    ```bash
    if(oneWayEncryption.VerifyHash(encrypedString, "Not encryped string")) {
        Console.WriteLine("Hash is verified which is the expected result");
    }



