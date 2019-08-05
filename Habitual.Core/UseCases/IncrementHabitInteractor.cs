using Habitual.Core.Entities;
using Habitual.Core.UseCases.Base;

namespace Habitual.Core.UseCases
{
    public interface IncrementHabitInteractorCallback : InteractorCallback
    {
        void OnHabitIncremented(Habit habit, int pointsAdded);
    }
    public interface IncrementHabitInteractor : Interactor
    {
    }
}
