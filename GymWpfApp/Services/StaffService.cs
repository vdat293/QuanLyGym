using GymWpfApp.Models;
using GymWpfApp.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace GymWpfApp.Services
{
    public static class StaffService
    {
        private static string dataFile = "staff.json";

        /// <summary>
        /// ObservableCollection giúp WPF tự nhận biết khi có thêm/xóa phần tử
        /// </summary>
        public static ObservableCollection<Staff> StaffList { get; private set; } = new ObservableCollection<Staff>();

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
                    var list = JsonConvert.DeserializeObject<ObservableCollection<Staff>>(json);
                    if (list != null)
                    {
                        StaffList.Clear();
                        foreach (var item in list)
                        {
                            StaffList.Add(item);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.Write("Lỗi load staff: " + ex.Message);
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
                string json = JsonConvert.SerializeObject(StaffList, Formatting.Indented);
                File.WriteAllText(dataFile, json);
            }
            catch (Exception ex)
            {
                Logger.Write("Lỗi save staff: " + ex.Message);
            }
        }

        /// <summary>
        /// Lấy ID tiếp theo cho staff mới
        /// </summary>
        public static int NextId()
        {
            return StaffList.Count == 0 ? 1 : StaffList.Max(s => s.Id) + 1;
        }
    }
}
