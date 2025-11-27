namespace GymWpfApp.Models
{
    public class Staff
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public string Position { get; set; } // Chức vụ: Lễ Tân, Huấn Luyện Viên, Quản Lý...
        public string Shift { get; set; } // Ca làm việc: Ca 1, Ca 2, Ca 3
    }
}
