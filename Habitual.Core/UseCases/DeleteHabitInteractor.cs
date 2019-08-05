using System;
using Habitual.Core.UseCases.Base;

namespace Habitual.Core.UseCases
{
    public interface DeleteHabitInteractorCallback : InteractorCallback
    {
        void OnHabitDeleted(Guid habitID);
    }
    public interface DeleteHabitInteractor : Interactor
    {
    }
}
