using Habitual.Core.Entities;
using Habitual.Core.UseCases.Base;

namespace Habitual.Core.UseCases
{
    public interface CreateRoutineInteractorCallback : InteractorCallback
    {
        void OnRoutineCreated(Routine routine);
    }
    public interface CreateRoutineInteractor : Interactor
    {
    }
}
