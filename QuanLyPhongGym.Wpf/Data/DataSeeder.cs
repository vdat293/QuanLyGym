using System;
using System.Linq;
using QuanLyPhongGym.Models;

namespace QuanLyPhongGym.Data
{
    public static class DataSeeder
    {
        public static void SeedIfEmpty(GymDbContext db)
        {
            // Seed Plans khi trống (rút gọn ví dụ)
            if (!db.MembershipPlans.Any())
            {
                // ⬇⬇⬇ dán đoạn dưới vào đây
                db.MembershipPlans.AddRange(new[]
                {
        // 🥉 Gói hội viên Đồng
        new MembershipPlan { Name = "Gói hội viên Đồng - 1 tháng",  Tier = PlanTier.Dong, Months = 1,  Price = 200000,  DurationDays = 30 },
        new MembershipPlan { Name = "Gói hội viên Đồng - 3 tháng",  Tier = PlanTier.Dong, Months = 3,  Price = 550000,  DurationDays = 90 },
        new MembershipPlan { Name = "Gói hội viên Đồng - 6 tháng",  Tier = PlanTier.Dong, Months = 6,  Price = 1000000, DurationDays = 180 },
        new MembershipPlan { Name = "Gói hội viên Đồng - 12 tháng", Tier = PlanTier.Dong, Months = 12, Price = 1800000, DurationDays = 360 },

        // 🥈 Gói hội viên Bạc
        new MembershipPlan { Name = "Gói hội viên Bạc - 1 tháng",   Tier = PlanTier.Bac, Months = 1,  Price = 300000,  DurationDays = 30 },
        new MembershipPlan { Name = "Gói hội viên Bạc - 3 tháng",   Tier = PlanTier.Bac, Months = 3,  Price = 800000,  DurationDays = 90 },
        new MembershipPlan { Name = "Gói hội viên Bạc - 6 tháng",   Tier = PlanTier.Bac, Months = 6,  Price = 1500000, DurationDays = 180 },
        new MembershipPlan { Name = "Gói hội viên Bạc - 12 tháng",  Tier = PlanTier.Bac, Months = 12, Price = 2700000, DurationDays = 360 },

        // 🥇 Gói hội viên Vàng
        new MembershipPlan { Name = "Gói hội viên Vàng - 6 tháng",  Tier = PlanTier.Vang, Months = 6,  Price = 2500000, DurationDays = 180 },
        new MembershipPlan { Name = "Gói hội viên Vàng - 12 tháng", Tier = PlanTier.Vang, Months = 12, Price = 4600000, DurationDays = 360 },

        // 💎 Gói hội viên Kim cương
        new MembershipPlan { Name = "Gói hội viên Kim cương - 6 tháng",  Tier = PlanTier.KimCuong, Months = 6,  Price = 3500000, DurationDays = 180 },
        new MembershipPlan { Name = "Gói hội viên Kim cương - 12 tháng", Tier = PlanTier.KimCuong, Months = 12, Price = 6500000, DurationDays = 360 },
    });

                db.SaveChanges();
            }


            if (!db.Members.Any())
            {
                db.Members.Add(new Member { FamilyName = "Đặng", GivenName = "Minh", MembershipPlanId = db.MembershipPlans.First().Id, JoinDate = DateTime.Today, PtStatus = PtStatus.KhongCoPT, IsActive = true });
                db.SaveChanges();
            }

            if (!db.Trainers.Any())
            {
                db.Trainers.Add(new Trainer { FullName = "Nguyễn Văn Nam", Phone = "0981xxxxxx" });
                db.SaveChanges();
            }
        }
    }
}
