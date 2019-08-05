using System;
using Habitual.Core.Entities;
using Habitual.Core.Executors;
using Habitual.Core.Repositories;
using Habitual.Core.UseCases;
using Habitual.Core.UseCases.Impl;

namespace Habitual.Droid.Presenters.Impl
{
    public class ManagePresenterImpl : AbstractPresenter, ManagePresenter, GetTaskContainerCallback, CreateHabitInteractorCallback, CreateRoutineInteractorCallback, CreateTodoInteractorCallback, DeleteHabitInteractorCallback, DeleteRoutineInteractorCallback, DeleteTodoInteractorCallback
    {
        private ManageView view;
        private HabitRepository habitRepository;
        private RoutineRepository routineRepository;
        private TodoRepository todoRepository;
        private UserRepository userRepository;
        private string username;
        private string password;

        public ManagePresenterImpl(Executor executor, MainThread mainThread, ManageView view, HabitRepository habitRepository, RoutineRepository routineRepository, TodoRepository todoRepository, UserRepository userRepository, string username, string password) : base(executor, mainThread)
        {
            this.view = view;
            this.habitRepository = habitRepository;
            this.userRepository = userRepository;
            this.routineRepository = routineRepository;
            this.todoRepository = todoRepository;
            this.username = username;
            this.password = password;
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

        public void GetTasks(string username, string password)
        {
            GetTaskContainerInteractor getTaskInteractor = new GetTaskContainerInteractorImpl(executor, mainThread, this, habitRepository, routineRepository, todoRepository, username, password, DateTime.Today.DayOfWeek, true, false);
            getTaskInteractor.Execute();
        }

        public void OnTaskContainerFilled(TaskContainer taskContainer)
        {
            view.OnTasksRetrieved(taskContainer);
        }

        public void CreateHabit(Habit habit)
        {
            CreateHabitInteractor interactor = new CreateHabitInteractorImpl(executor, mainThread, this, habitRepository, username, habit);
            interactor.Execute();
        }

        public void CreateRoutine(Routine routine)
        {
            CreateRoutineInteractor interactor = new CreateRoutineInteractorImpl(executor, mainThread, this, routineRepository, username, routine);
            interactor.Execute();
        }

        public void CreateTodo(Todo todo)
        {
            CreateTodoInteractor interactor = new CreateTodoInteractorImpl(executor, mainThread, this, todoRepository, username, todo);
            interactor.Execute();
        }

        public void OnHabitCreated(Habit habit)
        {
            view.OnHabitCreated(habit);
        }

        public void OnTodoCreated(Todo todo)
        {
            view.OnTodoCreated(todo);
        }

        public void OnRoutineCreated(Routine routine)
        {
            view.OnRoutineCreated(routine);
        }

        public void DeleteHabit(Habit habit)
        {
            DeleteHabitInteractor interactor = new DeleteHabitInteractorImpl(executor, mainThread, this, habitRepository, habit);
            interactor.Execute();
        }

        public void DeleteRoutine(Routine routine)
        {
            DeleteRoutineInteractor interactor = new DeleteRoutineInteractorImpl(executor, mainThread, this, routineRepository, routine);
            interactor.Execute();
        }

        public void DeleteTodo(Todo todo)
        {
            DeleteTodoInteractor interactor = new DeleteTodoInteractorImpl(executor, mainThread, this, todoRepository, todo);
            interactor.Execute();
        }

        public void OnHabitDeleted(Guid habitID)
        {
            view.OnTaskDeleted();
        }

        public void OnRoutineDeleted(Guid routineID)
        {
            view.OnTaskDeleted();
        }

        public void OnTodoDeleted(Guid id)
        {
            view.OnTaskDeleted();
        }
    }
}