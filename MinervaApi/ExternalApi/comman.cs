using Microsoft.Extensions.Hosting.Internal;
using Microsoft.AspNetCore.Hosting;
using System.Text;
using System.Security.Cryptography;
using SendGrid.Helpers.Mail;
using SendGrid;
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



        public async static Task SendEmail(string uploadLink, string ToEmail,string Name,string ccEmailAddress)
        {
            var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("noreply@zyq.ai", "Upload your document");
            var subject = "Upload Your Documents";
            var to = new EmailAddress(ToEmail, "Upload Docs");
            var plainTextContent = "Dear "+ Name;
            var htmlContent = @"
                <!DOCTYPE html>
<html lang=""en"">
<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>Upload Documents</title>
    <style>
        .button {
            display: inline-block;
            padding: 10px 20px;
            background-color: #04AA6D;
            color: white;
            text-decoration: none;
            border-radius: 5px;
            cursor: pointer;
        }
        .button-text {
            color: #000; /* Black color */
        }
    </style>
</head>
<body>
    <H2>Dear Borrower
    <h2>Upload Your Documents</h2>
    <p>Please click on the following link to upload your documents:</p>
    <p><a href=""https://dev.minerva.zyq.ai/upload/~link~"" class=""button"">Upload Documents</a></p>
</body>
</html>
            ";
            htmlContent = htmlContent.Replace("~link~", uploadLink);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

            if (!string.IsNullOrEmpty(ccEmailAddress))
            {
                msg.AddCc(new EmailAddress(ccEmailAddress));
            }
            var response = await client.SendEmailAsync(msg);
        }

    }
    public class APIStatus
    {
        public string? Code { get; set; }    
        public string? Message { get; set; }
    }
}
