using System.Security.Cryptography;
namespace PasswordManagerApi.Services
{
    public class EncryptionService : IEncryptionService
    {
        private byte[] _key;
        private byte[] _iv;
        public EncryptionService(IConfiguration configuration) 
        {
            var config = configuration.GetSection("EncryptionSettings");
            _key = Convert.FromBase64String(config["Key"]);
            _iv = Convert.FromBase64String(config["IV"]);
        }

        public string Encrypt(string plainText)
        {
            using (var aes = Aes.Create())
            {
                aes.Key = _key;
                aes.IV = _iv;
                var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        using (var writer = new StreamWriter(cs))
                        {
                            writer.Write(plainText);
                        }
                    }
                    return Convert.ToBase64String(ms.ToArray());
                }
            }
        }

        public string Decrypt(string encryptedText)
        {
            using (var aes = Aes.Create()) 
            {
                aes.Key = _key;
                aes.IV = _iv;
                var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                using (var ms = new MemoryStream(Convert.FromBase64String(encryptedText)))
                {
                    using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        using (var reader = new StreamReader(cs))
                        {
                            return reader.ReadToEnd();
                        }
                    }
                }
            }
        }
    }
}
