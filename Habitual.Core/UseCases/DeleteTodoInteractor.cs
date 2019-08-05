using System;
using Habitual.Core.UseCases.Base;

namespace Habitual.Core.UseCases
{
    public interface DeleteTodoInteractorCallback : InteractorCallback
	{
        void OnTodoDeleted(Guid id);
	}
    public interface DeleteTodoInteractor : Interactor
    {
    }
}

