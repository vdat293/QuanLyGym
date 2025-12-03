namespace GymWpfApp.Models
{
    /// <summary>
    /// Represents a gym member
    /// Inherits common person properties from Person base class
    /// </summary>
    public class Member : Person
    {
        public string Package { get; set; } // Gói tập: Cơ bản, Nâng cao, Cao cấp
        public string Timing { get; set; }  // Thời hạn: 1 tháng, 3 tháng, 6 tháng, 12 tháng

        /// <summary>
        /// Property hiển thị đẹp trên ListBox (nếu dùng)
        /// </summary>
        public string DisplayInfo => $"{Id} - {Name} | {Timing}";

        public override string DisplayName => $"{Name} ({Package})";
    }
}
