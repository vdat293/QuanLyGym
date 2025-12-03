using GymWpfApp.Infrastructure;
using GymWpfApp.Interfaces;
using GymWpfApp.Models;
using System.Collections.ObjectModel;

namespace GymWpfApp
{
    /// <summary>
    /// Backward compatibility layer
    /// Provides static interface to instance-based services via ServiceContainer
    /// Allows existing code to work without modification
    /// </summary>
    public static class DataStore
    {
        private static IDataService<Member> MemberService =>
            ServiceContainer.Instance.Resolve<IDataService<Member>>();

        public static ObservableCollection<Member> Members => MemberService.GetAll();

        public static void Load() => MemberService.Load();

        public static void Save() => MemberService.Save();

        public static int NextId() => MemberService.NextId();
    }

    public static class StaffDataStore
    {
        private static IDataService<Staff> StaffService =>
            ServiceContainer.Instance.Resolve<IDataService<Staff>>();

        public static ObservableCollection<Staff> StaffList => StaffService.GetAll();

        public static void Load() => StaffService.Load();

        public static void Save() => StaffService.Save();

        public static int NextId() => StaffService.NextId();
    }

    public static class EquipmentDataStore
    {
        private static IDataService<Equipment> EquipmentService =>
            ServiceContainer.Instance.Resolve<IDataService<Equipment>>();

        public static ObservableCollection<Equipment> EquipmentList => EquipmentService.GetAll();

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
