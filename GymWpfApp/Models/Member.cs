namespace GymWpfApp.Models
{
    public class Member
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public string Package { get; set; }
        public string Timing { get; set; }

        /// <summary>
        /// Property hiển thị đẹp trên ListBox (nếu dùng)
        /// </summary>
        public string DisplayInfo => $"{Id} - {Name} | {Timing}";
    }
}
