using System.Security.Cryptography;

namespace AES256_DotnetExample
{
    public static class InfoSec
    {
        public static string GenerateKey()
        {
            string keyBase64 = string.Empty;

            using (Aes aes = Aes.Create())
            {
                aes.KeySize = 256;
                aes.GenerateKey();

                keyBase64 = Convert.ToBase64String(aes.Key);
            }

            return keyBase64;
        }

        public static string Encrypt(string PlainText, string key, out string IVKey)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Padding = PaddingMode.Zeros;
                aes.Key = Convert.FromBase64String(key);
                aes.GenerateIV();

                IVKey = Convert.ToBase64String(aes.IV);

                ICryptoTransform encryptor = aes.CreateEncryptor();

                byte[] encryptedData;

                using MemoryStream ms = new MemoryStream();
                using CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write);
                
                using (StreamWriter sw = new StreamWriter(cs))
                {
                    sw.Write(PlainText);
                }

                encryptedData = ms.ToArray();

                return Convert.ToBase64String(encryptedData);
            }
        }

        public static string Decrypt(string cipherText, string key, string IVKey)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Padding = PaddingMode.Zeros;
                aes.Key = Convert.FromBase64String(key);
                aes.IV = Convert.FromBase64String(IVKey);

                IVKey = Convert.ToBase64String(aes.IV);

                ICryptoTransform decryptor = aes.CreateDecryptor();

                string plainText = string.Empty;
                byte[] cipher = Convert.FromBase64String(cipherText);

                using MemoryStream ms = new MemoryStream(cipher);
                using CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);

                using (StreamReader sw = new StreamReader(cs))
                {
                    plainText = sw.ReadToEnd();
                }

                return plainText;
            }
        }
    }
}
