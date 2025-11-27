using System;
using System.IO;

namespace GymWpfApp.Utils
{
    public static class Logger
    {
        private static string logFile = "GymSystem.log";

        /// <summary>
        /// Ghi log vào file
        /// </summary>
        /// <param name="message">Nội dung log</param>
        public static void Write(string message)
        {
            try
            {
                string line = $"[{DateTime.Now:dd/MM/yyyy HH:mm:ss}] {message}";
                File.AppendAllText(logFile, line + Environment.NewLine);
            }
            catch
            {
                // Không làm gì nếu ghi log thất bại
            }
        }
    }
}
