using GymWpfApp.Models;
using GymWpfApp.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace GymWpfApp.Services
{
    public static class MemberService
    {
        private static string dataFile = "members.json";

        /// <summary>
        /// ObservableCollection giúp WPF tự nhận biết khi có thêm/xóa phần tử
        /// </summary>
        public static ObservableCollection<Member> Members { get; private set; } = new ObservableCollection<Member>();

        /// <summary>
        /// Load dữ liệu từ file JSON
        /// </summary>
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
                        foreach (var item in list)
                        {
                            Members.Add(item);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.Write("Lỗi load members: " + ex.Message);
                }
            }
        }

        /// <summary>
        /// Lưu dữ liệu vào file JSON
        /// </summary>
        public static void Save()
        {
            try
            {
                string json = JsonConvert.SerializeObject(Members, Formatting.Indented);
                File.WriteAllText(dataFile, json);
            }
            catch (Exception ex)
            {
                Logger.Write("Lỗi save members: " + ex.Message);
            }
        }

        /// <summary>
        /// Lấy ID tiếp theo cho member mới
        /// </summary>
        public static int NextId()
        {
            return Members.Count == 0 ? 1 : Members.Max(m => m.Id) + 1;
        }
    }
}
