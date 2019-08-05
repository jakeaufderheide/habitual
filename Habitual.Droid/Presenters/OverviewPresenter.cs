
using Habitual.Core.Entities;
using Habitual.Droid.UI;

namespace Habitual.Droid.Presenters
{
    public interface OverviewView : BaseView
    {
        void OnTasksRetrieved(TaskContainer tasks);
        void OnHabitMarkedDone(Habit habit, int pointsAdded);
        void OnRoutineMarkedDone(Routine routine, int pointsAdded);
        void OnTodoMarkedDone(Todo todo, int pointsAdded);
        void OnError(string message);
    }

    public interface OverviewPresenter : BasePresenter
    {
        void GetTasks(string username, string password);
        void MarkHabitDone(Habit habit);
        void MarkRoutineDone(Routine routine);
        void MarkTodoDone(Todo todo);
    }
}