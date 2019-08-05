using System;
using Habitual.Core.Entities;
using Habitual.Core.Executors;
using Habitual.Core.Repositories;
using Habitual.Core.UseCases.Base;

namespace Habitual.Core.UseCases.Impl
{
    public class CreateHabitInteractorImpl : AbstractInteractor, CreateHabitInteractor
    {
        private CreateHabitInteractorCallback callback;
        private HabitRepository habitRepository;

        private string username;
        private Habit habit;

        public CreateHabitInteractorImpl(Executor taskExecutor, MainThread mainThread,
                                        CreateHabitInteractorCallback callback, HabitRepository habitRepository, string username,
                                        Habit habit) : base(taskExecutor, mainThread)
        {
            this.callback = callback;
            this.habitRepository = habitRepository;
            this.habit = habit;
            this.username = username;
        }

        public override void Run()
        {
            try
            {
                habit.Username = username;
                habit.ID = Guid.NewGuid();
                habitRepository.Create(habit);

                mainThread.Post(() => callback.OnHabitCreated(habit));
            } catch (Exception)
            {
                mainThread.Post(() => callback.OnError("Error creating habit. Try again."));
            }
            
        }
    }
}
