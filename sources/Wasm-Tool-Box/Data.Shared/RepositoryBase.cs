using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using Data.Shared.Interfaces;
using Data.Database;
using Microsoft.EntityFrameworkCore;

namespace Data.Shared
{
    public class RepositoryBase<T> : IRepository<T> where T : class
    {
        private readonly DatabaseContext _context;
        private readonly DbSet<T> _db;

        public RepositoryBase(DatabaseContext context)
        {
            _context = context;
            _db = _context.Set<T>();
        }

        public async Task<IList<T>> GetAllAsync(Expression<Func<T, bool>>? expression = null, bool asNoTracking = true)
        {
            var query = _db as IQueryable<T>;

            if (expression != null)
            {
                query = query.Where(expression);
            }

            return asNoTracking ?
                await query.AsNoTracking().ToListAsync() :
                await query.ToListAsync();
        }

        public async Task<T?> Get(Expression<Func<T, bool>> expression, bool asNoTracking = true)
        {
            var query = _db as IQueryable<T>;

            return asNoTracking ?
                await query.AsNoTracking().FirstOrDefaultAsync(expression) :
                await query.FirstOrDefaultAsync(expression);
        }

        public async Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> expression, bool asNoTracking = true)
        {
            var query = _db as IQueryable<T>;

            return asNoTracking ?
                await query.AsNoTracking().FirstOrDefaultAsync(expression) :
                await query.FirstOrDefaultAsync(expression);
        }

        public async Task<T?> Insert(T entity)
        {
            var entry = await _db.AddAsync(entity);

            return entry.Entity;
        }

        public void Update(T entity)
        {
            _db.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public async Task Delete(int id)
        {
            var entity = await _db.FindAsync(id);

            if (entity != null)
            {
                _context.Remove(entity);
            }
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            _db.RemoveRange(entities);
        }
    }
}