using System;
using Habitual.Core.Entities;
using Habitual.Core.Executors;
using Habitual.Core.Repositories;
using Habitual.Core.UseCases.Base;

namespace Habitual.Core.UseCases.Impl
{
    public class MarkTodoDoneInteractorImpl : AbstractInteractor, MarkTodoDoneInteractor
    {
        private MarkTodoDoneInteractorCallback callback;
        private TodoRepository todoRepository;
        private UserRepository userRepository;
        private Todo todo;

        public MarkTodoDoneInteractorImpl(Executor taskExecutor, MainThread mainThread,
                                        MarkTodoDoneInteractorCallback callback, TodoRepository todoRepository,
                                        UserRepository userRepository, Todo todo) : base(taskExecutor, mainThread)
        {
            this.callback = callback;
            this.todoRepository = todoRepository;
            this.userRepository = userRepository;
            this.todo = todo;
        }

        public async override void Run()
        {
            try
            {
                todo.IsDone = !todo.IsDone; 
                await todoRepository.MarkDone(todo);

                
                var pointsAdded = 0;
                if (todo.Difficulty == Difficulty.Easy) pointsAdded = todo.IsDone ? 10 : -10;
                if (todo.Difficulty == Difficulty.Medium) pointsAdded = todo.IsDone ? 20 : -20;
                if (todo.Difficulty == Difficulty.Hard) pointsAdded = todo.IsDone ? 30 : -30;
                if (todo.Difficulty == Difficulty.VeryHard) pointsAdded = todo.IsDone ? 40 : -40;

                await userRepository.IncrementPoints(todo.Username, pointsAdded);

                mainThread.Post(() => callback.OnTodoMarkedDone(todo, pointsAdded));
            } catch (Exception)
            {
                mainThread.Post(() => callback.OnError("Error marking todo done. Try again."));
            }
            
        }
    }
}
