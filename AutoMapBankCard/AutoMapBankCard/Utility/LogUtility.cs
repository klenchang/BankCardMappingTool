using System;
using System.IO;
using System.Web;

namespace AutoMapBankCard.Utility
{
    public class LogUtility
    {
        public static void WriteLog(string data, string action, string provider)
        {
            try
            {
                string folder = $"{provider}_{action}_{DateTime.Now.ToString("yyyyMMdd")}";
                string fileName = $"{action}_{DateTime.Now.ToString("yyyyMMddHH")}.txt";

                string folderPath = HttpContext.Current.Server.MapPath($"~/logs/{folder}");
                var filePath = HttpContext.Current.Server.MapPath($"~/logs/{folder}/{fileName}");
                if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);
                if (!File.Exists(filePath)) File.Create(filePath).Close();

                using (var sw = new StreamWriter(filePath, true))
                {
                    sw.BaseStream.Seek(0, SeekOrigin.End);
                    sw.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}:{Environment.NewLine}{data}{Environment.NewLine}");
                    sw.Flush();
                }
            }
            catch (Exception ex)
            {
                //Ignore the exception while writing a log
            }
        }
    }
}