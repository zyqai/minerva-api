using Microsoft.Extensions.Hosting.Internal;
using Microsoft.AspNetCore.Hosting;
using System.Text;
using System.Security.Cryptography;
namespace MinervaApi.ExternalApi
{
    public class Comman
    {
        public static void logEvent(string MethodName, string values)
        {
            try
            {
                string logsDirectory = "logs/Req";
                string Directorypath = Path.Combine(Directory.GetCurrentDirectory(), logsDirectory);
                string Filepath = Directorypath + "\\" + MethodName + "\\" + DateTime.Now.ToString("ddMMMyyy") + ".txt";
                if (!Directory.Exists(Directorypath))
                {
                    Directory.CreateDirectory(Directorypath);
                }
                if (!Directory.Exists(Directorypath + "\\" + MethodName))
                {
                    Directory.CreateDirectory(Directorypath + "\\" + MethodName);
                }
                if (!File.Exists(Filepath))
                {
                    File.Create(Filepath).Close();
                }
                StreamWriter sw = new StreamWriter(Filepath, true);
                sw.WriteLine(DateTime.Now.ToString("dd/MM/yyyy  h:mm:ss tt") + ":" + values);
                sw.Flush();
                sw.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static void logError(string MethodName, string values)
        {
            try
            {
                string logsDirectory = "~/logs/Error";
                string Directorypath = Path.Combine(Directory.GetCurrentDirectory(), logsDirectory);
                string Filepath = Directorypath + "\\" + MethodName + "\\" + DateTime.Now.ToString("ddMMMyyy") + ".txt";
                if (!Directory.Exists(Directorypath))
                {
                    Directory.CreateDirectory(Directorypath);
                }
                if (!Directory.Exists(Directorypath + "\\" + MethodName))
                {
                    Directory.CreateDirectory(Directorypath + "\\" + MethodName);
                }
                if (!File.Exists(Filepath))
                {
                    File.Create(Filepath).Close();
                }
                StreamWriter sw = new StreamWriter(Filepath, true);
                sw.WriteLine(DateTime.Now.ToString("dd/MM/yyyy  h:mm:ss tt") + ":" + values);
                sw.Flush();
                sw.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static void logRes(string MethodName, string values)
        {
            try
            {
                string logsDirectory = "~/logs/Res";
                string Directorypath = Path.Combine(Directory.GetCurrentDirectory(), logsDirectory);
                string Filepath = Directorypath + "\\" + MethodName + "\\" + DateTime.Now.ToString("ddMMMyyy") + ".txt";
                if (!Directory.Exists(Directorypath))
                {
                    Directory.CreateDirectory(Directorypath);
                }
                if (!Directory.Exists(Directorypath + "\\" + MethodName))
                {
                    Directory.CreateDirectory(Directorypath + "\\" + MethodName);
                }
                if (!File.Exists(Filepath))
                {
                    File.Create(Filepath).Close();
                }
                StreamWriter sw = new StreamWriter(Filepath, true);
                sw.WriteLine(DateTime.Now.ToString("dd/MM/yyyy  h:mm:ss tt") + ":" + values);
                sw.Flush();
                sw.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static string GenerateRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            var result = new StringBuilder(length);
            for (int i = 0; i < length; i++)
            {
                result.Append(chars[random.Next(chars.Length)]);
            }
            return result.ToString();
        }
        public static string EncryptDatastring(string data)
        {
            byte[] dataBytes = Encoding.UTF8.GetBytes(data);

            using (RSA rsa = RSA.Create())
            {
                RSAParameters rsaPublicKey= rsa.ExportParameters(false);
                rsa.ImportParameters(rsaPublicKey);
                byte[] encryptedData = rsa.Encrypt(dataBytes, RSAEncryptionPadding.OaepSHA256);
                return Convert.ToBase64String(encryptedData);
            }
        }

       

        //public static byte[] DecryptData(byte[] encryptedData, RSAParameters rsaPrivateKey)
        //{
        //    using (RSA rsa = RSA.Create())
        //    {
        //        rsa.ImportParameters(rsaPrivateKey);
        //        return rsa.Decrypt(encryptedData, RSAEncryptionPadding.OaepSHA256);
        //    }
        //}
        public static string DecryptDatastring(string encryptedText)
        {
            byte[] encryptedData = Convert.FromBase64String(encryptedText);

            using (RSA rsa = RSA.Create())
            {
                RSAParameters rsaPrivateKey = rsa.ExportParameters(true);
                rsa.ImportParameters(rsaPrivateKey);
                byte[] decryptedData = rsa.Decrypt(encryptedData, RSAEncryptionPadding.OaepSHA256);
                return Encoding.UTF8.GetString(decryptedData);
            }
        }
        //public static byte[] EncryptDatastring(string data)
        //{

        //        byte[] dataBytes = Encoding.UTF8.GetBytes(data);

        //    using (RSA rsa = RSA.Create())
        //    {
        //        RSAParameters rsaPublicKey = rsa.ExportParameters(false);
        //        rsa.ImportParameters(rsaPublicKey);
        //        return rsa.Encrypt(dataBytes, RSAEncryptionPadding.OaepSHA256);
        //    }
        //}


        public static string EncryptDatastringNew(string data, RSAParameters rsaPublicKey)
        {
            using (var csp = new RSACryptoServiceProvider())
            {
                csp.ImportParameters(rsaPublicKey);

                // Convert plaintext data to bytes
                byte[] bytesPlainTextData = Encoding.Unicode.GetBytes(data);

                // Encrypt bytes
                byte[] bytesCypherText = csp.Encrypt(bytesPlainTextData, false);

                // Convert encrypted bytes to Base64 string
                return Convert.ToBase64String(bytesCypherText);
            }
        }
        public static string DecryptDatastringNew(string cypherText, RSAParameters rsaPrivateKey)
        {
            using (var csp = new RSACryptoServiceProvider())
            {
                csp.ImportParameters(rsaPrivateKey);

                // Convert Base64 string to encrypted bytes
                byte[] bytesCypherText = Convert.FromBase64String(cypherText);

                // Decrypt bytes
                byte[] bytesPlainTextData = csp.Decrypt(bytesCypherText, false);

                // Convert decrypted bytes to plaintext string
                return Encoding.Unicode.GetString(bytesPlainTextData);
            }
        }

    }
    public class APIStatus
    {
        public string? Code { get; set; }    
        public string? Message { get; set; }
    }
}
