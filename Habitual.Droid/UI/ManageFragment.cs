using System;
using System.Collections.Generic;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;
using Habitual.Core.Entities;
using Habitual.Core.Entities.Base;
using Habitual.Core.Executors;
using Habitual.Core.Executors.Impl;
using Habitual.Droid.Helpers;
using Habitual.Droid.Presenters;
using Habitual.Droid.Presenters.Impl;
using Habitual.Droid.Threading;
using Habitual.Droid.Util;
using Habitual.Storage;
using Habitual.Storage.Local;

namespace Habitual.Droid.UI
{
    public class ManageFragment : Android.Support.V4.App.Fragment, ManageView
    {
        private ListView manageList;
        private List<BaseTask> items;
        private ManageListAdapter adapter;
        private ManagePresenter presenter;
        private MainThread mainThread;
        private MainApplicationCallback callback;

        public ManageFragment(MainApplicationCallback callback)
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
            presenter = new ManagePresenterImpl(TaskExecutor.GetInstance(), mainThread, this, new HabitRepositoryImpl(), new RoutineRepositoryImpl(), new TodoRepositoryImpl(), new UserRepositoryImpl(), LocalData.Username, LocalData.Password);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = inflater.Inflate(Resource.Layout.Manage, container, false);
            InitializeElements(view);

            return view;
        }

        private void InitializeElements(View view)
        {
            if (items == null) items = new List<BaseTask>();
            manageList = view.FindViewById<ListView>(Resource.Id.manageList);
            var addButton = (FloatingActionButton)view.FindViewById(Resource.Id.addTaskButton);
            addButton.Click += AddButton_Click;
            adapter = new ManageListAdapter(Activity, items, callback);
            manageList.ItemClick += ManageList_ItemClick;
            manageList.Adapter = adapter;
            Update();
        }

        private void ManageList_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            TextDialogBuilder builder = new TextDialogBuilder();
            var dialog = builder.BuildStandardYesNoDialog(this.Activity, $"Delete {items[e.Position].Description}?", "Would you like to delete this task?", DeleteTask, e);
            dialog.Show();
        }

        private void DeleteTask(int result, AdapterView.ItemClickEventArgs e)
        {
            if (result == 1)
            {
                var task = items[e.Position];
                var habit = task as Habit;
                var routine = task as Routine;
                var todo = task as Todo;
                if (habit != null)
                {
                    presenter.DeleteHabit(habit);
                }
                if (routine != null)
                {
                    presenter.DeleteRoutine(routine);
                }
                if (todo != null)
                {
                    presenter.DeleteTodo(todo);
                }
            }
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            PromptNewTask();
        }

        private void PromptNewTask()
        {
            TextDialogBuilder builder = new TextDialogBuilder();
            var dialog = builder.BuildNewTaskDialog(this.Activity, AddNewTask);
            dialog.Show();
        }

        private void AddNewTask(BaseTask task)
        {
            var habit = task as Habit;
            var routine = task as Routine;
            var todo = task as Todo;

            if (habit != null)
            {
                presenter.CreateHabit(habit);
            }

            if (routine != null)
            {
                presenter.CreateRoutine(routine);
            }

            if (todo != null)
            {
                presenter.CreateTodo(todo);
            }
        }

        public void Update()
        {
            presenter.GetTasks(LocalData.Username, LocalData.Password);
        }

        public void UpdateTasks(List<BaseTask> tasks)
        {
            this.items = tasks;
            adapter.Update(items);
        }

        public void OnTasksRetrieved(TaskContainer tasks)
        {
            try
            {
                items.Clear();
                items.AddRange(tasks.Habits);
                items.AddRange(tasks.Routines);
                items.AddRange(tasks.Todos);

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

        public void OnHabitCreated(Habit habit)
        {
            callback.UpdateAllRequested();
        }

        public void OnRoutineCreated(Routine routine)
        {
            callback.UpdateAllRequested();
        }

        public void OnTodoCreated(Todo todo)
        {
            callback.UpdateAllRequested();
        }

        public void OnTaskDeleted()
        {
            callback.UpdateAllRequested();
        }

        public void Reset()
        {
            Init();
        }

        public void OnError(string message)
        {
            Activity.RunOnUiThread(() => Toast.MakeText(Activity, message, ToastLength.Short));
        }
    }
}