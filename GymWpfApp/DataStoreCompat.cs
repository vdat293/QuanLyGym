using GymWpfApp.Models;
using GymWpfApp.Services;
using GymWpfApp.Utils;
using System.Collections.ObjectModel;

namespace GymWpfApp
{
    /// <summary>
    /// File tương thích ngược để giữ namespace cũ hoạt động
    /// Sử dụng alias để trỏ đến các service mới
    /// </summary>
    public static class DataStore
    {
        public static ObservableCollection<Member> Members => MemberService.Members;

        public static void Load() => MemberService.Load();

        public static void Save() => MemberService.Save();

        public static int NextId() => MemberService.NextId();
    }

    public static class StaffDataStore
    {
        public static ObservableCollection<Staff> StaffList => StaffService.StaffList;

        public static void Load() => StaffService.Load();

        public static void Save() => StaffService.Save();

        public static int NextId() => StaffService.NextId();
    }

    public static class EquipmentDataStore
    {
        public static ObservableCollection<Equipment> EquipmentList => EquipmentService.EquipmentList;

        public static void Load() => EquipmentService.Load();

        public static void Save() => EquipmentService.Save();

        public static int NextId() => EquipmentService.NextId();
    }

    // Alias cho các Model để giữ tương thích
    public class Member : Models.Member { }
    public class Staff : Models.Staff { }
    public class Equipment : Models.Equipment { }

    // Alias cho Logger
    public static class Logger
    {
        public static void Write(string message) => Utils.Logger.Write(message);
    }
}
