namespace GymWpfApp.Models
{
    /// <summary>
    /// Represents gym equipment
    /// Inherits common entity properties from BaseEntity
    /// </summary>
    public class Equipment : BaseEntity
    {
        public string Code { get; set; }     // Mã thiết bị
        public string Name { get; set; }     // Tên thiết bị
        public string Category { get; set; } // Loại: Máy chạy bộ, Tạ đơn, Ghế tập...
        public string Location { get; set; } // Vị trí: Cơ sở 1, Cơ sở 2...
        public string Status { get; set; }   // Trạng thái: Tốt, Hư hại, Đang bảo dưỡng, Ngừng sử dụng
        public string Notes { get; set; }    // Ghi chú

        /// <summary>
        /// Display name for equipment
        /// </summary>
        public string DisplayName => $"{Code} - {Name}";
    }
}
