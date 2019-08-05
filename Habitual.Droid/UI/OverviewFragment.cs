using System;
using System.Collections.Generic;
using Android.OS;
using Android.Views;
using Android.Widget;
using Habitual.Core.Entities;
using Habitual.Core.Entities.Base;
using Habitual.Core.Executors;
using Habitual.Core.Executors.Impl;
using Habitual.Droid.Presenters;
using Habitual.Droid.Presenters.Impl;
using Habitual.Droid.Threading;
using Habitual.Droid.UI.ViewModels;
using Habitual.Droid.Util;
using Habitual.Storage;
using Habitual.Storage.Local;

namespace Habitual.Droid.UI
{
    public class OverviewFragment : Android.Support.V4.App.Fragment, OverviewView
    {
        private ListView overviewList;
        private OverviewItemList items;
        private List<HabitLog> habitLogs;
        private List<RoutineLog> routineLogs;
        private OverviewListAdapter adapter;
        private OverviewPresenter presenter;
        private MainThread mainThread;
        private MainApplicationCallback callback;

        public OverviewFragment(MainApplicationCallback callback)
        {
            this.callback = callback;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Init();
        }

        private void Init()
        {
            mainThread = new MainThreadImpl(this.Activity);
            presenter = new OverviewPresenterImpl(TaskExecutor.GetInstance(), mainThread, this, new HabitRepositoryImpl(), new RoutineRepositoryImpl(), new TodoRepositoryImpl(), new UserRepositoryImpl(), LocalData.Username, LocalData.Password);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            
            var view = inflater.Inflate(Resource.Layout.Overview, container, false);
            InitializeElements(view);
            
            return view;
        }

        private void InitializeElements(View view)
        {
            if (items == null) items = new OverviewItemList();
            overviewList = view.FindViewById<ListView>(Resource.Id.overviewList);
            adapter = new OverviewListAdapter(Activity, items, callback);
            overviewList.ItemClick += TaskClicked;
            overviewList.Adapter = adapter;
            Update();
        }

        internal void Reset()
        {
            Init();
        }

        private void TaskClicked(object sender, AdapterView.ItemClickEventArgs e)
        {
            var item = items[e.Position];
            var habit = item.Task as Habit;
            var routine = item.Task as Routine;
            var todo = item.Task as Todo;
            if (habit != null)
            {
                presenter.MarkHabitDone(habit);
                var log = new HabitLog();
                log.HabitID = habit.ID;
                log.Timestamp = DateTime.Today;
                adapter.IncrementItem(log);
            }
            if (routine != null)
            {
                if (adapter.WasTodayLogged(routine, routineLogs))
                    return;
                presenter.MarkRoutineDone(routine);
            }
            if (todo != null)
            {
                if (todo.IsDone)
                    return;
                presenter.MarkTodoDone(todo);
                adapter.MarkDone(todo);
            }
        }

        public void Update()
        {
            presenter.GetTasks(LocalData.Username, LocalData.Password);
        }

        public void UpdateTasks(OverviewItemList tasks)
        {
            this.items = tasks;
            adapter.Update(items);
        }

        public void OnTasksRetrieved(TaskContainer tasks)
        {
            try
            {
                items.Clear();
                var taskList = new List<BaseTask>();
                taskList.AddRange(tasks.Habits);
                taskList.AddRange(tasks.Routines);
                taskList.AddRange(tasks.Todos);
                
                items.AddTasks(taskList);

                habitLogs = tasks.HabitLogs;
                routineLogs = tasks.RoutineLogs;

                adapter.UpdateLogs(habitLogs, routineLogs);
                UpdateTasks(items);
            }
            catch (Exception) { }
        }

        public void ShowProgress()
        {
            throw new NotImplementedException();
        }

        public void HideProgress()
        {
            throw new NotImplementedException();
        }


        public void OnHabitMarkedDone(Habit habit, int pointsAdded)
        {
            adapter.MakeTouchableAgain(habit);
            NotifyPoints(pointsAdded);
            Update();
        }

        public void OnRoutineMarkedDone(Routine routine, int pointsAdded)
        {
            adapter.MakeTouchableAgain(routine);
            NotifyPoints(pointsAdded);
            Update();
        }

        private void NotifyPoints(int pointsAdded)
        {
            callback.ShowPointsUpdate(pointsAdded);
        }

        public void OnTodoMarkedDone(Todo todo, int pointsAdded)
        {
            adapter.MakeTouchableAgain(todo);
            NotifyPoints(pointsAdded);
            Update();
        }

        public void OnError(string message)
        {
            Toast.MakeText(Activity, message, ToastLength.Short);
        }
    }

   
}