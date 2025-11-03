using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuanLyPhongGym.Data;
using QuanLyPhongGym.Models;

namespace QuanLyPhongGym.Services
{
    public class GymService
    {
        private readonly GymDbContext _ctx;
        public readonly EfRepository<Member> Members;
        public readonly EfRepository<MembershipPlan> Plans;
        public readonly EfRepository<Trainer> Trainers;
        public readonly EfRepository<WorkoutSchedule> Schedules;

        public GymService()
        {
            _ctx = new GymDbContext();
            _ctx.Database.EnsureCreated();
            Members = new EfRepository<Member>(_ctx);
            Plans = new EfRepository<MembershipPlan>(_ctx);
            Trainers = new EfRepository<Trainer>(_ctx);
            Schedules = new EfRepository<WorkoutSchedule>(_ctx);
        }


        public async Task<List<Member>> SearchMembersAsync(int? id, string? name)
        {
            var q = _ctx.Members.Include(m => m.MembershipPlan).AsQueryable();

            if (id.HasValue)
                q = q.Where(m => m.Id == id.Value);

            if (!string.IsNullOrWhiteSpace(name))
            {
                var key = name.Trim().ToLower();
                q = q.Where(m =>
                    m.GivenName.ToLower().Contains(key) ||
                    m.FamilyName.ToLower().Contains(key) ||
                    (m.MiddleName ?? "").ToLower().Contains(key));
            }

            return await q.OrderBy(m => m.Id).AsNoTracking().ToListAsync();
        }

        public async Task<List<WorkoutSchedule>> GetTodaySchedulesAsync()
        {
            var today = DateTime.Today;
            var tomorrow = today.AddDays(1);
            return await _ctx.WorkoutSchedules
                .Include(s => s.Member)
                .Include(s => s.Trainer)
                .Where(s => s.StartTime >= today && s.StartTime < tomorrow)
                .OrderBy(s => s.StartTime)
                .ThenBy(s => s.Id)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<Trainer>> SearchTrainersAsync(int? id, string? name)
        {
            var q = _ctx.Trainers
                .Include(t => t.WorkoutSchedules)
                    .ThenInclude(ws => ws.Member)
                .AsQueryable();

            if (id.HasValue)
                q = q.Where(t => t.Id == id.Value);

            if (!string.IsNullOrWhiteSpace(name))
            {
                var key = name.Trim().ToLower();
                q = q.Where(t => t.FullName.ToLower().Contains(key));
            }

            return await q.OrderBy(t => t.Id).AsNoTracking().ToListAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var entity = await _ctx.Members.FindAsync(id);
            if (entity != null)
            {
                _ctx.Members.Remove(entity);
                await _ctx.SaveChangesAsync();
            }
        }
    }
}