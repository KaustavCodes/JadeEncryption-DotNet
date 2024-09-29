using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace JadeEncryption;

public class TwoWayEncryption
{
    private readonly byte[] _key;
    private readonly byte[] _iv;

    /// <summary>
    /// Constructor that takes in IConfiguration to get the key and iv from the appsettings.json
    /// </summary>
    /// <param name="configuration"></param>
    public TwoWayEncryption(IConfiguration configuration)
    {
        _key = Encoding.UTF8.GetBytes(configuration["DataEncryption:Key"]);
        _iv = Encoding.UTF8.GetBytes(configuration["DataEncryption:IV"]);
    }

    /// <summary>
    /// Constructor that takes in a key and iv used for the encryption
    /// </summary>
    /// <param name="key"></param>
    /// <param name="iv"></param>
    public TwoWayEncryption(string key, string iv)
    {
        _key = Encoding.UTF8.GetBytes(key);
        _iv = Encoding.UTF8.GetBytes(iv);
    }

    /// <summary>
    /// Encrypts the plain text using the key and iv provided in the constructor
    /// </summary>
    /// <param name="plainText"></param>
    /// <returns></returns>
    public string Encrypt(string plainText)
    {
        using (var aes = Aes.Create())
        {
            aes.Key = _key;
            aes.IV = _iv;

            var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

            using (var msEncrypt = new MemoryStream())
            {
                using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                using (var swEncrypt = new StreamWriter(csEncrypt))
                {
                    swEncrypt.Write(plainText);
                }

                return Convert.ToBase64String(msEncrypt.ToArray());
            }
        }
    }

    /// <summary>
    /// Decrypts the cipher text using the key and iv provided in the constructor
    /// </summary>
    /// <param name="cipherText"></param>
    /// <returns></returns>
    public string Decrypt(string cipherText)
    {
        using (var aes = Aes.Create())
        {
            aes.Key = _key;
            aes.IV = _iv;

            var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

            using (var msDecrypt = new MemoryStream(Convert.FromBase64String(cipherText)))
            using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
            using (var srDecrypt = new StreamReader(csDecrypt))
            {
                return srDecrypt.ReadToEnd();
            }
        }
    }
}