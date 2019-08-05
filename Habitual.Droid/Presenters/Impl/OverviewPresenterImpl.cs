using System;
using Habitual.Core.Entities;
using Habitual.Core.Executors;
using Habitual.Core.Repositories;
using Habitual.Core.UseCases;
using Habitual.Core.UseCases.Impl;

namespace Habitual.Droid.Presenters.Impl
{
    public class OverviewPresenterImpl : AbstractPresenter, OverviewPresenter, GetTaskContainerCallback, IncrementHabitInteractorCallback, MarkRoutineDoneInteractorCallback, MarkTodoDoneInteractorCallback
    {
        private OverviewView view;
        private HabitRepository habitRepository;
        private RoutineRepository routineRepository;
        private TodoRepository todoRepository;
        private UserRepository userRepository;
        private string username;
        private string password;

        public OverviewPresenterImpl(Executor executor, MainThread mainThread, OverviewView view, HabitRepository habitRepository, RoutineRepository routineRepository, TodoRepository todoRepository, UserRepository userRepository, string username, string password) : base(executor, mainThread)
        {
            this.view = view;
            this.habitRepository = habitRepository;
            this.userRepository = userRepository;
            this.routineRepository = routineRepository;
            this.todoRepository = todoRepository;
            this.username = username;
            this.password = password;
        }

        public void GetTasks(string username, string password)
        {
            GetTaskContainerInteractor getTaskInteractor = new GetTaskContainerInteractorImpl(executor, mainThread, this, habitRepository, routineRepository, todoRepository, username, password, DateTime.Today.DayOfWeek, false, true);
            getTaskInteractor.Execute();
        }

        public void Resume()
        {
            throw new NotImplementedException();
        }

        public void Pause()
        {
            throw new NotImplementedException();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }

        public void Destroy()
        {
            throw new NotImplementedException();
        }

        public void OnError(string message)
        {
            view.OnError(message);
        }

        public void OnTaskContainerFilled(TaskContainer taskContainer)
        {
            view.OnTasksRetrieved(taskContainer);
        }

        public void MarkHabitDone(Habit habit)
        {
            IncrementHabitInteractor interactor = new IncrementHabitInteractorImpl(executor, mainThread, this, habitRepository, userRepository, habit);
            interactor.Execute();
        }

        public void OnHabitIncremented(Habit habit, int pointsAdded)
        {
            view.OnHabitMarkedDone(habit, pointsAdded);
        }

        public void MarkRoutineDone(Routine routine)
        {
            MarkRoutineDoneInteractor interactor = new MarkRoutineDoneInteractorImpl(executor, mainThread, this, routineRepository, userRepository, routine);
            interactor.Execute();
        }

        public void OnRoutineMarkedDoneForToday(Routine routine, int pointsAdded)
        {
            view.OnRoutineMarkedDone(routine, pointsAdded);
        }

        public void MarkTodoDone(Todo todo)
        {
            MarkTodoDoneInteractor interactor = new MarkTodoDoneInteractorImpl(executor, mainThread, this, todoRepository, userRepository, todo);
            interactor.Execute();
        }

        public void OnTodoMarkedDone(Todo todo, int pointsAdded)
        {
            view.OnTodoMarkedDone(todo, pointsAdded);
        }
    }
}