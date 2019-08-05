using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Habitual.Core.Entities;
using Habitual.Storage.DB.Base;
using Newtonsoft.Json;

namespace Habitual.Storage.DB
{
    public class HabitDB : BaseDB
    {
        public async Task CreateHabit(Habit habit)
        {
            var jsonData = JsonConvert.SerializeObject(habit);
            var jsonResult = await PostDataAsync("api/habit/create", jsonData);
            return;
        }

        public async Task<List<Habit>> GetAllHabits(string username)
        {
            var jsonResult = await GetDataAsync($"api/habit/getall/{username}");
            var habits = JsonConvert.DeserializeObject<List<Habit>>(jsonResult);
            return habits;
        }

        public async Task DeleteHabit(Guid id)
        {
            var jsonResult = await DeleteDataAsync($"api/habit/delete/{id.ToString()}");
            return;
        }

        public async Task LogHabit(HabitLog log)
        {
            var jsonData = JsonConvert.SerializeObject(log);
            var jsonResult = await PostDataAsync("api/habit/log", jsonData);
            return;
        }

        public async Task<List<HabitLog>> GetAllHabitLogs(DateTime date, string username)
        {
            var dateString = date.ToString("yyyy-MM-dd");
            var jsonResult = await GetDataAsync($"api/habit/getalllogs/{username}/{dateString}");
            var habitLogs = JsonConvert.DeserializeObject<List<HabitLog>>(jsonResult);
            return habitLogs;
        }
    }
}
