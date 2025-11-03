using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace QuanLyPhongGym.Models
{
    public class Trainer : BaseEntity
    {
        [Required, MaxLength(100)]
        public string FullName { get; set; } = string.Empty;
        [MaxLength(20)] public string? Phone { get; set; }
        public string? Specialty { get; set; }

        // navigation để include
        public ICollection<WorkoutSchedule> WorkoutSchedules { get; set; } = new List<WorkoutSchedule>();


        // ✅ CHỈ đếm hội viên đang hoạt động + Có PT (PT nhà)
        [NotMapped]
        public int TraineeCount => WorkoutSchedules?
            .Where(ws => ws.Member != null
                         && ws.Member.IsActive
                         && ws.Member.PtStatus == PtStatus.CoPT)
            .Select(ws => ws.MemberId)
            .Distinct()
            .Count() ?? 0;
    }
}
