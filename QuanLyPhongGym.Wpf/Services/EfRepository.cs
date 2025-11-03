using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuanLyPhongGym.Data;
using QuanLyPhongGym.Models;

namespace QuanLyPhongGym.Services
{
    public class EfRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly GymDbContext _ctx;
        public EfRepository(GymDbContext ctx) => _ctx = ctx;

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null, params string[] includes)
        {
            IQueryable<T> q = _ctx.Set<T>();
            foreach (var inc in includes) q = q.Include(inc);
            if (predicate != null) q = q.Where(predicate);
            q = q.OrderBy(x => x.Id);
            return await q.AsNoTracking().ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id, params string[] includes)
        {
            IQueryable<T> q = _ctx.Set<T>();
            foreach (var inc in includes) q = q.Include(inc);
            return await _ctx.Set<T>().FindAsync(id);
        }

        public async Task<T> AddAsync(T entity)
        {
            _ctx.Set<T>().Add(entity);
            await _ctx.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            _ctx.Set<T>().Update(entity);
            await _ctx.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var e = await _ctx.Set<T>().FindAsync(id);
            if (e != null)
            {
                _ctx.Set<T>().Remove(e);
                await _ctx.SaveChangesAsync();
            }
        }
    }
}