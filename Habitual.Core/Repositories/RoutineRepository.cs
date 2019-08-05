using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Habitual.Core.Entities;

namespace Habitual.Core.Repositories
{
    public interface RoutineRepository : Repository<Routine>
    {
        Task MarkDone(RoutineLog log);
        Task<List<RoutineLog>> GetLogs(DateTime date, string username);
        Task<List<Routine>> GetAllRoutinesForToday(string username);
    }
}
