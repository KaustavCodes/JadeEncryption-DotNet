using System.Security.Cryptography;

namespace JadeEncryption;

public class OnewayEncryption
{
    private const int SaltSize = 16; // 128 bit 
    private const int KeySize = 32; // 256 bit
    private int Iterations = 10; // Number of iterations for PBKDF2

    public OnewayEncryption()
    {
        Iterations = 10;
    }

    public OnewayEncryption(int iterations)
    {
        if(iterations <= 1)
        {
            throw new ArgumentException("Iterations must be greater than 1");
        }
        Iterations = iterations;
    }

    /// <summary>
    /// Hashes the data using PBKDF2 with HMAC-SHA256
    /// To use just pass a string that you wish to hash
    /// </summary>
    /// <param name="dataToHash"></param>
    /// <returns></returns>
    public string HashData(string dataToHash)
    {
        using (var algorithm = new Rfc2898DeriveBytes(dataToHash, SaltSize, Iterations, HashAlgorithmName.SHA256))
        {
            var key = Convert.ToBase64String(algorithm.GetBytes(KeySize));
            var salt = Convert.ToBase64String(algorithm.Salt);

            return $"{Iterations}.{salt}.{key}";
        }
    }

    /// <summary>
    /// Verifies the hash with the unhashed string. 
    /// This is used to verify the hash with the original string. 
    /// If it matches then the hash is valid.
    /// </summary>
    /// <param name="hash">The hashed string</param>
    /// <param name="unhashedString">The unhashed string to verify match</param>
    /// <returns></returns>
    /// <exception cref="FormatException"></exception>
    public bool VerifyHash(string hash, string unhashedString)
    {
        var parts = hash.Split('.', StringSplitOptions.RemoveEmptyEntries);

        if (parts.Length != 3)
        {
            throw new FormatException("Unexpected hash format. Should be formatted as `{iterations}.{salt}.{hash}`");
        }

        var iterations = Convert.ToInt32(parts[0]);
        var salt = Convert.FromBase64String(parts[1]);
        var key = Convert.FromBase64String(parts[2]);

        using (var algorithm = new Rfc2898DeriveBytes(unhashedString, salt, iterations, HashAlgorithmName.SHA256))
        {
            var keyToCheck = algorithm.GetBytes(KeySize);

            return keyToCheck.SequenceEqual(key);
        }
    }
}
