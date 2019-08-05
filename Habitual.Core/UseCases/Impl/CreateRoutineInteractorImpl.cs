using System;
using Habitual.Core.Entities;
using Habitual.Core.Executors;
using Habitual.Core.Repositories;
using Habitual.Core.UseCases.Base;

namespace Habitual.Core.UseCases.Impl
{
    public class CreateRoutineInteractorImpl : AbstractInteractor, CreateRoutineInteractor
    {
        private CreateRoutineInteractorCallback callback;
        private RoutineRepository routineRepository;

        private string username;
        private Routine routine;

        public CreateRoutineInteractorImpl(Executor taskExecutor, MainThread mainThread,
                                        CreateRoutineInteractorCallback callback, RoutineRepository routineRepository,
                                        string username, Routine routine) : base(taskExecutor, mainThread)
        {
            this.callback = callback;
            this.routineRepository = routineRepository;
            this.username = username;
            this.routine = routine;
        }

        public override void Run()
        {
            try
            {
                routine.Username = username;
                routine.ID = Guid.NewGuid();
                routineRepository.Create(routine);

                mainThread.Post(() => callback.OnRoutineCreated(routine));
            }
            catch (Exception)
            {
                mainThread.Post(() => callback.OnError("Error creating routine. Try again."));
            }
        }
    }
}
