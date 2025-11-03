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
                var plans = db.MembershipPlans.ToList();
                var random = new Random();

                var members = new[]
                {
                    // Hội viên tham gia 6 tháng trước
                    new Member { FamilyName = "Nguyễn", MiddleName = "Văn", GivenName = "An", Phone = "0901234567", JoinDate = DateTime.Today.AddMonths(-6), MembershipPlanId = plans[5].Id, PtStatus = PtStatus.CoPT, IsActive = true, TotalTrainingDays = 72 },
                    new Member { FamilyName = "Trần", MiddleName = "Thị", GivenName = "Bình", Phone = "0902345678", JoinDate = DateTime.Today.AddMonths(-6).AddDays(-5), MembershipPlanId = plans[6].Id, PtStatus = PtStatus.KhongCoPT, IsActive = true, TotalTrainingDays = 68 },
                    new Member { FamilyName = "Lê", MiddleName = "Hoàng", GivenName = "Cường", Phone = "0903456789", JoinDate = DateTime.Today.AddMonths(-6).AddDays(-10), MembershipPlanId = plans[9].Id, PtStatus = PtStatus.CoPT, IsActive = true, TotalTrainingDays = 70 },

                    // Hội viên tham gia 5 tháng trước
                    new Member { FamilyName = "Phạm", MiddleName = "Minh", GivenName = "Dũng", Phone = "0904567890", JoinDate = DateTime.Today.AddMonths(-5), MembershipPlanId = plans[7].Id, PtStatus = PtStatus.PTNgoai, IsActive = true, TotalTrainingDays = 60 },
                    new Member { FamilyName = "Hoàng", MiddleName = "Thị", GivenName = "Hoa", Phone = "0905678901", JoinDate = DateTime.Today.AddMonths(-5).AddDays(-3), MembershipPlanId = plans[4].Id, PtStatus = PtStatus.KhongCoPT, IsActive = true, TotalTrainingDays = 58 },
                    new Member { FamilyName = "Vũ", MiddleName = "Đức", GivenName = "Hùng", Phone = "0906789012", JoinDate = DateTime.Today.AddMonths(-5).AddDays(-7), MembershipPlanId = plans[8].Id, PtStatus = PtStatus.CoPT, IsActive = true, TotalTrainingDays = 62 },

                    // Hội viên tham gia 4 tháng trước
                    new Member { FamilyName = "Đỗ", MiddleName = "Thị", GivenName = "Lan", Phone = "0907890123", JoinDate = DateTime.Today.AddMonths(-4), MembershipPlanId = plans[5].Id, PtStatus = PtStatus.KhongCoPT, IsActive = true, TotalTrainingDays = 50 },
                    new Member { FamilyName = "Bùi", MiddleName = "Văn", GivenName = "Long", Phone = "0908901234", JoinDate = DateTime.Today.AddMonths(-4).AddDays(-2), MembershipPlanId = plans[6].Id, PtStatus = PtStatus.CoPT, IsActive = true, TotalTrainingDays = 48 },
                    new Member { FamilyName = "Đinh", MiddleName = "Quang", GivenName = "Minh", Phone = "0909012345", JoinDate = DateTime.Today.AddMonths(-4).AddDays(-8), MembershipPlanId = plans[10].Id, PtStatus = PtStatus.CoPT, IsActive = true, TotalTrainingDays = 52 },
                    new Member { FamilyName = "Ngô", MiddleName = "Thị", GivenName = "Nga", Phone = "0910123456", JoinDate = DateTime.Today.AddMonths(-4).AddDays(-12), MembershipPlanId = plans[4].Id, PtStatus = PtStatus.KhongCoPT, IsActive = true, TotalTrainingDays = 46 },

                    // Hội viên tham gia 3 tháng trước
                    new Member { FamilyName = "Trương", MiddleName = "Văn", GivenName = "Phúc", Phone = "0911234567", JoinDate = DateTime.Today.AddMonths(-3), MembershipPlanId = plans[2].Id, PtStatus = PtStatus.PTNgoai, IsActive = true, TotalTrainingDays = 36 },
                    new Member { FamilyName = "Lý", MiddleName = "Thị", GivenName = "Quỳnh", Phone = "0912345678", JoinDate = DateTime.Today.AddMonths(-3).AddDays(-5), MembershipPlanId = plans[5].Id, PtStatus = PtStatus.CoPT, IsActive = true, TotalTrainingDays = 38 },
                    new Member { FamilyName = "Mai", MiddleName = "Xuân", GivenName = "Sơn", Phone = "0913456789", JoinDate = DateTime.Today.AddMonths(-3).AddDays(-10), MembershipPlanId = plans[8].Id, PtStatus = PtStatus.KhongCoPT, IsActive = true, TotalTrainingDays = 35 },
                    new Member { FamilyName = "Võ", MiddleName = "Thị", GivenName = "Thảo", Phone = "0914567890", JoinDate = DateTime.Today.AddMonths(-3).AddDays(-15), MembershipPlanId = plans[1].Id, PtStatus = PtStatus.KhongCoPT, IsActive = true, TotalTrainingDays = 32 },

                    // Hội viên tham gia 2 tháng trước
                    new Member { FamilyName = "Đặng", MiddleName = "Minh", GivenName = "Tuấn", Phone = "0915678901", JoinDate = DateTime.Today.AddMonths(-2), MembershipPlanId = plans[1].Id, PtStatus = PtStatus.CoPT, IsActive = true, TotalTrainingDays = 24 },
                    new Member { FamilyName = "Dương", MiddleName = "Thị", GivenName = "Uyên", Phone = "0916789012", JoinDate = DateTime.Today.AddMonths(-2).AddDays(-3), MembershipPlanId = plans[4].Id, PtStatus = PtStatus.KhongCoPT, IsActive = true, TotalTrainingDays = 22 },
                    new Member { FamilyName = "Tô", MiddleName = "Văn", GivenName = "Việt", Phone = "0917890123", JoinDate = DateTime.Today.AddMonths(-2).AddDays(-7), MembershipPlanId = plans[2].Id, PtStatus = PtStatus.PTNgoai, IsActive = true, TotalTrainingDays = 26 },
                    new Member { FamilyName = "Phan", MiddleName = "Thị", GivenName = "Xuân", Phone = "0918901234", JoinDate = DateTime.Today.AddMonths(-2).AddDays(-12), MembershipPlanId = plans[5].Id, PtStatus = PtStatus.CoPT, IsActive = true, TotalTrainingDays = 28 },

                    // Hội viên tham gia 1 tháng trước
                    new Member { FamilyName = "Hồ", MiddleName = "Quốc", GivenName = "Anh", Phone = "0919012345", JoinDate = DateTime.Today.AddMonths(-1), MembershipPlanId = plans[0].Id, PtStatus = PtStatus.KhongCoPT, IsActive = true, TotalTrainingDays = 12 },
                    new Member { FamilyName = "Cao", MiddleName = "Thị", GivenName = "Bảo", Phone = "0920123456", JoinDate = DateTime.Today.AddMonths(-1).AddDays(-2), MembershipPlanId = plans[1].Id, PtStatus = PtStatus.KhongCoPT, IsActive = true, TotalTrainingDays = 14 },
                    new Member { FamilyName = "Lâm", MiddleName = "Văn", GivenName = "Chính", Phone = "0921234567", JoinDate = DateTime.Today.AddMonths(-1).AddDays(-5), MembershipPlanId = plans[4].Id, PtStatus = PtStatus.CoPT, IsActive = true, TotalTrainingDays = 16 },
                    new Member { FamilyName = "Tạ", MiddleName = "Thị", GivenName = "Diễm", Phone = "0922345678", JoinDate = DateTime.Today.AddMonths(-1).AddDays(-10), MembershipPlanId = plans[0].Id, PtStatus = PtStatus.KhongCoPT, IsActive = true, TotalTrainingDays = 10 },

                    // Hội viên tham gia 3 tuần trước
                    new Member { FamilyName = "Từ", MiddleName = "Minh", GivenName = "Em", Phone = "0923456789", JoinDate = DateTime.Today.AddDays(-21), MembershipPlanId = plans[0].Id, PtStatus = PtStatus.PTNgoai, IsActive = true, TotalTrainingDays = 9 },
                    new Member { FamilyName = "Chu", MiddleName = "Thị", GivenName = "Giang", Phone = "0924567890", JoinDate = DateTime.Today.AddDays(-20), MembershipPlanId = plans[1].Id, PtStatus = PtStatus.KhongCoPT, IsActive = true, TotalTrainingDays = 8 },

                    // Hội viên tham gia 2 tuần trước
                    new Member { FamilyName = "Kiều", MiddleName = "Văn", GivenName = "Hải", Phone = "0925678901", JoinDate = DateTime.Today.AddDays(-14), MembershipPlanId = plans[0].Id, PtStatus = PtStatus.CoPT, IsActive = true, TotalTrainingDays = 6 },
                    new Member { FamilyName = "La", MiddleName = "Thị", GivenName = "Khánh", Phone = "0926789012", JoinDate = DateTime.Today.AddDays(-12), MembershipPlanId = plans[4].Id, PtStatus = PtStatus.KhongCoPT, IsActive = true, TotalTrainingDays = 5 },

                    // Hội viên tham gia 1 tuần trước
                    new Member { FamilyName = "Tăng", MiddleName = "Minh", GivenName = "Linh", Phone = "0927890123", JoinDate = DateTime.Today.AddDays(-7), MembershipPlanId = plans[0].Id, PtStatus = PtStatus.KhongCoPT, IsActive = true, TotalTrainingDays = 3 },
                    new Member { FamilyName = "Ông", MiddleName = "Thị", GivenName = "My", Phone = "0928901234", JoinDate = DateTime.Today.AddDays(-6), MembershipPlanId = plans[1].Id, PtStatus = PtStatus.CoPT, IsActive = true, TotalTrainingDays = 2 },

                    // Hội viên mới tham gia
                    new Member { FamilyName = "Lương", MiddleName = "Văn", GivenName = "Nam", Phone = "0929012345", JoinDate = DateTime.Today.AddDays(-3), MembershipPlanId = plans[0].Id, PtStatus = PtStatus.KhongCoPT, IsActive = true, TotalTrainingDays = 1 },
                    new Member { FamilyName = "Hà", MiddleName = "Thị", GivenName = "Oanh", Phone = "0930123456", JoinDate = DateTime.Today.AddDays(-1), MembershipPlanId = plans[4].Id, PtStatus = PtStatus.PTNgoai, IsActive = true, TotalTrainingDays = 1 },
                };

                db.Members.AddRange(members);
                db.SaveChanges();
            }

            if (!db.Trainers.Any())
            {
                var trainers = new[]
                {
                    new Trainer { FullName = "Nguyễn Văn Nam", Phone = "0981234567", Specialty = "Tăng cơ, Giảm mỡ" },
                    new Trainer { FullName = "Trần Thị Hương", Phone = "0982345678", Specialty = "Yoga, Pilates" },
                    new Trainer { FullName = "Lê Minh Tuấn", Phone = "0983456789", Specialty = "Boxing, Kickboxing" },
                    new Trainer { FullName = "Phạm Thị Lan", Phone = "0984567890", Specialty = "Dance Fitness, Zumba" },
                    new Trainer { FullName = "Hoàng Quốc Anh", Phone = "0985678901", Specialty = "CrossFit, Functional Training" },
                };
                db.Trainers.AddRange(trainers);
                db.SaveChanges();
            }
        }
    }
}
