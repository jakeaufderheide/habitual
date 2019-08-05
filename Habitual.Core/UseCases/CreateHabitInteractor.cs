using Habitual.Core.Entities;
using Habitual.Core.UseCases.Base;

namespace Habitual.Core.UseCases
{
    public interface CreateHabitInteractorCallback : InteractorCallback
    {
        void OnHabitCreated(Habit habit);
    }
    public interface CreateHabitInteractor : Interactor
    {
    }
}
