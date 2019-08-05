using Habitual.Core.Entities;
using Habitual.Core.UseCases.Base;

namespace Habitual.Core.UseCases
{
    public interface CreateTodoInteractorCallback : InteractorCallback
	{
        void OnTodoCreated(Todo todo);
	}
    public interface CreateTodoInteractor : Interactor
    {
    }
}
