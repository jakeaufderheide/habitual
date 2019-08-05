using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Habitual.Core.Entities;

namespace Habitual.Core.Repositories
{
    public interface HabitRepository : Repository<Habit>
    {
        void IncrementHabit(HabitLog log); // returns new count
        Task<List<HabitLog>> GetLogs(DateTime date, string username);
    }
}
