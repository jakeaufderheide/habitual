using Habitual.Core.Entities;
using Habitual.Core.UseCases.Base;

namespace Habitual.Core.UseCases
{
    public interface MarkRoutineDoneInteractorCallback : InteractorCallback
    {
        void OnRoutineMarkedDoneForToday(Routine routine, int pointsAdded);
    }
    public interface MarkRoutineDoneInteractor : Interactor
    {
    }
}
