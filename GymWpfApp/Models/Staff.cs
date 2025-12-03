namespace GymWpfApp.Models
{
    /// <summary>
    /// Represents a staff member/employee
    /// Inherits common person properties from Person base class
    /// </summary>
    public class Staff : Person
    {
        public string Position { get; set; } // Chức vụ: Lễ Tân, Huấn Luyện Viên, Quản Lý...
        public string Shift { get; set; }    // Ca làm việc: Ca 1, Ca 2, Ca 3

        public override string DisplayName => $"{Name} - {Position}";
    }
}
