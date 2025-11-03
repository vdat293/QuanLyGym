using System;

namespace QuanLyPhongGym.Models
{
    public class Attendance : BaseEntity
    {
        public int MemberId { get; set; }
        public Member? Member { get; set; }
        public DateTime CheckIn { get; set; } = DateTime.Now;
    }
}