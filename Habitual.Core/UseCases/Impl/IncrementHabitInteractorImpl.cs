using System;
using Habitual.Core.Entities;
using Habitual.Core.Executors;
using Habitual.Core.Repositories;
using Habitual.Core.UseCases.Base;

namespace Habitual.Core.UseCases.Impl
{
    public class IncrementHabitInteractorImpl : AbstractInteractor, IncrementHabitInteractor
    {
        private IncrementHabitInteractorCallback callback;
        private HabitRepository habitRepository;
        private UserRepository userRepository;
        private Habit habit;

        public IncrementHabitInteractorImpl(Executor taskExecutor, MainThread mainThread,
                                        IncrementHabitInteractorCallback callback, HabitRepository habitRepository,
                                        UserRepository userRepository, Habit habit) : base(taskExecutor, mainThread)
        {
            this.callback = callback;
            this.habitRepository = habitRepository;
            this.userRepository = userRepository;
            this.habit = habit;
        }

        public override void Run()
        {
            try
            {
                var habitLog = new HabitLog();
                habitLog.ID = Guid.NewGuid();
                habitLog.HabitID = habit.ID;
                habitLog.Timestamp = DateTime.Today;
                habitRepository.IncrementHabit(habitLog);

                var pointsAdded = 0;
                if (habit.Difficulty == Difficulty.Easy) pointsAdded = 5;
                if (habit.Difficulty == Difficulty.Medium) pointsAdded = 10;
                if (habit.Difficulty == Difficulty.Hard) pointsAdded = 15;
                if (habit.Difficulty == Difficulty.VeryHard) pointsAdded = 20;

                userRepository.IncrementPoints(habit.Username, pointsAdded);

                mainThread.Post(() => callback.OnHabitIncremented(habit, pointsAdded));
            }
            catch (Exception)
            {
                mainThread.Post(() => callback.OnError("Error incrementing habit count. Try again."));
            }
        }
    }
}
