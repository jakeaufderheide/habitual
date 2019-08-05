using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Habitual.Core.Entities;
using Habitual.Core.Repositories;
using Habitual.Storage.DB;

namespace Habitual.Storage
{
    public class RoutineRepositoryImpl : RoutineRepository
    {
        public async Task Create(Routine entity)
        {
            RoutineDB routineManager = new RoutineDB();
            await routineManager.CreateRoutine(entity);
            return;
        }

        public async Task Delete(Guid id)
        {
            RoutineDB routineManager = new RoutineDB();
            await routineManager.DeleteRoutine(id);
            return;
        }

        public List<Routine> GetAllForUser(string username)
        {
            return null;
        }

        private List<Routine> RoutinesMatchingDay(List<Routine> routines, DayOfWeek dayOfWeek)
        {
            var day = dayOfWeek;
            if (day == DayOfWeek.Sunday)
            {
                var matchingRoutines = routines.Where(r => r.IsActiveSunday);
                return matchingRoutines.ToList();
            }
            if (day == DayOfWeek.Monday)
            {
                var matchingRoutines = routines.Where(r => r.IsActiveMonday);
                return matchingRoutines.ToList();
            }
            if (day == DayOfWeek.Tuesday)
            {
                var matchingRoutines = routines.Where(r => r.IsActiveTuesday);
                return matchingRoutines.ToList();
            }
            if (day == DayOfWeek.Wednesday)
            {
                var matchingRoutines = routines.Where(r => r.IsActiveWednesday);
                return matchingRoutines.ToList();
            }
            if (day == DayOfWeek.Thursday)
            {
                var matchingRoutines = routines.Where(r => r.IsActiveThursday);
                return matchingRoutines.ToList();
            }
            if (day == DayOfWeek.Friday)
            {
                var matchingRoutines = routines.Where(r => r.IsActiveFriday);
                return matchingRoutines.ToList();
            }
            if (day == DayOfWeek.Saturday)
            {
                var matchingRoutines = routines.Where(r => r.IsActiveSaturday);
                return matchingRoutines.ToList();
            }
            return new List<Routine>();
        }

        public Routine GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<RoutineLog>> GetLogs(DateTime date, string username)
        {
            RoutineDB routineManager = new RoutineDB();
            var logs = await routineManager.GetAllRoutineLogs(date, username);
            return logs;
        }

        public async Task MarkDone(RoutineLog log)
        {
            RoutineDB routineManager = new RoutineDB();
            await routineManager.LogRoutine(log);
            return;
        }

        public async Task<List<Routine>> GetAllRoutinesForToday(string username)
        {
            var routines = await GetAll(username);
            return RoutinesMatchingDay(routines, DateTime.Today.DayOfWeek);
        }

        public async Task<List<Routine>> GetAll(string username)
        {
            RoutineDB routineManager = new RoutineDB();
            var routines = await routineManager.GetAllRoutines(username);
            return routines;
        }
    }
}
