using System;
using Habitual.Core.UseCases.Base;

namespace Habitual.Core.UseCases
{
    public interface DeleteRoutineInteractorCallback : InteractorCallback
    {
        void OnRoutineDeleted(Guid routineID);
    }
    public interface DeleteRoutineInteractor : Interactor
    {
    }
}
