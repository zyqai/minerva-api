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
        public static string key = "minerva";
        public static string EncryptString(string plainText)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = DeriveKey(key, aes.KeySize / 8);
                aes.IV = new byte[16]; // IV is 16 bytes for AES

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        byte[] dataBytes = Encoding.UTF8.GetBytes(plainText);
                        cs.Write(dataBytes, 0, dataBytes.Length);
                    }

                    byte[] encryptedBytes = ms.ToArray();
                    return Convert.ToBase64String(encryptedBytes);
                }
            }
        }

        public static string DecryptString(string encryptedText)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = DeriveKey(key, aes.KeySize / 8);
                aes.IV = new byte[16]; // IV is 16 bytes for AES

                using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(encryptedText)))
                {
                    using (CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Read))
                    {
                        using (StreamReader reader = new StreamReader(cs))
                        {
                            return reader.ReadToEnd();
                        }
                    }
                }
            }
        }

        public static byte[] DeriveKey(string key, int keySize)
        {
            using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(key)))
            {
                return hmac.ComputeHash(Encoding.UTF8.GetBytes(key));
            }
        }

    }
    public class APIStatus
    {
        public string? Code { get; set; }    
        public string? Message { get; set; }
    }
}
