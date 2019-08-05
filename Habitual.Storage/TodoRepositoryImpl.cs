using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Habitual.Core.Entities;
using Habitual.Core.Repositories;
using Habitual.Storage.DB;
using Habitual.Storage.Local;
using Newtonsoft.Json;

namespace Habitual.Storage
{
    public class TodoRepositoryImpl : TodoRepository
    {

        public async Task Create(Todo entity)
        {
            TodoDB todoManager = new TodoDB();
            await todoManager.CreateTodo(entity);
            return;
        }

        public async Task Delete(Guid id)
        {
            TodoDB todoManager = new TodoDB();
            await todoManager.DeleteTodo(id);
            return;
        }

        public async Task<List<Todo>> GetAll(string username)
        {
            TodoDB todoManager = new TodoDB();
            var todos = await todoManager.GetAllTodos(username);
            return todos;
        }

        public List<Todo> GetAllForUser(string username)
        {
            var taskContainer = JsonConvert.DeserializeObject<TaskContainer>(LocalData.TaskContainer);
            var unDoneTodos = taskContainer.Todos.Where(t => t.IsDone == false);
            return unDoneTodos.ToList();
        }

        public async Task MarkDone(Todo todo)
        {
            TodoDB todoManager = new TodoDB();
            await todoManager.LogTodo(todo);
            return;
        }

        public void Update(Todo entity)
        {
            throw new NotImplementedException();
        }
    }
}
