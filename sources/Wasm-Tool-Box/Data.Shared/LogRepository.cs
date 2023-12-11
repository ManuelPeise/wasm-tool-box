using Data.Database;
using Data.Models.Entities.Log;
using Data.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Shared
{
    public class LogRepository : ILogRepository
    {
        private readonly DatabaseContext _context;
        private readonly DbSet<LogEntity> _db;
        private bool disposedValue;

        public LogRepository(DatabaseContext context)
        {
            _context = context;
            _db = _context.Set<LogEntity>();
        }

        public async Task AddMessage(LogEntity logEntity)
        {
            await _db.AddAsync(logEntity);
        }

        public async Task<IList<LogEntity>> GetAllAsync(System.Linq.Expressions.Expression<Func<LogEntity, bool>>? expression = null, bool asNoTracking = true)
        {
            var query = _db as IQueryable<LogEntity>;

            if (expression != null)
            {
                query = query.Where(expression);
            }

            return asNoTracking ?
                await query.AsNoTracking().ToListAsync() :
                await query.ToListAsync();
        }

        #region dispose

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _context.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
