using System.Security.Cryptography;
using System.Text;

namespace MinervaApi.ExternalApi
{
    public class RSACrypto
    {
        public static byte[] EncryptData(string data, RSAParameters rsaPublicKey)
        {
            byte[] dataBytes = Encoding.UTF8.GetBytes(data);

            using (RSA rsa = RSA.Create())
            {
                rsa.ImportParameters(rsaPublicKey);
                return rsa.Encrypt(dataBytes, RSAEncryptionPadding.OaepSHA256);
            }
        }

        public static byte[] DecryptData(byte[] encryptedData, RSAParameters rsaPrivateKey)
        {
            using (RSA rsa = RSA.Create())
            {
                rsa.ImportParameters(rsaPrivateKey);
                return rsa.Decrypt(encryptedData, RSAEncryptionPadding.OaepSHA256);
            }
        }
    }
}
