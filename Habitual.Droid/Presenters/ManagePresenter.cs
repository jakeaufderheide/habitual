
using Habitual.Core.Entities;
using Habitual.Droid.UI;

namespace Habitual.Droid.Presenters
{
    public interface ManageView : BaseView
    {
        void OnTasksRetrieved(TaskContainer tasks);
        void OnHabitCreated(Habit habit);
        void OnRoutineCreated(Routine routine);
        void OnTodoCreated(Todo todo);
        void OnTaskDeleted();
        void OnError(string message);
    }

    public interface ManagePresenter : BasePresenter
    {
        void GetTasks(string username, string password);
        void CreateHabit(Habit habit);
        void CreateRoutine(Routine routine);
        void CreateTodo(Todo todo);
        void DeleteHabit(Habit habit);
        void DeleteRoutine(Routine routine);
        void DeleteTodo(Todo todo);
    }
}