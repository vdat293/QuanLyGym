using System;

namespace QuanLyPhongGym.Models
{
    public class WorkoutSchedule : BaseEntity
    {
        // FK tới Member (bắt buộc)
        public int MemberId { get; set; }
        public Member? Member { get; set; }

        // FK tới Trainer (có thể null nếu không có PT)
        public int? TrainerId { get; set; }
        public Trainer? Trainer { get; set; }

        public string? Title { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
