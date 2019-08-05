using System;
using Habitual.Core.Entities;
using Habitual.Core.Executors;
using Habitual.Core.Repositories;
using Habitual.Core.UseCases.Base;

namespace Habitual.Core.UseCases.Impl
{
    public class GetTaskContainerInteractorImpl : AbstractInteractor, GetTaskContainerInteractor
    {
        private GetTaskContainerCallback callback;
        private HabitRepository habitRepository;
        private RoutineRepository routineRepository;
        private TodoRepository todoRepository;
        private string username;
        private string password;
        private DayOfWeek dayOfWeek;
        private bool includeAllRoutines;
        private bool includeLogs;

        public GetTaskContainerInteractorImpl(Executor taskExecutor, MainThread mainThread, GetTaskContainerCallback callback, HabitRepository habitRepository, RoutineRepository routineRepository, TodoRepository todoRepository, string username, string password, DayOfWeek dayOfWeek, bool includeAllRoutines = false, bool includeLogs = false) : base(taskExecutor, mainThread)
        {
            this.callback = callback;
            this.habitRepository = habitRepository;
            this.routineRepository = routineRepository;
            this.todoRepository = todoRepository;
            this.username = username;
            this.password = password;
            this.dayOfWeek = dayOfWeek;
            this.includeAllRoutines = includeAllRoutines;
            this.includeLogs = includeLogs;
        }

        public async override void Run()
        {
            try
            {
                var newTaskContainer = TaskContainer.GetTaskContainer();
                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                {
                    callback.OnTaskContainerFilled(newTaskContainer);
                    return;
                }

                newTaskContainer.Habits = await habitRepository.GetAll(username);
                newTaskContainer.Routines = includeAllRoutines ? await routineRepository.GetAll(username) : await routineRepository.GetAllRoutinesForToday(username);
                newTaskContainer.Todos = await todoRepository.GetAll(username);
                if (includeLogs)
                {
                    if (newTaskContainer.Habits != null && newTaskContainer.Habits.Count > 0) newTaskContainer.HabitLogs = await habitRepository.GetLogs(DateTime.Today, username);
                    if (newTaskContainer.Routines != null && newTaskContainer.Routines.Count > 0) newTaskContainer.RoutineLogs = await routineRepository.GetLogs(DateTime.Today, username);
                }

                mainThread.Post(() => { callback.OnTaskContainerFilled(newTaskContainer); });
            }
            catch (Exception)
            {
                mainThread.Post(() => callback.OnError("Error gettting task container."));
            }
        }
    }
}
