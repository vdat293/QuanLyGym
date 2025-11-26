using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel; // Dành riêng cho WPF để binding dữ liệu
using System.IO;
using System.Linq;
using System.Xml;

namespace GymWpfApp
{
    public class Member
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public decimal Amount { get; set; }
        public string Timing { get; set; }
        // Property hiển thị đẹp trên ListBox (nếu dùng)
        public string DisplayInfo => $"{Id} - {Name} | {Timing}";
    }

    public static class Logger
    {
        private static string logFile = "GymSystem.log";
        public static void Write(string message)
        {
            try
            {
                string line = $"[{DateTime.Now:dd/MM/yyyy HH:mm:ss}] {message}";
                File.AppendAllText(logFile, line + Environment.NewLine);
            }
            catch { }
        }
    }

    public static class DataStore
    {
        // ObservableCollection giúp WPF tự nhận biết khi có thêm/xóa phần tử
        public static ObservableCollection<Member> Members = new ObservableCollection<Member>();
        private static string dataFile = "members.json";

        public static void Load()
        {
            if (File.Exists(dataFile))
            {
                try
                {
                    string json = File.ReadAllText(dataFile);
                    var list = JsonConvert.DeserializeObject<ObservableCollection<Member>>(json);
                    if (list != null)
                    {
                        Members.Clear();
                        foreach (var item in list) Members.Add(item);
                    }
                }
                catch (Exception ex) { Logger.Write("Lỗi load: " + ex.Message); }
            }
        }

        public static void Save()
        {
            try
            {
                string json = JsonConvert.SerializeObject(Members, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText(dataFile, json);
            }
            catch (Exception ex) { Logger.Write("Lỗi save: " + ex.Message); }
        }

        public static int NextId() => Members.Count == 0 ? 1 : Members.Max(m => m.Id) + 1;
    }
}