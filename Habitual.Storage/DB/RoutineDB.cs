using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Habitual.Core.Entities;
using Habitual.Storage.DB.Base;
using Newtonsoft.Json;

namespace Habitual.Storage.DB
{
    public class RoutineDB :BaseDB
    {
        public async Task CreateRoutine(Routine routine)
        {
            var jsonData = JsonConvert.SerializeObject(routine);
            var jsonResult = await PostDataAsync("api/routine/create", jsonData);
            return;
        }

        public async Task<List<Routine>> GetAllRoutines(string username)
        {
            var jsonResult = await GetDataAsync($"api/routine/getall/{username}");
            var routines = JsonConvert.DeserializeObject<List<Routine>>(jsonResult);
            return routines;
        }

        public async Task DeleteRoutine(Guid id)
        {
            await DeleteDataAsync($"api/routine/delete/{id}");
            return;
        }

        public async Task LogRoutine(RoutineLog log)
        {
            var jsonData = JsonConvert.SerializeObject(log);
            var jsonResult = await PostDataAsync("api/routine/log", jsonData);
            return;
        }

        public async Task<List<RoutineLog>> GetAllRoutineLogs(DateTime date, string username)
        {
            var dateString = date.ToString("yyyy-MM-dd");
            var jsonResult = await GetDataAsync($"api/routine/getalllogs/{username}/{dateString}");
            var routineLogs = JsonConvert.DeserializeObject<List<RoutineLog>>(jsonResult);
            return routineLogs;
        }
    }
}
