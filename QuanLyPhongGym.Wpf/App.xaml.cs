using Microsoft.EntityFrameworkCore;
using QuanLyPhongGym.Data;
using System.Linq;
using System.Windows;
using static QuanLyPhongGym.Data.GymDbContext;

namespace QuanLyPhongGym
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            using var db = new GymDbContext();

            // ✅ BẮT BUỘC: tạo DB & bảng nếu chưa có
            db.Database.Migrate();

            // (tùy chọn) chỉ seed dữ liệu nếu DB trống
            DataSeeder.SeedIfEmpty(db);
        }
        }
    }

