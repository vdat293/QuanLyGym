using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace QuanLyPhongGym.Models
{
    public enum PtStatus
    {
        KhongCoPT = 0,  // Không có PT
        CoPT = 1,       // Có PT (PT nội bộ của phòng)
        PTNgoai = 2     // Thuê PT ngoài (không tính vào lương HLV của phòng)
    }
    public class Member : BaseEntity
    {
        [Required, MaxLength(50)]
        public string FamilyName { get; set; } = string.Empty;   // Họ

        [MaxLength(80)]
        public string? MiddleName { get; set; }                  // Tên đệm

        [Required, MaxLength(50)]
        public string GivenName { get; set; } = string.Empty;    // Tên (dùng để tìm)

        [MaxLength(20)]
        public string? Phone { get; set; }

        public DateTime? BirthDate { get; set; }

        public int? MembershipPlanId { get; set; }
        public MembershipPlan? MembershipPlan { get; set; }

        public DateTime JoinDate { get; set; } = DateTime.Today;

        [NotMapped]
        public DateTime EndDate =>
            MembershipPlan == null || MembershipPlan.DurationDays == 0
                ? JoinDate
                : JoinDate.AddDays(MembershipPlan.DurationDays);

        // Tổng ngày tập (tạm set cứng/seed)
        public int TotalTrainingDays { get; set; } = 0;

        public bool IsActive { get; set; } = true;

        // Hiển thị họ tên ghép cho DataGrid
        [NotMapped]
        public string FullNameDisplay =>
            string.Join(" ", new[] { FamilyName, MiddleName, GivenName }
                .Where(s => !string.IsNullOrWhiteSpace(s)));

        public PtStatus PtStatus { get; set; } = PtStatus.KhongCoPT;

        [NotMapped]
        public string PtStatusDisplay => PtStatus switch
        {
            PtStatus.KhongCoPT => "Không có PT",
            PtStatus.CoPT => "Có PT",
            PtStatus.PTNgoai => "PT ngoài",
            _ => "Không rõ"
        };
    }
}
