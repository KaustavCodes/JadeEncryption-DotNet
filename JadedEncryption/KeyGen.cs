using System.Security.Cryptography;
using System.Text;

namespace JadedEncryption;

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

    public static string GenerateAesKey(KeySize keySize)
    {
        return GenerateRandomString(Convert.ToInt32(keySize));
    }

    public static string GenerateIv()
    {
        return GenerateRandomString(Convert.ToInt32(KeySize.KeySize_128));
    }
}
