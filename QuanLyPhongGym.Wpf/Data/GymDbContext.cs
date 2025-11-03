using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;
using Microsoft.EntityFrameworkCore.Tools;
using QuanLyPhongGym.Models;
using System;
using System.IO;
using System.Linq;

namespace QuanLyPhongGym.Data
{
    public class GymDbContext : DbContext
    {
        public DbSet<Member> Members => Set<Member>();
        public DbSet<MembershipPlan> MembershipPlans => Set<MembershipPlan>();
        public DbSet<Trainer> Trainers => Set<Trainer>();
        public DbSet<WorkoutSchedule> WorkoutSchedules => Set<WorkoutSchedule>();
        public DbSet<Attendance> Attendances => Set<Attendance>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var folder = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "QuanLyPhongGym");
                Directory.CreateDirectory(folder);
                var path = Path.Combine(folder, "gym.db");
                optionsBuilder.UseSqlite($"Data Source={path}");
            }
        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // WorkoutSchedule -> Trainer (optional), set null khi xóa Trainer
            modelBuilder.Entity<WorkoutSchedule>()
                .HasOne(ws => ws.Trainer)
                .WithMany(t => t.WorkoutSchedules)
                .HasForeignKey(ws => ws.TrainerId)
                .OnDelete(DeleteBehavior.SetNull);

            // WorkoutSchedule -> Member (required), cascade khi xóa Member
            modelBuilder.Entity<WorkoutSchedule>()
                .HasOne(ws => ws.Member)
                .WithMany() // nếu bạn có Member.WorkoutSchedules thì thay bằng .WithMany(m => m.WorkoutSchedules)
                .HasForeignKey(ws => ws.MemberId)
                .OnDelete(DeleteBehavior.Cascade);
        }




    }
    }