using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyPhongGym.Models
{
    public enum PlanTier { Dong, Bac, Vang, KimCuong }

    public class MembershipPlan : BaseEntity
    {
        [Required, MaxLength(80)]
        public string Name { get; set; } = string.Empty;

        public decimal Price { get; set; }

        // MỚI
        public PlanTier Tier { get; set; }          // Đồng/Bạc/Vàng/Kim cương
        public int Months { get; set; }             // 1,3,6,12 (để tính EndDate)

        public int DurationDays { get; set; }       // vẫn dùng cho EndDate (Months*30)
        public string? Description { get; set; }

        public ICollection<Member> Members { get; set; } = new List<Member>();

        [NotMapped]
        public string TierDisplay => Tier switch
        {
            PlanTier.Dong => "Hội viên Đồng",
            PlanTier.Bac => "Hội viên Bạc",
            PlanTier.Vang => "Hội viên Vàng",
            PlanTier.KimCuong => "Hội viên Kim cương",
            _ => "Hội viên"
        };
    }
}
