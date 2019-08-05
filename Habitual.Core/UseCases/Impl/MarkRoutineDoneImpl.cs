using System;
using Habitual.Core.Entities;
using Habitual.Core.Executors;
using Habitual.Core.Repositories;
using Habitual.Core.UseCases.Base;

namespace Habitual.Core.UseCases.Impl
{
    public class MarkRoutineDoneInteractorImpl : AbstractInteractor, MarkRoutineDoneInteractor
    {
        private MarkRoutineDoneInteractorCallback callback;
        private RoutineRepository routineRepository;
        private UserRepository userRepository;
        private Routine routine;

        public MarkRoutineDoneInteractorImpl(Executor taskExecutor, MainThread mainThread,
                                        MarkRoutineDoneInteractorCallback callback, RoutineRepository routineRepository,
                                        UserRepository userRepository, Routine routine) : base(taskExecutor, mainThread)
        {
            this.callback = callback;
            this.routineRepository = routineRepository;
            this.userRepository = userRepository;
            this.routine = routine;
        }

        public async override void Run()
        {
            try
            {
                var log = new RoutineLog();
                log.ID = Guid.NewGuid();
                log.RoutineID = routine.ID;
                log.Timestamp = DateTime.Today;
                await routineRepository.MarkDone(log);

                var pointsAdded = 0;
                if (routine.Difficulty == Difficulty.Easy) pointsAdded = 10;
                if (routine.Difficulty == Difficulty.Medium) pointsAdded = 20;
                if (routine.Difficulty == Difficulty.Hard) pointsAdded = 30;
                if (routine.Difficulty == Difficulty.VeryHard) pointsAdded = 40;
                await userRepository.IncrementPoints(routine.Username, pointsAdded);

                mainThread.Post(() => callback.OnRoutineMarkedDoneForToday(routine, pointsAdded));
            }
            catch (Exception)
            {
                mainThread.Post(() => callback.OnError("Error logging routine. Try again."));
            }
        }
    }
}
