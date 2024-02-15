using Microsoft.Extensions.Hosting.Internal;
using Microsoft.AspNetCore.Hosting;
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
    }
    public class APIStatus
    {
        public string? Code { get; set; }    
        public string? Message { get; set; }
    }
}
