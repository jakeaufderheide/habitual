using System;
using Habitual.Core.Entities;
using Habitual.Core.Executors;
using Habitual.Core.Repositories;
using Habitual.Core.UseCases.Base;

namespace Habitual.Core.UseCases.Impl
{
    public class CreateTodoInteractorImpl : AbstractInteractor, CreateTodoInteractor
    {
        private CreateTodoInteractorCallback callback;
        private TodoRepository todoRepository;

        private string username;
        private Todo todo;
        

        public CreateTodoInteractorImpl(Executor taskExecutor, MainThread mainThread,
                                        CreateTodoInteractorCallback callback, TodoRepository todoRepository, string username, Todo todo) : base(taskExecutor, mainThread)
        {
            this.callback = callback;
            this.todoRepository = todoRepository;
            this.username = username;
            this.todo = todo;
        }

        public override void Run()
        {
            try
            {
                todo.Username = username;
                todo.ID = Guid.NewGuid();
                todoRepository.Create(todo);
                mainThread.Post(() => callback.OnTodoCreated(todo));
            } catch (Exception)
            {
                mainThread.Post(() => callback.OnError("Error creating todo. Try again."));
            } 
        }
    }
}
