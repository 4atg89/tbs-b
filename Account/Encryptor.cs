using System.Security.Cryptography;
using System.Text;

// !!!todo move to another place!!!
namespace Account;

public class Encryptor : IEncryptor
{
    private const int SALT_SIZE = 40;
    private const int HASH_SIZE = 40;
    private const int ITERATIONS_COUNT = 100000;

    public string GetSalt()
    {
        var saltBytes = new byte[SALT_SIZE];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(saltBytes);
        return Convert.ToBase64String(saltBytes);
    }

    public string GetHash(string value, string salt)
    {
        var saltBytes = Convert.FromBase64String(salt);

        using var pbkdf2 = new Rfc2898DeriveBytes(
            password: Encoding.UTF8.GetBytes(value),
            salt: saltBytes,
            iterations: ITERATIONS_COUNT,
            hashAlgorithm: HashAlgorithmName.SHA256);

        return Convert.ToBase64String(pbkdf2.GetBytes(HASH_SIZE));
    }
}