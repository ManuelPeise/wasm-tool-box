using Data.Models.Entities.Log;
using System.Linq.Expressions;

namespace Data.Shared.Interfaces
{
    public interface ILogRepository: IDisposable
    {
        Task<IList<LogEntity>> GetAllAsync(
         Expression<Func<LogEntity, bool>>? expression = null, bool asNoTracking = true);
        Task AddMessage(LogEntity logEntity);
    }
}
