using GymWpfApp.Models;
using GymWpfApp.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace GymWpfApp.Services
{
    public static class EquipmentService
    {
        private static string dataFile = "equipment.json";

        /// <summary>
        /// ObservableCollection giúp WPF tự nhận biết khi có thêm/xóa phần tử
        /// </summary>
        public static ObservableCollection<Equipment> EquipmentList { get; private set; } = new ObservableCollection<Equipment>();

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
                    var list = JsonConvert.DeserializeObject<ObservableCollection<Equipment>>(json);
                    if (list != null)
                    {
                        EquipmentList.Clear();
                        foreach (var item in list)
                        {
                            EquipmentList.Add(item);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.Write("Lỗi load equipment: " + ex.Message);
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
                string json = JsonConvert.SerializeObject(EquipmentList, Formatting.Indented);
                File.WriteAllText(dataFile, json);
            }
            catch (Exception ex)
            {
                Logger.Write("Lỗi save equipment: " + ex.Message);
            }
        }

        /// <summary>
        /// Lấy ID tiếp theo cho equipment mới
        /// </summary>
        public static int NextId()
        {
            return EquipmentList.Count == 0 ? 1 : EquipmentList.Max(e => e.Id) + 1;
        }
    }
}
