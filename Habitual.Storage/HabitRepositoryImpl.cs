using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Habitual.Core.Entities;
using Habitual.Core.Repositories;
using Habitual.Storage.DB;

namespace Habitual.Storage
{
    public class HabitRepositoryImpl : HabitRepository
    {
        public async Task Create(Habit habit)
        {
            HabitDB habitManager = new HabitDB();
            await habitManager.CreateHabit(habit);
            return;
        }

        public async Task Delete(Guid id)
        {
            HabitDB habitManager = new HabitDB();
            await habitManager.DeleteHabit(id);
            return;
        }

        public async Task<List<Habit>> GetAll(string username)
        {
            HabitDB habitManager = new HabitDB();
            var habits = await habitManager.GetAllHabits(username);
            return habits;
        }

        public async Task<List<HabitLog>> GetLogs(DateTime date, string username)
        {
            HabitDB habitManager = new HabitDB();
            var logs = await habitManager.GetAllHabitLogs(date, username);
            return logs;
        }  

        public async void IncrementHabit(HabitLog log)
        {
            HabitDB habitManager = new HabitDB();
            await habitManager.LogHabit(log);
            return;
        }
    }
}
