using System;
using Habitual.Core.Entities;
using Habitual.Core.Executors;
using Habitual.Core.Repositories;
using Habitual.Core.UseCases.Base;

namespace Habitual.Core.UseCases.Impl
{
    public class DeleteHabitInteractorImpl : AbstractInteractor, DeleteHabitInteractor
    {
        private DeleteHabitInteractorCallback callback;
        private HabitRepository habitRepository;


        private Habit habit;

        public DeleteHabitInteractorImpl(Executor taskExecutor, MainThread mainThread,
                                        DeleteHabitInteractorCallback callback, HabitRepository habitRepository, Habit habit) : base(taskExecutor, mainThread)
        {
            this.callback = callback;
            this.habitRepository = habitRepository;
            this.habit = habit;
        }

        public override void Run()
        {
            try
            {
                habitRepository.Delete(habit.ID);

                mainThread.Post(() => callback.OnHabitDeleted(habit.ID));
            }
            catch (Exception)
            {
                mainThread.Post(() => callback.OnError("Error deleting habit. Try again."));
            }
        }
    }
}
