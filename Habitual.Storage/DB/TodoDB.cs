using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Habitual.Core.Entities;
using Habitual.Storage.DB.Base;
using Newtonsoft.Json;

namespace Habitual.Storage.DB
{
    public class TodoDB : BaseDB
    {
        public async Task CreateTodo(Todo todo)
        {
            var jsonData = JsonConvert.SerializeObject(todo);
            var jsonResult = await PostDataAsync("api/todo/create", jsonData);
            return;
        }

        public async Task<List<Todo>> GetAllTodos(string username)
        {
            var jsonResult = await GetDataAsync($"api/todo/getall/{username}");
            var todos = JsonConvert.DeserializeObject<List<Todo>>(jsonResult);
            return todos;
        }

        public async Task DeleteTodo(Guid id)
        {
            var jsonResult = await DeleteDataAsync($"api/todo/delete/{id.ToString()}");
            return;
        }

        public async Task LogTodo(Todo todo)
        {
            var jsonData = JsonConvert.SerializeObject(todo);
            await PostDataAsync($"api/todo/log/", jsonData);
            return;
        }
    }
}
