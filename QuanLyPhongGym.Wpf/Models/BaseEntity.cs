namespace QuanLyPhongGym.Models
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public System.DateTime CreatedAt { get; set; } = System.DateTime.UtcNow;
        public System.DateTime? UpdatedAt { get; set; }
    }
}