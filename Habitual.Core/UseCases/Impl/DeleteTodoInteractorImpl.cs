using System;
using Habitual.Core.Entities;
using Habitual.Core.Executors;
using Habitual.Core.Repositories;
using Habitual.Core.UseCases.Base;

namespace Habitual.Core.UseCases.Impl
{
    public class DeleteTodoInteractorImpl : AbstractInteractor, DeleteTodoInteractor
    {
        private DeleteTodoInteractorCallback callback;
        private TodoRepository todoRepository;

        private Todo todo;

        public DeleteTodoInteractorImpl(Executor taskExecutor, MainThread mainThread,
                                        DeleteTodoInteractorCallback callback, TodoRepository TodoRepository, Todo todo) : base(taskExecutor, mainThread)
        {
            this.callback = callback;
            this.todoRepository = TodoRepository;
            this.todo = todo;
        }

        public override void Run()
        {
            try
            {
                todoRepository.Delete(todo.ID);

                mainThread.Post(() => callback.OnTodoDeleted(todo.ID));
            }
            catch (Exception)
            {
                mainThread.Post(() => callback.OnError("Error deleting todo. Try again."));
            }
        }
    }
}
