# JadedEncryption: .NET 8 Encryption Library

[![.NET 8](https://img.shields.io/badge/.NET-8-blue.svg)]([https://aka.ms/new-console-template](https://aka.ms/new-console-template))
[![Nuget](https://img.shields.io/nuget/v/BytesAssetManagement.svg)](https://www.nuget.org/packages/JadedEncryption) 
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

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
    ```

    Or you can also create an instance by passing a number of iterations to the constructor. Higher the number of iterations more secure but will also require more system resource to process.
    ```bash
    OnewayEncryption oneWayEncryption = new OnewayEncryption(5);
    ```

    Alternatively you can also save your Key and IV in the appsettings.json file if you are working on a .net web application as below
   ```bash
    {
      "DataEncryption": {
        "Key": "YOUR KEY",
        "IV": "YOUR IV"
      }
    }
   

3. **Encrypt the data:**
    The application will include the salt so no need to separately store the salt.
    ```bash
    string encrypedString = oneWayEncryption.HashData("This string is not encrypted");

4. **Verify the hash against a probably match. Ideally passwords**
    Here you pass in the original hashed string as the first argument and the second parameter is the string that should match.
    ```bash
    if(oneWayEncryption.VerifyHash(encrypedString, "Not encryped string")) 
    {
        Console.WriteLine("Hash is verified which is the expected result");
    }


## Demo for Two way Encryption ideally for storing data encryped at rest and then decrypting it to use in your application.

So this will require a 16 or 24 or 32 bit key and a 16 bit IV. You can use the Key Gen class to generate these. Note that everytime the Key Gen may generate new keys so generate once and save in your application. And in future encrypt decrypt requests use the same key. You can also bring in your own keys but make sure they follow the AES Encryption rules.

!Important: Don't directly pass the generate key function in the TwoWayEncryption constructor. If done, the encryptions will not match as the encryption and decription keys need to be same.

1. **Generate Keys or Bring your own AES Keys**
    ```bash
    string key = KeyGen.GenerateAesKey(KeySize.KeySize_256);
    string iv = KeyGen.GenerateIv();


2. **Instanciate the TwoWayEncryption class:**
    ```bash
    TwoWayEncryption twoWayEncrypt = new TwoWayEncryption(key, iv);

3. **Encrypt the data by calling the Encrypt() Method:**
    ```bash
    string twoWayEncryptedString = twoWayEncrypt.Encrypt("HELLO WORLD");

4. **Decrypt the data**
    ```bash
    string twoWayDecryptedString = twoWayEncrypt.Decrypt([YOUR TWO WAY ENCRYPTED STRING]]);



## Important Note
Both encryptions use different methods. The hashing method's encription cannot be passed to the two way decryption to be decrypted. 


## Conclusion
Happy Coding!
