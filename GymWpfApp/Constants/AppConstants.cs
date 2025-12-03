namespace GymWpfApp.Constants
{
    /// <summary>
    /// Application-wide constants
    /// Centralizes all magic strings and values
    /// </summary>
    public static class AppConstants
    {
        // Authentication
        public static class Auth
        {
            public const string DefaultUsername = "admin";
            public const string DefaultPassword = "123456";
        }

        // Messages
        public static class Messages
        {
            // Error messages
            public const string ErrorTitle = "Lỗi";
            public const string ErrorMissingInfo = "Vui lòng nhập đủ thông tin!";
            public const string ErrorInvalidAge = "Tuổi phải từ 1 đến 120!";
            public const string ErrorInvalidPhone = "Số điện thoại không hợp lệ! (10-11 số)";
            public const string ErrorLoginFailed = "Sai tên đăng nhập hoặc mật khẩu!";
            public const string ErrorLoadData = "Không thể tải dữ liệu!";

            // Success messages
            public const string SuccessTitle = "Thành công";
            public const string SuccessAdded = "Đã thêm thành công!";
            public const string SuccessUpdated = "Đã cập nhật thành công!";
            public const string SuccessDeleted = "Đã xóa thành công!";

            // Info messages
            public const string InfoTitle = "Thông báo";
            public const string InfoNoSelection = "Vui lòng chọn một mục!";
            public const string InfoForgotPassword = "Vui lòng liên hệ quản trị viên để lấy lại mật khẩu!";

            // Confirmation messages
            public const string ConfirmTitle = "Xác nhận";
            public const string ConfirmDelete = "Bạn có chắc muốn xóa mục này không?";
        }

        // Member Package Types
        public static class MemberPackages
        {
            public const string Basic = "Cơ bản";
            public const string Advanced = "Nâng cao";
            public const string Premium = "Cao cấp";
        }

        // Member Timing Options
        public static class MemberTimings
        {
            public const string OneMonth = "1 tháng";
            public const string ThreeMonths = "3 tháng";
            public const string SixMonths = "6 tháng";
            public const string TwelveMonths = "12 tháng";
        }

        // Staff Positions
        public static class StaffPositions
        {
            public const string Receptionist = "Lễ Tân";
            public const string Trainer = "Huấn Luyện Viên";
            public const string Manager = "Quản Lý";
        }

        // Staff Shifts
        public static class StaffShifts
        {
            public const string Shift1 = "Ca 1";
            public const string Shift2 = "Ca 2";
            public const string Shift3 = "Ca 3";
        }

        // Equipment Categories
        public static class EquipmentCategories
        {
            public const string Treadmill = "Máy chạy bộ";
            public const string Dumbbell = "Tạ đơn";
            public const string Bench = "Ghế tập";
            public const string BikeExercise = "Xe đạp tập";
        }

        // Equipment Locations
        public static class EquipmentLocations
        {
            public const string Location1 = "Cơ sở 1";
            public const string Location2 = "Cơ sở 2";
        }

        // Equipment Status
        public static class EquipmentStatuses
        {
            public const string Good = "Tốt";
            public const string Damaged = "Hư hại";
            public const string Maintenance = "Đang bảo dưỡng";
            public const string Retired = "Ngừng sử dụng";
        }

        // Gender Options
        public static class Genders
        {
            public const string Male = "Nam";
            public const string Female = "Nữ";
            public const string Other = "Khác";
        }

        // Validation Constraints
        public static class Validation
        {
            public const int MinAge = 1;
            public const int MaxAge = 120;
            public const int MinPhoneLength = 10;
            public const int MaxPhoneLength = 11;
        }
    }
}
