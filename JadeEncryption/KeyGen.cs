using System.Security.Cryptography;
using System.Text;

namespace JadeEncryption;

public enum KeySize
{
    KeySize_128 = 16,
    KeySize_198 = 24,
    KeySize_256 = 32
}

public static class KeyGen
{
    private static readonly char[] chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*()-_=+<>?".ToCharArray();

    private static string GenerateRandomString(int length)
    {
        byte[] data = new byte[4 * length];
        using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider())
        {
            crypto.GetBytes(data);
        }

        StringBuilder result = new StringBuilder(length);
        for (int i = 0; i < length; i++)
        {
            uint rnd = BitConverter.ToUInt32(data, i * 4);
            long idx = rnd % chars.Length;
            result.Append(chars[idx]);
        }

        return result.ToString();
    }

    /// <summary>
    /// Generates a random key for AES encryption. Be sure to save the key somewhere safe.
    /// This is only used to generate a key for the AES encryption. 
    /// Everytime a new key is generated so don't directly use with encryption functions.
    /// </summary>
    /// <param name="keySize"></param>
    /// <returns></returns>
    public static string GenerateAesKey(KeySize keySize)
    {
        return GenerateRandomString(Convert.ToInt32(keySize));
    }

    /// <summary>
    /// Generates a random IV for AES encryption. Be sure to save the IV somewhere safe.
    /// This is only used to generate a key for the AES encryption.
    /// Everytime a new key is generated so don't directly use with encryption functions.
    /// </summary>
    /// <returns></returns>
    public static string GenerateIv()
    {
        return GenerateRandomString(Convert.ToInt32(KeySize.KeySize_128));
    }
}
